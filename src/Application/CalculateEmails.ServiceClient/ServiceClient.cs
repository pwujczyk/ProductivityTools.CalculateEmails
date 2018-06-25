using Autofac;
using CalculateEmails.Autofac;
using CalculateEmails.Contract.DataContract;
using CalculateEmails.Contract.ServiceContract;
using CalculateEmails.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DateTimePT;

namespace CalculateEmails.ServiceClient
{
    public class WcfClient
    {

        DateTime Now
        {
            get
            {
                //todo: fda
                return DateTime.Now;
               // throw new Exception();
                var datetime=AutofacContainer.Container.Resolve<IDateTimePT>();
                return datetime.Now;
            }
        }

        protected ICalculateEmailsWCFMQService Client
        {
            get
            {

                IConfig client = AutofacContainer.Container.Resolve<IConfig>();
                string address = client.Address;
                NetMsmqBinding mqbinding = new NetMsmqBinding(securityMode: NetMsmqSecurityMode.None);
                mqbinding.CloseTimeout = TimeSpan.FromMinutes(20);
                ChannelFactory<ICalculateEmailsWCFMQService> factory = new ChannelFactory<ICalculateEmailsWCFMQService>(mqbinding, new EndpointAddress(address));

                //factory.Endpoint.EndpointBehaviors.Add(new HeartBeatCheckEndpointAttribute());


                ICalculateEmailsWCFMQService proxy = factory.CreateChannel();

                return proxy;
            }
        }



        public void ProcessOutlookMail(InboxType inboxType, EmailActionType actionType)
        {

            this.Client.ProcessMail(inboxType, actionType, Now);

        }

        public CalculationDay ProcesOutlookTask(TaskActionType taskActionType)
        {
            return new CalculationDay();
        }

        public void HeartBeat()
        {
            //todo:remove
            this.Client.HeartBeat();
        }
    }
}
