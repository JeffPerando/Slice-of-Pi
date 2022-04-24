using OpenQA.Selenium;
using SpecFlow.Actions.Selenium;

namespace TestBDD.PageObjects
{
    // Base class for all pages, provides common functionality
    public class Page
    {
        protected readonly IBrowserInteractions _browserInteractions;

        public Page(IBrowserInteractions browserInteractions)
        {
            _browserInteractions = browserInteractions;
        }
        
        protected string PageName { get; set; } = SiteData.HomePageName;

        // Goto this page.  Preferred way for derived classes
        public virtual void Goto()
        {
            _browserInteractions.GoToUrl("https://localhost:44355/home");
        }

        // If you need to just go to a named page
        public virtual void Goto(string pageName)
        {
            _browserInteractions.GoToUrl(SiteData.UrlFor(pageName));
        }

        // Not ideal to have the page itself return the URL but the page is currently our only
        // access to the browser, so for now put it here.  i.e. each page is a proxy for the browser
        public virtual string GetCurrentUrl()
        {
            return _browserInteractions.GetUrl();
        }
    }
}
