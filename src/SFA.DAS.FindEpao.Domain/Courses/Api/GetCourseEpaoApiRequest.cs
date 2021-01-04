using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Domain.Courses.Api
{
    public class GetCourseEpaoApiRequest : IGetApiRequest
    {
        public GetCourseEpaoApiRequest(string courseId, string epaoId)
        {
            CourseId = courseId;
            EpaoId = epaoId;
        }

        public string GetUrl => $"courses/{CourseId}/epaos/{EpaoId}";
        public string CourseId { get; }
        public string EpaoId { get; }
    }
}
