using OpenQA.Selenium;
using SpecFlow.Actions.Selenium;
using System.Collections.ObjectModel;

namespace TestBDD.PageObjects
{
    public class HomePage : Page
    {
        private IWebElement Title => _browserInteractions.WaitAndReturnElement(By.Id("displayMessage"));
        private IWebElement WelcomeText => _browserInteractions.WaitAndReturnElement(By.Id("message"));
        private IEnumerable<IWebElement> AppleButtons => _browserInteractions.WaitAndReturnElements(By.CssSelector("#listOfApples button"));
        private IWebElement ServerNav => _browserInteractions.WaitAndReturnElement(By.Id("serversNav"));
        private IWebElement DropDownList => _browserInteractions.WaitAndReturnElement(By.Id("navbarDropdownMenuLink"));
        private IWebElement DropDown_CityLookUp => _browserInteractions.WaitAndReturnElement(By.Id("CityCrimeLookUpLink"));
        public HomePage(IBrowserInteractions browserInteractions)
            : base(browserInteractions)
        {
            PageName = Common.HomePageName;
        }

        public string GetServerNav => ServerNav.Text;
        public string GetTitle => Title.Text;
        public bool GetWelcomeText => WelcomeText.Displayed;
        
        public bool GetCityLookUp => DropDown_CityLookUp.Displayed;
        public string GetDropDownText => DropDownList.Text;

        public string GetAppleButtonText(int index) => AppleButtons.ElementAt(index).Text;


        public IEnumerable<string> GetAppleButtonTexts() => AppleButtons.Select(x => x.Text);

        public void ClickSearchesButton()
        {
            DropDownList.ClickWithRetry();
        }
        public void ClickCityLookUpButton()
        {
            DropDown_CityLookUp.ClickWithRetry();
        }

        public void ClickAppleButton(int index)
        {
            AppleButtons.ElementAt(index).Click();
        }


    }
}