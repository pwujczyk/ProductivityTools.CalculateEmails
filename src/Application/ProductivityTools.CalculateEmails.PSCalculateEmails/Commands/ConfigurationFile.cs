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
    [Cmdlet(VerbsCommon.Get, "ConfigurationFile")]
    public class ConfigurationFile : PSCmdlet.PSCommandPT<PSCalculateEmails>
    {

        private const int DefaultDays = 10;

        public ConfigurationFile(PSCalculateEmails cmdlet) : base(cmdlet) { }

        protected override bool Condition => !string.IsNullOrEmpty(this.Cmdlet.ConfigurationFile);

        protected override void Invoke()
        {
            MasterConfiguration.MConfiguration.SetConfigurationFileName(this.Cmdlet.ConfigurationFile);
        }
    }
}
