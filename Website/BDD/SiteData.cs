
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestBDD
{
    public class SiteData
    {
        public const string BaseUrl = "https://localhost:44355";
        // Page names that everyone should use
        public const string HomePageName = "Home";
        public const string LoginPageName = "Login";

        // A handy way to look these up
        public static readonly Dictionary<string, string> Paths = new()
        {
            { HomePageName, "/" },
            { LoginPageName, "/Identity/Account/Login" }
        };

        public static string PathFor(string pathName) => Paths[pathName];
        public static string UrlFor(string pathName) => BaseUrl + Paths[pathName];
    }
}
