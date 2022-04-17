using System;
using TechTalk.SpecFlow;

namespace TestBDD.StepDefinitions
{
    [Binding]
    public class LookUpInformationWithYearDisplayed
    {

        private readonly Index _indexPageObject = new Index();

        [Given(@"I have entered a city name")]
        public void GivenIHaveEnteredACityName()
        {
            _indexPageObject.cityNameInput = "Monmouth";
        }

    }
}   
