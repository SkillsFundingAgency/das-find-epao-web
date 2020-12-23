Feature: Course EPAOs
	In order to find an assessor
	As an employer or provider
	I want to see a list of assessors that provide a given course

Scenario: Basic content
When I navigate to the following url: /courses/5/assessment-organisations 
Then an http status code of 200 is returned
And the page content includes the following: assessment organisations
And there is a row for each epao

Scenario: No EPAOs for selected course
When I navigate to the following url: /courses/2/assessment-organisations
Then an http status code of 200 is returned
And the page content includes the following: 0 end-point assessment organisations 