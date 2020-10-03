using System.Collections.Generic;

namespace SFA.DAS.FindEpao.Domain.Courses
{
    public class CourseList
    {
        public IReadOnlyList<CourseListItem> Courses { get; set; }
    }
}
