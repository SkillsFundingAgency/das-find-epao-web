using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.FindEpao.Domain.Validation;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaos
{
    public class GetCourseEpaosQueryValidator : IValidator<GetCourseEpaosQuery>
    {
        private readonly ICourseService _courseService;

        public GetCourseEpaosQueryValidator(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<ValidationResult> ValidateAsync(GetCourseEpaosQuery item)
        {
            var validationResult = new ValidationResult();

            var courseList = await _courseService.GetCourses();

            if (courseList.Courses.All(listItem => listItem.Id != item.CourseId))
            {
                validationResult.AddError(
                    nameof(GetCourseEpaosQuery.CourseId), 
                    $"{nameof(GetCourseEpaosQuery.CourseId)} not found");
            }

            return validationResult;
        }
    }
}
