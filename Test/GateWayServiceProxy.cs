using Protocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    internal sealed class GateWayServiceProxy
    {
        private IGatewayService _service;
        private bool _isBroken = true;

        internal static readonly GateWayServiceProxy Default = new GateWayServiceProxy();

        private GateWayServiceProxy() { }

        internal void Place(TransactionData transaction)
        {
            try
            {
                _service.Place(transaction);
            }
            catch (CommunicationException ex)
            {
                Console.WriteLine(ex);
                _isBroken = true;
            }
        }

        internal void ServiceDiscoveryHandle(EndpointAddress[] addresses)
        {
            if (addresses.Length == 0 || !_isBroken) return;
            _service = ChannelFactory<IGatewayService>.CreateChannel(new NetTcpBinding(SecurityMode.None), addresses[0]);
        }

    }
}
