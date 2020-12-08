using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpao
{
    public class GetCourseEpaoQueryHandler : IRequestHandler<GetCourseEpaoQuery, GetCourseEpaoResult>
    {
        private readonly ICourseService _courseService;
        private readonly IEpaoService _epaoService;

        public GetCourseEpaoQueryHandler(
            ICourseService courseService,
            IEpaoService epaoService)
        {
            _courseService = courseService;
            _epaoService = epaoService;
        }

        public async Task<GetCourseEpaoResult> Handle(GetCourseEpaoQuery query, CancellationToken cancellationToken)
        {
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
