using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public static class GenericLogger 
    {
        private static NLog.Logger log = LogManager.GetLogger("GenericLogger");

        public static void Error(string infoToLog)
        {
            log.Fatal($"Error occured : {infoToLog} ");
        }

        public static void Error(string infoToLog, Exception ex)
        {
            log.Fatal($"Error occured {ex.Message} \n {infoToLog} \n {ex.StackTrace}");
        }

        public static void Fatal(string infoToLog)
        {
            log.Fatal($"FatalError occured : {infoToLog} ");

        }

        public static void Fatal(string infoToLog, Exception ex)
        {
            log.Fatal($"FatalError occured {ex.Message} \n {infoToLog} \n {ex.StackTrace}");
        }

        public static void Info(string infoToLog)
        {
            log.Info(infoToLog);
        }
    }
}
