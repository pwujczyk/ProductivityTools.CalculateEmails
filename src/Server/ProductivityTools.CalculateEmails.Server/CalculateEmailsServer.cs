using System;

namespace ProductivityTools.CalculateEmails.Server
{
    public class CalculateEmailsServer
    {
        public void OpenHost()
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

        public void StopServer()
        {
            host.Close();
        }
    }
}
