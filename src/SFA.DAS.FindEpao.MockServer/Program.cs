﻿using System;

namespace SFA.DAS.FindEpao.MockServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Mock Server starting on http://localhost:5007");

            MockApiServer.Start();

            Console.WriteLine(("Press any key to stop the server"));
            Console.ReadKey();
        }
    }
}
