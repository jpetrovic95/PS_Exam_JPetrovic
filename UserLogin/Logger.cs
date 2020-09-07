using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using UserLogin.Controller;

namespace UserLogin
{
    static public class Logger
    {
        private static readonly List<string> CurrentSessionActivities = new List<string>();
        private const string LogFile = @"log_file.txt";

        public static void LogActivity(string activity, bool logToFile)
        {
            string activityLine = DateTime.Now + ";"
                + LoginValidation.currentUserUsername + ";"
                + activity;

            CurrentSessionActivities.Add(activityLine);

            if (logToFile)
            {
                using (StreamWriter outputFile = new StreamWriter(LogFile, true))
                {
                    outputFile.WriteLine(activityLine);
                }
            }
        }

        public static IReadOnlyCollection<string> GetCurrentSessionActivities(string filter)
        {
            return CurrentSessionActivities.Where(activity => activity.Contains(filter)).ToList();
        }

        public static string GetAllUserActivities()
        {
            StringBuilder userActivities = new StringBuilder();
            StreamReader sr = new StreamReader(LogFile);

            while (sr.Peek() >= 0)
            {
                userActivities.AppendLine(sr.ReadLine());
            }

            return userActivities.ToString();
        }
    }
}
