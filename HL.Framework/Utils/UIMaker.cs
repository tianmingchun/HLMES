using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;

namespace HL.Framework.Utils
{
    /// <summary>
    /// 控件显示和数据绑定静态类
    /// </summary>
    public static class UIMaker
    {
        /// <summary>
        /// 进行数据绑定显示
        /// </summary>
        /// <param name="listControl">待绑定控件</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="displayMember">显示字段</param>
        /// <param name="valueMember">值字段</param>
        /// <remarks></remarks>
        public static void DisplayBinding(ListControl listControl, object dataSource,
            string displayMember, string valueMember)
        {
            DisplayBinding(listControl, dataSource, displayMember, valueMember, false);
        }

        /// <summary>
        /// 进行数据绑定显示，绑定的数据源不能用来更新
        /// </summary>
        /// <param name="listControl">待绑定控件</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="displayMember">显示字段</param>
        /// <param name="valueMember">值字段</param>
        /// <param name="addNullRow">是否增加一值为DBNull.Value的空行</param>      
        /// <remarks></remarks>
        public static void DisplayBinding(ListControl listControl, object dataSource,
           string displayMember, string valueMember, bool addNullRow)
        {
            DisplayBinding(listControl, dataSource, displayMember, valueMember, addNullRow, "无");
        }
        /// <summary>
        /// 进行数据绑定显示，绑定的数据源不能用来更新
        /// </summary>
        /// <param name="listControl">待绑定控件</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="displayMember">显示字段</param>
        /// <param name="valueMember">值字段</param>
        /// <param name="addNullRow">是否增加一值为DBNull.Value的空行</param>
        /// <param name="nullText">空值的显示文本</param>
        /// <remarks></remarks>
        public static void DisplayBinding(ListControl listControl, object dataSource,
            string displayMember, string valueMember, bool addNullRow, string nullText)
        {
            if (listControl == null) throw new ArgumentNullException("listControl");
            CheckFieldName(dataSource, ref displayMember, ref valueMember);
            if (addNullRow && (dataSource is DataTable))
            {
                DataTable dt = dataSource as DataTable;
                DataRow dr = dt.NewRow();
                dr[displayMember] = nullText;
                dr[valueMember] = DBNull.Value;
                dt.Rows.InsertAt(dr, 0);
            }
            listControl.DataSource = null;
            listControl.DisplayMember = displayMember;
            listControl.ValueMember = valueMember;
            listControl.DataSource = dataSource;
        }

        /// <summary>
        /// 检查数据源字段名称，以适应大小写
        /// </summary>
        /// <param name="dataSource">数据源</param>
        /// <param name="displayMember">显示字段</param>
        /// <param name="valueMember">值字段</param>
        /// <remarks></remarks>
        private static void CheckFieldName(object dataSource, ref string displayMember, ref string valueMember)
        {
            if (dataSource is DataTable)
            {
                //由于Columns[]方法不区分大小写，故直接如此
                DataTable dt = dataSource as DataTable;
                dt.Columns[displayMember].ColumnName = displayMember;
                dt.Columns[valueMember].ColumnName = valueMember;
            }
            else if (dataSource is DataView) //DataView
            {
                //由于Columns[]方法不区分大小写，故直接如此
                DataView dv = dataSource as DataView;
                dv.Table.Columns[displayMember].ColumnName = displayMember;
                dv.Table.Columns[valueMember].ColumnName = valueMember;
            }
        }

        /// <summary>
        /// 根据给定的Text，设置ComboBox选中指定的项。
        /// </summary>
        /// <param name="cboBox"></param>
        /// <param name="selectedText"></param>
        public static void SetSelectedText(ComboBox cboBox, string selectedText)
        {
            cboBox.SelectedIndex = -1;
            for (int i = 0; i < cboBox.Items.Count; i++)
            {
                if (cboBox.GetItemText(cboBox.Items[i]) == selectedText)
                {
                    cboBox.SelectedIndex = i;
                    return;
                }
            }
        }

        /// <summary>
        /// 根据给定的Text，设置ListBox选中指定的项。
        /// </summary>
        /// <param name="cboBox"></param>
        /// <param name="selectedText"></param>
        public static void SetSelectedText(ListBox listBox, string selectedText)
        {
            listBox.SelectedIndex = -1;
            for (int i = 0; i < listBox.Items.Count; i++)
            {
                if (string.IsNullOrEmpty(listBox.DisplayMember))
                {
                    if (listBox.GetItemText(listBox.Items[i]) == selectedText)
                    {
                        listBox.SelectedIndex = i;
                        return;
                    }
                }
                else
                {
                    DataRowView drv = listBox.Items[i] as DataRowView;
                    if (drv != null)
                    {
                        if (drv[listBox.DisplayMember] != DBNull.Value && drv[listBox.DisplayMember].ToString() == selectedText)
                        {
                            listBox.SelectedIndex = i;
                            return;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 进行数据绑定，以便自动显示和更新
        /// </summary>
        /// <param name="dataBindings">绑定集合</param>
        /// <param name="propertyName">绑定属性</param>
        /// <param name="dataSource">数据源</param>
        /// <param name="dataMember">绑定字段</param>
        public static void DataBinding(ControlBindingsCollection dataBindings, string propertyName, object dataSource,
            string dataMember)
        {
            if (dataBindings == null) throw new ArgumentNullException("dataBindings");
            dataBindings.Clear();
            dataBindings.Add(GetParseBinding(propertyName, dataSource, dataMember));
        }

        public static Binding GetParseBinding(string propertyName, object dataSource, string dataMember)
        {
            Binding binding = new Binding(propertyName, dataSource, dataMember);
            binding.Parse += new ConvertEventHandler(EmptyToDbNullConvert);
            return binding;
        }

        private static void EmptyToDbNullConvert(object sender, ConvertEventArgs e)
        {
            if (e.DesiredType == typeof(string) && e.Value is string && e.Value != DBNull.Value
                && e.Value != null && string.IsNullOrEmpty(e.Value.ToString()))
                e.Value = DBNull.Value;
        }
    }
}
