Feature: LookUpCityCityNameDisplayed

Scenario Outline: Check to verify the city name is correct.
    Given I am on the CityLookup Page
    When I find the search box
    When I type Mobile into search box
    When I click submit button
    Then I should see the city name