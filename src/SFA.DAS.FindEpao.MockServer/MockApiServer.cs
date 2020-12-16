using System.Text.RegularExpressions;
using WireMock.Logging;
using WireMock.Net.StandAlone;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using WireMock.Settings;

namespace SFA.DAS.FindEpao.MockServer
{
    public class MockApiServer
    {
        public static IWireMockServer Start()
        {
            var settings = new WireMockServerSettings
            {
                Port = 5007,
                Logger = new WireMockConsoleLogger()
            };
            
            var server = StandAloneApp.Start(settings);
            
            server.Given(Request.Create().WithPath(s => Regex.IsMatch(s,"/courses$"))
                .UsingGet()
            ).RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile("course-list.json"));

            server.Given(Request.Create().WithPath(s => Regex.IsMatch(s,"/courses/2/epaos$"))
                .UsingGet()
            ).RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile("course-no-epaos.json"));
            
            server.Given(Request.Create().WithPath(s => Regex.IsMatch(s,"/courses/(?!(?:2))\\d/epaos$"))
                .UsingGet()
            ).RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile("course-epaos.json"));

            server.Given(Request.Create().WithPath(s => Regex.IsMatch(s,"/epaos/delivery-areas$"))
                .UsingGet()
            ).RespondWith(
                Response.Create()
                    .WithStatusCode(200)
                    .WithHeader("Content-Type", "application/json")
                    .WithBodyFromFile("delivery-areas.json"));

            return server;
        }
    }
}
