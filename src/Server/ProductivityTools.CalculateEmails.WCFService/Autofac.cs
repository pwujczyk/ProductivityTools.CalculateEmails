using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ProductivityTools.CalculateEmails.DALContracts;
using ProductivityTools.DAL;

namespace ProductivityTools.CalculateEmails.WCFService
{
    public class Autofac:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacModuleDal>();
            builder.RegisterType<DBManager>().As<IDBManager>();
            base.Load(builder);
        }
    }
}
