using OpenQA.Selenium;
using SpecFlow.Actions.Selenium;
using System.Collections.ObjectModel;

namespace TestBDD.PageObjects
{
    public class StreetView : Page
    {
        //prepare object for selenium to work with
        private IWebElement DropDownList => _browserInteractions.WaitAndReturnElement(By.Id("navbarDropdownMenuLink"));
        private IWebElement DropDownTextStreetView => _browserInteractions.WaitAndReturnElement(By.Id("StreetViewLink"));
        private IWebElement CheckIfRedirectionIsTrue => _browserInteractions.WaitAndReturnElement(By.Id("message"));
        private IEnumerable<IWebElement> AppleButtons => _browserInteractions.WaitAndReturnElements(By.CssSelector("#navigation "));
        public StreetView(IBrowserInteractions browserInteractions)
            : base(browserInteractions)
        {
            PageName = Common.HomePageName;
        }
        //give objects life
        public bool CheckRedirection => CheckIfRedirectionIsTrue.Displayed;
        public bool CheckStreetViewText => DropDownTextStreetView.Displayed;
        public string TheDropDownText => DropDownList.Text;
        public bool GetTheDropDownText => DropDownList.Displayed;
        public bool CheckDropDownText => DropDownList.Displayed;
        //give selenium methods to interact with objects
        public void ClickSearchesButton()
        {
            DropDownList.ClickWithRetry();
        }
        public void ClickStreetViewButton()
        {
            var z = DropDownList.FindElement(By.LinkText("StreetViewLink"));
            var nav = z.GetAttribute("href");

            //var x = DropDownTextStreetView.Equals("StreetViewLink");
            //var navigation = x.ToString();

            
        }
        public void RedirectToStreetView()
        {
            var x = DropDownTextStreetView.FindElement(By.LinkText("StreetViewLink"));
            var navigation = x.GetAttribute("href");
        }
    }
}
