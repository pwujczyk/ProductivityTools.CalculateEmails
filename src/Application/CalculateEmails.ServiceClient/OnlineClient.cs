using Autofac;
using CalculateEmails.Autofac;
using CalculateEmails.Configuration;
using CalculateEmails.Contract.DataContract;
using CalculateEmails.Contract.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.ServiceClient
{
    public class OnlineClient
    {
        protected ICalculateEmailsStatsService Client
        {
            get
            {
                IConfig client = AutofacContainer.Container.Resolve<IConfig>();
                string address = client.OnlineAddress;
                NetTcpBinding mqbinding = new NetTcpBinding();
                ChannelFactory<ICalculateEmailsStatsService> factory = new ChannelFactory<ICalculateEmailsStatsService>(mqbinding, new EndpointAddress(address));

                ICalculateEmailsStatsService proxy = factory.CreateChannel();

                return proxy;
            }
        }

        public CalculationDay GetCalculationDay()
        {
            //todo
            var result=Client.GetDay(DateTime.Now);
            return result;
        }

        public void HeartBeat()
        {
            //todo:remove
            this.Client.HeartBeat();
        }
    }
}
