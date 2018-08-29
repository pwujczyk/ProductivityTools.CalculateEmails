using Autofac;
using ProductivityTools.CalculateEmails.Autofac;
using ProductivityTools.CalculateEmails.Configuration;
using ProductivityTools.CalculateEmails.Contract.ServiceContract;
using ProductivityTools.CalculateEmails.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.Server
{
    public class PSCalculateEmailsServer
    {
        ServiceHost host;

        public void OpenHost()
        {
            Configure();

            IConfig client = AutofacContainer.Container.Resolve<IConfig>();
            var mqBinding = new NetMsmqBinding(NetMsmqSecurityMode.None);
            string queneAddress = $".\\private$\\{client.QueneName}";
            string mqAddress = client.MQAdress;
            string onlineAddress = client.OnlineAddress;
            string webAddres = client.OnlineWebAddress;

            if (MessageQueue.Exists(queneAddress) == false)
            {
                MessageQueue.Create(queneAddress, true);
            }

            host = new ServiceHost(typeof(CalculateEmailsService));
            host.AddServiceEndpoint(typeof(ICalculateEmailsProcessing), mqBinding, mqAddress);
            host.AddServiceEndpoint(typeof(ICalculateEmailsStatsService), new NetTcpBinding(), onlineAddress);

            ServiceEndpoint serviceEndpoint = new ServiceEndpoint(
                    ContractDescription.GetContract(typeof(ICalculateEmailsStatsWebService))
                    , new WebHttpBinding()
                    , new EndpointAddress(webAddres));
            WebHttpBehavior behavior = new WebHttpBehavior();
            serviceEndpoint.EndpointBehaviors.Add(behavior);

            host.AddServiceEndpoint(serviceEndpoint);
            host.Open();
        }

        public void StopServer()
        {
            host.Close();
        }

        private static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<CalculateEmails.Service.Autofac>();
            AutofacContainer.Container = builder.Build();
        }
    }
}
