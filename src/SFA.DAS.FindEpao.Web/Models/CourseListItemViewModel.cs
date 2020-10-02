using System;
using SFA.DAS.FindEpao.Domain.Courses;

namespace SFA.DAS.FindEpao.Web.Models
{
    public class CourseListItemViewModel
    {
        public string Id { get; }
        public string Title { get; }
        public int Level { get; }
        public string Selected { get; }
        public string Description { get; }

        public CourseListItemViewModel(CourseListItem course, string selectedCourseId = null)
        {
            if (course == null)
            {
                course = new CourseListItem(null,null,0);
                Description = course.CourseDescription;
                return;
            }

            Id = course.Id;
            Title = course.Title;
            Level = course.Level;
            Description = course.CourseDescription;
            Selected = Id!=null && Id.Equals(selectedCourseId, StringComparison.InvariantCulture)
                ? "selected"
                : null;
        }
    }
}
