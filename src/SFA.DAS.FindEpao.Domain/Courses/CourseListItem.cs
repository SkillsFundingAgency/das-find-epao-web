using System;
using Newtonsoft.Json;

namespace SFA.DAS.FindEpao.Domain.Courses
{
    public class CourseListItem
    {
        public CourseListItem(string id, string title, int level, bool integratedApprenticeship, string standardVersions)
        {
            Title = SetDefaultTitleIfEmpty(title);
            Id = id;
            Level = level;
            IntegratedApprenticeship = integratedApprenticeship;
            StandardVersions = standardVersions;
        }

        [JsonProperty("CourseId")]
        public string Id { get; }
  
        public string Title { get; }

        public int Level { get; }
        public bool IntegratedApprenticeship { get;}

        public string StandardVersions { get; }

        public string Description => Title.Equals("UNKNOWN",StringComparison.CurrentCultureIgnoreCase) ? Title : $"{Title} (level {Level})";

        private static string SetDefaultTitleIfEmpty(string title)
        {
            return string.IsNullOrEmpty(title) ? "Unknown" : title;
        }
    }
}
