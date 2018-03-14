using System;
using System.Collections.Generic;
using System.Text;
using Web.Service.Domain;
using Web.Service.IRepository;
using MySql.Data.MySqlClient;
using Dapper;
using System.Data.Common;
using System.Linq;
using System.Dynamic;
using Web.Service.Connection;
using Web.Service.DataRepository.Interceptor;

namespace Web.Service.DataRepository
{
    [MasterSlaveInterceptorAttribute]
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }
        public IList<Teacher> list(string name)
        {
            var sql = "select * from Shool where Name like '@Name'";
            var query = dapperHelper.Query<Teacher>(sql, new { Name = name });
            return query.ToList();
        }

    }

}
