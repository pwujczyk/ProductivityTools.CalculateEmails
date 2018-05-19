using CalculateEmails.Contract;
using CalculateEmails.Contract.DataContract;
using CalculateEmails.Contract.ServiceContract;
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
    public class CalculateEmailsWCFService : ICalculateEmailsWCFService
    {
        public CalculateEmailsWCFService()
        {
        }

        public Task<CalculationDay> ProcessMail(InboxType inboxType, EmailActionType actionType)
        {
            return Task.Run(() => PerformOperation());
        }

        private CalculationDay PerformOperation()
        {
            Thread.Sleep(20000);
            return new CalculationDay { MailCountAdd = 1 };
        }

        public CalculationDay ProcessTask(TaskActionType taskActionType)
        {
            return new CalculationDay();
        }

        public bool HeartBeat()
        {
            return true;
        }
    }
}
