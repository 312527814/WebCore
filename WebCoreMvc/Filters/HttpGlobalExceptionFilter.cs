using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCoreMvc
{
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            //var error = context.HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IStatusCodePagesFeature>();
            //context.HttpContext.Response.StatusCode = 500;
            //context.HttpContext.Response.ContentLength = 100;
            //context.HttpContext.Response.ContentType = null;
            //context.Result = new RedirectResult("/home/About");
        }
    }
}
