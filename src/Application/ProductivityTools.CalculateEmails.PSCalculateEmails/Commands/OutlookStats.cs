using Autofac;
using ProductivityTools.CalculateEmails.Autofac;
using ProductivityTools.CalculateEmails.Contract.DataContract;
using ProductivityTools.CalculateEmails.ServiceClient;
using ProductivityTools.DateTimeTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.PSCalculateEmails.Commands
{
    [Cmdlet(VerbsCommon.Get, "OutlookStats")]
    public class OutlookStats : PSCmdlet.PSCommandPT<PSCalculateEmails>
    {

        private const int DefaultDays = 10;

        public OutlookStats(PSCalculateEmails cmdlet) : base(cmdlet) { }

        protected override bool Condition => string.IsNullOrEmpty(this.Cmdlet.ConfigurationFile);

        protected override void Invoke()
        {
            Func<DateTime> GetToday = () =>
                  {

                      IDateTimePT dt = AutofacContainer.Container.Resolve<IDateTimePT>();
                      var today = dt.Now;
                      return today;
                  };

            DateTime startDate, endDate;

            if (base.Cmdlet.StartDate.HasValue)
            {
                startDate = base.Cmdlet.StartDate.Value;
                if (base.Cmdlet.EndDate.HasValue)
                {
                    endDate = base.Cmdlet.EndDate.Value;
                }
                else
                {
                    endDate = GetToday();
                }
            }
            else
            {
                endDate = GetToday();
                if (base.Cmdlet.LastDays.HasValue)
                {
                    startDate = endDate.SubtrackDays(base.Cmdlet.LastDays.Value);
                }
                else
                {
                    startDate = endDate.SubtrackDays(DefaultDays);
                }
            }

            IStatsClient client = Autofac.AutofacContainer.Container.Resolve<IStatsClient>();
            List<CalculationDay> stats = client.GetStats(startDate, endDate);
            Func<int, string> f = s => s.ToString().FillWithZeros(3);
            foreach (var stat in stats.OrderBy(x => x.Date)) 
            {
                string result = $"[{stat.Date}] m-added: {f(stat.MailCountAdd)}, m-sent: {f(stat.MailCountSent)}, m-processed: {f(stat.MailCountProcessed)}, t-added {f(stat.TaskCountAdded)}, t-finished {f(stat.TaskCountFinished)}, t-removed {f(stat.TaskCountRemoved)}";
                WriteOutput(result);
            }
        }
    }
}
