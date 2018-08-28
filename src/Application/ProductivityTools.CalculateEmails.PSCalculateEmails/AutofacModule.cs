using Autofac;
using ProductivityTools.DateTimeTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.PSCalculateEmails
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DateTimeTools.DateTimePT>().As<IDateTimePT>();
            builder.RegisterModule<ServiceClient.AutofacModule>();
            base.Load(builder);
        }
    }
}
