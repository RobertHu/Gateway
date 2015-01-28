using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Protocal
{
    public delegate void ServiceDiscoveryHandle(EndpointAddress[] address);

    public sealed class ServiceFinder<T>
    {
        public event ServiceDiscoveryHandle ServiceDiscovied;
        private Thread _thread;

        public ServiceFinder()
        {
            _thread = new Thread(this.Discovery);
            _thread.IsBackground = true;
        }

        public void Start()
        {
            _thread.Start();
        }


        private void Discovery()
        {
            while (true)
            {
                Thread.Sleep(500);
                var result = this.FindServiceAddress();
                if (result.Length == 0) continue;
                this.OnDiscovery(result);
            }
        }

        private EndpointAddress[] FindServiceAddress()
        {
            DiscoveryClient discoveryClient = new DiscoveryClient(new UdpDiscoveryEndpoint());
            FindResponse findResponse = discoveryClient.Find(new FindCriteria(typeof(T)));
            return findResponse.Endpoints.Select(m => m.Address).ToArray();
        }

        private void OnDiscovery(EndpointAddress[] addresses)
        {
            var handle = this.ServiceDiscovied;
            if (handle != null)
            {
                handle(addresses);
            }
        }
    }
}
