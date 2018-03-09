using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service.Connection
{
    public interface IConnectionManager
    {
        string MainConnstr { get; set; }
        string ReadonlyConnstr { get; set; }

    }
}
