using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.FindEpao.Web.Extensions
{
    public static class StandardVersionsFormatExtensions
    {
        public static string VersionsArrayToString(string[] standardVersions)
        {
            if ((standardVersions.Length < 0) || (standardVersions == null))
            {
                return string.Empty;
            }
            else
            {
                return string.Join(", ", standardVersions);
            }
        }
    }

}
