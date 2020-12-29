using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourse
{
    public class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, GetCourseResult>
    {
        private readonly ICourseService _courseService;

        public GetCourseQueryHandler (ICourseService courseService)
        {
            _courseService = courseService;
        }
        public async Task<GetCourseResult> Handle(GetCourseQuery request, CancellationToken cancellationToken)
        {
            var result = await _courseService.GetCourse(request.CourseId);
            
            return new GetCourseResult
            {
                Course = result
            };
        }
    }
}