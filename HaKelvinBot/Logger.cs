using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using HaKelvinBot.Structures;

namespace HaKelvinBot
{
    public enum Verbosity : ushort
    {
        None = 0,
        Regular = 1,
        High = 2
    }

    public class Logger
    {
        public static Verbosity LoggerVerbosity { get; set; }

        private static string LogFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "botlog.log");
        
        public static void Error(string msg)
        {
            string toPrint = string.Format("{0} ERROR: {1}", GetCurrentTime(), msg);
            WriteToLog(toPrint);

            if (LoggerVerbosity != Verbosity.None)
                Console.WriteLine(toPrint);
        }

        public static void Error(Exception ex)
        {
            string toPrint = string.Format("{0} ERROR: {1}", GetCurrentTime(), ex.Message);
            WriteToLog(toPrint);
            WriteToLog(ex.StackTrace);

            if (LoggerVerbosity != Verbosity.None)
                Console.WriteLine(toPrint);
        }

        public static void Warn(string msg)
        {
            string toPrint = string.Format("{0} WARN: {1}", GetCurrentTime(), msg);
            WriteToLog(toPrint);

            if (LoggerVerbosity != Verbosity.None)
                Console.WriteLine(toPrint);
        }

        public static void Info(string msg)
        {
            string toPrint = string.Format("{0} INFO: {1}", GetCurrentTime(), msg);
            WriteToLog(toPrint);

            if (LoggerVerbosity != Verbosity.None)
                Console.WriteLine(toPrint);
        }

        private static void WriteToLog(string msg)
        {
            using (StreamWriter sw = new StreamWriter(LogFilePath))
                sw.WriteLineAsync(msg);
        }

        public static void CreateLogFile()
        {
            if (!File.Exists(LogFilePath))
                File.Create(LogFilePath);
        }

        private static string GetCurrentTime()
        {
            DateTime time = new DateTime();
            return time.ToString("en-US");
        }
    }
}
