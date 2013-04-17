using System;
using System.Data;
using System.Xml;
using System.Collections;
using System.Data.SQLite;
using System.Data.Common;
namespace HL.DAL
{
    /// <summary>
    /// SQLite数据库操作组件 
    /// </summary>
    public sealed class SQLiteDA : DataAccess
    {

        #region Constructor
        SQLiteCommand _cmd = null;

        /// <summary>
        /// 生成数据访问对象
        /// </summary>
        /// <param name="conn">数据库连接对象</param>
        public SQLiteDA(SQLiteConnection conn)
        {
            this.m_DbConnection = conn;
        }

        /// <summary>
        /// 生成数据访问对象
        /// </summary>
        /// <param name="connectionString">数据库连接字符串</param>
        public SQLiteDA(string connectionString)
            : this(new SQLiteConnection(connectionString))
        { }

        #endregion

        #region DataAccess

        #region Support Property & method

        /// <summary>
        /// 数据库属性
        /// </summary>
        public override DatabaseType DatabaseType
        {
            get { return DatabaseType.SQLite; }
        }

        #endregion Support Property & method

        #region ExecuteNonQuery

        /// <summary>
        /// 执行 SQL 语句，并返回受影响的行数
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>受影响的行数</returns>
        public override int ExecuteNonQuery(CommandType commandType, string commandText, QueryParameterCollection commandParameters)
        {
            SQLiteCommand cmd = PrepareCommand(commandType, commandText, commandParameters);
            int tmpValue = cmd.ExecuteNonQuery();
            SyncParameter(commandParameters);

            return tmpValue;
        }

        #endregion ExecuteNonQuery

        #region ExecuteDataSet

        /// <summary>
        /// 执行SQL语句，并且以DataSet的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataSet的形式返回的结果</returns>
        public override DataSet ExecuteDataset(CommandType commandType, string commandText, QueryParameterCollection commandParameters)
        {
            SQLiteCommand cmd = PrepareCommand(commandType, commandText, commandParameters);
            SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            SyncParameter(commandParameters);

            return ds;
        }
        #endregion ExecuteDataSet

        #region ExecuteDataTable,add by tmc,2010-5-10

        /// <summary>
        /// 执行SQL语句，并且以DataTable的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataTable的形式返回的结果</returns>
        public override DataTable ExecuteDataTable(CommandType commandType, string commandText, QueryParameterCollection commandParameters)
        {
            IDataReader dataReader = ExecuteReader(commandType, commandText, commandParameters);
            return DoGetDataTable(dataReader);
        }
        #endregion ExecuteDataTable

        #region ExecuteReader

        /// <summary>
        /// 执行SQL语句，并且以DataReader的形式返回结果
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>以DataReader的形式返回的结果</returns>
        public override IDataReader ExecuteReader(CommandType commandType, string commandText, QueryParameterCollection commandParameters)
        {
            SQLiteCommand cmd = PrepareCommand(commandType, commandText, commandParameters);
            SQLiteDataReader dr = cmd.ExecuteReader();
            
            SyncParameter(commandParameters);

            return dr;
        }

        #endregion ExecuteReader

        #region ExecuteScalar

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略额外的列或行。
        /// </summary>
        /// <param name="commandType">指示或指定如何解释 CommandText 属性。默认值为 Text。</param>
        /// <param name="commandText">准备要执行的 SQL 语句</param>
        /// <param name="commandParameters">相关的参数集合</param>
        /// <returns>查询所返回的结果集中第一行的第一列</returns>
        public override object ExecuteScalar(CommandType commandType, string commandText, QueryParameterCollection commandParameters)
        {
            SQLiteCommand cmd = PrepareCommand(commandType, commandText, commandParameters);
            object tmpValue = cmd.ExecuteScalar();
            SyncParameter(commandParameters);

            return tmpValue;
        }

        #endregion ExecuteScalar

        #endregion

        #region private
        /// <summary>
        /// 初始化命令对象
        /// </summary>
        /// <param name="commandType">命令类型</param>
        /// <param name="commandText">命令文本</param>
        /// <param name="commandParameters">命令参数集合</param>
        /// <returns></returns>
        private SQLiteCommand PrepareCommand(CommandType commandType, string commandText, QueryParameterCollection commandParameters)
        {
            this.Open();
            //SQLiteCommand cmd = new SQLiteCommand();
            if (_cmd == null)
            {
                _cmd = this.m_DbConnection.CreateCommand() as SQLiteCommand;
                //_cmd.Transaction = m_Transaction as SQLiteTransaction;
            }
            _cmd.CommandType = commandType;
            _cmd.CommandText = commandText;
            //_cmd.Connection = this.m_DbConnection as SQLiteConnection;
            //_cmd.Transaction = m_Transaction as SQLiteTransaction;
            if ((commandParameters != null) && (commandParameters.Count > 0))
            {
                for (int i = 0; i < commandParameters.Count; i++)
                {
                    if (!commandParameters[i].ParameterName.StartsWith("@")) commandParameters[i].ParameterName = "@" + commandParameters[i].ParameterName;
                    commandParameters[i].InitRealParameter(DatabaseType.SQLite);
                    _cmd.Parameters.Add(commandParameters[i].RealParameter as SQLiteParameter);
                }
            }

            return _cmd;
        }
        #endregion

        #region CreateDataAdapter
        public override DbDataAdapter CreateDataAdapter(string commandText)
        {
            this.Open();
            _cmd = this.m_DbConnection.CreateCommand() as SQLiteCommand;
            _cmd.CommandType =  CommandType.Text;
            _cmd.CommandText = commandText;
            SQLiteDataAdapter  newda = new SQLiteDataAdapter(_cmd);
            using (SQLiteCommandBuilder cb = new SQLiteCommandBuilder())
            {
                cb.DataAdapter = newda;              
                newda.InsertCommand = (SQLiteCommand)((ICloneable)cb.GetInsertCommand(true)).Clone();
                newda.DeleteCommand = (SQLiteCommand)((ICloneable)cb.GetDeleteCommand(true)).Clone();
                newda.UpdateCommand = (SQLiteCommand)((ICloneable)cb.GetUpdateCommand(true)).Clone();
            }
            return newda;          
        }
        #endregion     
    }
}
