using iExchange.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Protocal
{
    [ServiceContract]
    public interface ITransactionService
    {
        [OperationContract]
        void Broadcast(Command command);
    }


    [ServiceContract]
    public interface ITransactionServerService
    {
        [OperationContract]
        void Place(TransactionData tranData);

        [OperationContract]
        void SetQuoation(OverridedQuotation[] overridedQs);

    }

}
