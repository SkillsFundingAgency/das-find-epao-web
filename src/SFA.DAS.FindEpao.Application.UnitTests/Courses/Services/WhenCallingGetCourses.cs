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
    public class WhenCallingGetCourses
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Courses_From_Api(
            CourseList coursesFromApi,
            [Frozen] Mock<IApiClient> mockApiClient,
            CourseService service)
        {
            mockApiClient
                .Setup(client => client.Get<CourseList>(It.IsAny<GetCoursesApiRequest>()))
                .ReturnsAsync(coursesFromApi);

            var result = await service.GetCourses();

            result.Courses.Should().BeEquivalentTo(coursesFromApi.Courses);
        }
    }
}
