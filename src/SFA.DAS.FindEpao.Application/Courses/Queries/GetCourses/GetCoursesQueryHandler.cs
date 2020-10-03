using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourses
{
    public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, GetCoursesResult>
    {
        private readonly ICourseService _courseService;

        public GetCoursesQueryHandler(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<GetCoursesResult> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
        {
            var courseList = await _courseService.GetCourses();

            return new GetCoursesResult
            {
                Courses = courseList.Courses
            };
        }
    }
}
