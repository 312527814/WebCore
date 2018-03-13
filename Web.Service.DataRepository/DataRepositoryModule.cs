using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Core;
using Microsoft.Extensions.Options;

namespace Web.Service.DataRepository
{
    public class DataRepositoryModule : Module
    {

        //public DataRepositoryModule(IOptions<ConnectionSettings> conn)
        //{
        //    //ConnectionManager.MasterConnectionString = conn.Value.Main[0].DbContext;
        //    //ConnectionManager.ReadOnlyConnectionString = conn.Value.ReadOnly[0].DbContext;
        //}
        protected override void Load(ContainerBuilder builder)
        {
            //var contain = builder.Build();
            //var r = contain.Resolve<ConnectionSettings>();
            //var ss = contain.Resolve<IOptions<ConnectionSettings>>();

            var ass = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(ass).AsImplementedInterfaces().AsSelf().InstancePerLifetimeScope();
           // builder.RegisterType<DapperHelper>().InstancePerDependency();
        }
    }


}
