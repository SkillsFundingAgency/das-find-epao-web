using MediatR;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpao
{
    public class GetCourseEpaoQuery : IRequest<GetCourseEpaoResult>
    {
        public string CourseId { get; set; }
        public string EpaoId { get; set; }
    }
}
