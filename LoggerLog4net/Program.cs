using System;

namespace LoggerLog4net
{
    public class Program
    {
        private static readonly ILogger log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static void Main(string[] args)
        {
            log.Info("This is Info");
            log.Debug("This is Debug");
            log.Error("This is Error");
            log.Warn("This is Warn");
            Console.WriteLine("Hit enter");
            Console.ReadLine();
        }
    }
}
