using ProductivityTools.CalculateEmails.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.Contract.ServiceContract
{
    [ServiceContract]
    public interface ICalculateEmailsProcessing
    {
        [OperationContract(IsOneWay = true)]
        void ProcessMail(InboxType inboxType, EmailActionType actionType, DateTime occured);

        [OperationContract(IsOneWay = true)]
        void ProcessTask(TaskActionType taskActionType, DateTime occured);
    }
}


