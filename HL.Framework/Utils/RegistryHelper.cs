using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace HL.Framework.Utils
{
    /// <summary>
    /// 注册表静态方法。
    /// </summary>
    public static class RegistryHelper
    {
        /// <summary>
        /// 保存名值对。
        /// </summary>
        /// <param name="name">名</param>
        /// <param name="value">值</param>
        public static void SetValue(string name, string value)
        {
            SetValue(@"Software\HFMain", name, value);
        }
 
        /// <summary>
        /// 
        /// </summary>
        public static void SetProgramNoWarning( )
        {
            SetValue(@"Security\Policies\Policies", "0000101a", 1);
        }

        /// <summary>
        /// 保存名值对。
        /// </summary>
        /// <param name="key">子项名称</param>
        /// <param name="name">名</param>
        /// <param name="value">值</param>
        public static void SetValue(string keyName, string name, object value)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName, true);
            if (key == null)
            {
                key = Registry.LocalMachine.CreateSubKey(keyName);
            }            
            key.SetValue(name, value);           
            key.Close();
        }

        /// <summary>
        /// 保存名值对。
        /// </summary>
        /// <param name="name">名</param>
        /// <param name="value">值</param>
        public static object GetValue(string name)
        {
            return GetValue(@"Software\HFMain", name);
        }

        /// <summary>
        /// 保存名值对。
        /// </summary>
        /// <param name="key">子项名称</param>
        /// <param name="name">名</param>
        /// <param name="value">值</param>
        public static object GetValue(string keyName, string name)
        {
            RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName, false);
            if (key == null)
            {
                return null;
            }
            object value = key.GetValue(name);
            key.Close();
            return value;
        }
    }
}
