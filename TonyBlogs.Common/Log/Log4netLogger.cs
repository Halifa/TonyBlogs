using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TonyBlogs.Common.Log
{
    public class Log4netLogger : ILogger
    {
        private static ILog logger;

        static Log4netLogger()
        {
            var fileInfo = new FileInfo(AppDomain.CurrentDomain.SetupInformation.ApplicationBase + (@"\Config\log4net.config"));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(fileInfo);
            logger = log4net.LogManager.GetLogger("default");
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Debug(string message, Exception exception)
        {
            logger.Debug(message, exception);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }

        public void Error(string message, Exception exception)
        {
            logger.Error(message, exception);
        }

        public void Fatal(string message)
        {
            logger.Fatal(message);
        }

        public void Fatal(string message, Exception exception)
        {
            logger.Fatal(message, exception);
        }

        public void Info(string message)
        {
            logger.Info(message);
        }

        public void Info(string message, Exception exception)
        {
            logger.Info(message, exception);
        }

        public void Warn(string message)
        {
            logger.Warn(message);
        }

        public void Warn(string message, Exception exception)
        {
            logger.Warn(message, exception);
        }
    }
}
