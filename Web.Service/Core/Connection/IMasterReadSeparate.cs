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
        /// <summary>
        /// 使用主数据库
        /// </summary>
        void UseMaster();
        /// <summary>
        /// 使用从数据库
        /// </summary>
        void UseRead();
    }
}
