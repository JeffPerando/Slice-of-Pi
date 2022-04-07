
using CsvHelper.Configuration;
using System.Globalization;

namespace Main.Models
{
    public sealed class SCSRMap : ClassMap<StateCrimeSearchResult>
    {
        public SCSRMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Id).Ignore();
            Map(m => m.User).Ignore();
            Map(m => m.User.Id).Ignore();
            Map(m => m.User.Address).Ignore();
            Map(m => m.User.Name).Ignore();
            Map(m => m.User.EmailAddress).Ignore();
            Map(m => m.UserId).Ignore();

        }

    }

}
