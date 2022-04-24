Feature: LookUpCity

Scenario Outline: Visitor can lookup crime statistics
    Given I am on the City Lookup Page
    When I see the search box
    When I enter Mobile into search box
    Then I click the submit button
