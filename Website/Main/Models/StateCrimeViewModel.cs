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
            foreach (var item in info["results"])
            {
                try
                {
                    this.State_abbr = (string)item["state_abbr"] ?? "N/A";
                    this.Year = (int?)item["year"] ?? 0;
                    this.Population = (int?)item["population"] ?? 0;
                    
                    this.Violent_crime = (int?)item["violent_crime"] ?? 0;
                    this.Homicide = (int?)item["homicide"] ?? 0;
                    this.Rape_legacy = (int?)item["rape_legacy"] ?? 0;
                    this.Rape_revised = (int?)item["rape_revised"] ?? 0;
                    this.Robbery = (int?)item["robbery"] ?? 0;
                    this.Aggravated_assault = (int?)item["aggravated_assault"] ?? 0;
                    this.Property_crime = (int?)item["property_crime"] ?? 0;
                    this.Burglary = (int?)item["burglary"] ?? 0;
                    this.Larceny = (int?)item["larceny"] ?? 0;
                    this.Motor_vehicle_theft = (int?)item["motor_vehicle_theft"] ?? 0;
                    this.Arson = (int?)item["arson"] ?? 0;
                    
                }
                catch
                {
                    continue;
                }
            }

            return this;
        }

    }
}
