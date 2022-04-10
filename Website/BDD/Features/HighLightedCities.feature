Feature: HighLightedCities

A short summary of the feature

Scenario Outline: Visitor or user can select a text field from the suggested cities
    Given I am on the Home page
    And I submit the <IvansText>
    Then I am redirected to the <CrimePage> Page
        And <CityName> is equal to <city>

    Examples: 
    | city | state |
    | LakeOswego | OR |
    | Arlington | TX|
    | New York City | NY | 
    | Washington | DC |