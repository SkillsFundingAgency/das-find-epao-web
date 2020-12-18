using System.Threading.Tasks;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Courses.Api;
using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Application.Courses.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICacheStorageService _cacheStorageService;
        private readonly IApiClient _apiClient;
        private const int CacheDurationInHours = 1;

        public CourseService(
            ICacheStorageService cacheStorageService,
            IApiClient apiClient)
        {
            _cacheStorageService = cacheStorageService;
            _apiClient = apiClient;
        }

        public async Task<CourseList> GetCourses()
        {
            var cachedCourseList = await _cacheStorageService.RetrieveFromCache<CourseList>(nameof(CourseList));

            if (cachedCourseList != default)
            {
                return cachedCourseList;
            }

            var apiRequest = new GetCoursesApiRequest();
            var courseList = await _apiClient.Get<CourseList>(apiRequest);

            await _cacheStorageService.SaveToCache(nameof(CourseList), courseList, CacheDurationInHours);

            return courseList;
        }

        public async Task<CourseEpaos> GetCourseEpaos(string courseId)
        {
            var apiRequest = new GetCourseEpaosApiRequest{CourseId = courseId};
            var courseEpaos = await _apiClient.Get<CourseEpaos>(apiRequest);
            return courseEpaos;
        }

        public async Task<CourseListItem> GetCourse(string courseId)
        {
            var request = new GetCourseApiRequest(courseId);
            var course = await _apiClient.Get<CourseItem>(request);
            return course.Course;
        }
    }
}
