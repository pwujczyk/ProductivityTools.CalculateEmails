using CalculateEmails.Contract;
using CalculateEmails.WCFService;
using MasterConfiguration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.WindowsService
{
    public partial class CalculateEmailsWindowsService : ServiceBase
    {

        ServiceHost host;
        public CalculateEmailsWindowsService()
        {
            InitializeComponent();
        }

        public void OnDebug()
        {
            StartServer();
        }

        protected override void OnStart(string[] args)
        {
            StartServer();
        }

        protected override void OnStop()
        {
            StopServer();
        }

        private void StartServer()
        {
            var binding = new NetTcpBinding();
            var address = MConfiguration.Configuration["Address"];

            host = new ServiceHost(typeof(CalculateEmailsWCFService));
            host.AddServiceEndpoint(typeof(ICalculateEmailsWCFService), binding, address);

            host.Open();
        }

        private void StopServer()
        {
            host.Close();
        }
    }
}
