using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetworkController;
using NetworkController.Interfaces;

namespace p2p_test
{
    class P2PServer
    {
        private readonly CancellationTokenSource exitTokenSource;
        private INetworkController Network { get; set; }
        public int PortNum { get; private set; }
        public P2PServer(int port)
        {
            PortNum = port;
            Network = new NetworkManagerFactory()
                .AddConnectionResetRule((externalNode) =>
                {
                    return false;
                })
                .AddNewUnannouncedConnectionAllowanceRule((guid) =>
                {
                    Console.WriteLine($"{guid} wants to connect. y/n?");
                    string answer = Console.ReadLine();
                    if (answer == "y")
                    {
                        Console.WriteLine("Allowed");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Rejected");
                        return false;
                    }
                })
                .Create();

            exitTokenSource = new CancellationTokenSource();

        }


        public async Task Start()
        {
            Network.StartListening(PortNum);

            Console.WriteLine("Current id {0}", Network.DeviceId);

            await Task.Delay(Timeout.Infinite, exitTokenSource.Token);
        }
    }
}
