using Main.Helpers;
using Main.Services.Abstract;
using Main.Services.Concrete;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.DAL.Abstract;
using Main.DAL.Concrete;
using Main.Models.Listings;

namespace Test
{
    public class GoogleAPIService
    {
        [SetUp]
        public void Setup() { }

        [TearDown]
        public void TearDown() { }

        [Test]
        public void GetStaticStreetView()
        {
            StreetViewViewModel model = new StreetViewViewModel()
            {
                Address = "1986 wallace rd nw salem or",
                Url = "https://burrito/carneasada/address=",
                Akey = "welcome to Chilies",
                CityName = "Martian",
                StateName = "AZ"

            };

            GoogleStreetViewAPIService google = new GoogleStreetViewAPIService();
            
            string a = google.ParseAddress(model.Address+' '+ model.CityName + ' '+model.StateName);

            Assert.AreEqual(a, "1986+wallace+rd+nw+salem+or+Martian+AZ");

        }
        [Test]
        public void GetEmbeddedMap()
        {
            StreetViewViewModel model = new StreetViewViewModel()
            {
                Address = "1986 wallace rd nw salem or",
                Url = "https://burrito/carneasada/address=",
                Akey = "welcome to Chilies",
                CityName = "Martian",
                StateName = "AZ"

            };

            GoogleStreetViewAPIService google = new GoogleStreetViewAPIService();

            string a = google.ParseAddressEmbededMap(model.Address + ' ' + model.CityName + ' ' + model.StateName);

            Assert.AreEqual(a, "1986%wallace%rd%nw%salem%or%Martian%AZ");

        }

        [Test]
        public void CheckParseSubmission()
        {
            StreetViewViewModel model = new StreetViewViewModel()
            {
                Address = "1986 wallace rd nw salem or 97304",
                Url = "https://burrito/carneasada/address=",
                Akey = "welcome to Chilies",
                CityName = "",
                StateName = ""

            };

            StreetViewViewModel model2 = new StreetViewViewModel()
            {
                Address = "1986 wallace rd nw, salem, or 97304",
                Url = "https://burrito/carneasada/address=",
                Akey = "welcome to Chilies",
                CityName = "salem",
                StateName = "or"

            };


            StreetViewViewModel model3 = new StreetViewViewModel()
            {
                Address = "1986 wallace rd nw",
                Url = "https://burrito/carneasada/address=",
                Akey = "welcome to Chilies",
                CityName = "salem",
                StateName = "or"

            };

            GoogleStreetViewAPIService google = new GoogleStreetViewAPIService();

            StreetViewViewModel a = new StreetViewViewModel();
            a = google.ParseAddressSubmission(model2.Address);

            Assert.AreEqual(model3.Address, a.Address);
            Assert.AreEqual(model3.CityName, a.CityName);
            Assert.AreEqual(model3.StateName, a.StateName);

        }
    }

}
