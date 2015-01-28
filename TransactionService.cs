using Protocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Gateway
{
    public sealed class TransactionService: ITransactionService
    {
        public void Broadcast(Command command)
        {
            Broadcaster.Default.AddCommand(command);
        }
    }
}
