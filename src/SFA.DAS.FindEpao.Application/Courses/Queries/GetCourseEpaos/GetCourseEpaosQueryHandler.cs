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
        private readonly IEpaoService _epaoService;

        public GetCourseEpaosQueryHandler(
            IValidator<GetCourseEpaosQuery> validator,
            ICourseService courseService,
            IEpaoService epaoService)
        {
            _validator = validator;
            _courseService = courseService;
            _epaoService = epaoService;
        }

        public async Task<GetCourseEpaosResult> Handle(GetCourseEpaosQuery query, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(query);

            if (!validationResult.IsValid())
            {
                throw new ValidationException(validationResult.DataAnnotationResult,null, null);
            }

            var courseEpaosTask = _courseService.GetCourseEpaos(query.CourseId);
            var deliveryAreasTask = _epaoService.GetDeliveryAreas();

            await Task.WhenAll(courseEpaosTask, deliveryAreasTask);

            return new GetCourseEpaosResult
            {
                Course = courseEpaosTask.Result.Course,
                Epaos = courseEpaosTask.Result.Epaos,
                DeliveryAreas = deliveryAreasTask.Result.DeliveryAreas
            };
        }
    }
}
