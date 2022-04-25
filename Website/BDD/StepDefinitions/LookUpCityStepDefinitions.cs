using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBDD.PageObjects;

namespace TestBDD.StepDefinitions
{   
    [Binding]
    public sealed class LookUpCityStepDefinitions
    {   
        private readonly CityLookUpPage _CityLookUp;

        public  LookUpCityStepDefinitions(CityLookUpPage cityLookUpPage)
        {
            _CityLookUp = cityLookUpPage;
        }

        [Given(@"I am on the City Lookup Page")]
        public void CheckPageLocation()
        {
            _CityLookUp.Goto(Common.CityLookUpPageName);
        }

        [When(@"I see the search box")]
        public void CheckCitySearchBox()
        {
            _CityLookUp.GetSearchBox.Should().BeTrue();
        }
        [When(@"I enter (.*) into search box")]
        public void EnterCityNameToSearchBox(string cityName)
        {
            _CityLookUp.EnterCityName(cityName);
        }

        [Then (@"I click the submit button")]
        public void CheckSubmitButton()
        {
            _CityLookUp.SubmitButtonClick();
        }
    }
    
}
