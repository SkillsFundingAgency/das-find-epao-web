using System;
using System.Linq;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SFA.DAS.FindEpao.Web.Extensions
{
    public static class DateFormatExtensions
    {
        public static string LongDateFormat(DateTime? sourceDateTime)
        {
            if (sourceDateTime.HasValue)
            {
                return sourceDateTime.Value.ToString("d MMMM yyyy");
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
