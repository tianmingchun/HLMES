using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HL.Framework.Utils
{
    public static class StringHelper
    {
        /// <summary>
        /// 判断字符串是否存在汉字。
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static bool ExistsChineseChar(string value)
        {
            return Regex.IsMatch(value, @"[\u4e00-\u9fa5]+");
        }

        /// <summary>
        /// 判断字符串是否全部是汉字。
        /// </summary>
        /// <param name="value">字符串</param>
        /// <returns></returns>
        public static bool AllChineseChar(string value)
        {
            return Regex.IsMatch(value, @"^[\u4e00-\u9fa5]+$");
        }
    }
}
