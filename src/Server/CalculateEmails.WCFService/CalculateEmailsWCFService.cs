using CalculateEmails.Contract;
using CalculateEmails.Contract.DataContract;
using CalculateEmails.Contract.ServiceContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CalculateEmails.WCFService
{
    public class CalculateEmailsWCFService : ICalculateEmailsWCFService
    {
        public CalculateEmailsWCFService()
        {
        }

        public CalculationDay ProcessMail(InboxType inboxType, EmailActionType actionType)
        {
            return new CalculationDay();
        }

        public CalculationDay ProcessTask(TaskActionType taskActionType)
        {
            return new CalculationDay();
        }
    }
}
