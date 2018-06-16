using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Configuration;

namespace DAL
{
    public class AutofacModuleDal : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Config>().As<IConfig>();
            base.Load(builder);
        }
    }
}
