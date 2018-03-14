using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service.Connection
{
    /// <summary>
    /// 主从分离接口
    /// </summary>
    public interface IMasterReadSeparate
    {

        Action<IConnectionString> Invoke { get; set; }
    }


}
