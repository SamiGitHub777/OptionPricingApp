using System;

namespace LoggerLog4net
{
    public interface ILogger
    {
        void Debug(object message);
        bool IsDebugEnabled { get; }
        void Info(object message);
        void Error(object message);
        void Warn(object message);
    }
    public class Log4NetWrapper : ILogger
    {
        private readonly log4net.ILog logger;

        public Log4NetWrapper(Type type)
        {
            this.logger = log4net.LogManager.GetLogger(type);
        }

        public void Debug(object message)
        {
            logger.Debug(message);
        }

        public void Info(object message)
        {
            logger.Info(message);
        }

        public void Error(object message)
        {
            logger.Error(message);
        }

        public void Warn(object message)
        {
            logger.Warn(message);
        }

        public bool IsDebugEnabled
        {
            get { return logger.IsDebugEnabled; }
        }
    }
}
