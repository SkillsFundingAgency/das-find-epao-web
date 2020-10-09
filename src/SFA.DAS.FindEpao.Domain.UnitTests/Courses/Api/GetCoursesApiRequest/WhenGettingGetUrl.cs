using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;

namespace SFA.DAS.FindEpao.Domain.UnitTests.Courses.Api.GetCoursesApiRequest
{
    public class WhenGettingGetUrl
    {
        [Test, AutoData]
        public void Then_Returns_Correct_Path(Domain.Courses.Api.GetCoursesApiRequest request)
        {
            request.GetUrl.Should().Be("courses");
        }
    }
}
