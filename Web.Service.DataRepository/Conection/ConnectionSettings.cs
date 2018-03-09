using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service.DataRepository
{
    public class ConnectionSettings
    {
        public ConnectionStr[] Master { get; set; }

        public ConnectionStr[] ReadOnly { get; set; }

    }
    public class ConnectionStr
    {

        public string DbContext { get; set; }

        public int Weight { get; set; }
    }
}
