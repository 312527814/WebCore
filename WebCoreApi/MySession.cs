using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Service.Core;

namespace WebCoreApi
{
    public class MySession : IMySession
    {
        private IHttpContextAccessor accessor;
        public MySession(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }
        
        public int UserId
        {
            get
            {
                var context = accessor.HttpContext;
                var base64Json = context.Request.Headers["token"];
                var sbytes = Convert.FromBase64String(base64Json);
                var json = Encoding.UTF8.GetString(sbytes);
                var token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(json);
                return token.Id;
            }
        }
    }
}
