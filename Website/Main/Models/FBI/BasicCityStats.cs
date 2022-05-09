﻿
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace Main.Models.FBI
{
    public class BasicCityStats : BasicCrimeStats
    {
        public BasicCityStats() {}
        public BasicCityStats(int year, JToken? data)
        {
            Year = year;

            if (data == null)
                return;

            var crimes = data.Where(data => (int?)data["data_year"] == year);

            foreach (var crime in crimes)
            {
                int amount = (int?)crime["actual"] ?? 0;
                var offense = crime["offense"]?.ToString();

                switch (offense)
                {
                    case "violent-crime": ViolentCrimes += amount; break;
                    case "property-crime": PropertyCrimes += amount; break;
                }

            }

        }

    }

}
