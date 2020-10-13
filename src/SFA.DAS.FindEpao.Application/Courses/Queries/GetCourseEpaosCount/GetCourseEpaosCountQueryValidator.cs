using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.FindEpao.Domain.Validation;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaosCount
{
    public class GetCourseEpaosCountQueryValidator : IValidator<GetCourseEpaosCountQuery>
    {
        private readonly ICourseService _courseService;

        public GetCourseEpaosCountQueryValidator(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<ValidationResult> ValidateAsync(GetCourseEpaosCountQuery item)
        {
            var validationResult = new ValidationResult();

            var courseList = await _courseService.GetCourses();

            if (courseList.Courses.All(listItem => listItem.Id != item.CourseId))
            {
                validationResult.AddError(
                    nameof(GetCourseEpaosCountQuery.CourseId), 
                    $"{nameof(GetCourseEpaosCountQuery.CourseId)} not found");
            }

            return validationResult;
        }
    }
}
