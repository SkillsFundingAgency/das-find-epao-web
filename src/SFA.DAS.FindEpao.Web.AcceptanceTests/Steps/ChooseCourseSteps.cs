using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using FluentAssertions;
using Newtonsoft.Json;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.MockServer;
using SFA.DAS.FindEpao.Web.AcceptanceTests.Infrastructure;
using TechTalk.SpecFlow;

namespace SFA.DAS.FindEpao.Web.AcceptanceTests.Steps
{
    [Binding]
    public class ChooseCourseSteps
    {
        private readonly ScenarioContext _context;

        public ChooseCourseSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Then("there is a row for each course")]
        public async Task ThenThereIsARowForEachCourse()
        {
            var response = _context.Get<HttpResponseMessage>(ContextKeys.HttpResponse);

            var actualContent = await response.Content.ReadAsStringAsync();

            var json = DataFileManager.GetFile("course-list.json");
            var expectedApiResponse = JsonConvert.DeserializeObject<CourseList>(json);

            foreach (var course in expectedApiResponse.Courses)
            {
                actualContent.Should().Contain(HttpUtility.HtmlEncode(course.Title));
            }
        }
    }
}
