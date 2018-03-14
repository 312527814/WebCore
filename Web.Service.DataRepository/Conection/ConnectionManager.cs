using Microsoft.Extensions.Options;
using Web.Service;
using Web.Service.Connection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service.DataRepository
{
    public class ConnectionManager : IConnectionManager
    {
        private ConnectionSettings connectionSettings;
        public ConnectionManager(IOptions<ConnectionSettings> con)
        {
            this.connectionSettings = con.Value;
        }

        public string MasterConnstr
        {
            get { return GetMainConnstr(); }
            set { }
        }
        public string SlaveConnstr
        {
            get { return GetReadonlyConnstr(); }
            set { }
        }

        private string GetMainConnstr()
        {
            return connectionSettings.Master[0].DbContext;
        }

        private string GetReadonlyConnstr()
        {
            return connectionSettings.ReadOnly[0].DbContext;
        }

    }
}
