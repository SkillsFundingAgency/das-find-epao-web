using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaos;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourses;
using SFA.DAS.FindEpao.Web.Infrastructure;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.Controllers
{
    [Route("[controller]")]
    public class CoursesController : Controller
    {
        private readonly IMediator _mediator;

        public CoursesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("", Name = RouteNames.ChooseCourse)]
        public async Task<IActionResult> ChooseCourse(GetChooseCourseRequest request = null)
        {
            if (!string.IsNullOrEmpty(request?.SelectedCourseId))
            {
                ModelState.AddModelError(
                    ValidationKeys.SelectedCourseId, 
                    ValidationKeys.Messages[ValidationKeys.SelectedCourseId]);
            }

            var result = await _mediator.Send(new GetCoursesQuery());

            var viewModel = new ChooseCourseViewModel
            {
                Courses = result.Courses.Select(item => (CourseListItemViewModel)item)
            };

            return View(viewModel);
        }

        [HttpPost]
        [Route("", Name = RouteNames.ChooseCourse)]
        public async Task<IActionResult> PostChooseCourse(PostChooseCourseRequest request)
        {
            var query = new GetCourseEpaosQuery {CourseId = request.SelectedCourseId};

            try
            {
                var result = await _mediator.Send(query);
            }
            catch (ValidationException)
            {
                return RedirectToRoute(
                    RouteNames.ChooseCourse, 
                    new GetChooseCourseRequest{SelectedCourseId = "-1"});
            }

            return RedirectToRoute(RouteNames.ServiceStartDefault);
        }
    }
}
