using Dapper;
using Web.Service;
using Web.Service.Connection;
using Web.Service.Domain;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service.DataRepository
{
    public abstract class Repository<TEntity> : BaseRepository<TEntity, int> where TEntity : Entity
    {
        public Repository(DapperHelper dapperHelper) : base(dapperHelper)
        {
        }
        public override TEntity Insert(TEntity entity)
        {
            var list = CommonHelper.GetProperties(entity);
            var parament = CommonHelper.GetParament(entity);
            var sql = $"INSERT INTO [{tableName}] ({string.Join(",", list)}) VALUES (@{string.Join(",@", list)})";
            var id = dapperHelper.Scalar<int>(sql, parament);
            entity.Id = id;
            return entity;
        }

        public override IEnumerable<TEntity> InsertList(IEnumerable<TEntity> list)
        {
            System.Threading.Tasks.Parallel.ForEach(list, s =>
            {
                Insert(s);
            });
            return list;
        }

    }
}
