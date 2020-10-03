using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SFA.DAS.FindEpao.Domain.Configuration;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Courses.Api;
using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Application.Courses.Services
{
    public class CourseService : ICourseService
    {
        private readonly IApiClient _apiClient;
        private readonly FindEpaoApi _config;

        public CourseService(IApiClient apiClient, IOptions<FindEpaoApi> options)
        {
            _apiClient = apiClient;
            _config = options.Value;
        }

        public async Task<CourseList> GetCourses()
        {
            var apiRequest = new GetCoursesApiRequest(_config.BaseUrl);
            var courseList = await _apiClient.Get<CourseList>(apiRequest);
            return courseList;
        }
    }
}
