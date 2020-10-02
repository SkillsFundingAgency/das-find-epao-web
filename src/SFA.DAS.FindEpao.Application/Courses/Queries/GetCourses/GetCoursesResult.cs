using System.Collections.Generic;
using SFA.DAS.FindEpao.Domain.Courses;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourses
{
    public class GetCoursesResult
    {
        public IReadOnlyList<CourseListItem> Courses { get; set; }
    }
}
