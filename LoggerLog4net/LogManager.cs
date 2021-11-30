using System;

namespace LoggerLog4net
{
    public static class LogManager
    {
        public static ILogger GetLogger(Type type)
        {
            return new Log4NetWrapper(type);
            // TODO : if it's someone else type => return the dedicated logger ...
        }
    }
}
