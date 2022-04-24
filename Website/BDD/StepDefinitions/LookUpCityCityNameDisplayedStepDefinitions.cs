using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBDD.PageObjects;

namespace TestBDD.StepDefinitions
{   
    [Binding]
    public sealed class LookUpCityCityNameDisplayedStepDefinitions
    {   
        private readonly CityLookUpPage _CityLookUp;
        private readonly CrimeStatsPage _CrimeStats;

        public  LookUpCityCityNameDisplayedStepDefinitions(CityLookUpPage cityLookUpPage, CrimeStatsPage crimeStats)
        {
            _CityLookUp = cityLookUpPage;
            _CrimeStats = crimeStats;
        }

        [Given(@"I am on the CityLookup Page")]
        public void CheckPageLocation()
        {
            _CityLookUp.Goto(Common.CityLookUpPageName);
        }

        [When(@"I find the search box")]
        public void CheckCitySearchBox()
        {
            _CityLookUp.GetSearchBox.Should().BeTrue();
        }
        [When(@"I type (.*) into search box")]
        public void EnterCityNameToSearchBox(string cityName)
        {
            _CityLookUp.EnterCityName(cityName);
        }

        [When (@"I click Submit button")]
        public void CheckSubmitButton()
        {
            _CityLookUp.SubmitButtonClick();
        }
        [Then (@"I should see the city name")]
        public void VerifyCityName()
        {
            _CrimeStats.GetCityName.Should().BeTrue();
        }
    }
    
}