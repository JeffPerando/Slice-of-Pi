using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBDD.PageObjects;

namespace TestBDD.StepDefinitions
{   
    [Binding]
    public sealed class CheckGradingScaleStepDefinitions
    {   
        
        private readonly HomePage _HomePage;
        private readonly StreetViewLookUp _StreetLookUpPage;
        private readonly StreetView _StreetView;
        private readonly CityLookUpPage _CityLookUp;
        public  CheckGradingScaleStepDefinitions(HomePage homePage, StreetViewLookUp streetLookUpPage, StreetView streetViewPage, CityLookUpPage citySearch)
        {
            _HomePage = homePage;
            _StreetLookUpPage = streetLookUpPage;
            _StreetView = streetViewPage;
            _CityLookUp = citySearch;
        }

        [Given(@"")]
        public void CheckHomePage()
        {
            _HomePage.Goto(Common.HomePageName);
        }

        [When(@"")]
        public void NavigateToDropDown()
        {
            _HomePage.ClickSearchesButton();
        }
        [Then(@"I click city view tab")]
        public void ClickingStreetView()
        {
            _HomePage.ClickCityLookUpButton();
        }
        [Then(@"I enter California as state")]
        public void ClickCaliforniaDropdown()
        {
            _CityLookUp.ClickStatesDropdown();
            _CityLookUp.ClickCaliforniaDropdown();
        }
        [Then(@"I enter Moreno Valley as the city")]
        public void ClickMorenoValleyDropdown()
        {
            _CityLookUp.ClickMorenoValleyDropdown();
        }
        [Then(@"I click submit")]
        public void SubmitButtonClickAction()
        {
            _CityLookUp.SubmitButtonClick();
        }
        
    }
}