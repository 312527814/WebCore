using Web.Service.Domain;
using Web.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service.DataRepository
{
    public class ShoolRepository : Repository<Shool>, IShoolRepository
    {
        public ShoolRepository(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }
        public void list()
        {

        }
    }
}
