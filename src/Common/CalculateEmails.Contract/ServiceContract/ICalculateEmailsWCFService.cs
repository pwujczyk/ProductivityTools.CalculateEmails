using CalculateEmails.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace CalculateEmails.Contract.ServiceContract
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICalculateEmailsWCFService
    {
        [OperationContract]
        CalculationDay ProcessMail(InboxType inboxType, EmailActionType actionType);

        [OperationContract]
        CalculationDay ProcessTask(TaskActionType taskActionType);
    }
}


