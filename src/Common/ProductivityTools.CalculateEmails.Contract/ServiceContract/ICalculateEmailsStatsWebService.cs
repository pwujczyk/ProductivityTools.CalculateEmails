using ProductivityTools.CalculateEmails.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.Contract.ServiceContract
{
    [ServiceContract]
    public interface ICalculateEmailsStatsWebService
    {
        [OperationContract]
        [WebGet(UriTemplate= "stats?startDate={startDate}&endDate={endDate}", ResponseFormat =WebMessageFormat.Json)]
        List<CalculationDay> GetDays(DateTime startDate, DateTime endDate);
    }
}
