using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Services;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Courses.Api;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Application.UnitTests.Courses.Services
{
    public class WhenCallingGetCourse
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Course_From_Api(
            string courseId,
            CourseItem apiResponse,
            [Frozen] Mock<IApiClient> mockApiClient,
            CourseService service)
        {
            mockApiClient
                .Setup(client => client.Get<CourseItem>(
                    It.Is<GetCourseApiRequest>(request => request.GetUrl == $"courses/{courseId}")))
                .ReturnsAsync(apiResponse);

            var result = await service.GetCourse(courseId);

            result.Should().BeEquivalentTo(apiResponse.Course);
        }
    }
}