using Microsoft.Extensions.DependencyInjection;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaos;
using SFA.DAS.FindEpao.Domain.Validation;

namespace SFA.DAS.FindEpao.Web.AppStart
{
    public static class MediatRExtensions
    {
        public static void AddMediatRValidation(this IServiceCollection services)
        {
            services.AddScoped(typeof(IValidator<GetCourseEpaosQuery>), typeof(GetCourseEpaosQueryValidator));
        }
    }
}
