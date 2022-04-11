using OpenQA.Selenium;
using SpecFlow.Actions.Selenium;

namespace TestBDD.PageObjects
{
    public class LoginPage : Page
    {
        // Select all the elements needed for tests
        private IWebElement EmailInput => _browserInteractions.WaitAndReturnElement(By.Id("Input_Email"));
        private IWebElement PasswordInput => _browserInteractions.WaitAndReturnElement(By.Id("Input_Password"));
        private IWebElement LoginButton => _browserInteractions.WaitAndReturnElement(By.Id("login-submit"));
        // Look for a div that has class validation-summary-errors.
        // This one is fragile because it assumes this is the method to show the error.  UI tests are obviously tightly coupled to the UI though.
        // Probably could be improved by having the UI add an Id to a div that only exists if there are errors with the login
        private IEnumerable<IWebElement> ValidationErrors => _browserInteractions.WaitAndReturnElements(By.CssSelector("div.validation-summary-errors"));


        // Need to still have the constructor parameter here for dependency injection, just pass it up to the base class.  You can't
        // have constructor injection in a base class because it is invoked from the derived class instead of the DI container
        public LoginPage(IBrowserInteractions browserInteractions)
            :base(browserInteractions)
        {
            PageName = SiteData.LoginPageName;
        }

        public void EnterEmail(string email)
        {
            EmailInput.SendKeysWithClear(email);
        }

        public void EnterPassword(string password)
        {
            PasswordInput.SendKeysWithClear(password);
        }

        public void Login()
        {
            LoginButton.ClickWithRetry();
        }

        public bool HasLoginError() => ValidationErrors.Count() > 0;

    }
}
