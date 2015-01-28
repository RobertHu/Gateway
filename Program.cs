using Protocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Gateway
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceFinder<ICommandCollectService> finder = new ServiceFinder<ICommandCollectService>();
            finder.ServiceDiscovied += address =>
                {
                    Broadcaster.Default.AddServices(address);
                };
            finder.Start();

            ServiceFinder<ITransactionServerService> transactionServerFinder = new ServiceFinder<ITransactionServerService>();
            transactionServerFinder.ServiceDiscovied += TransactoinServerProxy.Default.ServiceDiscoveryHandle;
            transactionServerFinder.Start();

            Hoster.Default.Start();
            Console.WriteLine("Started");
            Console.ReadLine();
        }

    }
}
