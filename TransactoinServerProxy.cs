using iExchange.Common;
using Protocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Gateway
{
    internal sealed class TransactoinServerProxy
    {
        private bool _isChannelBroken = true;
        private ITransactionServerService _serverService;

        internal static readonly TransactoinServerProxy Default = new TransactoinServerProxy();

        private TransactoinServerProxy() { }

        internal void Place(TransactionData transaction)
        {
            try
            {
                if (_serverService == null) return;
                _serverService.Place(transaction);
            }
            catch (CommunicationException ex)
            {
                _isChannelBroken = true;
            }
        }

        internal void SetQuoation(OverridedQuotation[] overridedQs)
        {
            try
            {
                if (_serverService == null) return;
                _serverService.SetQuoation(overridedQs);
            }
            catch (CommunicationException ex)
            {
                _isChannelBroken = true;
            }
        }


        internal void ServiceDiscoveryHandle(EndpointAddress[] addresses)
        {
            if (addresses.Length == 0 || !_isChannelBroken) return;
            Console.WriteLine("Discovery  transaction service");
            _serverService = ChannelFactory<ITransactionServerService>.CreateChannel(new NetTcpBinding(SecurityMode.None), addresses[0]);
            _isChannelBroken = false;
        }

    }
}
