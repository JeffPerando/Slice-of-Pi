using OpenQA.Selenium;
using SpecFlow.Actions.Selenium;
using System.Collections.ObjectModel;

namespace TestBDD.PageObjects
{
    public class HomePage : Page
    {
        private IWebElement Title => _browserInteractions.WaitAndReturnElement(By.Id("displayMessage"));
        private IWebElement WelcomeText => _browserInteractions.WaitAndReturnElement(By.Id("message"));
        private IWebElement DropDownText => _browserInteractions.WaitAndReturnElement(By.Id("navbarDropdownMenuLink"));
        private IEnumerable<IWebElement> AppleButtons => _browserInteractions.WaitAndReturnElements(By.CssSelector("#listOfApples button"));
        private IEnumerable<IWebElement> NavBar => _browserInteractions.WaitAndReturnElements(By.CssSelector("#navbarDropdownMenuLink a"));
        private IWebElement ServerNav => _browserInteractions.WaitAndReturnElement(By.Id("serversNav"));
        private IWebElement DropDownTextHomeListing => _browserInteractions.WaitAndReturnElement(By.Id("HomeListingsLink"));

        public HomePage(IBrowserInteractions browserInteractions)
            : base(browserInteractions)
        {
            PageName = Common.HomePageName;
        }

        public string GetServerNav => ServerNav.Text;
        public string GetTitle => Title.Text;
        public bool GetWelcomeText => WelcomeText.Displayed;
        public bool GetDropDownText => DropDownText.Displayed;
        public bool GetDropDownTextHomeListingItem => DropDownTextHomeListing.Displayed;
        public string GetAppleButtonText(int index) => AppleButtons.ElementAt(index).Text;
        public IEnumerable<string> GetAppleButtonTexts() => AppleButtons.Select(x => x.Text);

        public void ClickAppleButton(int index)
        {
            AppleButtons.ElementAt(index).Click();
        }
        public void ClickHomeListingButton()
        {
            var x = DropDownTextHomeListing.FindElement(By.LinkText("HomeListingsLink"));
            var navigation = x.GetAttribute("href");
        }
    }
}