using System;
using System.Collections.Generic;
using System.Text;
using Web.Service.DataRepository;

namespace Web.Service.DataRepository
{
    public interface IDapper
    {
        DapperHelper dapperHelper { get; set; }
    }
}
