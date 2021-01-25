using Microsoft.AspNetCore.Mvc;
using SFA.DAS.FindEpao.Web.Infrastructure;

namespace SFA.DAS.FindEpao.Web.Controllers
{
    [Route("[controller]")]
    public class PrivacyController : Controller
    {
        [Route("", Name = RouteNames.Privacy)]
        public IActionResult Privacy()
        {
            return View();
        }
    }
}
