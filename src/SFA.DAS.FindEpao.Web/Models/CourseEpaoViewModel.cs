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
            ApprenticeshipTrainingCourses = new List<ApprenticeshipTrainingCourses>();
            AllVersions = new List<ApprenticeshipTrainingCourses>(); 
        }
        public CourseListItemViewModel Course { set; get; }
        public EpaoDetailsViewModel Epao { get; set; }
        public int CourseEpaosCount { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public IReadOnlyList<CourseListItemViewModel> AllCourses { get; set; }
        public List<EpaoStandardsListItem> StandardVersions { get; set; }
        public List<ApprenticeshipTrainingCourses> ApprenticeshipTrainingCourses {get; set;}
        public List<ApprenticeshipTrainingCourses> AllVersions { get; set; }

        public void CreateListOfVersions()
        {
            var versionsSb = new System.Text.StringBuilder();

            if (StandardVersions != null)
            {
                foreach (var standard in StandardVersions)
                {
                    ApprenticeshipTrainingCourses.Add(new ApprenticeshipTrainingCourses
                    {
                        EffectiveFrom = standard.EffectiveFrom,
                        EffectiveTo = standard.EffectiveTo,
                        Version = standard.Version,
                        Level = standard.Level,
                        StandardName = standard.Title,
                        LarsCode = standard.LarsCode
                    });

                    if (AllVersions.Any(x => x.LarsCode == standard.LarsCode))
                    {
                        foreach (var vers in AllVersions)
                        {
                            if (vers.LarsCode == standard.LarsCode && standard.Status == "Live")
                            {
                                versionsSb.Append(", ");
                                versionsSb.Append(standard.Version);
                                vers.Version = versionsSb.ToString();
                            }
                        }
                    }
                    else
                    {
                        AllVersions.Add(new ApprenticeshipTrainingCourses
                        {
                            EffectiveFrom = null,
                            EffectiveTo = null,
                            Version = standard.Version,
                            Level = standard.Level,
                            StandardName = standard.Title,
                            LarsCode = standard.LarsCode
                        });
                        versionsSb.Append(standard.Version);
                    }
                }
            }
        }

    }

    public class ApprenticeshipTrainingCourses
    {
        public string StandardName { get; set; }
        public int LarsCode { get; set; }
        public int Level { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public string Version { get; set; }
    }

}
