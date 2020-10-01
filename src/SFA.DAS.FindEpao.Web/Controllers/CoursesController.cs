using Microsoft.AspNetCore.Mvc;
using SFA.DAS.FindEpao.Web.Infrastructure;

namespace SFA.DAS.FindEpao.Web.Controllers
{
    [Route("[controller]")]
    public class CoursesController : Controller
    {
        [Route("", Name = RouteNames.ChooseCourse, Order = 0)]
        public IActionResult ChooseCourse()
        {
            return View();
        }
    }
}
