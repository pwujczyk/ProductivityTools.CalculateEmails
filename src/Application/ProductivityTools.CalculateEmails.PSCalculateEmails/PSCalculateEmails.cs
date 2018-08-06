using Autofac;
using ProductivityTools.CalculateEmails.Autofac;
using ProductivityTools.CalculateEmails.PSCalculateEmails.Commands;
using ProductivityTools.PSCmdlet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.PSCalculateEmails
{
    [Cmdlet(VerbsCommon.Get, "OutlookStats")]
    public class PSCalculateEmails : PSCmdletPT
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? LastDays { get; set; }


        public PSCalculateEmails()
        {
            Autofac();
            MasterConfiguration.MConfiguration.SetConfigurationName("Configuration.config");
            base.AddCommand(new OutlookStats(this));
        }

        public PSCalculateEmails(bool debug)
        {
            if (debug)
            {
                MasterConfiguration.MConfiguration.SetConfigurationName("Configuration.config");
                base.AddCommand(new OutlookStats(this));
            }
        }


        private void Autofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacModule>();
            AutofacContainer.Container = builder.Build();
        }

        protected override void ProcessRecord()
        {
            this.ProcessCommands();
        }

        public void Test()
        {
            this.ProcessRecord();
        }
    }
}
