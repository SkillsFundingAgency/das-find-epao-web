using System.Threading.Tasks;
using SFA.DAS.FindEpao.Domain.Courses;

namespace SFA.DAS.FindEpao.Domain.Interfaces
{
    public interface ICourseService
    {
        Task<CourseList> GetCourses();
        Task<CourseEpaos> GetCourseEpaos(string courseId);
        Task<CourseEpao> GetCourseEpao(string courseId, string epaoId);
    }
}
