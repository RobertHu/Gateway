using Protocal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Gateway
{
    internal delegate void AgentCommunicateFailedHandle(TraderServerAgent agent);

    internal sealed class TraderServerAgent
    {
        private ICommandCollectService _service;
        private ActionBlock<Command> _actionBlock;
        private string _serviceUrl;

        internal event AgentCommunicateFailedHandle CommunicateFailed;

        internal TraderServerAgent(ICommandCollectService service, string serviceUrl)
        {
            _service = service;
            _serviceUrl = serviceUrl;
            _actionBlock = new ActionBlock<Command>(command =>
            {
                try
                {
                    service.AddCommand(command);
                }
                catch (CommunicationException ex)
                {
                    Console.WriteLine(ex);
                    this.OnCommunicationError();
                }
            });
        }


        internal string ServiceUrl
        {
            get { return _serviceUrl; }
        }

        private void OnCommunicationError()
        {
            var handle = this.CommunicateFailed;
            if (handle != null)
            {
                handle(this);
            }
        }


        internal void Send(Command command)
        {
            _actionBlock.Post(command);
        }

    }


    internal sealed class Broadcaster
    {
        private Dictionary<string, TraderServerAgent> _agentDict = new Dictionary<string, TraderServerAgent>();
        private object _mutex = new object();

        internal static readonly Broadcaster Default = new Broadcaster();

        private Broadcaster() { }

        internal void AddServices(EndpointAddress[] addresses)
        {
            lock (_mutex)
            {
                foreach (var eachAddress in addresses)
                {
                    string serviceUrl = eachAddress.Uri.ToString();
                    if (!_agentDict.ContainsKey(serviceUrl))
                    {
                        var channel = ChannelFactory<ICommandCollectService>.CreateChannel(new NetTcpBinding(SecurityMode.None), eachAddress);
                        var agent = new TraderServerAgent(channel, serviceUrl);
                        agent.CommunicateFailed += this.RemoveService;
                        _agentDict.Add(serviceUrl, agent);
                    }
                }
            }
        }

        internal void RemoveService(TraderServerAgent agent)
        {
            lock (_mutex)
            {
                _agentDict.Remove(agent.ServiceUrl);
                agent.CommunicateFailed -= this.RemoveService;
            }
        }



        internal void AddCommand(Command command)
        {
            foreach (var pair in _agentDict)
            {
                pair.Value.Send(command);
            }
        }

    }
}
