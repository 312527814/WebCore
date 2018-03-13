using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreApi.filters
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private ILog log;
        public HttpGlobalExceptionFilter(ILog log)
        {
            this.log = log;
        }
        public void OnException(ExceptionContext context)
        {
            var error = context.Exception;
            //context.HttpContext.Response.StatusCode = 500;
            //context.HttpContext.Response.ContentLength = 100;
            //context.HttpContext.Response.ContentType = null;
            //context.Result = new RedirectResult("/home/About");
            log.Error(error.ToString());
            
        }
    }
}
