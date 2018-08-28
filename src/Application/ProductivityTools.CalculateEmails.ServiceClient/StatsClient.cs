using Autofac;
using ProductivityTools.CalculateEmails.Autofac;
using ProductivityTools.CalculateEmails.Configuration;
using ProductivityTools.CalculateEmails.Contract.DataContract;
using ProductivityTools.CalculateEmails.Contract.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.ServiceClient
{
    public class StatsClient : IStatsClient
    {
        protected ICalculateEmailsStatsService Client
        {
            get
            {
                IConfig client = Autofac.AutofacContainer.Container.Resolve<IConfig>();
                string address = client.OnlineAddress;
                NetTcpBinding binding = new NetTcpBinding();
                 ChannelFactory<ICalculateEmailsStatsService> factory = new ChannelFactory<ICalculateEmailsStatsService>(binding, address);
                ICalculateEmailsStatsService proxy = factory.CreateChannel();
                return proxy;
            }
        }


        public List<CalculationDay> GetStats(DateTime startdate, DateTime endDate)
        {
            List<CalculationDay> result = this.Client.GetDays(startdate, endDate);
            (Client as IClientChannel).Close();
            return result;
        }

        public CalculationDay GetCalculationDay()
        {
            //todo
            var result=Client.GetDay(DateTime.Now);
            return result;
        }

        public void HeartBeat()
        {
            this.Client.HeartBeat();
        }
    }
}
