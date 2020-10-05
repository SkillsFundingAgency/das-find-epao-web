﻿using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SFA.DAS.FindEpao.Domain.Configuration;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Courses.Api;
using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Application.Courses.Services
{
    public class CourseService : ICourseService
    {
        private readonly ICacheStorageService _cacheStorageService;
        private readonly IApiClient _apiClient;
        private readonly FindEpaoApi _config;
        private const int CacheDurationInHours = 1;

        public CourseService(
            ICacheStorageService cacheStorageService,
            IApiClient apiClient, 
            IOptions<FindEpaoApi> options)
        {
            _cacheStorageService = cacheStorageService;
            _apiClient = apiClient;
            _config = options.Value;
        }

        public async Task<CourseList> GetCourses()
        {
            var cachedCourseList = await _cacheStorageService.RetrieveFromCache<CourseList>(nameof(CourseList));

            if (cachedCourseList != default)
            {
                return cachedCourseList;
            }

            var apiRequest = new GetCoursesApiRequest(_config.BaseUrl);
            var courseList = await _apiClient.Get<CourseList>(apiRequest);

            await _cacheStorageService.SaveToCache(nameof(CourseList), courseList, CacheDurationInHours);

            return courseList;
        }
    }
}