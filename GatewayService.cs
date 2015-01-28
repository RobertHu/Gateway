using Protocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway
{
    public sealed class GatewayService : IGatewayService
    {
        public void Place(TransactionData transaction)
        {
            TransactoinServerProxy.Default.Place(transaction);
        }

        public string Test()
        {
            return "From Gateway";
        }
    }
}
