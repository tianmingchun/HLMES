namespace HL.Framework
{
    using System;
    using System.Windows.Forms;

    public class MessageShow
    {
        public static void Alert(string caption, string text)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        }
    }
}

