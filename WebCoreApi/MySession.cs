using Microsoft.AspNetCore.Http;
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
        private Token token;
        private HttpContext context;
        public MySession(IHttpContextAccessor accessor)
        {
            this.context = accessor.HttpContext;
            var base64Json = context.Request.Headers["token"];
            var sbytes = Convert.FromBase64String(base64Json);
            var json = Encoding.UTF8.GetString(sbytes);
            token = Newtonsoft.Json.JsonConvert.DeserializeObject<Token>(json);
        }
        public int UserId => token.Id;
    }
}
