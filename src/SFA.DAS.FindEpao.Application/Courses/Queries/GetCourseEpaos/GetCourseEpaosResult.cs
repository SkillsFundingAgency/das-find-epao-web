using System.Collections.Generic;
using SFA.DAS.FindEpao.Domain.Courses;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaos
{
    public class GetCourseEpaosResult
    {
        public CourseDetails Course { get; set; }
        public IReadOnlyList<EpaoListItem> Epaos { get; set; }
        public int Total { get; set; }
    }
}
