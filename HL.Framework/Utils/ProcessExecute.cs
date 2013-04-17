using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace HL.Framework.Utils
{
    /// <summary>
    /// 进程处理静态类
    /// </summary>
    public static class ProcessExecute
    {        
        /// <summary>
        /// 启动应用程序
        /// </summary>
        /// <param name="fileName">文件名</param>
        public static Process StartProcess(string fileName)
        {
            try
            {
                if (!File.Exists(fileName))
                    return null;
                Process process = new Process();
                process.StartInfo.Arguments = "HLWarehoue";
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = fileName;
                process.Start();
                return process;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 启动快捷方式
        /// </summary>
        /// <param name="fileName">文件名</param>
        public static void StartLink(string fileName)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = fileName;
                process.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
