using log4net;
using log4net.Config;
using System;
using System.IO;

namespace NiceShot.Core.Helper
{
    public class Logger
    {
        private static ILog logger;
        static Logger()
        {
            if (logger == null)
            {
                var rep = LogManager.CreateRepository("NETCoreRepository");
                XmlConfigurator.Configure(rep, new FileInfo("log4net.config"));
                logger = LogManager.GetLogger(rep.Name, "InfoLogger");
            }
        }

        /// <summary>
        /// 普通日志
        /// </summary>
        public static void Info(string message, Exception exception = null)
        {
            if (exception == null)
                logger.Info(message);
            else
                logger.Info(message, exception);

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

        /// <summary>
        /// 告警日志
        /// </summary>
        public static void Warn(string message, Exception exception = null)
        {
            if (exception == null)
                logger.Warn(message);
            else
                logger.Warn(message, exception);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        public static void Error(string message, Exception exception = null)
        {
            if (exception == null)
                logger.Error(message);
            else
                logger.Error(message, exception);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }
    }
}
