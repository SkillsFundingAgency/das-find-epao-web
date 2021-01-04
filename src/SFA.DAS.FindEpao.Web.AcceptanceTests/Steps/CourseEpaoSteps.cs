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
    public class CourseEpaoSteps
    {
        private readonly ScenarioContext _context;

        public CourseEpaoSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Then("the page content includes the epao name")]
        public async Task ThenThePageContentIncludesTheEpaoName()
        {
            var response = _context.Get<HttpResponseMessage>(ContextKeys.HttpResponse);

            var actualContent = await response.Content.ReadAsStringAsync();

            var json = DataFileManager.GetFile("course-epao.json");
            var expectedApiResponse = JsonConvert.DeserializeObject<CourseEpao>(json);

            actualContent.Should().Contain(HttpUtility.HtmlEncode(expectedApiResponse.Epao.Name));
        }
    }
}
