using ProductivityTools.CalculateEmails.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.Contract.ServiceContract
{
    [ServiceContract]
    public interface ICalculateEmailsStatsService
    {
        [OperationContract]
        CalculationDay GetDay(DateTime date);

        [OperationContract]
        List<CalculationDay> GetDays(DateTime startDate, DateTime dateTime);

        [OperationContract]
        void HeartBeat();
    }
}
