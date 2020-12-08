using System.Collections.Generic;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpao
{
    public class GetCourseEpaoResult
    {
        public CourseListItem Course { get; set; }
        public EpaoListItem Epao { get; set; }
        public IReadOnlyList<DeliveryArea> DeliveryAreas { get; set; }
    }
}
