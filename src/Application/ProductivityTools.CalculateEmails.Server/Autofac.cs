using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductivityTools.CalculateEmails.Server
{
    public class AutofacModuleServer : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CalculateEmails.Service.Autofac>();
            base.Load(builder);
        }
    }
}
