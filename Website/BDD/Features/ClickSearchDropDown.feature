Feature: ClickSearchDropDown

Scenario Outline: Visitors can click the nav bar and go to the CityCrimeLookUp Page from Home.
    Given I am on the Home Page
    When I click the Searches drop down
    Then I will see the city crime look up
    When I click the city crime look up