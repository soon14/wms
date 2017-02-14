using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Nodes.UI
{
    public class MsgBox
    {
        #region 对MessageBox进行简单封装
        public static DialogResult OK(String msg)
        {
            Cursor.Current = Cursors.Default;
            return XtraMessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //return MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Warn(String msg)
        {
            Cursor.Current = Cursors.Default;
            return XtraMessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //return MessageBox.Show(msg, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static DialogResult Err(String msg)
        {
            Cursor.Current = Cursors.Default;
            //return XtraMessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return MessageBox.Show(msg, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static DialogResult AskYes(String msg)
        {
            Cursor.Current = Cursors.Default;
            return XtraMessageBox.Show(msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            //return MessageBox.Show(msg, "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public static DialogResult AskOK(String msg)
        {
            Cursor.Current = Cursors.Default;
            return XtraMessageBox.Show(msg, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            //return MessageBox.Show(msg, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public static DialogResult Save(String msg)
        {
            Cursor.Current = Cursors.Default;
            return XtraMessageBox.Show(msg, "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            //return MessageBox.Show(msg, "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }
        #endregion
    }
}
