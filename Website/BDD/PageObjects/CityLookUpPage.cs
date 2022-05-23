using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpecFlow.Actions.Selenium;
using System.Collections.ObjectModel;

namespace TestBDD.PageObjects
{
    public class CityLookUpPage : Page
    {
        private IWebElement StateSelect => _browserInteractions.WaitAndReturnElement(By.Name("states"));
        private SelectElement CitySelect => (SelectElement)_browserInteractions.WaitAndReturnElement(By.Name("cities"));
        private IWebElement SubmitButton => _browserInteractions.WaitAndReturnElement(By.Id("submit_button"));
        
        public CityLookUpPage(IBrowserInteractions browserInteractions)
            : base(browserInteractions)
        {
            PageName = Common.CityLookUpPageName;

        }

        public IWebElement StateSelected => StateSelect.GetSelectElement().SelectedOption;
        public bool CitySearchBox => CitySelect.WrappedElement.Displayed;
        public bool GetSubmitButton => SubmitButton.Displayed;

        public void EnterState(string stateAbbr)
        {
            StateSelect.SelectDropdownOptionByValue(stateAbbr);
        }

        public void EnterCityName(string cityName)
        {
            CitySelect.WrappedElement.SelectDropdownOptionByValue(cityName);
        }
        
        public int GetCityCount()
        {
            return CitySelect.Options.Count;
        }

        public void SubmitButtonClick()
        {
            SubmitButton.ClickWithRetry();
        }
    }
}