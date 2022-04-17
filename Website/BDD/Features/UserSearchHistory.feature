Feature: As a user, I want to be able to see and download my previous searches

Background:
	Given the following users exist
		| Email |
		| realmsofcaelum@gmail.com |

Scenario: Guests can't see searches page
	Given I am a user
	And I am not logged in
	And I am on the Searches page
	Then I will be redirected to the login page

Scenario: User can see searches page
	Given I am a user
	And I am logged in
	And I am on the Searches page
	Then I will see a table of searches, or a message telling me I have no searches

# Scenario: User can create search history
	# Given I am a user
	# And I am logged in
	# And I search for a state and year
	# Then it should appear in the Searches page

# Scenario: User can download searches
	# Given I am a user
	# And I am logged in
	# And I am on the Searches page
	# Then I should see a Download button
	# And clicking it should start a file download