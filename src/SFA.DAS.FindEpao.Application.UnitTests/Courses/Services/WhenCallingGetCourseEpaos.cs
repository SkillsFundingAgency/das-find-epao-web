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
    public class WhenCallingGetCourseEpaos
    {
        [Test, MoqAutoData]
        public async Task And_Not_Cached_Then_Gets_Courses_From_Api(
            string courseId,
            CourseEpaos apiResponse,
            [Frozen] Mock<IApiClient> mockApiClient,
            CourseService service)
        {
            mockApiClient
                .Setup(client => client.Get<CourseEpaos>(
                    It.Is<GetCourseEpaosApiRequest>(request => request.CourseId == courseId)))
                .ReturnsAsync(apiResponse);

            var result = await service.GetCourseEpaos(courseId);

            result.Should().BeEquivalentTo(apiResponse);
        }
    }
}
