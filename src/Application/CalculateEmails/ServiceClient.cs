//using CalculateEmails.Contract;
//using CalculateEmails.Contract.DataContract;
//using CalculateEmails.Contract.ServiceContract;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.ServiceModel;
//using System.ServiceModel.Description;
//using System.Text;
//using System.Threading.Tasks;

//namespace CalculateEmails
//{
//    class ServiceClient
//    {
//        protected ICalculateEmailsWCFMQService Client
//        {
//            get
//            {
//                string address = Configuration.Config.Address;
//                NetMsmqBinding mqbinding = new NetMsmqBinding(securityMode: NetMsmqSecurityMode.None);
//                mqbinding.CloseTimeout = TimeSpan.FromMinutes(20);
//                ChannelFactory<ICalculateEmailsWCFMQService> factory = new ChannelFactory<ICalculateEmailsWCFMQService>(mqbinding, new EndpointAddress(address));

//                //factory.Endpoint.EndpointBehaviors.Add(new HeartBeatCheckEndpointAttribute());


//                ICalculateEmailsWCFMQService proxy = factory.CreateChannel();

//                return proxy;
//            }
//        }



//        public void ProcessOutlookMail(InboxType inboxType, EmailActionType actionType)
//        {

//            this.Client.ProcessMail(inboxType, actionType);

//        }

//        public CalculationDay ProcesOutlookTask(TaskActionType taskActionType)
//        {
//            return new CalculationDay();
//        }

//        public void HeartBeat()
//        {
//            //todo:remove
//             this.Client.HeartBeat();
//        }
//    }
//}
