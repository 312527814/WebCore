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
    public class DapperHelper
    {


        public IConnectionManager connManager;
        public string Connstr { get; set; }

        public DapperHelper(IConnectionManager connManager)
        {
            this.connManager = connManager;
        }

        public void UseMainConnstr()
        {
            Connstr = connManager.MainConnstr;
        }
        public void UseReadConnstr()
        {
            Connstr = connManager.ReadonlyConnstr;
        }

        public TModel FirstOrDefault<TModel>(string sql, object parament = null)
        {
            var connstr = string.IsNullOrEmpty(Connstr) ? connManager.ReadonlyConnstr : Connstr;
            try
            {
                using (var conn = new MySqlConnection(connstr))
                {
                    return conn.QueryFirstOrDefault<TModel>(sql, parament);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (!string.IsNullOrEmpty(connstr))
                    Connstr = null;
            }
        }
        public IEnumerable<TModel> Query<TModel>(string sql, object parament = null)
        {

            var connstr = string.IsNullOrEmpty(Connstr) ? connManager.ReadonlyConnstr : Connstr;
            try
            {
                using (var conn = new MySqlConnection(connstr))
                {
                    return conn.Query<TModel>(sql, parament);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (!string.IsNullOrEmpty(connstr))
                    Connstr = null;
            }

        }

        public void QueryMultiple(string sql, object parament, Action<GridReader> readResult)
        {

            var connstr = string.IsNullOrEmpty(Connstr) ? connManager.ReadonlyConnstr : Connstr;
            try
            {
                using (var conn = new MySqlConnection(connstr))
                {
                    var read = conn.QueryMultiple(sql, parament);
                    readResult(read);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (!string.IsNullOrEmpty(connstr))
                    Connstr = null;
            }

        }

        public T Scalar<T>(string sql, object parament = null)
        {
            var connstr = string.IsNullOrEmpty(Connstr) ? connManager.ReadonlyConnstr : Connstr;
            try
            {
                using (var conn = new MySqlConnection(connstr))
                {
                    return conn.ExecuteScalar<T>(sql, parament);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (!string.IsNullOrEmpty(connstr))
                    Connstr = null;
            }
        }
        
        public PageEntity<TModel> Pagination<TModel>(string sql, int currentIndex, int pageSize, string fields = "*", string orderBy = "Id", object parament = null, bool isExactCount = true)
        {
            var result = new PageEntity<TModel>();
            var excuteSql = $"select {fields} from ( {sql} )as pag order by {orderBy} limit {pageSize * (currentIndex - 1)},{pageSize}";
            if (isExactCount)
            {
                excuteSql += ";select count(*) from {sql} as C";
            }
            else
            {
                excuteSql += $";explain {sql}";
            }
            var connstr = string.IsNullOrEmpty(Connstr) ? connManager.ReadonlyConnstr : Connstr;
            try
            {
                using (var conn = new MySqlConnection(connstr))
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
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (!string.IsNullOrEmpty(connstr))
                    Connstr = null;
            }
            
            return result;
        }

        public int Execute(string sql, object parament = null)
        {
            using (var conn = new MySqlConnection(connManager.MainConnstr))
            {
                return conn.Execute(sql, parament);
            }
        }
        public int ExecProc(string procName, object parament = null)
        {
            using (var conn = new MySqlConnection(connManager.MainConnstr))
            {
                return conn.Execute(procName, parament, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public int ExecProc(string procName, object parament, int outTimes)
        {
            using (var conn = new MySqlConnection(connManager.MainConnstr))
            {
                return conn.Execute(procName, parament, commandTimeout: outTimes, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public IEnumerable<TModel> ExecProc<TModel>(string procName, object parament = null)
        {
            var connstr = string.IsNullOrEmpty(Connstr) ? connManager.ReadonlyConnstr : Connstr;
            try
            {
                using (var conn = new MySqlConnection(connstr))
                {
                    return conn.Query<TModel>(procName, parament, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (!string.IsNullOrEmpty(connstr))
                    Connstr = null;
            }
        }

        public IEnumerable<TModel> ExecProc<TModel>(string procName, object parament, int outTimes)
        {
            var connstr = string.IsNullOrEmpty(Connstr) ? connManager.ReadonlyConnstr : Connstr;
            try
            {
                using (var conn = new MySqlConnection(connstr))
                {
                    return conn.Query<TModel>(procName, parament, commandTimeout: outTimes, commandType: System.Data.CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                if (!string.IsNullOrEmpty(connstr))
                    Connstr = null;
            }
        }



    }
}
