# standard libraries
import os
import json
import shlex
# ones you'll need to install
import click                                # pip install click     or if you have stupid Windows conflicts!  python3 -m pip install click
from azure.cli.core import get_default_cli  # pip install azure-cli
from requests import get                    # pip install requests
# also needs pyodbc                         # pip install pyodbc
# our other file
from dbtool import run_sql_script

# Tool to demonstrate automation of creating and deploying Azure webapp and databases
# All resources can be created with the lowest/cheapest settings: FREE for webapp and Basic for database
# They're placed inside a resource group.  There's no procedure for deleting any one resource; instead
# we just delete the resource group to remove all of them at once.
# To run:
#   1. Install Python libraries shown above (use a virtual environment if you want)
#   2. Use Python 3
#   3. Install Azure CLI (https://docs.microsoft.com/en-us/cli/azure/install-azure-cli)
#   4. Login to Azure from the command line: az login
#   5. Configure it if you want: az configure
#   6. Edit the sample config.json to include your project names, passwords, locations, etc.
#   7. DON'T PUT THESE FILES IN YOUR REPO (or at least not the config.json)
#   8. Edit your up and seed scripts so SQL commands end in a ; and there are no GO or other commands
#   9. Do some dry runs first.  
#       To "create" your databases:
#           python deploytool.py --help
#           python deploytool.py --dry-run config.json AutomationTest create-db
#       and your web app
#           python deploytool.py --dry-run config.json AutomationTest create-web-app
#       and then clean up with
#           python deploytool.py --dry-run config.json AutomationTest delete-resource-group
#       or to manually run a .sql script
#           python dbtool.py --dry-run server dbname username password path-to-script
#   10. When those look good, run them again without the --dry-run to run for real

# Run: 
# az webapp list-runtimes --os-type Windows
# or
# az webapp list-runtimes --os-type Linux
# to see available runtimes.  Note, the string is different on Windows vs. Linux

# SKU pricing tiers are currently:
# FREE or F1 for free
# D1 is the smallest shared instance on Windows but it isn't available on Linux
# B1 is Basic Small and is the cheapest, especially on Linux  Pricing also depends on location

# helper function to invoke Azure CLI commands using the Azure Python SDK
# from joek575: https://stackoverflow.com/questions/51546073/how-to-run-azure-cli-commands-using-python
def az_cli (args_str):
    #args = args_str.split()
    args = shlex.split(args_str)
    cli = get_default_cli()
    cli.invoke(args,out_file=open(os.devnull,'w'))
    if cli.result.result:
        return cli.result.result
    elif cli.result.error:
        print('exception...')
        raise cli.result.error
    return True

# helper function to do a dry run or actually execute the command
def run_or_dry(ctx,cmd):
    if ctx.obj['DRY_RUN']:
        print('\taz',cmd)
        return None
    else:
        return az_cli(cmd)

# beginning of command line interface functions

@click.group()
@click.argument('config-file',type=click.Path(exists=True,file_okay=True,dir_okay=False))
@click.argument('resource-group-name')
@click.option('--dry-run','-n',default=False,is_flag=True)
@click.pass_context
def cli(ctx,config_file,resource_group_name,dry_run):
    """
    Uses the RESOURCE-GROUP-NAME to deploy a complete ASP.NET Core MVC web 
    application and SQL Server database, that are defined in CONFIG-FILE
    to Azure.
    RESOURCE-GROUP-NAME  name of the resource group to house the deployed app
    and db, will be created if it doesn't already exist


    CONFIG-FILE .json file containing username, password, and other info 
                needed to deploy.  See the example for required info
    """
    ctx.ensure_object(dict)
    # merge the arguments and options from the command line and those in the config file
    # into a single dictionary that all subsequent commands will recieve
    ctx.obj['DRY_RUN'] = dry_run
    ctx.obj['RESOURCE_GROUP_NAME'] = resource_group_name
    if dry_run:
        print('**** DRY RUN ****')
        print('  Nothing will be created,\n  but will show commands')
        print('*****************\n')
    # unpack config file and place entries into our context
    with open(config_file,'r') as fin:
        data = json.load(fin)
        ctx.obj.update(data)    # update() merges two dicts
    # ensure the resource group exists, since everything depends on it
    try:
        # az group show --name $RESOURCE_GROUP_NAME
        cmd = 'group show --name {}'.format(ctx.obj['RESOURCE_GROUP_NAME'])
        response = run_or_dry(ctx,cmd)
        # returns a dict
        #print('Finding resource group: ',response)
        ctx.obj['resource_group'] = response
    except:
        print('Resource group ({}) does not exist, creating it now'.format(ctx.obj['RESOURCE_GROUP_NAME']))
        # az group create --location westus --name $RESOURCE_GROUP_NAME
        cmd = 'group create --location {} --name {}'.format(ctx.obj['LOCATION'],ctx.obj['RESOURCE_GROUP_NAME'])
        response = run_or_dry(ctx,cmd)
        if not ctx.obj['DRY_RUN'] and not response:
            print('Resource group could not be created')
            return
        else:
            ctx.obj['resource_group'] = response

@cli.command()
@click.pass_context
def create_web_app(ctx):
    """
    Create an appservice in the listed tier and then create and configure a webapp in it.  Then build
    and publish an application into it.
    """
    click.echo('Creating new Web App in Azure')
    # Create an appservice plan first, name will be the same as the webapp
    # identify if we need the linux flag
    is_linux = '--is-linux' if ctx.obj['OS'] == 'Linux' else ''
    cmd = 'appservice plan create {} --sku {} --name {} --resource-group {} --location {}'.format(is_linux,ctx.obj['APP_SKU'],ctx.obj['WEB_APP_NAME'],ctx.obj['RESOURCE_GROUP_NAME'],ctx.obj['LOCATION'])
    response = run_or_dry(ctx,cmd)
    if not ctx.obj['DRY_RUN'] and not response:
        print('Could not create appservice plan for web app')
        return

    # Create and deploy a webapp.  Must be executed from the project directory.  This is a direct deployment, just like when
    # we do so with publish.  It will automatically create an app service in the same resource group.
    # az webapp up --sku FREE --name $WEB_APP_NAME --resource-group $RESOURCE_GROUP_NAME --location $LOCATION --runtime $RUNTIME
    cmd = 'webapp up --sku {} --os-type {} --name {} --resource-group {} --location {} --runtime {} --plan {}'.format(ctx.obj['APP_SKU'],ctx.obj['OS'],ctx.obj['WEB_APP_NAME'],ctx.obj['RESOURCE_GROUP_NAME'],ctx.obj['LOCATION'],ctx.obj['RUNTIME'],ctx.obj['WEB_APP_NAME'])
    # this one must be run in the project directory
    cwd = os.getcwd()
    if os.path.exists(ctx.obj['PROJECT_DIR']):
        os.chdir(ctx.obj['PROJECT_DIR'])
    else:
        print('Project directory ({}) is not valid.  Cannot proceed'.format(ctx.obj['PROJECT_DIR']))
        return
    response = run_or_dry(ctx,cmd)
    # change current working directory back to where we were
    os.chdir(cwd)
    if not ctx.obj['DRY_RUN'] and not response:
        print('Could not create or deploy web app')
        return
    # Add Application Settings key/value pairs
    app_settings = ' '.join(['{}={}'.format(k,v) for k,v in ctx.obj['APPLICATION_SETTINGS'].items()])
    cmd = 'webapp config appsettings set --name {} --resource-group {} --settings {}'.format(ctx.obj['WEB_APP_NAME'],ctx.obj['RESOURCE_GROUP_NAME'],app_settings)
    response = run_or_dry(ctx,cmd)
    if not ctx.obj['DRY_RUN'] and not response:
        print('Could not set Application Settings for web app')
        return
    # Add Connection Strings key/value pairs, construct from a template of the connection string and known values for
    # the database in the config file
    for cs in ctx.obj['CONNECTION_STRINGS']:
        conn_str = ctx.obj['CONNECTION_STRING_TEMPLATE'].format(connection_name=cs['name'],server_name=ctx.obj['SQL_SERVER_NAME'],db_name=cs['db_name'],username=ctx.obj['DB_USERNAME'],password=ctx.obj['DB_PASSWORD'])
        cmd = 'webapp config connection-string set --connection-string-type SQLServer --name {} --resource-group {} --settings {}'.format(ctx.obj['WEB_APP_NAME'],ctx.obj['RESOURCE_GROUP_NAME'],conn_str)
        response = run_or_dry(ctx,cmd)
        if not ctx.obj['DRY_RUN'] and not response:
            print('Could not set Connection Strings for web app')
            return

@cli.command()
@click.pass_context
def delete_resource_group(ctx):
    """
    Delete the resource group (and therefore everything in it)
    """
    click.echo('Deleting resource group ({}) and all resources within it'.format(ctx.obj['RESOURCE_GROUP_NAME']))
    cmd = 'group delete --name {}'.format(ctx.obj['RESOURCE_GROUP_NAME'])
    response = run_or_dry(ctx,cmd)

@cli.command()
@click.pass_context
def create_db(ctx):
    """
    Create a SQL server and zero or more SQL databases in it. Sets two firewall rules for access
    by this machine and by other Azure services.
    """
    click.echo('Setting up new SQL database in Azure')
    # create SQL Server
    cmd = 'sql server create --name {} --resource-group {} --location {} --admin-user {} --admin-password {}'.format(ctx.obj['SQL_SERVER_NAME'],ctx.obj['RESOURCE_GROUP_NAME'],ctx.obj['LOCATION'],ctx.obj['DB_USERNAME'],ctx.obj['DB_PASSWORD'])
    response = run_or_dry(ctx,cmd)
    if not ctx.obj['DRY_RUN'] and not response:
        print('Could not create SQL Server')
        return
    # add firewall rule for this machine's IP address
    ip = get('https://api.ipify.org').text
    cmd = 'sql server firewall-rule create --server {} --resource-group {} --name dev --start-ip-address {} --end-ip-address {}'.format(ctx.obj['SQL_SERVER_NAME'],ctx.obj['RESOURCE_GROUP_NAME'],ip,ip)
    response = run_or_dry(ctx,cmd)
    if not ctx.obj['DRY_RUN'] and not response:
        print('Could not add firewall rule for this machine')
        return
    # add firewall rule for allowing access from Azure services
    ip = '0.0.0.0'
    cmd = 'sql server firewall-rule create --server {} --resource-group {} --name AllowAzure --start-ip-address {} --end-ip-address {}'.format(ctx.obj['SQL_SERVER_NAME'],ctx.obj['RESOURCE_GROUP_NAME'],ip,ip)
    response = run_or_dry(ctx,cmd)
    if not ctx.obj['DRY_RUN'] and not response:
        print('Could not add firewall rule for this machine')
        return

    # create db instances, using Basic and 5 DTU (5 DTU is $4.99/mo., didn't allow me to set capacity to 1 DTU
    dbs = [(db['name'],db['up'],db['seed']) for db in ctx.obj['DB_INSTANCES']]
    for (name,up_script,seed_script) in dbs:
        cmd = 'sql db create --service-objective Basic --server {} --resource-group {} --name {}'.format(ctx.obj['SQL_SERVER_NAME'],ctx.obj['RESOURCE_GROUP_NAME'],name)
        response = run_or_dry(ctx,cmd)
        if not ctx.obj['DRY_RUN'] and not response:
            print('Could not create database ({})'.format(name))
            return
        server_address = ctx.obj['SQL_SERVER_NAME'] + '.database.windows.net'
        server_username = ctx.obj['DB_USERNAME']
        server_password = ctx.obj['DB_PASSWORD']
        if up_script:
            run_sql_script(server_address,name,server_username,server_password,up_script,ctx.obj['DRY_RUN'])
            if seed_script:
                run_sql_script(server_address,name,server_username,server_password,seed_script,ctx.obj['DRY_RUN'])


if __name__ == '__main__':
    cli(obj={})