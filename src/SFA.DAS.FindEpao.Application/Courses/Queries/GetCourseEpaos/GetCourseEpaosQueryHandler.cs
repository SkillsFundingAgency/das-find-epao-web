using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.FindEpao.Domain.Validation;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaos
{
    public class GetCourseEpaosQueryHandler : IRequestHandler<GetCourseEpaosQuery, GetCourseEpaosResult>
    {
        private readonly IValidator<GetCourseEpaosQuery> _validator;
        private readonly ICourseService _courseService;

        public GetCourseEpaosQueryHandler(
            IValidator<GetCourseEpaosQuery> validator,
            ICourseService courseService)
        {
            _validator = validator;
            _courseService = courseService;
        }

        public async Task<GetCourseEpaosResult> Handle(GetCourseEpaosQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid())
            {
                throw new ValidationException(validationResult.DataAnnotationResult,null, null);
            }

            var courseEpaos = await _courseService.GetCourseEpaos(query.CourseId);

            return new GetCourseEpaosResult
            {
                Course = courseEpaos.Course,
                Epaos = courseEpaos.Epaos
            };
        }
    }
}
