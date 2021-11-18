using System;
using System.Collections.Generic;
using SFA.DAS.FindEpao.Domain.Courses;
using System.Linq;

namespace SFA.DAS.FindEpao.Web.Models
{
    public class CourseEpaoViewModel
    {

        public CourseEpaoViewModel()
        {
            apprenticeshipTrainingCourses = new List<ApprenticeShipTrainingCourses>();
            allVersions = new List<ApprenticeShipTrainingCourses>(); 
        }

        public CourseListItemViewModel Course { set; get; }
        public EpaoDetailsViewModel Epao { get; set; }
        public int CourseEpaosCount { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public IReadOnlyList<CourseListItemViewModel> AllCourses { get; set; }
        public List<EpaoStandardsListItem> standardVersions { get; set; }
        public List<ApprenticeShipTrainingCourses> apprenticeshipTrainingCourses {get; set;}
    
        public List<ApprenticeShipTrainingCourses> allVersions { get; set; }

        public void CreateListOfVersions()
        {
            foreach (var standard in standardVersions)
            {
                foreach (var ver in standard.standardVersions)
                {
                    apprenticeshipTrainingCourses.Add(new ApprenticeShipTrainingCourses { 
                        effectiveFrom = ver.effectiveFrom, effectiveTo = 
                        ver.effectiveTo, version = ver.version, standardName = ver.title, larsCode = ver.larsCode  });

                    if (allVersions.Any(x => x.larsCode == ver.larsCode))
                    {
                        foreach (var vers in allVersions)
                        {
                            if (vers.larsCode == ver.larsCode)
                            {
                                vers.version += ", " + ver.version;
                            }
                        }
                    }
                    else
                    {
                        allVersions.Add(new ApprenticeShipTrainingCourses
                        {
                            effectiveFrom = null,
                            effectiveTo = null,
                            version = ver.version,
                            standardName = ver.title,
                            larsCode = ver.larsCode
                        });
                    }
                }
            }
        }

    }

    public class ApprenticeShipTrainingCourses
    {
        public string standardName { get; set; }
        public int larsCode { get; set; }
        public DateTime? effectiveFrom { get; set; }
        public DateTime? effectiveTo { get; set; }
        public string version { get; set; }
    }

}
