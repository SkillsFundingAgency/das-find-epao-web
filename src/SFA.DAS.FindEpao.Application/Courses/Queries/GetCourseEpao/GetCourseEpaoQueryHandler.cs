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
        private readonly IEpaoService _epaoService;

        public GetCourseEpaoQueryHandler(
            IValidator<GetCourseEpaoQuery> validator,
            ICourseService courseService,
            IEpaoService epaoService)
        {
            _validator = validator;
            _courseService = courseService;
            _epaoService = epaoService;
        }

        public async Task<GetCourseEpaoResult> Handle(GetCourseEpaoQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid())
            {
                throw new ValidationException(validationResult.DataAnnotationResult,null, null);
            }

            var courseEpaoTask = _courseService.GetCourseEpao(query.CourseId, query.EpaoId);
            var deliveryAreasTask = _epaoService.GetDeliveryAreas();

            await Task.WhenAll(courseEpaoTask, deliveryAreasTask);

            return new GetCourseEpaoResult
            {
                Course = courseEpaoTask.Result.Course,
                Epao = courseEpaoTask.Result.Epao,
                DeliveryAreas = deliveryAreasTask.Result.DeliveryAreas
            };
        }
    }
}
