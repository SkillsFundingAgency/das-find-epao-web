using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaos;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaosCount;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Application.UnitTests.Courses.Queries.GetCourseEpaosCount
{
    public class WhenValidatingGetCourseEpaosCountQuery
    {
        [Test, MoqAutoData]
        public async Task And_Course_Not_Found_Then_Invalid(
            GetCourseEpaosCountQuery query,
            CourseList courseList,
            [Frozen] Mock<ICourseService> mockCourseService,
            GetCourseEpaosCountQueryValidator validator)
        {
            mockCourseService
                .Setup(service => service.GetCourses())
                .ReturnsAsync(courseList);

            var result = await validator.ValidateAsync(query);

            result.IsValid().Should().BeFalse();
            result.ValidationDictionary.Count.Should().Be(1);
            result.ValidationDictionary
                .Should().ContainKey(nameof(GetCourseEpaosCountQuery.CourseId))
                .WhichValue.Should().Be($"{nameof(GetCourseEpaosCountQuery.CourseId)} not found");
        }

        [Test, MoqAutoData]
        public async Task And_Course_Is_Found_Then_Valid(
            GetCourseEpaosCountQuery query,
            List<CourseListItem> courses,
            [Frozen] Mock<ICourseService> mockCourseService,
            GetCourseEpaosCountQueryValidator validator)
        {
            courses.Add(new CourseListItem(query.CourseId, "title", 2));
            var courseList = new CourseList
            {
                Courses = courses
            };
            mockCourseService
                .Setup(service => service.GetCourses())
                .ReturnsAsync(courseList);

            var result = await validator.ValidateAsync(query);

            result.IsValid().Should().BeTrue();
        }
    }
}
