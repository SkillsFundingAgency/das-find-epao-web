using System.Collections.Generic;

namespace SFA.DAS.FindEpao.Web.Models
{
    public class CourseEpaosViewModel
    {
        public int Total { get; set; }
        public CourseListItemViewModel Course { get; set; }
        public IEnumerable<EpaoListItemViewModel> Epaos { get; set; }
    }
}