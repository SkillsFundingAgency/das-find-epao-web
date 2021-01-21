using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.FindEpao.Domain.Validation;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpao
{
    public class GetCourseEpaoQueryHandler : IRequestHandler<GetCourseEpaoQuery, GetCourseEpaoResult>
    {
        private readonly IValidator<GetCourseEpaoQuery> _validator;
        private readonly ICourseService _courseService;

        public GetCourseEpaoQueryHandler(
            IValidator<GetCourseEpaoQuery> validator,
            ICourseService courseService)
        {
            _validator = validator;
            _courseService = courseService;
        }

        public async Task<GetCourseEpaoResult> Handle(GetCourseEpaoQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid())
            {
                throw new ValidationException(validationResult.DataAnnotationResult,null, null);
            }

            var courseEpao = await _courseService.GetCourseEpao(query.CourseId, query.EpaoId);

            return new GetCourseEpaoResult
            {
                Course = courseEpao.Course,
                Epao = courseEpao.Epao,
                CourseEpaosCount = courseEpao.CourseEpaosCount,
                EffectiveFrom = courseEpao.EffectiveFrom,
                EpaoDeliveryAreas = courseEpao.EpaoDeliveryAreas,
                DeliveryAreas = courseEpao.DeliveryAreas,
                OtherCourses = courseEpao.OtherCourses
            };
        }
    }
}
