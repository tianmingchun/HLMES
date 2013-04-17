using System;
using System.Collections.Generic;
using System.Text;

namespace HL.DAL
{
    /// <summary>
    /// 数据访问工厂，这里暂时只实现SQLiteDA。
    /// </summary>
    public sealed class DataAccessFactory
    {
        public static DataAccess CreateDataAccess(string databaseType, string connectionString)
        {
            if (!string.IsNullOrEmpty(databaseType))
            {
                switch (databaseType.Trim())
                {
                    case "SQLite":
                        return new SQLiteDA(connectionString);
                }
            }
            return new SQLiteDA(connectionString);
        }
    }
}
