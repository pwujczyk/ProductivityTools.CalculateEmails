using System;
using System.Collections.Generic;
using ProductivityTools.CalculateEmails.Contract.DataContract;

namespace ProductivityTools.CalculateEmails.ServiceClient
{
    public interface IStatsClient
    {
        CalculationDay GetCalculationDay();
        List<CalculationDay> GetStats(DateTime startdate, DateTime endDate);
    }
}