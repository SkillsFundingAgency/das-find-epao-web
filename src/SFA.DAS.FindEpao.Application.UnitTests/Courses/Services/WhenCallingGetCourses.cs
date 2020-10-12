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
        public async Task And_Not_Cached_Then_Gets_Courses_From_Api(
            CourseList coursesFromApi,
            [Frozen] Mock<ICacheStorageService> mockCacheService,
            [Frozen] Mock<IApiClient> mockApiClient,
            CourseService service)
        {
            mockCacheService
                .Setup(storageService => storageService.RetrieveFromCache<CourseList>(nameof(CourseList)))
                .ReturnsAsync((CourseList) default);
            mockApiClient
                .Setup(client => client.Get<CourseList>(It.IsAny<GetCoursesApiRequest>()))
                .ReturnsAsync(coursesFromApi);

            var result = await service.GetCourses();

            result.Courses.Should().BeEquivalentTo(coursesFromApi.Courses);
            mockCacheService.Verify(storageService => storageService.SaveToCache(nameof(CourseList), coursesFromApi, 1), 
                Times.Once);
        }

        [Test, MoqAutoData]
        public async Task And_Is_Cached_Then_Gets_Courses_From_Cache(
            CourseList coursesFromCache,
            CourseList coursesFromApi,
            [Frozen] Mock<ICacheStorageService> mockCacheService,
            [Frozen] Mock<IApiClient> mockApiClient,
            CourseService service)
        {
            mockCacheService
                .Setup(storageService => storageService.RetrieveFromCache<CourseList>(nameof(CourseList)))
                .ReturnsAsync(coursesFromCache);

            var result = await service.GetCourses();

            result.Courses.Should().BeEquivalentTo(coursesFromCache.Courses);
            mockApiClient.Verify(client => client.Get<CourseList>(It.IsAny<GetCoursesApiRequest>()), 
                Times.Never);
        }
    }
}
