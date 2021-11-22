using SFA.DAS.FindEpao.Domain.Courses;

namespace SFA.DAS.FindEpao.Web.Models
{
    public class CourseListItemViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public bool IntegratedApprenticeship { get ; set ; }

        public string Versions { get; set; }

        public static implicit operator CourseListItemViewModel(CourseListItem course)
        {
            return new CourseListItemViewModel
            {
                Id = course.Id,
                Title = course.Title,
                Level = course.Level,
                Description = course.Description,
                IntegratedApprenticeship = course.IntegratedApprenticeship,
                Versions = course.Versions
            };
        }
    }
}
