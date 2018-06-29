using CalculateEmails.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.Contract.ServiceContract
{
    [ServiceContract]
    public interface ICalculateEmailsStatsService
    {
        [OperationContract]
        CalculationDay GetDay(DateTime date);

        [OperationContract]
        void HeartBeat();
    }
}
