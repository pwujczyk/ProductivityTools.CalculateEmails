using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ProductivityTools.CalculateEmails.Configuration;

namespace ProductivityTools.DAL
{
    public class AutofacModuleDal : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacModule>();
            base.Load(builder);
        }
    }
}
