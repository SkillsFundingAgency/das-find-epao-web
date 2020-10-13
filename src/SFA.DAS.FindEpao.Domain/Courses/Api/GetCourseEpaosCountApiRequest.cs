using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Domain.Courses.Api
{
    public class GetCourseEpaosCountApiRequest : IGetApiRequest
    {
        public string GetUrl => $"courses/{CourseId}/epaos-count";
        public string CourseId { get; set; }
    }
}
