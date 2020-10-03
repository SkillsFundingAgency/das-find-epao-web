using SFA.DAS.FindEpao.Domain.Interfaces;

namespace SFA.DAS.FindEpao.Domain.Courses.Api
{
    public class GetCoursesApiRequest : IGetApiRequest
    {
        public string BaseUrl { get; }
        public string GetUrl => $"{BaseUrl}courses";

        public GetCoursesApiRequest(string baseUrl)
        {
            BaseUrl = baseUrl;
        }
    }
}
