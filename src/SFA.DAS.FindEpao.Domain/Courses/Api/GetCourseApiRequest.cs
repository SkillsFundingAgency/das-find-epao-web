using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Domain.Courses.Api
{
    public class GetCourseApiRequest : IGetApiRequest
    {
        private readonly string _courseId;

        public GetCourseApiRequest (string courseId)
        {
            _courseId = courseId;
        }

        public string GetUrl => $"courses/{_courseId}";
    }
}