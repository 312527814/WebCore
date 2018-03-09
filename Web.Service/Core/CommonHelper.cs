using System;
using System.Collections.Generic;
using System.Text;

namespace Web.Service
{
    public class CommonHelper
    {
        /// <summary>
        /// 根据对象获取DynamicParament
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetParament<TEntity>(TEntity entity)
        {
            var parament = new Dictionary<string, object>();
            foreach (var node in typeof(TEntity).GetProperties())
            {
                //if (node.GetValue(entity) == null) continue;
                parament.Add("@" + node.Name, node.GetValue(entity));
            }
            return parament;
        }

        /// <summary>
        /// 根据对象获取不为null的Properties
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static List<string> GetProperties<TEntity>(TEntity entity)
        {
            var list = new List<string>();
            foreach (var node in typeof(TEntity).GetProperties())
            {
                //if (node.GetValue(entity) == null) continue;
                list.Add(node.Name);
            }
            return list;
        }

    }
}
