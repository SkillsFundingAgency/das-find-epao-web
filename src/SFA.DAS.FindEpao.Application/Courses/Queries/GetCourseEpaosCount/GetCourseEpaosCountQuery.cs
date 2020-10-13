using MediatR;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaosCount
{
    public class GetCourseEpaosCountQuery : IRequest<GetCourseEpaosCountResult>
    {
        public string CourseId { get; set; }
    }
}
