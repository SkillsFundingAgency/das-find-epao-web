using System.Collections.Generic;
using SFA.DAS.FindEpao.Domain.Courses;
using SFA.DAS.FindEpao.Domain.Epaos;

namespace SFA.DAS.FindEpao.Application.Courses.Queries.GetCourseEpaos
{
    public class GetCourseEpaosResult
    {
        public CourseListItem Course { get; set; }
        public IReadOnlyList<EpaoListItem> Epaos { get; set; }
        public IReadOnlyList<DeliveryArea> DeliveryAreas { get; set; }
    }
}
