using System;
using System.Data;
using System.Xml;
using System.Collections;
using System.Globalization;
using System.Data.Common;

namespace HL.DAL
{
    /// <summary>
    /// 抽象类数据访问组件
    /// </summary>
    public abstract class DataAccess : IDataAccess
    {

        #region Support Property & method

        /// <summary>
        /// 数据库类型
        /// </summary>
        public abstract DatabaseType DatabaseType { get; }

        /// <summary>
        /// 数据库内部连接对象
        /// </summary>
        protected IDbConnection m_DbConnection;
        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection DbConnection
        {
            get
            {
                return m_DbConnection;
            }
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        public void Open()
        {
            if (this.IsClosed)
                this.DbConnection.Open();
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            if (!this.IsClosed)
                this.DbConnection.Close();
        }

        /// <summary>
        /// 指示数据库连接是否关闭了
        /// </summary>
        public bool IsClosed
        {
            get
            {
                return this.DbConnection.State.Equals(ConnectionState.Closed);
            }
        }

        /// <summary>
        /// 同步每一个列表中的内部参数
        /// </summary>
        /// <param name="commandParameters"></param>
        protected void SyncParameter(QueryParameterCollection commandParameters)
        {
            if ((commandParameters != null) && (commandParameters.Count > 0))
            {
                for (int i = 0; i < commandParameters.Count; i++)
                {
                    commandParameters[i].SyncParameter();
                }
            }
        }

        #endregion Support Property & method

        #region ExecuteNonQuery

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string commandText)
        {
            return this.ExecuteNonQuery(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            return this.ExecuteNonQuery(commandType, commandText, null);
        }

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string commandText, QueryParameterCollection commandParameters)
        {
            return this.ExecuteNonQuery(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>受影响的行数</returns>
        public abstract int ExecuteNonQuery(CommandType commandType, string commandText, QueryParameterCollection commandParameters);

        /// <summary>
        /// 通常用于执行数据库更新
        /// 已封装好事务处理，客户端把所有拼好的SQL文放入ArrayList传过来
        /// 但对于存储过程等不适用。
        /// </summary>
        /// <param name="arrSql">数据库命令字符串数组</param>
        public void ExecuteNonQuery(ArrayList arrSql)
        {
            this.Open();
            IDbTransaction trans = this.BeginTransaction();

            using (trans)
            {
                try
                {
                    for (int i = 0; i < arrSql.Count; i++)
                    {
                        ExecuteNonQuery(arrSql[i].ToString());
                    }

                    this.Commit();
                }
                catch (Exception ex)
                {
                    this.RollBack();
                    throw ex;
                }
            }
        }

        #endregion ExecuteNonQuery

        #region ExecuteDataSet

        /// <summary>
        /// 执行SQL语句，并且以DataSet的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataSet的形式返回的结果</returns>
        public DataSet ExecuteDataset(string commandText)
        {
            return ExecuteDataset(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// 执行SQL语句，并且以DataSet的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataSet的形式返回的结果</returns>
        public DataSet ExecuteDataset(CommandType commandType, string commandText)
        {
            return ExecuteDataset(commandType, commandText, null);
        }

        /// <summary>
        /// 执行SQL语句，并且以DataSet的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataSet的形式返回的结果</returns>
        public DataSet ExecuteDataset(string commandText, QueryParameterCollection commandParameters)
        {
            return ExecuteDataset(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 执行SQL语句，并且以DataSet的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataSet的形式返回的结果</returns>
        public abstract DataSet ExecuteDataset(CommandType commandType, string commandText, QueryParameterCollection commandParameters);

        #endregion ExecuteDataSet;

        #region ExecuteDataTable
        /// <summary>
        /// 将DataReader转化为DataTable
        /// </summary>
        /// <param name="dataReader">DataReader对象</param>
        /// <returns>以DataTable的形式返回的结果</returns>
        protected DataTable DoGetDataTable(IDataReader dataReader)
        {
            DataTable dataTable = new DataTable();
            try
            {                
                dataTable.Locale = CultureInfo.InvariantCulture;
                for (int i = 0; i < dataReader.FieldCount; i++)
                    dataTable.Columns.Add(dataReader.GetName(i), dataReader.GetFieldType(i));
                object[] objects = new object[dataReader.FieldCount];
                while (dataReader.Read())
                { 
                    dataReader.GetValues(objects);
                    dataTable.Rows.Add(objects);
                }
                dataReader.Close();                
            }
            catch (Exception ex)
            {
                dataReader.Close();
            }                       
            return dataTable;
        }

        /// <summary>
        /// 执行SQL语句，并且以DataTable的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataTable的形式返回的结果</returns>
        public DataTable ExecuteDataTable(string commandText)
        {
            return ExecuteDataTable(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// 执行SQL语句，并且以DataTable的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataTable的形式返回的结果</returns>
        public DataTable ExecuteDataTable(CommandType commandType, string commandText)
        {
            return ExecuteDataTable(commandType, commandText, null);
        }

        /// <summary>
        /// 执行SQL语句，并且以DataTable的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataTable的形式返回的结果</returns>
        public DataTable ExecuteDataTable(string commandText, QueryParameterCollection commandParameters)
        {
            return ExecuteDataTable(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 执行SQL语句，并且以DataTable的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataTable的形式返回的结果</returns>
        public abstract DataTable ExecuteDataTable(CommandType commandType, string commandText, QueryParameterCollection commandParameters);

        #endregion ExecuteDataTable;

        #region ExecuteReader

        /// <summary>
        /// 执行SQL语句，并且以DataReader的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataReader的形式返回的结果</returns>
        public IDataReader ExecuteReader(string commandText)
        {
            return ExecuteReader(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// 执行SQL语句，并且以DataReader的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataReader的形式返回的结果</returns>
        public IDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            return ExecuteReader(commandType, commandText, null);
        }

        /// <summary>
        /// 执行SQL语句，并且以DataReader的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataReader的形式返回的结果</returns>
        public IDataReader ExecuteReader(string commandText, QueryParameterCollection commandParameters)
        {
            return ExecuteReader(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 执行SQL语句，并且以DataReader的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataReader的形式返回的结果</returns>
        public abstract IDataReader ExecuteReader(CommandType commandType, string commandText, QueryParameterCollection commandParameters);

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>查询所返回的结果集中第一行的第一列</returns>
        public object ExecuteScalar(string commandText)
        {
            return ExecuteScalar(CommandType.Text, commandText, null);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>查询所返回的结果集中第一行的第一列</returns>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            return ExecuteScalar(commandType, commandText, null);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>查询所返回的结果集中第一行的第一列</returns>
        public object ExecuteScalar(string commandText, QueryParameterCollection commandParameters)
        {
            return ExecuteScalar(CommandType.Text, commandText, commandParameters);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>查询所返回的结果集中第一行的第一列</returns>
        public abstract object ExecuteScalar(CommandType commandType, string commandText, QueryParameterCollection commandParameters);

        #endregion ExecuteScalar

        #region Transcation

        /// <summary>
        /// 事务对象
        /// </summary>
        protected IDbTransaction m_Transaction = null;

        /// <summary>
        /// 事条对象
        /// </summary>
        public IDbTransaction Transaction
        {
            get
            {
                return this.m_Transaction;
            }
        }

        /// <summary>
        /// 开始一个事务处理
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            if (this.IsClosed) this.Open();
            m_Transaction = m_DbConnection.BeginTransaction();
            return m_Transaction;
        }

        /// <summary>
        /// 提交当前的事务
        /// </summary>
        public void Commit()
        {
            if (this.m_Transaction != null) this.m_Transaction.Commit();
        }

        /// <summary>
        /// 回滚当前事务
        /// </summary>
        public void RollBack()
        {
            if (this.m_Transaction != null) this.m_Transaction.Rollback();
        }

        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            this.Close();
        }
        #endregion

        #region CreateDataAdapter
        public abstract DbDataAdapter CreateDataAdapter(string commandText);
        #endregion

    }
}
