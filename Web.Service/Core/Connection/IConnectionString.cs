using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service.Connection
{
    public interface IConnectionString
    {
        string MasterConnstr { get; set; }
        string SlaveConnstr { get; set; }
    }
}
