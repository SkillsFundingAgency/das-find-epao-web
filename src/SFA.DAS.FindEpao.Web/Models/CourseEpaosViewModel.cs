using System.Collections.Generic;
using System.Linq;

namespace SFA.DAS.FindEpao.Web.Models
{
    public class CourseEpaosViewModel
    {
        public int Total => Epaos?.Count() ?? 0;
        public CourseListItemViewModel Course { get; set; }
        public IEnumerable<EpaoListItemViewModel> Epaos { get; set; }
    }
}
