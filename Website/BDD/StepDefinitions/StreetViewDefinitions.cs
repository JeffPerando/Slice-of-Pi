using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestBDD.PageObjects;

namespace TestBDD.StepDefinitions
{
    [Binding]
    public sealed class StreetViewStepDefinitions
    {
        private readonly StreetView _StreetView;

        public StreetViewStepDefinitions(StreetView homePage)
        {
            _StreetView = homePage;
        }

        [Given(@"I am in index")]
        public void CheckHomePage()
        {
            _StreetView.Goto(Common.HomePageName);
        }

        [When(@"I click searches in the navbar")]
        public void CheckSearchBar()
        {
            var navbarDropdownMenuLink = _StreetView.GetTheDropDownText;
            navbarDropdownMenuLink.Should().BeTrue();
        }

        [Then(@"I will see a drop downlist pop up")]
        public void CheckForItemInDropDown()
        {
            var navbarDropdownMenuLink = _StreetView.CheckDropDownText;
            navbarDropdownMenuLink.Should().BeTrue();
        }

        [When(@"I click StreetView search")]
        public void NavigateToANewPage()
        {
            _StreetView.ClickStreetViewButton();
        }

        [Then(@"I will be directed to the StreetView page")]
        public void RedirectToStreetView()
        {
            var message = _StreetView.CheckRedirection;
            message.Should().BeTrue();
        }
    }
}