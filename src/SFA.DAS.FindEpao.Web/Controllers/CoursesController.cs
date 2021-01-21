using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourse;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpao;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaos;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourses;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Exceptions;
using SFA.DAS.FindEpao.Web.Infrastructure;
using SFA.DAS.FindEpao.Web.Infrastructure.Interfaces;
using SFA.DAS.FindEpao.Web.Models;

namespace SFA.DAS.FindEpao.Web.Controllers
{
    [Route("[controller]")]
    public class CoursesController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILocationStringBuilder _locationStringBuilder;

        public CoursesController(
            IMediator mediator,
            ILocationStringBuilder locationStringBuilder)
        {
            _mediator = mediator;
            _locationStringBuilder = locationStringBuilder;
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


                if (result.Course.IntegratedApprenticeship)
                {
                    return RedirectToRoute(RouteNames.IntegratedApprenticeship,
                        new GetIntegratedApprenticeshipCourseRequest
                        {
                            Id = request.SelectedCourseId
                        });
                }
                
                if (result?.Epaos?.Count == 1)
                {
                    return RedirectToRoute(RouteNames.CourseEpao, new GetCourseEpaoDetailsRequest
                    {
                        Id = request.SelectedCourseId,
                        EpaoId = result.Epaos.First().EpaoId,
                        Single = true
                    });
                }

                return RedirectToRoute(RouteNames.CourseEpaos, new GetCourseEpaosRequest
                {
                    Id = request.SelectedCourseId
                });
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
                    Epaos = result.Epaos.Select(item => new EpaoListItemViewModel(
                        item, 
                        result.DeliveryAreas, 
                        _locationStringBuilder.BuildLocationString))
                };
                return View(model);
            }
            catch (ValidationException)
            {
                return RedirectToRoute(RouteNames.Error404);
            }
        }

        [HttpGet]
        [Route("{id}/course-integrated-apprenticeship", Name = RouteNames.IntegratedApprenticeship)]
        public async Task<IActionResult> CourseIntegrated(GetIntegratedApprenticeshipCourseRequest request)
        {
            try
            {
                var result = await _mediator.Send(new GetCourseQuery {CourseId = request.Id});

                if (result.Course == null || !result.Course.IntegratedApprenticeship)
                {
                    return RedirectToRoute(RouteNames.Error404);
                }
                
                var model = new IntegratedApprenticeshipCourseViewModel
                {
                    Course = result.Course
                };
                return View(model);
            }
            catch (Exception)
            {
                return RedirectToRoute(RouteNames.Error500);
            }
            
        }

        [HttpGet]
        [Route("{id}/assessment-organisations/{epaoId}", Name = RouteNames.CourseEpao)]
        public async Task<IActionResult> CourseEpao(GetCourseEpaoDetailsRequest request)
        {
            try
            {
                var query = new GetCourseEpaoQuery
                {
                    CourseId = request.Id,
                    EpaoId = request.EpaoId
                };
                var result = await _mediator.Send(query);
                var model = new CourseEpaoViewModel
                {
                    Course = result.Course,
                    Epao = new EpaoDetailsViewModel(
                        result.Epao, 
                        result.EpaoDeliveryAreas,
                        result.DeliveryAreas,
                        _locationStringBuilder.BuildLocationString,
                        request.Single),
                    CourseEpaosCount = result.CourseEpaosCount,
                    EffectiveFrom = result.EffectiveFrom,
                    OtherCourses = result.OtherCourses.Select(item => (CourseListItemViewModel)item)
                };
                return View(model);
            }
            catch(Exception ex) when (
                ex is ValidationException 
                || ex is NotFoundException<CourseEpao>)
            {
                
                return RedirectToRoute(RouteNames.ErrorEpaoUnavailable);
            }
        }
    }
}
