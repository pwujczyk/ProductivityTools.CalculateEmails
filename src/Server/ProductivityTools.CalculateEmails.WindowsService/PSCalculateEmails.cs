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
using ProductivityTools.CalculateEmails.Configuration;
using ProductivityTools.CalculateEmails.Autofac;
using System.ServiceModel.Description;
using ProductivityTools.CalculateEmails.Server;

namespace ProductivityTools.CalculateEmails.WindowsService
{
    public partial class PSCalculateEmails : ServiceBase
    {
        PSCalculateEmailsServer server;

        public PSCalculateEmails()
        {
            InitializeComponent();
            server = new PSCalculateEmailsServer();
        }

        public void OnTest()
        {
            server.OpenHost();
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
            server.StopServer();
        }

        private void StartServer()
        {
            Configure();
            server.OpenHost();
        }

        private static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<Server.AutofacModuleServer>();
            AutofacContainer.Container = builder.Build();
        }
    }
}
