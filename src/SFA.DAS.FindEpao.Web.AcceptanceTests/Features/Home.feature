Feature: Home
	As a Find EPAO user
	I want a clear home page
	So that it is clear what actions I can take

@mytag
Scenario: Navigate to start page
When I navigate to the following url: /
Then an http status code of 200 is returned
And the page content includes the following: Find an apprenticeship assessment organisation
