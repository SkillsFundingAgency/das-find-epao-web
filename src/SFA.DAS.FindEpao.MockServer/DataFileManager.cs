﻿using System.IO;

namespace SFA.DAS.FindEpao.MockServer
{
    public class DataFileManager
    {
        public static string GetFile(string filename)
        {
            return File.ReadAllText(filename);
        }
    }
}
