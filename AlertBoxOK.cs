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
    public partial class AlertBoxOK : Form
    {
        public AlertBoxOK()
        {
            InitializeComponent();
        }

        private void AlertBoxOK_Load(object sender, EventArgs e)
        {
            bunifuFormFadeTransition1.ShowAsyc(this);
        }

        public Image MessageIcon
        {
            get
            {
                return guna2PictureBox1.Image;
            }
            set
            {
                guna2PictureBox1.Image = value;
            }
        }

        public string Message
        {
            get { return guna2HtmlLabel1.Text; }
            set { guna2HtmlLabel1.Text = value; }
        }

    }
}
