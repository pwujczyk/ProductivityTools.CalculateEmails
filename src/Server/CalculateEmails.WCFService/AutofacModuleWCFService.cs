using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DAL;
using DALContracts;

namespace CalculateEmails.WCFService
{
    public class AutofacModuleWCFService:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<AutofacModuleDal>();
            builder.RegisterType<DBManager>().As<IDBManager>();
            base.Load(builder);
        }
    }
}
