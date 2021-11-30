using System;
using System.Threading;
using LoggerLog4net;
using OptionPricingInfrastructure;

namespace OptionPricingClientConsole
{
    public class Program
    {
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // launch several clients
        public static void Main(string[] args)
        {
            // Usually we use Thread.Join() method on all created threads to wait until all threads are completed.
            // another way to join threads if threads are on thread pool : CountdownEvent
            RequestType requestType = RequestType.PriceOption;
            int threadCount = 10;
            logger.Info("Starting Client Threads");
            using (CountdownEvent counter = new CountdownEvent(threadCount))
            {
                for (int task = 0; task < 5; task++)
                {
                    ThreadPool.QueueUserWorkItem(_ => {
                        if (requestType == RequestType.PriceOption)
                        {
                            NetMQClient.ClientTaskCallBack(task, counter);
                        } else if (requestType == RequestType.GetAllOptions)
                        {
                            NetMQClient.ClientTaskCallBackGetOptionList(task, counter);

                        }
                    });
                    Thread.Sleep(1000);
                }
                counter.Wait();
            }
            logger.Info("All client threads finished execution");
        }

    }
}
