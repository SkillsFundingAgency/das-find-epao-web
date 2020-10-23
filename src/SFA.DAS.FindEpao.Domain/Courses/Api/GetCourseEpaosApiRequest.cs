using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Domain.Courses.Api
{
    public class GetCourseEpaosApiRequest : IGetApiRequest
    {
        public string GetUrl => $"courses/{CourseId}/epaos";
        public string CourseId { get; set; }
    }
}
