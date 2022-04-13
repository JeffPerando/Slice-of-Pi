Feature: HighLightedCities

A short summary of the feature

Scenario: Clicking the texts sens you to approriate city crime lookup
    Given I am on the homepage
    Then I will be able to find recommended states
    When I click the text 
    Then I will be redirected to the StateCrime page
    And I should see crime statistics for the selected state

Scenario Outline: Visitor are rediretced to Amherst, NH
    Given I am on the Home page
    And I click the text <IvansText>
    Then I am redirected to the <CrimePage> Page
        And <IvansText> is equal to <input Value>

    Examples: 
    | input Value |
    | Amherst, NH |

Scenario Outline: Visitor are rediretced to Sedona, Az
    Given I am on the Home page
    And I click the text <IvansText>
    Then I am redirected to the <CrimePage> Page
        And <IvansText> is equal to <input Value>

    Examples: 
    | input Value |
    | Sedona, Az |

Scenario Outline: Visitor are rediretced to Lake Oswego, OR
    Given I am on the Home page
    And I click the text <IvansText>
    Then I am redirected to the <CrimePage> Page
        And <IvansText> is equal to <input Value>

    Examples: 
    | input Value |
    | Lake Oswego, OR |

Scenario Outline: Visitor are rediretced to Greenwood Village, CO
    Given I am on the Home page
    And I click the text <IvansText>
    Then I am redirected to the <CrimePage> Page
        And <IvansText> is equal to <input Value>

    Examples: 
    | input Value |
    | Greenwood Village, CO |

Scenario Outline: Visitor are rediretced to Evanston, IL
    Given I am on the Home page
    And I click the text <IvansText>
    Then I am redirected to the <CrimePage> Page
        And <IvansText> is equal to <input Value>

    Examples: 
    | input Value |
    | Evanston, IL |

Scenario Outline: Visitor are rediretced to Shaker Heights, OH
    Given I am on the Home page
    And I click the text <IvansText>
    Then I am redirected to the <CrimePage> Page
        And <IvansText> is equal to <input Value>

    Examples: 
    | input Value |
    | Shaker Heights, OH |