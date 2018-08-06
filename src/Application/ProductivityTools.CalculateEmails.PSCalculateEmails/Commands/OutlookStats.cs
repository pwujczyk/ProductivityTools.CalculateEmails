using Autofac;
using ProductivityTools.CalculateEmails.Autofac;
using ProductivityTools.CalculateEmails.Contract.DataContract;
using ProductivityTools.DateTimeTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.PSCalculateEmails.Commands
{
    public class OutlookStats : PSCmdlet.PSCommandPT<PSCalculateEmails>
    {

        private const int DefaultDays = 10;

        public OutlookStats(PSCalculateEmails cmdlet) : base(cmdlet) { }

        protected override bool Condition => true;

        protected override void Invoke()
        {
            DateTime startDate, endDate;      

            if (base.Cmdlet.StartDate.HasValue && base.Cmdlet.EndDate.HasValue)
            {
                startDate = base.Cmdlet.StartDate.Value;
                endDate= base.Cmdlet.EndDate.Value;
            }
            else
            {
                IDateTimeTools dt = AutofacContainer.Container.Resolve<IDateTimeTools>();
                endDate = dt.Now;
                if (base.Cmdlet.LastDays.HasValue)
                {
                    startDate = endDate.SubtrackDays(base.Cmdlet.LastDays.Value);
                }
                else
                {
                    startDate = endDate.SubtrackDays(DefaultDays);
                }
            }
            
            ServiceClient.StatsClient client = new ServiceClient.StatsClient();
            List<CalculationDay> stats=client.GetStats(startDate, endDate);
            foreach (var stat in stats)
            {
                WriteOutput(stat.MailCountAdd.ToString());
            }
        }
    }
}
