using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaos;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.FindEpao.Application.UnitTests.Courses.Queries.GetCourseEpaos
{
    public class WhenValidatingGetCourseEpaosQuery
    {
        [Test, MoqAutoData]
        public async Task And_Course_Not_Found_Then_Invalid(
            GetCourseEpaosQuery query,
            CourseList courseList,
            [Frozen] Mock<ICourseService> mockCourseService,
            GetCourseEpaosQueryValidator validator)
        {
            mockCourseService
                .Setup(service => service.GetCourses())
                .ReturnsAsync(courseList);

            var result = await validator.ValidateAsync(query);

            result.IsValid().Should().BeFalse();
            result.ValidationDictionary.Count.Should().Be(1);
            result.ValidationDictionary
                .Should().ContainKey(nameof(GetCourseEpaosQuery.CourseId))
                .WhichValue.Should().Be($"{nameof(GetCourseEpaosQuery.CourseId)} not found");
        }

        [Test, MoqAutoData]
        public async Task And_Course_Is_Found_Then_Valid(
            GetCourseEpaosQuery query,
            List<CourseListItem> courses,
            [Frozen] Mock<ICourseService> mockCourseService,
            GetCourseEpaosQueryValidator validator)
        {
            courses.Add(new CourseListItem(query.CourseId, "title", 2, false, new string[]{"1.0"}));
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
