using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace HL.DAL
{
    /// <summary>
    /// 参数集合,用于传递数据库操作参数
    /// </summary>
    public sealed class QueryParameterCollection
    {
        private List<QueryParameter> items;
        public QueryParameterCollection()
        {
            items = new List<QueryParameter>();
        }

        /// <summary>
        /// 添加一个参数
        /// </summary>
        /// <param name="param"></param>
        public void Add(QueryParameter param)
        {
            items.Add(param);
        }
        /// <summary>
        /// 适用于Input参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="Value"></param>
        /// <param name="dbType"></param>
        public void Add(string ParameterName, object Value, DbType dbType)
        {
            Add(new QueryParameter(ParameterName, Value, dbType));
        }
        /// <summary>
        /// 适用于output参数,returnvalue参数,且输出参数类型为字符串
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        public void Add(string ParameterName, DbType dbType, int size, ParameterDirection direction)
        {
            Add(new QueryParameter(ParameterName, dbType, size, direction));
        }
        /// <summary>
        /// 适用于output参数,returnvalue参数,且输出参数类型不为字符串
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        public void Add(string ParameterName, DbType dbType, ParameterDirection direction)
        {
            Add(new QueryParameter(ParameterName, dbType, direction));
        }

        /// <summary>
        /// 适用于inputoutput参数
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <param name="Value"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        public void Add(string ParameterName, object Value, DbType dbType, int size, ParameterDirection direction)
        {
            Add(new QueryParameter(ParameterName, Value, dbType, size, direction));
        }

        /// <summary>
        /// 参数数字索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public QueryParameter this[int index]
        {
            get
            {
                return this.items[index];
            }
        }

        /// <summary>
        /// 参数名称索引
        /// </summary>
        /// <param name="ParameterName"></param>
        /// <returns></returns>
        public QueryParameter this[string ParameterName]
        {
            get
            {
                int item = -1;
                for (int i = 0; i < this.Count; i++)
                {
                    if (this[i].ParameterName.Trim() == ParameterName)
                    {
                        item = i;
                        break;
                    }
                }

                if (item < 0) throw new Exception("this item not exist!");

                return this[item];
            }

        }

        /// <summary>
        /// 参数个数
        /// </summary>
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        /// <summary>
        /// 为了解决一个参数不能被多个命令对象同时引用的问题
        /// 使用克隆快速复制一个对象出来
        /// </summary>
        /// <returns></returns>
        public QueryParameterCollection Clone()
        {
            QueryParameterCollection objColl = new QueryParameterCollection();
            objColl.items = new List<QueryParameter>(this.items.Count);
            for (int i = 0; i < this.items.Count; i++)
            {
                objColl.items.Add(this.items[i].Clone());
            }
            return objColl;
        }

    }
}
