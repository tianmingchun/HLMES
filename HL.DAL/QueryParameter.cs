using System;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;

namespace HL.DAL
{
    /// <summary>
    /// 数据参数,通过数据Command对象传递 
    /// </summary>
    public sealed class QueryParameter
    {
        #region 内部字段及共开属性

        private string m_name;
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParameterName
        {
            get
            {
                return this.m_name;
            }
            set
            {
                this.m_name = value;
            }
        }

        private object m_value;
        /// <summary>
        /// 参数值
        /// </summary>
        public object Value
        {
            get
            {
                return (this.m_value == null) ? DBNull.Value : this.m_value;
            }
            set
            {
                //空字符串或者空值时,都设为空数据
                this.m_value = (((value.GetType().Name.Trim() == "String") && (value.ToString() == string.Empty)) || value == null) ? System.DBNull.Value : value;
            }
        }

        private DbType m_dbType;
        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DbType
        {
            get
            {
                return this.m_dbType;
            }
            set
            {
                this.m_dbType = value;
            }
        }

        private int m_size;
        /// <summary>
        /// 参数数据大小
        /// </summary>
        public int Size
        {
            get
            {
                return this.m_size;
            }
            set
            {
                this.m_size = value;
            }
        }

        private ParameterDirection m_direction;
        /// <summary>
        /// 参数方向
        /// </summary>
        public ParameterDirection Direction
        {
            get
            {
                return this.m_direction;
            }
            set
            {
                this.m_direction = value;
            }
        }

        private IDbDataParameter m_realParameter;
        /// <summary>
        /// 参数实体(区分不同类型)
        /// </summary>
        public IDbDataParameter RealParameter
        {
            get
            {
                return this.m_realParameter;
            }
        }

        #endregion

        #region 构造

        /// <summary>
        /// 适用于inputoutput参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="Value"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        public QueryParameter(string parameterName, object value, DbType dbType, int size, ParameterDirection direction)
        {
            this.ParameterName = parameterName;
            this.DbType = dbType;
            this.Value = value;
            this.Direction = direction;
            this.Size = size;
        }


        /// <summary>
        /// 适用于Input参数
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="Value"></param>
        /// <param name="dbType"></param>
        public QueryParameter(string parameterName, object value, DbType dbType)
            : this(parameterName, value, dbType, 0, ParameterDirection.Input)
        { }

        /// <summary>
        /// 适用于output参数,returnvalue参数,且输出参数类型为字符串
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        public QueryParameter(string parameterName, DbType dbType, int size, ParameterDirection direction)
            : this(parameterName, DBNull.Value, dbType, size, direction)
        { }

        /// <summary>
        ///  适用于output参数,returnvalue参数,且输出参数类型为非字符串
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        public QueryParameter(string parameterName, DbType dbType, ParameterDirection direction)
            : this(parameterName, DBNull.Value, dbType, 0, direction)
        { }

        #endregion

        /// <summary>
        /// 初始化参数对象(匹配实际数据库参数类型)
        /// </summary>
        /// <param name="databaseType"></param>
        public void InitRealParameter(DatabaseType databaseType)
        {
            if (Object.Equals(m_realParameter, null))
            {
                switch (databaseType)
                {
                    case DatabaseType.SQLite:
                        m_realParameter = new SQLiteParameter();
                        break;
                }
            }
            m_realParameter.DbType = m_dbType;
            m_realParameter.Direction = m_direction;
            m_realParameter.ParameterName = m_name;
            m_realParameter.Size = m_size;
            m_realParameter.Value = m_value;
        }

        /// <summary>
        /// 同步内部字段与实体参数数据
        /// </summary>
        internal void SyncParameter()
        {
            if (Object.Equals(m_realParameter, null)) return;
            this.ParameterName = this.m_realParameter.ParameterName;
            this.DbType = this.m_realParameter.DbType;
            this.Value = this.m_realParameter.Value;
            this.Direction = this.m_realParameter.Direction;
            this.Size = this.m_realParameter.Size;
        }

        public QueryParameter Clone()
        {
            return new QueryParameter(m_name, m_value, m_dbType, m_size, m_direction);
        }

    }
}
