using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using AspectCore.DynamicProxy.Parameters;
using Web.Service.Core;
using Web.Service.Connection;

namespace Web.Service.Interceptor
{
    public class MyInterceptorAttribute : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            Console.WriteLine("MyInterceptorAttribute");
            await next(context);
        }
    }

    public class NyAttribute : AspectCore.DynamicProxy.Parameters.ParameterInterceptorAttribute
    {
        public override async Task Invoke(ParameterAspectContext context, ParameterAspectDelegate next)
        {
            await next(context);
        }
    }
}
