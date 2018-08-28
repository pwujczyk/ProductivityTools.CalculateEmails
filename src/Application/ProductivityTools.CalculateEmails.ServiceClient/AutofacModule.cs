using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.ServiceClient
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StatsClient>().As<IStatsClient>();
            builder.RegisterType<DateTimePT.DateTimeTools>().As<DateTimePT.IDateTimeTools>();
            builder.RegisterType<CalculateEmails.Configuration.Config>().As<Configuration.IConfig>();
            base.Load(builder);
        }
    }
}
