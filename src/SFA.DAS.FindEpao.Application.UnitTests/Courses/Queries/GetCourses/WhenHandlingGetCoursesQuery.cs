using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourses;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Application.UnitTests.Courses.Queries.GetCourses
{
    public class WhenHandlingGetCoursesQuery
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Courses_From_Service(
            GetCoursesQuery query,
            CourseList coursesFromService,
            [Frozen] Mock<ICourseService> mockService,
            GetCoursesQueryHandler handler)
        {
            mockService
                .Setup(service => service.GetCourses())
                .ReturnsAsync(coursesFromService);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Courses.Should().BeEquivalentTo(coursesFromService.Courses);
        }
    }
}
