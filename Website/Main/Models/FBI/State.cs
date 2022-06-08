using Newtonsoft.Json;

namespace Main.Models
{
    public class State
    {
        public string Name { get; set; } = "";
        public string Abbrev { get; set; } = "";

        public static readonly List<State> AllStates = new() {
            new State { Name = "Alabama",				Abbrev = "AL" },
            new State { Name = "Alaska",				Abbrev = "AK" },
            new State { Name = "Arizona",				Abbrev = "AZ" },
            new State { Name = "Arkansas",				Abbrev = "AR" },
            new State { Name = "California",			Abbrev = "CA" },
            new State { Name = "Colorado",				Abbrev = "CO" },
            new State { Name = "Connecticut",			Abbrev = "CT" },
            new State { Name = "Delaware",				Abbrev = "DE" },
            new State { Name = "District Of Columbia",	Abbrev = "DC" },
            new State { Name = "Florida",				Abbrev = "FL" },
            new State { Name = "Georgia",				Abbrev = "GA" },
            new State { Name = "Hawaii",				Abbrev = "HI" },
            new State { Name = "Idaho",					Abbrev = "ID" },
            new State { Name = "Illinois",				Abbrev = "IL" },
            new State { Name = "Indiana",				Abbrev = "IN" },
            new State { Name = "Iowa",					Abbrev = "IA" },
            new State { Name = "Kansas",				Abbrev = "KS" },
            new State { Name = "Kentucky",				Abbrev = "KY" },
            new State { Name = "Louisiana",				Abbrev = "LA" },
            new State { Name = "Maine",					Abbrev = "ME" },
            new State { Name = "Maryland",				Abbrev = "MD" },
            new State { Name = "Massachusetts",			Abbrev = "MA" },
            new State { Name = "Michigan",				Abbrev = "MI" },
            new State { Name = "Minnesota",				Abbrev = "MN" },
            new State { Name = "Mississippi",			Abbrev = "MS" },
            new State { Name = "Missouri",				Abbrev = "MO" },
            new State { Name = "Montana",				Abbrev = "MT" },
            new State { Name = "Nebraska",				Abbrev = "NE" },
            new State { Name = "Nevada",				Abbrev = "NV" },
            new State { Name = "New Hampshire",			Abbrev = "NH" },
            new State { Name = "New Jersey",			Abbrev = "NJ" },
            new State { Name = "New Mexico",			Abbrev = "NM" },
            new State { Name = "New York",				Abbrev = "NY" },
            new State { Name = "North Carolina",		Abbrev = "NC" },
            new State { Name = "North Dakota",			Abbrev = "ND" },
            new State { Name = "Ohio",					Abbrev = "OH" },
            new State { Name = "Oklahoma",				Abbrev = "OK" },
            new State { Name = "Oregon",				Abbrev = "OR" },
            new State { Name = "Pennsylvania",			Abbrev = "PA" },
            new State { Name = "Rhode Island",			Abbrev = "RI" },
            new State { Name = "South Carolina",		Abbrev = "SC" },
            new State { Name = "South Dakota",			Abbrev = "SD" },
            new State { Name = "Tennessee",				Abbrev = "TN" },
            new State { Name = "Texas",					Abbrev = "TX" },
            new State { Name = "Utah",					Abbrev = "UT" },
            new State { Name = "Vermont",				Abbrev = "VT" },
            new State { Name = "Virginia",				Abbrev = "VA" },
            new State { Name = "Washington",			Abbrev = "WA" },
            new State { Name = "West Virginia",			Abbrev = "WV" },
            new State { Name = "Wisconsin",				Abbrev = "WI" },
            new State { Name = "Wyoming",				Abbrev = "WY" }
			
        };

    }

}
