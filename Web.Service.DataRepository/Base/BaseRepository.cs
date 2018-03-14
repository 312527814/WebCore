using Web.Service.Domain;
using Web.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using MySql.Data.MySqlClient;
using Dapper;
using Web.Service;
using Web.Service.Connection;
using System.Linq;

namespace Web.Service.DataRepository
{
    public abstract class BaseRepository<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>, IDapper where TEntity : IEntity<TPrimaryKey>
    {
        public DapperHelper dapperHelper { get; set; }
        protected string tableName;
        public Action<IConnectionString> Invoke { get; set; }

        public BaseRepository(DapperHelper dapperHelper)
        {
            this.dapperHelper = dapperHelper;
            tableName = typeof(TEntity).Name;
        }

        //public void UseMaster()
        //{
        //    //dapperHelper.Connstr = dapperHelper.connManager.MasterConnstr;
        //}
        //public void UseRead()
        //{
        //    //dapperHelper.Connstr = dapperHelper.connManager.ReadonlyConnstr;
        //}



        //public void Invoker(Action<IConnectionString> action)
        //{
        //    action(dapperHelper);
        //}
        #region select


        public TEntity Get(TPrimaryKey id)
        {
            var list = CommonHelper.GetProperties(default(TEntity));
            var fields = string.Join(",", list);
            var sql = $"select {fields} from {tableName} where Id=@Id";
            return dapperHelper.FirstOrDefault<TEntity>(sql, new { Id = id });
        }



        #endregion


        #region delete
        public void Delete(TPrimaryKey id)
        {
            var sql = $"delete from {tableName} where Id = @Id";
            dapperHelper.Execute(sql, new { Id = id });
        }

        public void Delete(IEnumerable<TPrimaryKey> ids)
        {
            var sql = $"delete from {tableName} where Id in @Ids";
            dapperHelper.Execute(sql, new { Ids = ids });
        }


        #endregion

        #region insert

        public virtual TEntity Insert(TEntity entity)
        {
            var list = CommonHelper.GetProperties(entity);
            var parament = CommonHelper.GetParament(entity);
            var sql = $"INSERT INTO [{tableName}] ({string.Join(",", list)}) VALUES (@{string.Join(",@", list)})";
            dapperHelper.Execute(sql, parament);
            return entity;
        }
        public virtual IEnumerable<TEntity> InsertList(IEnumerable<TEntity> list)
        {
            System.Threading.Tasks.Parallel.ForEach(list, s =>
            {
                Insert(s);
            });
            return list;
        }

        #endregion

        #region update

        public TEntity Update(TEntity entity)
        {
            var list = CommonHelper.GetProperties(entity);
            var parament = CommonHelper.GetParament(entity);
            var agg = list.Aggregate(new StringBuilder(), (x, y) =>
            {
                x.Append(y);
                x.Append("=@");
                x.Append(y);
                x.Append(",");
                return x;
            });
            var sql = $"UPDATE [{tableName}] SET {agg.ToString().TrimEnd(',')} WHERE Id = @Id";
            dapperHelper.Execute(sql, parament);
            return entity;
        }

        public IEnumerable<TEntity> UpdateList(IEnumerable<TEntity> list)
        {
            System.Threading.Tasks.Parallel.ForEach(list, s =>
            {
                Update(s);
            });
            return list;
        }






        #endregion


    }
}
