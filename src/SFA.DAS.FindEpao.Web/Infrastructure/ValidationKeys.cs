using System.Collections.Generic;

namespace SFA.DAS.FindEpao.Web.Infrastructure
{
    public static class ValidationKeys
    {
        public static string SelectedCourseId = nameof(SelectedCourseId);

        public static Dictionary<string, string> Messages = new Dictionary<string, string>
        {
            { SelectedCourseId, "Select an apprenticeship training course" }
        };
    }
}
