using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Service.Core;

namespace WebCoreMvc
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
                var id = context.User.Claims.First(c => c.Type == "Id").Value;
                return Convert.ToInt32(id);
            }
        }
    }
}
