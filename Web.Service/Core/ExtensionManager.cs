using Web.Service.Connection;
using Web.Service.Domain;
using Web.Service.IRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Web.Service
{
    public static class ExtensionManager
    {
        public static Task<TResult> Run<TResult>(this IEntity entity, Func<TResult> func)
        {
            return Task.Run(() =>
            {
                return func();
            });
        }

        /// <summary>
        /// 使用主数据链接
        /// </summary>
        /// <typeparam name="Repository"></typeparam>
        /// <param name="repository"></param>
        /// <returns></returns>
        public static Repository UseMasterConn<Repository>(this IMasterReadSeparate repository)
            where Repository : IMasterReadSeparate
        {
            repository.UseMaster();
            return (Repository)repository;
        }
    }
}
