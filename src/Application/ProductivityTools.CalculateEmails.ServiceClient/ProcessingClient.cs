using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DateTimePT;
using ProductivityTools.CalculateEmails.Contract.DataContract;
using ProductivityTools.CalculateEmails.Contract.ServiceContract;
using ProductivityTools.CalculateEmails.Configuration;
using ProductivityTools.CalculateEmails.Autofac;

namespace ProductivityTools.CalculateEmails.ServiceClient
{
    public class ProcessingClient
    {

        DateTime Now
        {
            get
            {
                var datetime = AutofacContainer.Container.Resolve<IDateTimeTools>();
                return datetime.Now;
            }
        }

        protected ICalculateEmailsProcessing Client
        {
            get
            {
                IConfig client = AutofacContainer.Container.Resolve<IConfig>();
                string address = client.MQAdress;
                NetMsmqBinding mqbinding = new NetMsmqBinding(securityMode: NetMsmqSecurityMode.None);
                mqbinding.CloseTimeout = TimeSpan.FromMinutes(20);
                ChannelFactory<ICalculateEmailsProcessing> factory = new ChannelFactory<ICalculateEmailsProcessing>(mqbinding, new EndpointAddress(address));

                //factory.Endpoint.EndpointBehaviors.Add(new HeartBeatCheckEndpointAttribute());

                ICalculateEmailsProcessing proxy = factory.CreateChannel();

                return proxy;
            }
        }

        public void ProcessOutlookMail(InboxType inboxType, EmailActionType actionType)
        {
            this.Client.ProcessMail(inboxType, actionType, Now);
            (this.Client as IClientChannel).Close();
        }

        public void ProcesOutlookTask(TaskActionType taskActionType)
        {
            this.Client.ProcessTask(taskActionType, Now);
            (this.Client as IClientChannel).Close();
        }


    }
}
