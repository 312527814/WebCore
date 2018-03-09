using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Web.Service.Domain
{
    public abstract class Entity : IEntity<int>
    {
        public virtual int Id { get; set; }

        
    }
}
