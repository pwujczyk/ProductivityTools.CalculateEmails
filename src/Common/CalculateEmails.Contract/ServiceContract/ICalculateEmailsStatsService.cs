using CalculateEmails.Contract.DataContract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateEmails.Contract.ServiceContract
{
    public interface ICalculateEmailsStatsService
    {
        CalculationDay GetDay(DateTime date);
    }
}
