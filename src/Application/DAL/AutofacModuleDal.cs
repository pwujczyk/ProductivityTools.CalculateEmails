using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CalculateEmails.Configuration.Contract;
using ConfigurationServiceClient;

namespace DAL
{
    public class AutofacModuleDal : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigurationClient>().As<IConfigurationClient>();
            base.Load(builder);
        }
    }
}
