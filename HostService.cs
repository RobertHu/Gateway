using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading.Tasks;

namespace Gateway
{
    internal sealed class Hoster
    {
        private ServiceHost _transactionServiceHost;
        private ServiceHost _gatewayServiceHost;

        private ServiceHost _stateServerWebServiceHost;

        internal static readonly Hoster Default = new Hoster();

        private Hoster() { }

        internal void Start()
        {
            try
            {
                _transactionServiceHost = new ServiceHost(typeof(TransactionService));
                this.AddDiscoveryBehaviorAndEndpoint(_transactionServiceHost);
                _transactionServiceHost.Open();

                _gatewayServiceHost = new ServiceHost(typeof(GatewayService));
                this.AddDiscoveryBehaviorAndEndpoint(_gatewayServiceHost);
                _gatewayServiceHost.Open();

                _stateServerWebServiceHost = new ServiceHost(typeof(StateServerWebService));
                _stateServerWebServiceHost.Open();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void AddDiscoveryBehaviorAndEndpoint(ServiceHost host)
        {
            host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
            host.AddServiceEndpoint(new UdpDiscoveryEndpoint());
        }



        internal void Stop()
        {
            try
            {
                _transactionServiceHost.Close();
                _gatewayServiceHost.Close();
                _stateServerWebServiceHost.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }


    }
}
