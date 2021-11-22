using System;
using System.Collections.Generic;
using SFA.DAS.FindEpao.Domain.Courses;
using System.Linq;

namespace SFA.DAS.FindEpao.Web.Models
{
    public class CourseEpaoViewModel
    {
        public CourseListItemViewModel Course { set; get; }
        public EpaoDetailsViewModel Epao { get; set; }
        public int CourseEpaosCount { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public IReadOnlyList<CourseListItemViewModel> AllCourses { get; set; }
        public List<EpaoStandardsListItem> StandardVersions { get; set; }
    }
}
