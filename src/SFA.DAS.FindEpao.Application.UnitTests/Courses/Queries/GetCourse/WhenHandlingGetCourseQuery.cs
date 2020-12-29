using System.Threading;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourse;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Application.UnitTests.Courses.Queries.GetCourse
{
    public class WhenHandlingGetCourseQuery
    {
        [Test, MoqAutoData]
        public async Task Then_Gets_Courses_From_Service(
            GetCourseQuery query,
            CourseListItem courseFromService,
            [Frozen] Mock<ICourseService> mockService,
            GetCourseQueryHandler handler)
        {
            mockService
                .Setup(service => service.GetCourse(query.CourseId))
                .ReturnsAsync(courseFromService);

            var result = await handler.Handle(query, CancellationToken.None);

            result.Course.Should().BeEquivalentTo(courseFromService);
        }
    }
}