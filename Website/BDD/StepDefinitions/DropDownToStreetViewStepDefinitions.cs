using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBDD.PageObjects;

namespace TestBDD.StepDefinitions
{   
    [Binding]
    public sealed class DropDownToStreetViewStepDefinitions
    {   
        
        private readonly HomePage _HomePage;
        private readonly StreetViewLookUp _StreetLookUpPage;
        private readonly StreetView _StreetView;
        public  DropDownToStreetViewStepDefinitions(HomePage homePage, StreetViewLookUp streetLookUpPage, StreetView streetViewPage)
        {
            _HomePage = homePage;
            _StreetLookUpPage = streetLookUpPage;
            _StreetView = streetViewPage;
        }

        [Given(@"I am on the Home Page")]
        public void CheckHomePage()
        {
            _HomePage.Goto(Common.HomePageName);
        }

        [When(@"I click the Searches drop down")]
        public void NavigateToDropDown()
        {
            _HomePage.ClickSearchesButton();
        }
        [Then(@"I will see the home address look up")]
        public void GoThroughStreetLookup()
        {
            _HomePage.ClickStreetViewsButton();
        }

        [When (@"I enter (.*) into the street address")]
        public void EnterStreetAddress(string streetName)
        {
            _StreetLookUpPage.EnterStreetName(streetName);
        }
        [Then (@"I enter (.*) into the city")]
        public void EnterCity(string cityName)
        {
            _StreetLookUpPage.EnterCityName(cityName);
        }
        [Then (@"I select Alabama as the state")]
        public void EnterState()
        {
            _StreetLookUpPage.ClickStateDropdown();
            _StreetLookUpPage.SelectAlabama();
        }
        [Then (@"I click the submit button")]
        public void ClickSubmitButton()
        {
            _StreetLookUpPage.ClickSubmitButton();
        }
        [Then (@"I should be on the street view page")]
        public void RedirectToStreetView()
        {
            var message = _StreetView.CheckRedirection;
            message.Should().BeTrue();
        }
    }
    
}