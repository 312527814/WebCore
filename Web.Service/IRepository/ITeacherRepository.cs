using Web.Service.Domain;
using Web.Service.Interceptor;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service.IRepository
{
    //[MyInterceptor]
    public interface ITeacherRepository : IRepository<Teacher, int>
    {

        IList<Teacher> list(string name);

    }
}
