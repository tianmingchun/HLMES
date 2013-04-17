using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace HL.Framework
{
    /// <summary>
    /// 移动业务平台框架接口
    /// </summary>
    public interface IFramework
    {
        /// <summary>
        /// 主窗体对象
        /// </summary>
        Form MainForm { get; }
    }
}
