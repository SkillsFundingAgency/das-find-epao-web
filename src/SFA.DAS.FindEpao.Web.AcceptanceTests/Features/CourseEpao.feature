Feature: Course EPAO
    AS an employer
    I WANT to be able to see the contact details of an End-point assessment organisation
    SO THAT I can contact those that assess the apprenticeships I am interested in.

Scenario: Basic content
When I navigate to the following url: /courses/5/assessment-organisations/EPA0014
Then an http status code of 200 is returned
And the page content includes the epao name

Scenario: Invalid EpaoId
When I navigate to the following url: /courses/5/assessment-organisations/PEE0014
Then an http status code of 200 is returned
And the page content includes the following: This end-point assessment organisation is currently unavailable for the course

Scenario: EpaoId not found
When I navigate to the following url: /courses/5/assessment-organisations/EPA9999
Then an http status code of 200 is returned
And the page content includes the following: This end-point assessment organisation is currently unavailable for the course
