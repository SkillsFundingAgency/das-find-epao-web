using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;

namespace SFA.DAS.FindEpao.Domain.UnitTests.Courses.Api.GetCoursesApiRequest
{
    public class WhenGettingGetCourseUrl
    {
        [Test, AutoData]
        public void Then_Returns_Correct_Path(string courseId)
        {
            //Act
            var actual = new Domain.Courses.Api.GetCourseApiRequest(courseId);
            
            //Assert
            actual.GetUrl.Should().Be($"courses/{courseId}");
        }
    }
}