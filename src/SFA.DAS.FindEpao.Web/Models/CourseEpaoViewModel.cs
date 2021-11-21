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
            if (standardVersions != null)
            {
                foreach (var standard in standardVersions)
                {
                    apprenticeshipTrainingCourses.Add(new ApprenticeShipTrainingCourses
                    {
                        EffectiveFrom = standard.EffectiveFrom,
                        EffectiveTo = standard.EffectiveTo,
                        Version = standard.Version,
                        Level = standard.Level,
                        StandardName = standard.Title,
                        LarsCode = standard.LarsCode
                    });

                    if (allVersions.Any(x => x.LarsCode == standard.LarsCode))
                    {
                        foreach (var vers in allVersions)
                        {
                            if (vers.LarsCode == standard.LarsCode && standard.Status == "Live")
                            {
                                vers.Version += ", " + standard.Version;
                            }
                        }
                    }
                    else
                    {
                        allVersions.Add(new ApprenticeShipTrainingCourses
                        {
                            EffectiveFrom = null,
                            EffectiveTo = null,
                            Version = standard.Version,
                            Level = standard.Level,
                            StandardName = standard.Title,
                            LarsCode = standard.LarsCode
                        });
                    }
                }
            }
        }

    }

    public class ApprenticeShipTrainingCourses
    {
        public string StandardName { get; set; }
        public int LarsCode { get; set; }
        public int Level { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public string Version { get; set; }
    }

}
