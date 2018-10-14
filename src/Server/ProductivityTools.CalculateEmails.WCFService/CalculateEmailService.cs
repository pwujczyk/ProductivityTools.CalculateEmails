using Autofac;
using AutoMapper;
using ProductivityTools.CalculateEmails.Autofac;
using ProductivityTools.CalculateEmails.Contract.DataContract;
using ProductivityTools.CalculateEmails.Contract.ServiceContract;
using ProductivityTools.CalculateEmails.DALContracts;
using ProductivityTools.CalculateEmails.Service.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.Service
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class CalculateEmailsService : ICalculateEmailsProcessing, ICalculateEmailsStatsService, ICalculateEmailsStatsWebService
    {
        static CalculateEmailsService()
        {
            IDBManager DBManager = AutofacContainer.Container.Resolve<IDBManager>();
            DBManager.PerformDatabaseupdate();

        }

        public CalculateEmailsService()
        {

        }

        public void ProcessMail(InboxType inboxType, EmailActionType actionType, DateTime occured)
        {
            PerformOperation(inboxType, actionType, occured);
        }

        private void PerformOperation(InboxType inboxType, EmailActionType actionType, DateTime occured)
        {
            //           log.Info("Hello logging world!");
            MailManager bLManager = new MailManager();
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

        public List<CalculationDay> GetDays(DateTime startDate, DateTime endDay)
        {
            var baseManager = new BaseManager();
            List<CalculationDay> list=baseManager.GetCalcuationDays(startDate, endDay);
            return list;
        }
    }
}
