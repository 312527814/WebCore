using Dapper;
using Web.Service.Connection;
using Web.Service.IRepository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Dapper.SqlMapper;

namespace Web.Service.DataRepository
{
    public class DapperHelper : IConnectionString
    {
        public string MasterConnstr { get; set; }

        public string SlaveConnstr { get; set; }

        public DapperHelper(IConnectionManager connManager)
        {
            this.MasterConnstr = connManager.MasterConnstr;
            this.SlaveConnstr = connManager.SlaveConnstr;
        }

        public TModel FirstOrDefault<TModel>(string sql, object parament = null)
        {
            using (var conn = new MySqlConnection(SlaveConnstr))
            {
                return conn.QueryFirstOrDefault<TModel>(sql, parament);
            }
        }
        public IEnumerable<TModel> Query<TModel>(string sql, object parament = null)
        {
            using (var conn = new MySqlConnection(SlaveConnstr))
            {
                return conn.Query<TModel>(sql, parament);
            }
        }

        public void QueryMultiple(string sql, object parament, Action<GridReader> readResult)
        {
            using (var conn = new MySqlConnection(SlaveConnstr))
            {
                var read = conn.QueryMultiple(sql, parament);
                readResult(read);
            }
        }

        public T Scalar<T>(string sql, object parament = null)
        {
            using (var conn = new MySqlConnection(SlaveConnstr))
            {
                return conn.ExecuteScalar<T>(sql, parament);
            }
        }

        public PageEntity<TModel> Pagination<TModel>(string sql, int currentIndex, int pageSize, string fields = "*", string orderBy = "Id", object parament = null, bool isExactCount = true)
        {
            var result = new PageEntity<TModel>();
            var excuteSql = $"select {fields} from ( {sql} )as pag order by {orderBy} limit {pageSize * (currentIndex - 1)},{pageSize}";
            if (isExactCount)
            {
                excuteSql += $";select count(*) from {sql} as C";
            }
            else
            {
                excuteSql += $";explain {sql}";
            }
            using (var conn = new MySqlConnection(SlaveConnstr))
            {
                var read = conn.QueryMultiple(sql, parament);
                result.List = read.Read<TModel>();
                if (isExactCount)
                {
                    result.Count = read.Read<int>().Single();
                }
                else
                {
                    result.Count = Convert.ToInt32(read.Read().First().rows);
                }
            }
            return result;
        }

        public int Execute(string sql, object parament = null)
        {
            using (var conn = new MySqlConnection(SlaveConnstr))
            {
                return conn.Execute(sql, parament);
            }
        }
        public int ExecProc(string procName, object parament = null)
        {
            using (var conn = new MySqlConnection(SlaveConnstr))
            {
                return conn.Execute(procName, parament, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public int ExecProc(string procName, object parament, int outTimes)
        {
            using (var conn = new MySqlConnection(SlaveConnstr))
            {
                return conn.Execute(procName, parament, commandTimeout: outTimes, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public IEnumerable<TModel> ExecProc<TModel>(string procName, object parament = null)
        {
            using (var conn = new MySqlConnection(SlaveConnstr))
            {
                return conn.Query<TModel>(procName, parament, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public IEnumerable<TModel> ExecProc<TModel>(string procName, object parament, int outTimes)
        {
            using (var conn = new MySqlConnection(SlaveConnstr))
            {
                return conn.Query<TModel>(procName, parament, commandTimeout: outTimes, commandType: System.Data.CommandType.StoredProcedure);
            }
        }



    }
}
