using Autofac;
using AutoMapper;
using CalculateEmails.Autofac;
using CalculateEmails.Contract;
using CalculateEmails.Contract.DataContract;
using CalculateEmails.Contract.ServiceContract;
using CalculateEmails.WCFService.Application;
using DALContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CalculateEmails.WCFService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class CalculateEmailsWCFService : ICalculateEmailsWCFMQService
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static CalculateEmailsWCFService()
        {
            IDBManager DBManager = AutofacContainer.Container.Resolve<IDBManager>();
            DBManager.PerformDatabaseupdate();

        }

        public CalculateEmailsWCFService()
        {

        }

        public void ProcessMail(InboxType inboxType, EmailActionType actionType, DateTime occured)
        {
            PerformOperation(inboxType, actionType, occured);
        }

        private void PerformOperation(InboxType inboxType, EmailActionType actionType, DateTime occured)
        {
            //           log.Info("Hello logging world!");
            BLManager bLManager = new BLManager();
            bLManager.Process(actionType, inboxType, occured);
           // var config = new MapperConfiguration(cfg => cfg.CreateMap<CalculationDayDB, CalculationDay>());
        }

        public void ProcessTask(TaskActionType taskActionType, DateTime occured)
        {
            TaskManager taskManager = new TaskManager();
            taskManager.Process(taskActionType, occured);
        }

        public void HeartBeat()
        {
            //return true;
        }
    }
}
