
using CsvHelper;
using Main.Models;
using System.Globalization;
using System.Text;

namespace Main.Helpers
{
    public class CSVParseHelper
    {
        public static string fromStateSearchHistory(IEnumerable<StateCrimeSearchResult> results)
        {
            var sb = new StringBuilder();
            var sw = new StringWriter(sb);

            using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<SCSRMap>();
                csv.WriteRecords(results);

            }

            return sb.ToString();
        }

    }

}
