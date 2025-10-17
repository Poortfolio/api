Feature: Weather Forecast API
    As a user of the weather API
    I want to retrieve weather forecast information
    So that I can plan my activities

Background:
    Given the weather forecast API is available

Scenario: Get weather forecast returns data
    When I request the weather forecast
    Then the response should be successful
    And the response should contain forecast data
    And each forecast should have a date
    And each forecast should have a temperature
    And each forecast should have a summary

Scenario: Weather forecast returns multiple days
    When I request the weather forecast
    Then the response should contain 5 forecast items

Scenario: Weather forecast has valid temperature range
    When I request the weather forecast
    Then all temperatures should be between -20 and 50 degrees Celsius

Scenario: Weather forecast summaries are valid
    When I request the weather forecast
    Then all summaries should be valid weather descriptions
