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
