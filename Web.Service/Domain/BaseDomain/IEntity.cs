using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service.Domain
{
    public interface IEntity
    {
    }

    //
    // 摘要:
    //     Defines interface for base entity type. All entities in the system must implement
    //     this interface.
    //
    // 类型参数:
    //   TPrimaryKey:
    //     Type of the primary key of the entity
    public interface IEntity<TPrimaryKey> : IEntity
    {
        //
        // 摘要:
        //     Unique identifier for this entity.
        TPrimaryKey Id { get; set; }

    }
}
