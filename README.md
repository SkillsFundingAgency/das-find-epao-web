# das-find-epao-web


[![Build Status](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_apis/build/status/SkillsFundingAgency.das-find-epao-web?branchName=master)](https://sfa-gov-uk.visualstudio.com/Digital%20Apprenticeship%20Service/_build/latest?definitionId=2181&branchName=master)

## Requirements

DotNet Core 3.1 and any supported IDE for DEV running.

## About

This is the replacement for the Find EPAO capability of the current Find Apprenticeship Training Service. Find EPAO is now it's own service. It provides users with a way to find an EPAO for a particular course.

## Local running

appsettings.json should have the following section:

```
"FindEpaoApi": {
    "Key": "test",
    "BaseUrl": "http://localhost:5007/",
    "PingUrl": "http://localhost:5007/"
  }
```

The important part of the configuration is making sure that your BaseUrl is pointed to the MockServer url

You are able to run the website by doing the following:
* Run the console app ```SFA.DAS.FindEpao.MockServer``` - this will create a mock server on http://localhost:5007
* Start the web solution ```SFA.DAS.FindEpao.Web```


## Useful URLs


### Course EPAOs
https://localhost:5011/courses/14/assessment-organisations -> Integrated Apprenticeship

https://localhost:5011/courses/2/assessment-organisations -> No EPAOs available



### Course EPAO Details
https://localhost:5011/courses/14/assessment-organisations/epa0014 -> Integrated Course EPAO Detail

https://localhost:5011/courses/2/assessment-organisations/epa0014 -> No epao available

