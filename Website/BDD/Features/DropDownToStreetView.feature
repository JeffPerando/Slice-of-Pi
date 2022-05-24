Feature: DropDownToStreetView

Scenario Outline: Go from main page to city look up.
    Given I am on the Home Page
    When I click the Searches drop down
    Then I will see the home address look up
    When I enter 1006 McCay Ave into the street address
    Then I enter Mobile into the city
    Then I select Alabama as the state
    Then I click the submit button
    Then I should be on the street view page
    