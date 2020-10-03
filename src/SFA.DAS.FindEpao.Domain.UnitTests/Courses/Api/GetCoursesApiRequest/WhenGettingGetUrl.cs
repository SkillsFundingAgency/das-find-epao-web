using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;

namespace SFA.DAS.FindEpao.Domain.UnitTests.Courses.Api.GetCoursesApiRequest
{
    public class WhenGettingGetUrl
    {
        [Test, AutoData]
        public void Then_Returns_Correct_Url(string baseUrl)
        {
            var request = new Domain.Courses.Api.GetCoursesApiRequest(baseUrl);

            request.GetUrl.Should().Be($"{baseUrl}courses");
        }
    }
}
