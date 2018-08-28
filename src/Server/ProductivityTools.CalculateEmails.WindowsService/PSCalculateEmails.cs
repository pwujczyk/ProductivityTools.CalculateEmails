using Autofac;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Messaging;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using ProductivityTools.MasterConfiguration;
using ProductivityTools.CalculateEmails.Contract.ServiceContract;
using ProductivityTools.CalculateEmails.WCFService;
using ProductivityTools.CalculateEmails.Configuration;
using ProductivityTools.CalculateEmails.Autofac;
using System.ServiceModel.Description;

namespace ProductivityTools.CalculateEmails.WindowsService
{
    public partial class PSCalculateEmails : ServiceBase
    {
        ServiceHost host;
        public PSCalculateEmails()
        {
            InitializeComponent();
        }

        public void OnTest()
        {
            OpenHost();
        }
        public void OnDebug()
        {
            StartServer();
        }

        protected override void OnStart(string[] args)
        {
            MConfiguration.SetConfigurationName("Configuration.config");
            StartServer();
        }

        protected override void OnStop()
        {
            StopServer();
        }

        private void StartServer()
        {
            Configure();
            OpenHost();
        }

        private static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<CalculateEmails.WCFService.Autofac>();
            AutofacContainer.Container = builder.Build();
        }

        private void OpenHost()
        {
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

            host = new ServiceHost(typeof(CalculateEmailsWCFService));
            //host.AddServiceEndpoint(typeof(ICalculateEmailsProcessing), mqBinding, mqAddress);
            //host.AddServiceEndpoint(typeof(ICalculateEmailsStatsService), new NetTcpBinding(), onlineAddress);

            WebHttpBehavior behavior = new WebHttpBehavior();


            ServiceEndpoint serviceEndpoint = new ServiceEndpoint(

                    ContractDescription.GetContract(typeof(ICalculateEmailsStatsService))
                    , new WebHttpBinding()
                    , new EndpointAddress(webAddres));
            serviceEndpoint.EndpointBehaviors.Add(behavior);
            host.AddServiceEndpoint(serviceEndpoint);
            //host.AddServiceEndpoint(typeof(ICalculateEmailsStatsService), new WebHttpBinding(), webAddres);
            host.Open();
        }

        private void StopServer()
        {
            host.Close();
        }
    }
}
