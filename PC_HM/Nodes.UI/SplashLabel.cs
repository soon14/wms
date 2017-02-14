using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing;

namespace Nodes.UI
{
    public class SplashLabel : LabelControl
    {
        private Timer timer;

        public SplashLabel() {
            this.Visible = false;
            timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += new EventHandler(timer_Tick);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            this.Visible = false;
            timer.Enabled = false;
        }

        public void Show(bool success)
        {
            this.ForeColor = success ? Color.Green : Color.Red;
            timer.Enabled = true;
            this.Visible = true;
        }

        public void Show(string msg)
        {
            this.ForeColor = Color.Green;
            this.Text = msg;
            timer.Enabled = true;
            this.Visible = true;
        }

        public void Show(string msg, bool success)
        {
            this.ForeColor = success ? Color.Green : Color.Red;
            this.Text = msg;
            timer.Enabled = true;
            this.Visible = true;
        }
    }
}
