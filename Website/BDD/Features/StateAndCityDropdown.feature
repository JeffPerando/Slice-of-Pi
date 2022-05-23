
Feature: StateAndCityDropdown

Tests the basic functionality of the city dropdown feature. See the city crime search

Scenario: Selecting a State
	Given I am on the city crime search page
	When I select a state
	Then I should see a list of cities
