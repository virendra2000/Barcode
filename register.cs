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
    public partial class register : Form
    {
        string constr = @"Data Source=.;Initial Catalog=Barcodeappuser;Integrated Security=True";
        SqlConnection conn;
        int barcodegen, barcodescan;
        public register()
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
                guna2HtmlLabel1.ForeColor = Color.White;
                guna2HtmlLabel3.ForeColor = Color.White;
                guna2CheckBox1.ForeColor = Color.White;
                guna2Button1.FillColor = Color.White;
                guna2Button1.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel2.ForeColor = Color.DodgerBlue;

            }
            else
            {
                this.BackColor = Color.White;
                guna2HtmlLabel1.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel3.ForeColor = Color.DodgerBlue;
                guna2CheckBox1.ForeColor = Color.Black;
                guna2Button1.FillColor = Color.DodgerBlue;
                guna2Button1.ForeColor = Color.White;
                guna2HtmlLabel2.ForeColor = Color.DodgerBlue;
            }
        }

        private void guna2HtmlLabel3_Click(object sender, EventArgs e)
        {
            login l = new login();
            this.Close();
            l.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (guna2CheckBox1.Checked == true)
            {
                if (String.IsNullOrWhiteSpace(guna2TextBox1.Text) &&
                    String.IsNullOrWhiteSpace(guna2TextBox2.Text) &&
                    String.IsNullOrWhiteSpace(guna2TextBox3.Text) &&
                    String.IsNullOrWhiteSpace(guna2TextBox4.Text) &&
                    String.IsNullOrWhiteSpace(guna2TextBox5.Text) &&
                    String.IsNullOrWhiteSpace(guna2TextBox6.Text) &&
                    String.IsNullOrWhiteSpace(guna2TextBox7.Text)) MessageBox.Show("Please Fill All The Details", "Barcode Generator & Scanner", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (guna2TextBox6.Text == guna2TextBox7.Text)
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
                        String sql = "INSERT INTO [user] (fullname,email,gender,phonenumber,address,username,password) VALUES(@fullname,@email,@gender,@mobilenum,@address,@username,@pass)";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@fullname", guna2TextBox1.Text);
                        cmd.Parameters.AddWithValue("@email", guna2TextBox2.Text);
                        cmd.Parameters.AddWithValue("@gender", guna2TextBox3.Text);
                        cmd.Parameters.AddWithValue("@mobilenum", guna2TextBox4.Text);
                        cmd.Parameters.AddWithValue("@address", guna2TextBox5.Text);
                        cmd.Parameters.AddWithValue("@username", guna2TextBox6.Text);
                        cmd.Parameters.AddWithValue("@pass", s.hashing(guna2TextBox7.Text));
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        conn.Open();
                        barcodegen = 0;
                        barcodescan = 0;
                        sql = "INSERT INTO [UserServiceActivity] (username,barcodescanned,barcodegenerated) VALUES('" + guna2TextBox1.Text + "'," + barcodescan + "," + barcodegen + ")";
                        cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        String activityname = "Registration Done";
                        DateTime activitytime = DateTime.Now;
                        conn.Open();
                        sql = "INSERT INTO [UserActivity] (username,activity_name,activity_time) VALUES('" + guna2TextBox1.Text + "','" + activityname + "','" + activitytime + "')";
                        cmd = new SqlCommand(sql, conn);
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        if (AlertBox.ShowMessage("Register Successfully", "Barcode App Data Center", MessageBoxButtons.OK, MessageBoxIcon.None) == DialogResult.OK)
                        {
                            s.writeIni("SECTION", "username", guna2TextBox6.Text);
                            userdashboard ud = new userdashboard();
                            this.Close();
                            ud.Show();
                        }
                    }
                    catch (SqlException se)
                    {
                        MessageBox.Show(se.ToString(), "Barcode Generator & Scanner Database Center", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                }
            }
        }

        private void register_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Y)
            {
                guna2CheckBox1.Checked = true;
            }
            if (e.KeyCode == Keys.N)
            {
                guna2CheckBox1.Checked = false;
            }
            if (e.Control && e.KeyCode == Keys.Back)
            {
                login l = new login();
                this.Close();
                l.Show();
            }
        }

        private void register_Load(object sender, EventArgs e)
        {
            bunifuFormFadeTransition1.ShowAsyc(this);
        }
    }
}
