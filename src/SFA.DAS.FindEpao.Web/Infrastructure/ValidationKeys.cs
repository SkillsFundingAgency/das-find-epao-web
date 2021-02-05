using System.Collections.Generic;

namespace SFA.DAS.FindEpao.Web.Infrastructure
{
    public static class ValidationKeys
    {
        public static string SelectedCourseId = nameof(SelectedCourseId);
        public static string SelectedCourseIdNoJs = nameof(SelectedCourseIdNoJs);

        public static Dictionary<string, string> Messages = new Dictionary<string, string>
        {
            { SelectedCourseId, "Enter an apprenticeship training course" },
            { SelectedCourseIdNoJs, "Select an apprenticeship training course" }
        };
    }
}
