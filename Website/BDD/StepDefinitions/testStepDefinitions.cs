using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBDD.PageObjects;

namespace TestBDD.StepDefinitions
{
    [Binding]
    public sealed class testStepDefinitions
    {
        private readonly HomePage _HomePage;

        public testStepDefinitions(HomePage homePage)
        {
            _HomePage = homePage;
        }

        [Given(@"I am on the homepage")]
        public void CheckHomePage()
        {
            _HomePage.Goto(Common.HomePageName);
        }

        [Then(@"I will see a welcome message")]
        public void tester()
        {
            var message = _HomePage.GetWelcomeText;
            message.Should().BeTrue();
        }

        [When(@"I click searches")]
        public void CheckSearchBar()
        {
            var navbarDropdownMenuLink = _HomePage.GetDropDownText;
            navbarDropdownMenuLink.Should().BeTrue();
        }

        [Then(@"I will see a drop downlist")]
        public void CheckForItemInDropDown()
        {
            var navbarDropdownMenuLink = _HomePage.GetDropDownText;
            navbarDropdownMenuLink.Should().BeTrue();
        }

        [When(@"I click home listings search")]
        public void NavigateToANewPage()
        {
            _HomePage.ClickHomeListingButton();
        }
    }
}