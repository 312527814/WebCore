using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Service.Core;

namespace WebCoreApi
{
    public class MySession : IMySession
    {
        private Token token;
        public MySession(Token token)
        {
            this.token = token;
        }
        public int UserId => token.Id;
    }
}
