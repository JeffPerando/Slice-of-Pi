Feature: test

Scenario: TestWebOpen
Given I am on the homepage
When I click Searches 
Then I will see a dropdown list
And If I click CityCrimeSearch 
Then I will be redirected to the CityCrimeLookUp Page
