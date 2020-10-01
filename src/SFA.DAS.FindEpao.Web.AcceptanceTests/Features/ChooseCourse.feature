Feature: ChooseCourse
	In order to find an assessor
	As an employer or provider
	I want to choose the course I want an assessor for


Scenario: Basic content
When I navigate to the following url: /courses
Then an http status code of 200 is returned
And the page content includes the following: What is the apprenticeship training course?
# may also be possible to assert each course in list with no js

Scenario: Validation failure