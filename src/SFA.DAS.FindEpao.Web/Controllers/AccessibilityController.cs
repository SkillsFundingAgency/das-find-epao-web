using Microsoft.AspNetCore.Mvc;
using SFA.DAS.FindEpao.Web.Infrastructure;

namespace SFA.DAS.FindEpao.Web.Controllers
{
    [Route("[controller]")]
    public class AccessibilityController : Controller
    {
        [Route("", Name = RouteNames.AccessibilityStatement)]
        public IActionResult AccessibilityStatement()
        {
            return View();
        }
    }
}
