Feature: ChooseCourse
	In order to find an assessor
	As an employer or provider
	I want to choose the course I want an assessor for


Scenario: Basic content
When I navigate to the following url: /courses
Then an http status code of 200 is returned
And the page content includes the following: What is the apprenticeship training course?
And there is a row for each course

Scenario: Validation failure
When I navigate to the following url: /courses?selectedCourseId=-1
Then an http status code of 200 is returned
And the page content includes the following: There is a problem
And the page content includes the following: Select an apprenticeship training course
