using CalculateEmails.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.Contract.ServiceContract
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface ICalculateEmailsWCFService
    {
        [OperationContract]
        [HeartBeatCheck]
        Task<CalculationDay> ProcessMail(InboxType inboxType, EmailActionType actionType);

        [OperationContract]
        [HeartBeatCheck]
        CalculationDay ProcessTask(TaskActionType taskActionType);

        [OperationContract]
        [HeartBeatCheck]
        bool HeartBeat();
    }
}


