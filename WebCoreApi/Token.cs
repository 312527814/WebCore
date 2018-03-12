using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Service;
using Web.Service.Core;

namespace WebCoreApi
{
    public class Token
    {
        public int Id { get; set; }

        public string Roles { get; set; }

        public DateTime NotBefore { get; set; }

        public DateTime expires { get; set; }

        public string ETag { get; set; }

        public string GetEag()
        {
            var t = this.Id + this.Roles + NotBefore.ToString() + expires.ToString();
            var s = SecurityHelper.Sign(t);
            return s;
        }

    }

}
