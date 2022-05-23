using OpenQA.Selenium;
using System;
using System.Diagnostics;
using TechTalk.SpecFlow;
using TestBDD.PageObjects;

namespace TestBDD.StepDefinitions
{
    [Binding]
    public class StateAndCityDropdownStepDefinitions
    {
        private readonly CityLookUpPage _page;

        public StateAndCityDropdownStepDefinitions(CityLookUpPage page)//, IWebDriver driver)
        {
            _page = page;
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

        }

        [Given(@"I am on the city crime search page")]
        public void GivenIAmOnTheCityCrimeSearchPage()
        {
            _page.Goto(Common.CityLookUpPageName);

        }

        [When(@"I select a state")]
        public void WhenISelectAState()
        {
            _page.EnterState("OR");
            _page.StateSelected.Selected.Should().BeTrue();

        }

        [Then(@"I should see a list of cities")]
        public void ThenIShouldSeeAListOfCities()
        {
            var count = _page.GetCityCount();
            count.Should().BeGreaterThan(1);

        }

    }

}
