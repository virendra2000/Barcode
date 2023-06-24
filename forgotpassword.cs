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
    public partial class forgotpassword : Form
    {
        public forgotpassword()
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
                guna2ShadowPanel1.FillColor = Color.FromArgb(32, 33, 36);
                guna2HtmlLabel1.ForeColor = Color.White;
                guna2Button1.FillColor = Color.White;
                guna2Button1.ForeColor = Color.DodgerBlue;
                guna2Button2.FillColor = Color.White;
                guna2Button2.ForeColor = Color.DodgerBlue;
            }
            else
            {
                guna2ShadowPanel1.BackColor = Color.White;
                guna2HtmlLabel1.ForeColor = Color.DodgerBlue;
                guna2Button1.FillColor = Color.DodgerBlue;
                guna2Button1.ForeColor = Color.White;
                guna2Button2.FillColor = Color.DodgerBlue;
                guna2Button1.ForeColor = Color.White;
            }

        }

        private void forgotpassword_Load(object sender, EventArgs e)
        {
            bunifuFormFadeTransition1.ShowAsyc(this);
        }

        private void forgotpassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Back)
            {
                login l = new login();
                this.Close();
                l.Show();
            }
        }
    }
}
