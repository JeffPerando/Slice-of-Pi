using OpenQA.Selenium;
using SpecFlow.Actions.Selenium;
using System.Collections.ObjectModel;

namespace TestBDD.PageObjects
{
    public class CityLookUpPage : Page
    {
        private IWebElement SearchBox => _browserInteractions.WaitAndReturnElement(By.Id("cityName"));
        private IWebElement SubmitButton => _browserInteractions.WaitAndReturnElement(By.Id("submit_button"));
        public CityLookUpPage(IBrowserInteractions browserInteractions)
            : base(browserInteractions)
        {
            PageName = Common.CityLookUpPageName;
        }

        public bool GetSearchBox => SearchBox.Displayed;
        public bool GetSubmitButton => SubmitButton.Displayed;

        public void EnterCityName(string cityName)
        {
            SearchBox.SendKeys(cityName);
        }

        public void SubmitButtonClick()
        {
            SubmitButton.ClickWithRetry();
        }
    }
}