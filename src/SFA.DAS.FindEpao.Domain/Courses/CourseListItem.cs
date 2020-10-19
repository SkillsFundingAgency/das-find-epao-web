using System;
using Newtonsoft.Json;

namespace SFA.DAS.FindEpao.Domain.Courses
{
    public class CourseListItem
    {
        public CourseListItem(string id, string title, int level)
        {
            Title = SetDefaultTitleIfEmpty(title);
            Id = id;
            Level = level;
        }

        [JsonProperty("CourseId")]
        public string Id { get; }
  
        public string Title { get; }

        public int Level { get; }

        public string Description => Title.Equals("UNKNOWN",StringComparison.CurrentCultureIgnoreCase) ? Title : $"{Title} - Level {Level}";

        private static string SetDefaultTitleIfEmpty(string title)
        {
            return string.IsNullOrEmpty(title) ? "Unknown" : title;
        }
    }
}
