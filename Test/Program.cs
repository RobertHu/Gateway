using Protocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test
{
    internal sealed class CommandCollectService : ICommandCollectService
    {
        public void AddCommand(Command command)
        {
            var msg = string.Format("receive a command, sequence = {0}, type = {1}", command.Sequence, command.Type);
            Console.WriteLine(msg);
            var changeContentCommand = command as ChangeContentCommand;
            if (changeContentCommand != null)
            {
                Console.WriteLine(changeContentCommand.Content);
            }

        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            ServiceFinder<IGatewayService> finder = new ServiceFinder<IGatewayService>();
            finder.ServiceDiscovied += GateWayServiceProxy.Default.ServiceDiscoveryHandle;
            finder.Start();

            var binding = new CustomBinding();
            binding.Elements.Add(new TextMessageEncodingBindingElement(MessageVersion.Soap12, Encoding.UTF8));
            binding.Elements.Add(new HttpTransportBindingElement());
            var stateServerWebService = ChannelFactory<IStateServerWebService>.CreateChannel(binding, new EndpointAddress("http://localhost:7778/Gateway/webService"));
            stateServerWebService.NotifyManagerStarted("nettcp// mangaer", "my iExchange");

            using (ServiceHost host = new ServiceHost(typeof(CommandCollectService)))
            {
                host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());
                host.AddServiceEndpoint(new UdpDiscoveryEndpoint());
                host.Open();

                //Thread.Sleep(1000 * 60);
                //GateWayServiceProxy.Default.Place(CreateTransactionData());
                Console.WriteLine("started ");
                Console.Read();
            }
        }

        static Protocal.TransactionData CreateTransactionData()
        {
            Protocal.TransactionData tranData = new Protocal.TransactionData();
            tranData.Id = Guid.NewGuid();
            tranData.InstrumentId = Guid.Parse("33C4C6E2-E33C-4A21-A01A-35F4EC647890");
            tranData.AccountId = Guid.Parse("B940D4B7-4A4E-46DF-8EA4-77B0C3CC1A6B");
            tranData.Type = iExchange.Common.TransactionType.Single;
            tranData.SubType = iExchange.Common.TransactionSubType.None;
            tranData.OrderType = iExchange.Common.OrderType.SpotTrade;
            tranData.ExpireType = iExchange.Common.ExpireType.Day;
            var baseTime = DateTime.Now;
            tranData.BeginTime = baseTime;
            tranData.EndTime = baseTime.AddMinutes(30);
            tranData.SubmitTime = baseTime;
            tranData.SubmitorId = Guid.Parse("CB58B47D-A705-42DD-9308-6C6B26CE79A7");
            tranData.Orders = new List<Protocal.OrderData>();
            var order = CreateOrderData();
            tranData.Orders.Add(order);
            return tranData;
        }


        static Protocal.OrderData CreateOrderData()
        {
            var orderData = new Protocal.OrderData()
            {
                Id = Guid.NewGuid(),
                IsOpen = true,
                IsBuy = true,
                SetPrice = "0.9123",
                Lot = 3,
                LotBalance = 3,
                TradeOption = iExchange.Common.TradeOption.Better,
            };
            return orderData;
        }




    }
}
