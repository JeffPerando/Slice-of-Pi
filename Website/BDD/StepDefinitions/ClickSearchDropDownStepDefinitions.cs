using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBDD.PageObjects;

namespace TestBDD.StepDefinitions
{
    [Binding]
    public sealed class ClickSearchDropDownStepDefinitions
    {
        private readonly HomePage _HomePage;

        public ClickSearchDropDownStepDefinitions(HomePage homePage)
        {
            _HomePage = homePage;
        }

        [Given(@"I am on the Home Page")]
        public void CheckHomePage()
        {
            _HomePage.Goto(Common.HomePageName);
        }
        [When(@"I click the Searches drop down")]
        public void DropDownClicking()
        {
            _HomePage.ClickSearchesButton();
        }
        [Then(@"I will see the city crime look up")]
        public void VerifyCityCrimeLookUp()
        {
            _HomePage.GetCityLookUp.Should().BeTrue();
        }
        [When(@"I click the city crime look up")]
        public void ClickCityLookUp()
        {
            _HomePage.ClickCityLookUpButton();
        }
    }
}
