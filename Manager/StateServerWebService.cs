using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateway
{
    public sealed class StateServerWebService : Protocal.IStateServerWebService
    {
        public void NotifyManagerStarted(string managerAddress, string exchangeCode)
        {
            Console.WriteLine(string.Format("managerAddress = {0}, exchangeCode = {1}", managerAddress, exchangeCode));
            ManagerServiceProxy.Default.Initialize(managerAddress, exchangeCode);
        }
    }
}
