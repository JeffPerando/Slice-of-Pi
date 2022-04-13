using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace TestBDD.StepDefinitions
{
    public class TestUser
    {
        public string Email { get; set; }

    }

    [Binding]
    public class UserSearchHistorySteps
    {
        private readonly ScenarioContext _scenarioContext;

        public UserSearchHistorySteps(ScenarioContext context)
        {
            _scenarioContext = context;

        }

        [Given(@"the following users exist")]
        public void GivenTheFollowingUsersExist(Table table)
        {
            IEnumerable<TestUser> users = table.CreateSet<TestUser>();
            _scenarioContext["Users"] = users;
        }

        [Given(@"I am not logged in")]
        public void GivenIAmNotLoggedIn()
        {
            throw new PendingStepException();
        }

        [Given(@"I am on the Searches page")]
        public void GivenIAmOnTheSearchesPage()
        {
            throw new PendingStepException();
        }

        [Then(@"I will be redirected to the login page")]
        public void ThenIWillBeRedirectedToTheLoginPage()
        {
            throw new PendingStepException();
        }

        [Given(@"I am logged in")]
        public void GivenIAmLoggedIn()
        {
            throw new PendingStepException();
        }

        [Then(@"I will see a table of searches, or a message telling me I have no searches")]
        public void ThenIWillSeeATableOfSearchesOrAMessageTellingMeIHaveNoSearches()
        {
            throw new PendingStepException();
        }
    }
}
