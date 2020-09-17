using Microsoft.AspNetCore.Mvc;
using SFA.DAS.FindEpao.Web.Infrastructure;

namespace SFA.DAS.FindEpao.Web.Controllers
{
    [Route("[controller]")]
    public class ErrorController : Controller
    {

        [Route("404", Name = RouteNames.Error404)]
        public IActionResult PageNotFound()
        {
            return View();
        }

        [Route("500", Name = RouteNames.Error500)]
        public IActionResult ApplicationError()
        {
            return View();
        }
    }
}
