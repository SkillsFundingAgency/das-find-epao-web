Feature: Error
	As a Find EPAO user
	I want a clear error page
	So that it is clear what actions I can take


Scenario: Navigate to page not found
When I navigate to the following url: /error/404
Then an http status code of 200 is returned
And the page content includes the following: Page not found

Scenario: Navigate to application error
When I navigate to the following url: /error/500
Then an http status code of 200 is returned
And the page content includes the following: Sorry, there is a problem with the service