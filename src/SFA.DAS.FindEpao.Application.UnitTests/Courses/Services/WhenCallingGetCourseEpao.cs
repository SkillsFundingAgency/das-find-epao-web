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
    public class WhenCallingGetCourseEpao
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Course_Epao_From_Api(
            string courseId,
            string epaoId,
            CourseEpao apiResponse,
            [Frozen] Mock<IApiClient> mockApiClient,
            CourseService service)
        {
            mockApiClient
                .Setup(client => client.Get<CourseEpao>(
                    It.Is<GetCourseEpaoApiRequest>(request => 
                        request.CourseId == courseId
                        && request.EpaoId == epaoId)))
                .ReturnsAsync(apiResponse);

            var result = await service.GetCourseEpao(courseId, epaoId);

            result.Should().BeEquivalentTo(apiResponse);
        }
    }
}
