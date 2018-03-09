using Autofac;
using Web.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var ass = System.Reflection.Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(ass).AsImplementedInterfaces().AsSelf();

            //builder.RegisterAssemblyTypes(ass).AsClosedTypesOf(typeof(TestGeneric<>));
        }

    }
}
