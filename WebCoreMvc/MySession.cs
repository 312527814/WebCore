using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Service.Core;

namespace WebCoreMvc
{
    public class MySession : IMySession
    {
        public MySession()
        {
        }
        public int UserId => 1;
    }
}
