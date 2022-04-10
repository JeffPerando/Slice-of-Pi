using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpecFlow.Actions.Selenium;
using System.Collections.ObjectModel;

namespace TestBDD.PageObjects
{
    public class Index
    {
        //The URL of the calculator to be opened in the browser
        private const string CalculatorUrl = "https://specflowoss.github.io/Calculator-Demo/Calculator.html";

        //The Selenium web driver to automate the browser
        private readonly IWebDriver _webDriver;

        //The default wait time in seconds for wait.Until
        public const int DefaultWaitInSeconds = 5;

        public Index(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        //Finding elements by ID
        private IWebElement FirstNumberElement => _webDriver.FindElement(By.Id("tests"));
        private IWebElement SecondNumberElement => _webDriver.FindElement(By.Id("Lake Oswego, OR"));
        private IWebElement AddButtonElement => _webDriver.FindElement(By.Id("Tests"));


        public void EnterFirstNumber(string number)
        {
            //Clear text box
            FirstNumberElement.Clear();
            //Enter text
            FirstNumberElement.SendKeys(number);
        }

        public void EnterSecondNumber(string number)
        {
            //Clear text box
            SecondNumberElement.Clear();
            //Enter text
            SecondNumberElement.SendKeys(number);
        }

        public void ClickAdd()
        {
            //Click the add button
            AddButtonElement.Click();
        }
    
    }
}