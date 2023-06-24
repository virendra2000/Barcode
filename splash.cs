using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barcode
{
    public partial class splash : Form
    {
        int startpos;
        public splash()
        {
            InitializeComponent();
            getIni();
        }
public void getIni()
        {
            Settings s = new Settings();
            s.readIni();
            if (s.theme == "dark")
            {
                this.BackColor = Color.FromArgb(32, 33, 36);
                label2.ForeColor = Color.White;
                myprog.ProgressBrushMode = Guna.UI2.WinForms.Enums.BrushMode.Gradient;
                myprog.ProgressColor = Color.Orange;
                myprog.ProgressColor2 = Color.Pink;
            }
            else
            {
                this.BackColor = Color.White;
                label2.ForeColor = Color.Black;
                myprog.ProgressBrushMode = Guna.UI2.WinForms.Enums.BrushMode.Solid;
                myprog.ProgressColor = Color.DodgerBlue;
            }
        }
        

        private void splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
            bunifuFormFadeTransition1.ShowAsyc(this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            startpos += 1;
            myprog.Value = startpos;
            Settings s = new Settings();
            s.readIni();
            if (myprog.Value == 100)
            {
                myprog.Value = 0;
                timer1.Stop();
                if (String.IsNullOrEmpty(s.username))
                {
                    login l = new login();
                    this.Hide();
                    l.Show();
                }
                else
                {
                    userdashboard ud = new userdashboard();
                    this.Hide();
                    ud.Show();
                }
            }
        }
    }
}
