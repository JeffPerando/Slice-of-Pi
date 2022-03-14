using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using Main.Models;
using Main.DAL.Abstract;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Collections;
using Newtonsoft.Json;

namespace Main.Models
{
    public class StateCrimeViewModel
    {
        public string? State_abbr { get; set; }
        public int? Year { get; set; }
        public int? Population { get; set; }
        public int? Violent_crime { get; set; }
        public int? Homicide { get; set; }
        public int? Rape_legacy { get; set; }
        public int? Rape_revised { get; set; }
        public int? Robbery { get; set; }
        public int? Aggravated_assault { get; set; }
        public int? Property_crime { get; set; }
        public int? Burglary { get; set; }
        public int? Larceny { get; set; }
        public int? Motor_vehicle_theft { get; set; }
        public int? Arson { get; set; }
        public string? stateAbbrev { get; set; }
        public int? aYear { get; set; }

        public StateCrimeViewModel PresentJSONRespone(JObject info)
        {
            StateCrimeViewModel state_crime_stats = new StateCrimeViewModel();
            foreach (var item in info["results"])
            {
                try
                {
                    state_crime_stats.State_abbr = (string)item["state_abbr"];
                    if (state_crime_stats.State_abbr == null)
                        state_crime_stats.State_abbr = "N/A";

                    state_crime_stats.Year = (int?)item["year"];
                    if (state_crime_stats.Year == null)
                        state_crime_stats.Year = 0;

                    state_crime_stats.Population = (int?)item["population"];
                    if (state_crime_stats.Population == null)
                        state_crime_stats.Population = 0;

                    state_crime_stats.Violent_crime = (int?)item["violent_crime"];
                    if (state_crime_stats.Violent_crime == null)
                        state_crime_stats.Violent_crime = 0;

                    state_crime_stats.Homicide = (int?)item["homicide"];
                    if (state_crime_stats.Homicide == null)
                        state_crime_stats.Homicide = 0;

                    state_crime_stats.Rape_legacy = (int?)item["rape_legacy"];
                    if (state_crime_stats.Rape_legacy == null)
                        state_crime_stats.Rape_legacy = 0;

                    state_crime_stats.Rape_revised = (int?)item["rape_revised"];
                    if (state_crime_stats.Rape_revised == null)
                        state_crime_stats.Rape_revised = 0;

                    state_crime_stats.Robbery = (int?)item["robbery"];
                    if (state_crime_stats.Robbery == null)
                        state_crime_stats.Robbery = 0;

                    state_crime_stats.Aggravated_assault = (int?)item["aggravated_assault"];
                    if (state_crime_stats.Aggravated_assault == null)
                        state_crime_stats.Aggravated_assault = 0;

                    state_crime_stats.Property_crime = (int?)item["property_crime"];
                    if (state_crime_stats.Property_crime == null)
                        state_crime_stats.Property_crime = 0;

                    state_crime_stats.Burglary = (int?)item["burglary"];
                    if (state_crime_stats.Burglary == null)
                        state_crime_stats.Burglary = 0;

                    state_crime_stats.Larceny = (int?)item["larceny"];
                    if (state_crime_stats.Larceny == null)
                        state_crime_stats.Larceny = 0;

                    state_crime_stats.Motor_vehicle_theft = (int?)item["motor_vehicle_theft"];
                    if (state_crime_stats.Motor_vehicle_theft == null)
                        state_crime_stats.Motor_vehicle_theft = 0;

                    state_crime_stats.Arson = (int?)item["arson"];
                    if (state_crime_stats.Arson == null)
                        state_crime_stats.Arson = 0;

                }
                catch
                {
                    continue;
                }
            }
            return state_crime_stats;
        }

    }
}
