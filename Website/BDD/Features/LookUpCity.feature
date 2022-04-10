
Scenario Outline: Visitor can lookup crime statistics
    Given I am on the Home page
    When I find the <CityLookUp>
    And I search <city>
    And I select <state>
    And I submit the <statesearchbutton>
    Then I am redirected to the <CrimePage> Page
        And <CityName> is equal to <city>
    
    Examples: 
    | city | state |
    | Monmouth | OR |
    | Arlington | TX|
    | New York City | NY | 
    | Washington | DC | 