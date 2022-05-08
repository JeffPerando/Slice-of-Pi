
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Main.Extensions
{
    public static class FormExtension
    {
        public static string AddressFields(this IHtmlHelper helper)
        {
            return @$"<div class='row'>
    <div class='col-md-12'>
        <label for='street' class='form-label'>Street</label>
        <input name='street' type='text' class='form-control' placeholder='1313 Mockingbird Ln.' pattern='\d+ .+' required />
    </div>
    <div class='col-md-6'>
        <label for='city' class='form-label'>City</label>
        <input name='city' type='text' class='form-control' placeholder='Mockingbird Heights' required />
    </div>
    <div class='col-md-2'>
        <label for='state' class='form-label'>State</label>
        <select name='state' id='addrStates' type='text' class='form-control'>
        </select>
    </div>
    <div class='col-md-4'>
        <label for='zip' class='form-label'>ZIP</label>
        <input name='zip' type='tel' class='form-control' placeholder='90210 or 90210-0800' pattern='\d{{5}}(-\d{{4}})?' required />
    </div>
</div>
<div class='row justify-content-center'>
    <div class='col-2 mt-4'>
        <button type='submit' class='btn btn-primary btn-lg'>Submit Address</button>
    </div>
</div>";
        }

        public static IHtmlContent RenderAddressFields(this IHtmlHelper helper)
        {
            return new HtmlString(helper.AddressFields());
        }

    }

}
