using System.Collections.Generic;

namespace SFA.DAS.FindEpao.Domain.Courses
{
    public class CourseEpaos
    {
        public CourseListItem Course { get; set; }
        public IReadOnlyList<EpaoListItem> Epaos { get; set; }
    }
}
