using Microsoft.AspNetCore.Mvc;
using SFA.DAS.FindEpao.Web.Infrastructure;

namespace SFA.DAS.FindEpao.Web.Controllers
{
    public class CookiesController : Controller
    {
        [Route("cookies", Name = RouteNames.Cookies)]
        public IActionResult Cookies()
        {
            return View();
        }

        [Route("cookies-details", Name = RouteNames.CookiesDetails)]
        public IActionResult CookiesDetails()
        {
            return View();
        }
    }
}
