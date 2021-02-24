Feature: IntegratedApprenticeship
	In order to find an assessor
	As an employer or provider
	I want to know when a course is an integrated apprenticeship


Scenario: Basic content
When I navigate to the following url: /courses/14/course-integrated-apprenticeship
Then an http status code of 200 is returned
And the page content includes the following: The training provider will carry out the end-point assessment

Scenario: Not ingetrated course
When I navigate to the following url: /courses/1/course-integrated-apprenticeship
Then an http status code of 200 is returned
And the page content includes the following: Page not found
