using OpenQA.Selenium;
using SpecFlow.Actions.Selenium;
using System.Collections.ObjectModel;

namespace TestBDD.PageObjects
{
    public class CityLookUpPage : Page
    {
        private IWebElement SearchBox => _browserInteractions.WaitAndReturnElement(By.Id("cityName"));
        private IWebElement SubmitButton => _browserInteractions.WaitAndReturnElement(By.Id("submit_button"));
        private IWebElement DropDownMenu => _browserInteractions.WaitAndReturnElement(By.Id("states"));
        private IWebElement California => _browserInteractions.WaitAndReturnElement(By.Id("California"));
        private IWebElement MorenoValley => _browserInteractions.WaitAndReturnElement(By.Id("Moreno Valley"));
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
        public void ClickStatesDropdown()
        {
            DropDownMenu.ClickWithRetry();
        }
        public void SubmitButtonClick()
        {
            SubmitButton.ClickWithRetry();
        }
        public void ClickCaliforniaDropdown()
        {
            California.ClickWithRetry();
        }
        public void ClickMorenoValleyDropdown()
        {
            MorenoValley.ClickWithRetry();
        }
    }
}