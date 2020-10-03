using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;

namespace SFA.DAS.FindEpao.Domain.UnitTests.Courses.Api.GetCoursesApiRequest
{
    public class WhenConstructingAGetCoursesApiRequest
    {
        [Test, AutoData]
        public void Then_Sets_BaseUrl(string baseUrl)
        {
            var request = new Domain.Courses.Api.GetCoursesApiRequest(baseUrl);

            request.BaseUrl.Should().Be(baseUrl);
        }
    }
}
