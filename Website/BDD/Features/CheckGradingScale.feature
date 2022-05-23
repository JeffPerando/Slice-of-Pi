Feature: CheckGradingScale.feature

Scenario Outline: Look up city to see grade.
    Given I am on the Home Page
    When I click the Searches drop down
    Then I click city view tab
    Then I enter California as state
    Then I enter Moreno Valley as the city