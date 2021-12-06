using System;

namespace SFA.DAS.FindEpao.Web.Extensions
{
    public static class DateFormatExtensions
    {
        public static string LongDateFormat(this DateTime? sourceDateTime)
        {
            return sourceDateTime.HasValue ? sourceDateTime.Value.LongDateFormat() : string.Empty;
        }

        public static string LongDateFormat(this DateTime sourceDateTime)
        {
            return sourceDateTime.ToString("dd MMMM yyyy");
        }
    }
}
