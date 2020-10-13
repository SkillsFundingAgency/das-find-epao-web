using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;

namespace SFA.DAS.FindEpao.Domain.UnitTests.Courses.Api.GetCourseEpaosCountApiRequest
{
    public class WhenGettingGetUrl
    {
        [Test, AutoData]
        public void Then_Returns_Correct_Path(Domain.Courses.Api.GetCourseEpaosCountApiRequest request)
        {
            request.GetUrl.Should().Be($"courses/{request.CourseId}/epaos-count");
        }
    }
}
