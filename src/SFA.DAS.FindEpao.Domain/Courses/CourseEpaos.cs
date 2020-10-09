using System.Collections.Generic;

namespace SFA.DAS.FindEpao.Domain.Courses
{
    public class CourseEpaos
    {
        public CourseDetails Course { get; set; }
        public IReadOnlyList<EpaoListItem> Epaos { get; set; }
        public int Total { get; set; }
    }
}
