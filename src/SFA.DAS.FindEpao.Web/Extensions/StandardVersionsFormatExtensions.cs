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
            return standardVersions == null ? string.Empty : string.Join(", ", standardVersions);
        }
    }

}
