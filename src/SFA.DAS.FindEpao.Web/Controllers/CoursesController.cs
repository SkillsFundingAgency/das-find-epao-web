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
            try
            {
                var query = new GetCourseEpaosQuery {CourseId = request.SelectedCourseId};
                var result = await _mediator.Send(query);

                if (result?.Epaos?.Count == 1)
                {
                    return RedirectToRoute(RouteNames.CourseEpaoDetails, new GetCourseEpaoDetailsRequest
                    {
                        Id = request.SelectedCourseId,
                        EpaoId = result.Epaos.First().EpaoId
                    });
                }
                else
                {
                    return RedirectToRoute(RouteNames.CourseEpaos, new GetCourseEpaosRequest
                    {
                        Id = request.SelectedCourseId
                    });
                }
            }
            catch (ValidationException)
            {
                return RedirectToRoute(
                    RouteNames.ChooseCourse, 
                    new GetChooseCourseRequest{SelectedCourseId = "-1"});
            }
        }

        [HttpGet]
        [Route("{id}/assessment-organisations", Name = RouteNames.CourseEpaos)]
        public async Task<IActionResult> CourseEpaos(GetCourseEpaosRequest request)
        {
            try
            {
                var query = new GetCourseEpaosQuery {CourseId = request.Id};
                var result = await _mediator.Send(query);
                var model = new CourseEpaosViewModel
                {
                    Course = result.Course,
                    Epaos = result.Epaos.Select(item => new EpaoListItemViewModel(item, result.DeliveryAreas))
                };
                return View(model);
            }
            catch (ValidationException)
            {
                return RedirectToRoute(RouteNames.Error404);
            }
        }
    }
}
