using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace Barcode
{
    public partial class login : Form
    {
        string constr = @"Data Source=.;Initial Catalog=Barcodeappuser;Integrated Security=True";
        SqlConnection conn;
        public login()
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
                guna2ImageButton1.Checked = true;
                guna2ImageButton1.Image = Properties.Resources.darkmode;
                guna2ImageButton1.CheckedState.Image = Properties.Resources.darkmode;
                guna2ImageButton1.HoverState.Image = Properties.Resources.darkmode;
                guna2ImageButton1.PressedState.Image = Properties.Resources.darkmode;
                guna2ControlBox1.FillColor = Color.FromArgb(32, 33, 36);
                guna2ControlBox1.IconColor = Color.White;
                guna2ControlBox1.HoverState.FillColor = Color.White;
                guna2ControlBox1.HoverState.IconColor = Color.DodgerBlue;
                this.BackColor = Color.FromArgb(32, 33, 36);
                guna2HtmlLabel1.ForeColor = Color.White;
                guna2HtmlLabel2.ForeColor = Color.White;
                guna2HtmlLabel3.ForeColor = Color.White;
                guna2Button1.FillColor = Color.White;
                guna2Button1.ForeColor = Color.DodgerBlue;
            }
            else
            {
                guna2ImageButton1.Checked = false;
                guna2ImageButton1.Image = Properties.Resources.lighttheme;
                guna2ImageButton1.CheckedState.Image = Properties.Resources.lighttheme;
                guna2ImageButton1.HoverState.Image = Properties.Resources.lighttheme;
                guna2ImageButton1.PressedState.Image = Properties.Resources.lighttheme;
                guna2ControlBox1.FillColor = Color.White;
                guna2ControlBox1.IconColor = Color.DodgerBlue;
                guna2ControlBox1.HoverState.FillColor = Color.DodgerBlue;
                guna2ControlBox1.HoverState.IconColor = Color.White;
                this.BackColor = Color.White;
                guna2HtmlLabel1.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel2.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel3.ForeColor = Color.DodgerBlue;
                guna2Button1.FillColor = Color.DodgerBlue;
                guna2Button1.ForeColor = Color.White;
            }
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2ControlBox3_Click(object sender, EventArgs e)
        {

        }

    

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {
            register r = new register();
            this.Close();
            r.Show();
        }

        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {
            forgotpassword fp = new forgotpassword();
            this.Close();
            fp.Show();
        }

        private void login_Load(object sender, EventArgs e)
        {
            bunifuFormFadeTransition1.ShowAsyc(this);
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            if (guna2ImageButton1.Checked == false)
            {
                guna2ImageButton1.Checked = true;
                guna2ImageButton1.Image = Properties.Resources.darkmode;
                guna2ImageButton1.CheckedState.Image = Properties.Resources.darkmode;
                guna2ImageButton1.HoverState.Image = Properties.Resources.darkmode;
                guna2ImageButton1.PressedState.Image = Properties.Resources.darkmode;
                guna2ControlBox1.FillColor = Color.FromArgb(32, 33, 36);
                guna2ControlBox1.IconColor = Color.White;
                guna2ControlBox1.HoverState.FillColor = Color.White;
                guna2ControlBox1.HoverState.IconColor = Color.DodgerBlue;
                s.writeIni("SECTION", "key", "dark");
                this.BackColor = Color.FromArgb(32, 33, 36);
                guna2HtmlLabel1.ForeColor = Color.White;
                guna2HtmlLabel2.ForeColor = Color.White;
                guna2HtmlLabel3.ForeColor = Color.White;
                guna2Button1.FillColor = Color.White;
                guna2Button1.ForeColor = Color.DodgerBlue;
            }
            else
            {
                guna2ImageButton1.Checked = false;
                guna2ImageButton1.Image = Properties.Resources.lighttheme;
                guna2ImageButton1.CheckedState.Image = Properties.Resources.lighttheme;
                guna2ImageButton1.HoverState.Image = Properties.Resources.lighttheme;
                guna2ImageButton1.PressedState.Image = Properties.Resources.lighttheme;
                guna2ControlBox1.FillColor = Color.White;
                guna2ControlBox1.IconColor = Color.DodgerBlue;
                guna2ControlBox1.HoverState.FillColor = Color.DodgerBlue;
                guna2ControlBox1.HoverState.IconColor = Color.White;
                s.writeIni("SECTION", "key", "light");
                this.BackColor = Color.White;
                guna2HtmlLabel1.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel2.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel3.ForeColor = Color.DodgerBlue;
                guna2Button1.FillColor = Color.DodgerBlue;
                guna2Button1.ForeColor = Color.White;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(guna2TextBox1.Text) &&
                    String.IsNullOrWhiteSpace(guna2TextBox2.Text)) MessageBox.Show("Please Fill All The Details", "Barcode Generator & Scanner", MessageBoxButtons.OK, MessageBoxIcon.Error);

            if (guna2TextBox1.Text == guna2TextBox2.Text)
            {
                MessageBox.Show("Password Should not match with Username");
            }
            else
            {
                Settings s = new Settings();
                try
                {
                    conn = new SqlConnection(constr);
                    conn.Open();
                    String sql = "SELECT * FROM [user] WHERE username = @username AND password = @pass";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", guna2TextBox1.Text);
                    cmd.Parameters.AddWithValue("@pass", s.hashing(guna2TextBox2.Text));
                    SqlDataReader dr = cmd.ExecuteReader();
                    dr.Read();
                    String name = dr["fullname"].ToString();
                    conn.Close();
                    String activityname = "Logged In";
                    DateTime activitytime = DateTime.Now;
                    conn.Open();
                    sql = "INSERT INTO [UserActivity] (username,activity_name,activity_time) VALUES('" + name + "','" + activityname + "','" + activitytime + "')";
                    cmd = new SqlCommand(sql, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    if (AlertBox.ShowMessage("Login Successfully", "Barcode App Data Center", MessageBoxButtons.OK, MessageBoxIcon.None) == DialogResult.OK )
                    {
                        s.writeIni("SECTION", "username", guna2TextBox1.Text);
                        userdashboard ud = new userdashboard();
                        this.Close();
                        ud.Show();
                    }
                    

                }
                catch (SqlException se)
                {
                    MessageBox.Show(se.ToString(), "Barcode Generator & Scanner Database Center", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    if (AlertBox.ShowMessage(se.ToString(), "Barcode Generator & Scanner Database Center", MessageBoxButtons.OK, MessageBoxIcon.Error) == DialogResult.OK)
                    {
                        this.Close();
                    }
                }

            }
        }
    }
}
