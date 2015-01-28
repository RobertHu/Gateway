using Protocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Protocal
{
    [ServiceContract]
    public interface ICommandCollectService
    {
        [OperationContract(AsyncPattern = false)]
        void AddCommand(Command command);
    }

}