using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.FindEpao.Application.Courses.Services;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.FindEpao.Infrastructure.Api;

namespace SFA.DAS.FindEpao.Web.AppStart
{
    public static class AddServiceRegistrationExtension
    {
        public static void AddServiceRegistration(this IServiceCollection services)
        {
            services.AddHttpClient<IApiClient, ApiClient>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddHttpContextAccessor();
        }
    }
}
