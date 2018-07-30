using Autofac;
using AutoMapper;
using ProductivityTools.CalculateEmails.Autofac;
using ProductivityTools.CalculateEmails.Contract.DataContract;
using ProductivityTools.CalculateEmails.Contract.ServiceContract;
using ProductivityTools.CalculateEmails.DALContracts;
using ProductivityTools.CalculateEmails.WCFService.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.WCFService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class CalculateEmailsWCFService : ICalculateEmailsWCFMQService, ICalculateEmailsStatsService
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
            // 
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

        public CalculationDay GetDay(DateTime date)
        {
            BaseManager baseManager = new BaseManager();
            CalculationDay calculation = baseManager.GetLastCalculationDay(date);
            return calculation;
        }
    }
}
