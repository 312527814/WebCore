using Web.Service.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service.IRepository
{
    public interface IShoolRepository : IRepository<Shool, int>
    {
        string Test();
        void list();
    }
}
