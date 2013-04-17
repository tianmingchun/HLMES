using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Data;
using System.Text.RegularExpressions;

namespace HL.DAL
{
    /// <summary>
    /// 获取数据库字段值等静态类
    /// </summary>
    public static class DbValue
    {
        #region GetValue
        /// <summary>
        /// 获取指定对象的bool值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取bool值的对象</param>
        /// <returns>返回对象的bool值</returns>
        /// <remarks>DBNull返回false，0返回false，其他返回true</remarks>
        public static bool GetBool(object value)
        {
            return GetBool(value, false);
        }

        /// <summary>
        /// 获取指定对象的bool值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取bool值的对象</param>
        /// <param name="nullValue">为DBNull值时的返回值</param>
        /// <returns>返回对象的bool值</returns>
        /// <remarks>0返回false，其他返回true</remarks>
        public static bool GetBool(object value, bool nullValue)
        {
            if (value == null || value is DBNull)
                return nullValue;
            else if (value is bool)  //SQL Server
                return (bool)value;
            else if (value.ToString().ToLower() == "t" || value.ToString().ToLower() == "true") //PMS
                return true;
            else if (value.ToString().ToLower() == "f" || value.ToString().ToLower() == "false") //PMS
                return false;
            else 
                return value.ToString() != "0";
        }

        /// <summary>
        /// 获取指定对象的long值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取int值的对象</param>
        /// <returns>返回对象的long值</returns>
        /// <remarks>DBNull返回0，超出long范围则舍弃</remarks>
        public static long GetLong(object value)
        {
            return GetLong(value, 0);
        }

        /// <summary>
        /// 获取指定对象的long值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取int值的对象</param>
        /// <param name="nullValue">为DBNull值时的返回值</param>
        /// <returns>返回对象的long值</returns>
        /// <remarks>超出long范围则舍弃</remarks>
        public static long GetLong(object value, long nullValue)
        {
            if (value == null || value is DBNull)
                return nullValue;
            else if ((value is byte) || (value is short) || (value is int) || (value is long))
                return (long)value;
            else if (value is decimal)
                return decimal.ToInt64((decimal)value);
            else if (value is bool)
                return ((bool)value) ? 1 : 0;
            else
                return Convert.ToInt64(value);
        }

        /// <summary>
        /// 获取指定对象的int值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取int值的对象</param>
        /// <returns>返回对象的int值</returns>
        /// <remarks>DBNull返回0，超出int范围则舍弃</remarks>
        public static int GetInt(object value)
        {
            return GetInt(value, 0);
        }

        /// <summary>
        /// 获取指定对象的int值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取int值的对象</param>
        /// <param name="nullValue">为DBNull值时的返回值</param>
        /// <returns>返回对象的int值</returns>
        /// <remarks>超出int范围则舍弃</remarks>
        public static int GetInt(object value, int nullValue)
        {
            if (value == null || value is DBNull || value == string.Empty)
                return nullValue;
            else if ((value is byte) || (value is short) || (value is int))
                return (int)value;
            else if (value is decimal)
                return decimal.ToInt32((decimal)value);
            else if (value is bool)
                return ((bool)value) ? 1 : 0;
            else
                return Convert.ToInt32(value);
        }

        /// <summary>
        /// 获取指定对象的short值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取short值的对象</param>
        /// <returns>返回对象的short值</returns>
        /// <remarks>DBNull返回0，超出short范围则舍弃</remarks>
        public static short GetShort(object value)
        {
            return GetShort(value, 0);
        }

        /// <summary>
        /// 获取指定对象的short值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取short值的对象</param>
        /// <param name="nullValue">为DBNull值时的返回值</param>
        /// <returns>返回对象的short值</returns>
        /// <remarks>超出short范围则舍弃</remarks>
        public static short GetShort(object value, short nullValue)
        {
            if (value == null || value is DBNull)
                return nullValue;
            else if ((value is byte) || (value is short))
                return (short)value;
            else if (value is decimal)
                return decimal.ToInt16((decimal)value);
            else if (value is bool)
                return ((bool)value) ? (short)1 : (short)0;
            else
                return Convert.ToInt16(value);
        }

        /// <summary>
        /// 获取指定对象的byte值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取byte值的对象</param>
        /// <returns>返回对象的byte值</returns>
        /// <remarks>DBNull返回0，超出byte范围则舍弃</remarks>
        public static byte GetByte(object value)
        {
            return GetByte(value, 0);
        }

        /// <summary>
        /// 获取指定对象的byte值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取byte值的对象</param>
        /// <param name="nullValue">为DBNull值时的返回值</param>
        /// <returns>返回对象的byte值</returns>
        /// <remarks>超出byte范围则舍弃</remarks>
        public static byte GetByte(object value, byte nullValue)
        {
            if (value == null || value is DBNull)
                return nullValue;
            else if (value is byte)
                return (byte)value;
            else if (value is decimal)
                return decimal.ToByte((decimal)value);
            else if (value is bool)
                return ((bool)value) ? (byte)1 : (byte)0;
            else
                return Convert.ToByte(value);
        }

        /// <summary>
        /// 获取指定对象的decimal值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取decimal值的对象</param>
        /// <returns>返回对象的decimal值</returns>
        /// <remarks>DBNull返回0</remarks>
        public static decimal GetDecimal(object value)
        {
            return GetDecimal(value, 0);
        }

        /// <summary>
        /// 获取指定对象的decimal值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取decimal值的对象</param>
        /// <param name="nullValue">为DBNull值时的返回值</param>
        /// <returns>返回对象的decimal值</returns>
        public static decimal GetDecimal(object value, decimal nullValue)
        {
            if (value == null || value is DBNull)
                return nullValue;
            else if (value is decimal)
                return (decimal)value;
            else if (value is bool)
                return ((bool)value) ? 1 : 0;
            else
                return Convert.ToDecimal(value);
        }

        /// <summary>
        /// 获取指定对象的float值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取float值的对象</param>
        /// <returns>返回对象的float值</returns>
        /// <remarks>DBNull返回0</remarks>
        public static float GetFloat(object value)
        {
            return GetFloat(value, 0);
        }

        /// <summary>
        /// 获取指定对象的float值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取float值的对象</param>
        /// <param name="nullValue">为DBNull值时的返回值</param>
        /// <returns>返回对象的float值</returns>
        public static float GetFloat(object value, float nullValue)
        {
            if (value == null || value is DBNull)
                return nullValue;
            else if (value is float)
                return (float)value;
            else if (value is double)
                return (float)(double)value;
            else if (value is decimal)
                return decimal.ToSingle((decimal)value);
            else if (value is bool)
                return ((bool)value) ? 1 : 0;
            else
                return Convert.ToSingle(value);
        }

        /// <summary>
        /// 获取指定对象的double值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取double值的对象</param>
        /// <returns>返回对象的double值</returns>
        /// <remarks>DBNull返回0</remarks>
        public static double GetDouble(object value)
        {
            return GetDouble(value, 0);
        }

        /// <summary>
        /// 获取指定对象的double值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取double值的对象</param>
        /// <param name="nullValue">为DBNull值时的返回值</param>
        /// <returns>返回对象的double值</returns>
        public static double GetDouble(object value, double nullValue)
        {
            if (value == null || value is DBNull)
                return nullValue;
            else if (value is float)
                return (float)value;
            else if (value is double)
                return (double)value;
            else if (value is decimal)
                return decimal.ToDouble((decimal)value);
            else if (value is bool)
                return ((bool)value) ? 1 : 0;
            else
                return Convert.ToDouble(value);
        }

        /// <summary>
        /// 获取指定对象的DateTime值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取DateTime值的对象</param>
        /// <returns>返回对象的decimal值</returns>
        /// <remarks>DBNull返回DateTime.MinValue</remarks>
        public static DateTime GetDateTime(object value)
        {
            return GetDateTime(value, DateTime.MinValue);
        }

        /// <summary>
        /// 获取指定对象的DateTime值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取DateTime值的对象</param>
        /// <param name="nullValue">为DBNull值时的返回值</param>
        /// <returns>返回对象的decimal值</returns>
        public static DateTime GetDateTime(object value, DateTime nullValue)
        {
            if (value == null || value is DBNull)
                return nullValue;
            else if (value is DateTime)
                return (DateTime)value;
            else
                return DateTime.Parse(value.ToString(), CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// 获取指定对象的String值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取string值的对象</param>
        /// <returns>返回对象的String值</returns>
        /// <remarks>DBNull返回string.Empty</remarks>
        public static string GetString(object value)
        {
            return GetString(value, string.Empty);
        }

        /// <summary>
        /// 获取指定对象的String值，用于获取数据库字段值
        /// </summary>
        /// <param name="value">要获取string值的对象</param>
        /// <param name="nullValue">为DBNull值时的返回值</param>
        /// <returns>返回对象的String值</returns>
        /// <remarks>如果是bool类型，则返回1或0</remarks>
        public static string GetString(object value, string nullValue)
        {
            if (value == null || value is DBNull)
                return nullValue;
            else
                return value.ToString();
        }

        /// <summary>
        /// 判断指定对象是否整形
        /// </summary>
        /// <param name="value">要获取string值的对象</param>     
        /// <returns>返回对象的bool值</returns>
        /// <remarks>如果是bool类型，则返回1或0</remarks>
        public static bool GetObject(string value)
        {
             bool blnResult = false;
             if (string.IsNullOrEmpty(value))
             {
                 return blnResult;
             }
             else
             {
                 try
                 {
                     Match m = Regex.Match(value.Trim(), @"^[.]?\d+(\.\d+)?$");//数字型：如10、10.5、10.52，.52
                     if (m.Success)
                     {
                         blnResult = true;
                     }
                 }
                 catch
                 {
                     return blnResult;
                 }
             }
             return blnResult;
        }

        // /// <summary>///// 判断指定对象是否为空
        ///// </summary>
        ///// <param name="value">要获取string值的对象</param>     
        ///// <returns>返回对象的bool值</returns>
        ///// <remarks>如果是bool类型，则返回1或0</remarks>
        //public static bool GetObject1(string value)
        //{
        //     bool blnResult = false;
        //     if (string.IsNullOrEmpty(value))
        //     {
        //         return blnResult;
        //     }
        //}
        #endregion

        /// <summary>
        /// 格式化SQL的值。
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string FormatSqlText(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = text.Replace("'", "''");
            }
            return text;
        }

        #region CopyTableByFilter
        /// <summary>
        /// 根据筛选表达式，从指定的DataTable复制产生一个新的DataTable。
        /// </summary>
        /// <param name="source">原DataTable</param>
        /// <param name="filterExpression">筛选表达式</param>
        /// <returns></returns>
        public static DataTable CopyTableByFilter(DataTable source,string filterExpression)
        {
            DataRow[] rows = source.Select(filterExpression);
            return CopyTableByRows(source, rows);      
        }


        /// <summary>
        /// 根据筛选表达式和排序表达式，从指定的DataTable复制产生一个新的DataTable。
        /// </summary>
        /// <param name="source">原DataTable</param>
        /// <param name="filterExpression">筛选表达式</param>
        /// <param name="sort">排序表达式</param>
        /// <returns></returns>
        public static DataTable CopyTableByFilter(DataTable source, string filterExpression, string sort)
        {
            DataRow[] rows = source.Select(filterExpression, sort);
            return CopyTableByRows(source, rows);
        }

        private static DataTable CopyTableByRows(DataTable source, DataRow[] rows)
        {
            DataTable result = source.Clone();
            for (int i = 0; i < rows.Length; i++)
            {
                result.ImportRow(rows[i]);
            }
            return result;
        }
        #endregion
    }
}
