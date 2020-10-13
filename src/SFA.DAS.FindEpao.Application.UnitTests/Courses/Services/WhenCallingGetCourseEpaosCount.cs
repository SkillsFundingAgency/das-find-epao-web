using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Services;
using SFA.DAS.FindEpao.Domain.Courses.Api;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Application.UnitTests.Courses.Services
{
    public class WhenCallingGetCourseEpaosCount
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Count_From_Api(
            string courseId,
            int apiResponse,
            [Frozen] Mock<IApiClient> mockApiClient,
            CourseService service)
        {
            mockApiClient
                .Setup(client => client.Get<int>(
                    It.Is<GetCourseEpaosCountApiRequest>(request => request.CourseId == courseId)))
                .ReturnsAsync(apiResponse);

            var result = await service.GetCourseEpaosCount(courseId);

            result.Should().Be(apiResponse);
        }
    }
}
