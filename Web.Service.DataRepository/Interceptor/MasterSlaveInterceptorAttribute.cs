using AspectCore.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Service.Connection;

namespace Web.Service.DataRepository.Interceptor
{
    public class MasterSlaveInterceptorAttribute : AbstractInterceptorAttribute
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var dapperHelper = (context.Implementation as IDapper).dapperHelper;
            var connectionString = context.ServiceProvider.GetService<IConnectionString>();
            dapperHelper.MasterConnstr = connectionString.MasterConnstr;
            dapperHelper.SlaveConnstr = connectionString.SlaveConnstr;
            (context.Implementation as IMasterReadSeparate).Invoke?.Invoke(connectionString);
            await next(context);
        }
    }
}
