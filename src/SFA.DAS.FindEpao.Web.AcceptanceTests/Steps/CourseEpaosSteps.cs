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
    public class CourseEpaosSteps
    {
        private readonly ScenarioContext _context;

        public CourseEpaosSteps(ScenarioContext context)
        {
            _context = context;
        }

        [Then("there is a row for each epao")]
        public async Task ThenThereIsARowForEachEpao()
        {
            var response = _context.Get<HttpResponseMessage>(ContextKeys.HttpResponse);

            var actualContent = await response.Content.ReadAsStringAsync();

            var json = DataFileManager.GetFile("course-epaos.json");
            var expectedApiResponse = JsonConvert.DeserializeObject<CourseEpaos>(json);

            foreach (var epao in expectedApiResponse.Epaos)
            {
                actualContent.Should().Contain(HttpUtility.HtmlEncode(epao.Name));
            }
        }
    }
}
