using NetMQ;
using NetMQ.Sockets;
using OptionPricingDomain;
using OptionPricingDomainService;
using OptionPricingInfrastructure;
using System;
using System.Threading;
using LoggerLog4net;
using System.Collections.Generic;

namespace OptionPricingInterfaceService
{
    /*
     * The server would spin up a worker for each client request
     *  Topology:
     * Client: RequestSocket
     * Server: RouterSocket (TCP 5555) – DealerSocket (TCP 5556) & Poller
     * Worker: DealerSocket
     * The server has a frontend for the requests coming from the client. 
     * It has the backend for communicating with the workers. 
     * Poller is used to handle the messages
     */
    public class NetMQServer
    {
        private readonly string frontEndPoint;
        private readonly int frontPort;
        private readonly string backEndPoint = "localhost";
        private readonly int backPort = 5556;
        private static readonly ILogger logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public NetMQServer(string frontEndPoint, int frontPort)
        {
            this.frontEndPoint = frontEndPoint;
            this.frontPort = frontPort;
        }
        public void StartListening()
        {
            logger.Info("Starting server");
            using (var front = new RouterSocket())
            {
                using (var back = new DealerSocket())
                {
                    using (var poller = new NetMQPoller { front, back })
                    {
                        front.Bind($"tcp://{frontEndPoint}:{frontPort}");
                        back.Bind($"tcp://{backEndPoint}:{backPort}");

                        poller.Add(front);
                        poller.Add(back);

                        // creating the workers
                        // When the server’s frontend receives a message, we want to spin up a new worker
                        front.ReceiveReady += (sender, eventArgs) =>
                        {
                            var optionPricingInterfaceServiceRegistration = new OptionPricingInterfaceServiceRegistration();
                            optionPricingInterfaceServiceRegistration.Register();
                            IOptionPricingJsonSerializer<Price> serializerPrice = optionPricingInterfaceServiceRegistration.DependencyInjectionManager.Resolve<IOptionPricingJsonSerializer<Price>>();
                            IOptionPricingJsonSerializer<List<Option>> serializerOptionList = optionPricingInterfaceServiceRegistration.DependencyInjectionManager.Resolve<IOptionPricingJsonSerializer<List<Option>>>();
                            IOptionPricingPersistenceService optionPricingPersistenceService = optionPricingInterfaceServiceRegistration.DependencyInjectionManager.Resolve<IOptionPricingPersistenceService>();

                            var mqMessage = eventArgs.Socket.ReceiveMultipartMessage(3);
                            var id = mqMessage.First;
                            RequestType requestType = (RequestType) Enum.Parse(typeof(RequestType), mqMessage[2].ConvertToString());
                            var content = ""; 
                            if (requestType != RequestType.GetAllOptions) // payload expected
                            {
                                content = mqMessage[3].ConvertToString();
                            }
                            logger.Debug("Front received request : " + requestType);

                            ThreadPool.QueueUserWorkItem(ctx =>
                             {
                                // The worker
                                // Parameters are available from the context.
                                var context = (Tuple<NetMQFrame, RequestType, string>)ctx;
                                 var clientId = context.Item1;

                                // Send message to server's backend which then will return the reply to the client
                                using (var workerConnection = new DealerSocket())
                                 {
                                     workerConnection.Connect($"tcp://{backEndPoint}:{backPort}");
                                     logger.Debug($"[{Thread.CurrentThread.ManagedThreadId}] worker");
                                     var messageToClient = new NetMQMessage();
                                     messageToClient.Append(clientId);
                                     messageToClient.AppendEmptyFrame();

                                     if (requestType == RequestType.PriceOption)
                                     {
                                         var message = context.Item3;
                                         Price price = serializerPrice.Deserialize(message);
                                         optionPricingPersistenceService.InsertOption(price.OptionObj);
                                         PricingModelEnum pricingModel = price.PricingModel;
                                         IOptionPricingMethodService pricer = optionPricingInterfaceServiceRegistration.DependencyInjectionManager.ResolveWithKey<IOptionPricingMethodService>(pricingModel.ToString());
                                         Price priceComputed = new Price(pricer.Price(price.OptionObj), price.PricingModel, price.OptionObj);
                                         optionPricingPersistenceService.InsertPrice(priceComputed);
                                         string serializedPrice = serializerPrice.Serialize(priceComputed);
                                         messageToClient.Append(serializedPrice);
                                     }
                                     else if (requestType == RequestType.GetAllOptions)
                                     {
                                         List<Option> optionList = optionPricingPersistenceService.GetAllOptions();
                                         string serializedRes = serializerOptionList.Serialize(optionList);
                                         messageToClient.Append(serializedRes);
                                     }

                                     workerConnection.SendMultipartMessage(messageToClient);
                                 }
                             }, Tuple.Create(id, requestType, content));

                        };
                        // Returning the message to client
                        back.ReceiveReady += (sender, eventArgs) =>
                        {
                            logger.Debug("Back received message, route to client");
                            var mqMessage = eventArgs.Socket.ReceiveMultipartMessage();
                            front.SendMultipartMessage(mqMessage);
                        };
                        poller.Run();
                    }
                }
            }
        }
    }
}
