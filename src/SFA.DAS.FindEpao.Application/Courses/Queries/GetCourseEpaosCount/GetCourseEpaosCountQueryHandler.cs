using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.FindEpao.Domain.Validation;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaosCount
{
    public class GetCourseEpaosCountQueryHandler : IRequestHandler<GetCourseEpaosCountQuery, GetCourseEpaosCountResult>
    {
        private readonly IValidator<GetCourseEpaosCountQuery> _validator;
        private readonly ICourseService _courseService;

        public GetCourseEpaosCountQueryHandler(
            IValidator<GetCourseEpaosCountQuery> validator,
            ICourseService courseService)
        {
            _validator = validator;
            _courseService = courseService;
        }

        public async Task<GetCourseEpaosCountResult> Handle(GetCourseEpaosCountQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid())
            {
                throw new ValidationException(validationResult.DataAnnotationResult,null, null);
            }

            var epaosCount = await _courseService.GetCourseEpaosCount(query.CourseId);

            return new GetCourseEpaosCountResult
            {
                Total = epaosCount
            };
        }
    }
}
