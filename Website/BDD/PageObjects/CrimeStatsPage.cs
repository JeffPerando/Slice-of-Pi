using OpenQA.Selenium;
using SpecFlow.Actions.Selenium;
using System.Collections.ObjectModel;

namespace TestBDD.PageObjects
{
    public class CrimeStatsPage : Page
    {
        private IWebElement cityName => _browserInteractions.WaitAndReturnElement(By.Id("cityNameDisplayed"));
        public CrimeStatsPage(IBrowserInteractions browserInteractions)
            : base(browserInteractions)
        {
            PageName = Common.CrimeStatsPageName;
        }

        public bool GetCityName => cityName.Displayed;
    }
}