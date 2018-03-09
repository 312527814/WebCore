using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service.IRepository
{
    public class PageEntity<T>
    {
        public IEnumerable<T> List { get; set; }

        public int Count { get; set; }


    }
}
