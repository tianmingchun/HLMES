using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Common;

namespace HL.DAL
{
    /// <summary>
    /// DataAccess接口定义，提供数据库访问功能的基本接口。 
    /// </summary>
    public interface IDataAccess : IDisposable
    {
        #region Support Property & Method
        /// <summary>
        /// 数据库类型
        /// </summary>
        DatabaseType DatabaseType { get; }
        /// <summary>
        /// 数据库连接
        /// </summary>
        IDbConnection DbConnection { get; }
        /// <summary>
        /// 打开连接
        /// </summary>
        void Open();
        /// <summary>
        /// 关闭连接
        /// </summary>
        void Close();
        /// <summary>
        /// 指示数据库连接是否关闭了
        /// </summary>
        bool IsClosed { get; }

        #endregion

        #region ExecuteNonQuery
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText);
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(string commandText);
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(string commandText, QueryParameterCollection commandParameters);
        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(CommandType commandType, string commandText, QueryParameterCollection commandParameters);
        /// <summary>
        /// 在一个事务中执行多条sql语句
        /// </summary>
        /// <param name="arrSql">sql语句集合</param>
        void ExecuteNonQuery(ArrayList arrSql);

        #endregion ExecuteNonQuery

        #region ExecuteDataSet
        /// <summary>
        /// 执行SQL语句，并且以DataSet的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataSet的形式返回的结果</returns>
        DataSet ExecuteDataset(string commandText);
        /// <summary>
        /// 执行SQL语句，并且以DataSet的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataSet的形式返回的结果</returns>
        DataSet ExecuteDataset(string commandText, QueryParameterCollection commandParameters);
        /// <summary>
        /// 执行SQL语句，并且以DataSet的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataSet的形式返回的结果</returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText);
        /// <summary>
        /// 执行SQL语句，并且以DataSet的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataSet的形式返回的结果</returns>
        DataSet ExecuteDataset(CommandType commandType, string commandText, QueryParameterCollection commandParameters);

        #endregion ExecuteDataSet

        #region ExecuteDataTable
        /// <summary>
        /// 执行SQL语句，并且以DataTable的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataTable的形式返回的结果</returns>
        DataTable ExecuteDataTable(string commandText);
        /// <summary>
        /// 执行SQL语句，并且以DataTable的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataTable的形式返回的结果</returns>
        DataTable ExecuteDataTable(string commandText, QueryParameterCollection commandParameters);
        /// <summary>
        /// 执行SQL语句，并且以DataTable的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataTable的形式返回的结果</returns>
        DataTable ExecuteDataTable(CommandType commandType, string commandText);
        /// <summary>
        /// 执行SQL语句，并且以DataTable的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataTable的形式返回的结果</returns>
        DataTable ExecuteDataTable(CommandType commandType, string commandText, QueryParameterCollection commandParameters);

        #endregion ExecuteDataSet

        #region ExecuteReader

        /// <summary>
        /// 执行SQL语句，并且以DataReader的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataReader的形式返回的结果</returns>
        IDataReader ExecuteReader(string commandText);
        /// <summary>
        /// 执行SQL语句，并且以DataReader的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>以DataReader的形式返回的结果</returns>
        IDataReader ExecuteReader(CommandType commandType, string commandText);
        /// <summary>
        /// 执行SQL语句，并且以DataReader的形式返回结果
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataReader的形式返回的结果</returns>
        IDataReader ExecuteReader(string commandText, QueryParameterCollection commandParameters);
        /// <summary>
        /// 执行SQL语句，并且以DataReader的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataReader的形式返回的结果</returns>
        IDataReader ExecuteReader(CommandType commandType, string commandText, QueryParameterCollection commandParameters);

        #endregion ExecuteReader

        #region ExecuteScalar
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>查询所返回的结果集中第一行的第一列</returns>
        object ExecuteScalar(string commandText);
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <returns>查询所返回的结果集中第一行的第一列</returns>
        object ExecuteScalar(CommandType commandType, string commandText);
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>查询所返回的结果集中第一行的第一列</returns>
        object ExecuteScalar(string commandText, QueryParameterCollection commandParameters);
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>查询所返回的结果集中第一行的第一列</returns>
        object ExecuteScalar(CommandType commandType, string commandText, QueryParameterCollection commandParameters);

        #endregion ExecuteScalar

        #region Transaction

        IDbTransaction Transaction { get; }

        /// <summary>
        /// 数据库事务
        /// </summary>
        /// <returns></returns>
        IDbTransaction BeginTransaction();

        /// <summary>
        /// 提交当前的事务
        /// </summary>
        void Commit();

        /// <summary>
        /// 回滚当前事务
        /// </summary>
        void RollBack();

        #endregion

        #region CreateDataAdapter
        DbDataAdapter CreateDataAdapter(string commandText);    
        #endregion
    }

}
