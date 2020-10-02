using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Web.Infrastructure;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.Controllers
{
    [Route("[controller]")]
    public class CoursesController : Controller
    {
        [HttpGet]
        [Route("", Name = RouteNames.ChooseCourse)]
        public async Task<IActionResult> ChooseCourse()
        {
            await Task.CompletedTask;

            var viewModel = new ChooseCourseViewModel
            {
                Courses = new List<CourseListItemViewModel>
                {
                    new CourseListItemViewModel(new CourseListItem("1","title", 5))
                }
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("", Name = RouteNames.ChooseCourse)]
        public IActionResult PostChooseCourse()
        {
            return RedirectToRoute(RouteNames.ServiceStartDefault);
        }
    }
}
