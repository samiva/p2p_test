using System;
using System.Threading.Tasks;

namespace p2p_test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            P2PServer p2p = new P2PServer(8888);
            await p2p.Start();
        }
    }
}
