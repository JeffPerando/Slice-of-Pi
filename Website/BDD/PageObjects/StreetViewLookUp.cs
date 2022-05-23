using OpenQA.Selenium;
using SpecFlow.Actions.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.ObjectModel;

namespace TestBDD.PageObjects
{
    public class StreetViewLookUp : Page
    {
        private IWebElement SearchBoxStreetName => _browserInteractions.WaitAndReturnElement(By.Id("streetAddress"));
        private IWebElement SearchBoxCityName => _browserInteractions.WaitAndReturnElement(By.Id("city"));
        private IWebElement SearchBoxStates => _browserInteractions.WaitAndReturnElement(By.Id("states"));
        private IWebElement SubmitButton => _browserInteractions.WaitAndReturnElement(By.Id("submit_button"));
        public StreetViewLookUp(IBrowserInteractions browserInteractions)
            : base(browserInteractions)
        {
            PageName = Common.HomePageName;
        }

        public void EnterStreetName(string streetName)
        {
            SearchBoxStreetName.SendKeys(streetName);
        }
        public void EnterCityName(string cityName)
        {
            SearchBoxCityName.SendKeys(cityName);
        }
        public void ClickStateDropdown()
        {
            SearchBoxStates.ClickWithRetry();
        }
        public void ClickSubmitButton()
        {
            SubmitButton.ClickWithRetry();
        }
    }
}