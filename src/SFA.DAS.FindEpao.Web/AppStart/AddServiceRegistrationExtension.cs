using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.FindEpao.Application.Courses.Services;
using SFA.DAS.FindEpao.Application.Epaos.Services;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.FindEpao.Infrastructure.Api;
using SFA.DAS.FindEpao.Infrastructure.Services;
using SFA.DAS.FindEpao.Web.Infrastructure.Interfaces;
using SFA.DAS.FindEpao.Web.Services;

namespace SFA.DAS.FindEpao.Web.AppStart
{
    public static class AddServiceRegistrationExtension
    {
        public static void AddServiceRegistration(this IServiceCollection services)
        {
            services.AddHttpClient<IApiClient, ApiClient>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<IEpaoService, EpaoService>();
            services.AddHttpContextAccessor();
            services.AddTransient<ICacheStorageService, CacheStorageService>();
            services.AddTransient<ILocationStringBuilder, LocationStringBuilderService>();
        }
    }
}
