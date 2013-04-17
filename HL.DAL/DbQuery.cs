using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Globalization;

namespace HL.DAL
{   
    public sealed class DbQuery : IDisposable
    {
        private bool _disposed;
        /// <summary>
        /// 数据集
        /// </summary>
        private DataSet _dataSet = new DataSet();
        /// <summary>
        /// 是否有数据载入
        /// </summary>
        private bool _loaded;
        private DataAccess _dataAccess;
        /// <summary>
        /// 当前使用的DbDataAdapter
        /// </summary>
        private Dictionary<string, DbDataAdapter> _dataAdapters = new Dictionary<string, DbDataAdapter>();

        public DataAccess DataAccess
        {
            get
            {
                return _dataAccess;
            }
        }

        public DataSet DataSet
        {
            get
            {
                return _dataSet;
            }
            set
            {
                _dataSet = value;
            }
        }

        public DbQuery(DataAccess dataAccess)
        {
            this._dataAccess = dataAccess;
        }

        /// <summary>
        /// 释放资源。
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (!this._disposed)
                {
                    this._dataSet.Dispose();
                    this.DisposeDataAdapters();
                }
                this._disposed = true;
            }
            finally
            {
               
            }
        }

        /// <summary>
        /// 释放所有DbDataAdapter对象
        /// </summary>
        private void DisposeDataAdapters()
        {
            foreach (KeyValuePair<string, DbDataAdapter> kvp in this._dataAdapters)
                kvp.Value.Dispose();
            this._dataAdapters.Clear();
        }

        /// <summary>
        /// 填充数据
        /// </summary>
        /// <param name="dataAdapter">DbDataAdapter对象</param>
        /// <param name="srcTable">用于表映射的源表的名称</param>
        /// <param name="selectCommandText">查询语句</param>
        private DataTable Fill(DbDataAdapter dataAdapter, string srcTable,  string selectCommandText)
        {
            DataTable dataTable;
            if (this._dataSet.Tables.Contains(srcTable))
            {
                dataTable = this._dataSet.Tables[srcTable];
                dataTable.Clear();
            }
            else
            {
                dataTable = new DataTable();
                dataTable.Locale = CultureInfo.InvariantCulture;
                //耗时较长，暂取消。需要获得Schema时，请单独调用FillSchema方法。
                //dataAdapter.FillSchema(dataTable, SchemaType.Source);
                this._dataSet.Tables.Add(dataTable);
            }       
            dataAdapter.Fill(this._dataSet, srcTable);
            if (!this._dataAdapters.ContainsKey(srcTable))
            {
                this._dataAdapters.Add(srcTable, dataAdapter);
            }
            else
            {
                this._dataAdapters[srcTable].Dispose();
                this._dataAdapters[srcTable] = dataAdapter;
            }
            this._loaded = true;
            return this._dataSet.Tables[srcTable];
        }


        public void FillSchema(string destTable, DataTable srcTable)
        {
            if (this._dataSet.Tables[destTable] != null)
            {
                DataTable dt = this._dataSet.Tables[destTable];
                DataColumn[] pkColumns = srcTable.PrimaryKey;
                if (pkColumns.Length > 0)
                {
                    DataColumn[] keys = new DataColumn[pkColumns.Length];
                    for (int i = 0; i < pkColumns.Length; i++)
                    {
                        if (dt.Columns.Contains(pkColumns[i].ColumnName))
                        {
                            keys[i] = dt.Columns[pkColumns[i].ColumnName];
                        }
                    }
                    dt.PrimaryKey = keys;
                }
            }
        }

        /// <summary>
        /// 从数据库中载入数据。
        /// </summary>
        /// <param name="srcTable">用于表映射的源表的名称</param>
        /// <param name="selectCommandText">Select语句</param>
        /// <param name="loadDataTableType">数据载入方式</param>       
        public DataTable Load(string srcTable, string selectCommandText)
        {
            DbDataAdapter dataAdapter = DataAccess.CreateDataAdapter(selectCommandText);
            return this.Fill(dataAdapter, srcTable, selectCommandText);
        }

        /// <summary>
        /// 保存所做的修改
        /// </summary>
        public void Update()
        {
            this.Update(false);
        }

        /// <summary>
        /// 保存所做的修改
        /// </summary>
        /// <param name="hasTrans">是否进行事物控制</param>
        public void Update(bool hasTrans)
        {
            if (this._loaded)
            {
                if (hasTrans)
                {
                    DbTransaction myTrans = (DbTransaction)DataAccess.BeginTransaction();
                    try
                    {
                        DbDataAdapter dataAdapter;
                        foreach (KeyValuePair<string, DbDataAdapter> kvp in this._dataAdapters)
                        {
                            dataAdapter = kvp.Value;
                            dataAdapter.SelectCommand.Transaction = myTrans;
                            dataAdapter.UpdateCommand.Transaction = myTrans;
                            dataAdapter.DeleteCommand.Transaction = myTrans;
                            dataAdapter.InsertCommand.Transaction = myTrans;
                            dataAdapter.Update(this._dataSet.Tables[kvp.Key]);
                        }
                        myTrans.Commit();
                    }
                    catch (Exception)
                    {
                        myTrans.Rollback();
                        throw; 
                    }                    
                }
                else
                {
                    foreach (KeyValuePair<string, DbDataAdapter> kvp in this._dataAdapters)
                        kvp.Value.Update(this._dataSet.Tables[kvp.Key]);
                }
            }
        }

        /// <summary>
        /// 放弃所做的修改
        /// </summary>
        public void Cancel()
        {
            if (this._loaded)
                this._dataSet.RejectChanges();
        }

        /// <summary>
        /// 重新查询全部数据
        /// </summary>
        public void Requery()
        {
            if (this._loaded)
            {
                DataTable dt;
                foreach (KeyValuePair<string, DbDataAdapter> kvp in this._dataAdapters)
                {
                    dt = this._dataSet.Tables[kvp.Key];
                    dt.Clear();
                    kvp.Value.Fill(dt);
                }
            }
        }
    }
}
