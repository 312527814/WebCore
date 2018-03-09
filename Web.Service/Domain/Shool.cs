using Web.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service.Domain
{
    public class Shool : Entity
    {
        private IShoolRepository shoolRepository;
        public Shool() { }
        public Shool(IShoolRepository shoolRepository)
        {
            this.shoolRepository = shoolRepository;
        }
    }
}
