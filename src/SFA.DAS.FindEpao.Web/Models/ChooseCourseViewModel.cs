using System.Collections.Generic;

namespace SFA.DAS.FindEpao.Web.Models
{
    public class ChooseCourseViewModel
    {
        public IEnumerable<CourseListItemViewModel> Courses { get; set; }
        public string CourseId { get; set; }
    }
}
