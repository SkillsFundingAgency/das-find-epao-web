using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;

namespace SFA.DAS.FindEpao.Domain.UnitTests.Courses.Api.GetCoursesApiRequest
{
    public class WhenCallingBuildGetUrl
    {
        [Test, AutoData]
        public void Then_Sets_BaseUrl(string baseUrl, Domain.Courses.Api.GetCoursesApiRequest request)
        {
            request.BuildGetUrl(baseUrl).Should().Be($"{baseUrl}courses");
        }
    }
}
