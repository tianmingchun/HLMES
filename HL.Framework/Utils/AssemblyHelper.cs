using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace HL.Framework.Utils
{
    /// <summary>
    /// 程序集反射静态类
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// 反射创建对象实例
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static object CreateInstance(string assemblyFile, string className)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            Type type = assembly.GetType(className, false);            
            if (type != null)
            {
                object instance = Activator.CreateInstance(type);
                return instance;
            }
            else
                return null;
        }

        /// <summary>
        /// 反射调用方法
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public static object InvokeMethod(string assemblyFile, string className, string methodName, object[] paramters)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            Type type = assembly.GetType(className,false);
            if (type != null)
            {
                object instance = Activator.CreateInstance(type);
                return type.GetMethod(methodName).Invoke(instance, paramters);
            }
            else
                return null;
        }

        /// <summary>
        /// 反射调用静态方法
        /// </summary>
        /// <param name="assemblyFile"></param>
        /// <param name="className"></param>
        /// <param name="methodName"></param>
        /// <param name="paramters"></param>
        /// <returns></returns>
        public static object InvokeStaticMethod(string assemblyFile, string className, string methodName, object[] paramters)
        {
            Assembly assembly = Assembly.LoadFrom(assemblyFile);
            Type type = assembly.GetType(className, false);
            if (type != null)
                return type.GetMethod(methodName).Invoke(null, paramters);
            else
                return null;
        }
    }
}
