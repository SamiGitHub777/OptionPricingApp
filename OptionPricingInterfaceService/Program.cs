using System;
using System.Threading.Tasks;

namespace OptionPricingInterfaceService
{
    class Program
    {
        /*
        public static void Main(string[] args)
        {
            /*
             * Once a socket has been created it needs to be bound or connected. 
             * To do this a string is passed in of the form “transport:endpoint”. The transport can be any of the following values:
             * inproc
             * thread to thread within a single process
             * ipc
             * inter-process communication (Linux only and not available in any of the native ports as yet)
             * tcp
             * box to box communication and inter-process when “ipc” isn’t available
             * epgm, pgm
             * multicast protocols that make my head hurt, the guide has more information if you really want to use these
             * Once you’ve decided on a transport you need to define an endpoint as follows:

             * inproc
             * unique (enough) ASCII string
             * ipc
             * unique (enough) ASCII string (usually postfixed with “.ipc”)
             * tcp
             * internet address and port number
             * 
            using (var responseSocket = new ResponseSocket("@tcp://localhost:5555"))
            {
                while (true)
                {
                    Console.WriteLine("Waiting for requests");
                    var msg = responseSocket.ReceiveFrameString();
                    Console.WriteLine($"Received message : {msg}");
                    responseSocket.SendFrame("Hello from server");
                }
            }
        }*/
        static void Main(string[] args)
        {
            string endPoint = "localhost";// "192.168.1.19";
            int port = 5555;
            var server = new NetMQServer(endPoint, port);
            Task.Factory.StartNew(() => server.StartListening());
            Console.ReadLine(); //let server run until user pressed Enter key
        }
    }
}

