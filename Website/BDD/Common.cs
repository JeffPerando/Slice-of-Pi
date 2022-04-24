    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace TestBDD
{
        // Sitewide definitions and useful methods
        public class Common
        {
            public const string BaseUrl = "https://localhost:44355";
            // Page names that everyone should use
            public const string HomePageName = "Home";

            // A handy way to look these up
            public static readonly Dictionary<string, string> Paths = new()
            {
                { HomePageName, "/" },
            };

            public static string PathFor(string pathName) => Paths[pathName];
            public static string UrlFor(string pathName) => BaseUrl + Paths[pathName];
        }
    }
