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
    public partial class manageprofile : Form
    {
        string constr = @"Data Source=.;Initial Catalog=Barcodeappuser;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        bool menustatus = false;
        string sql;
        Settings s = new Settings();
        public manageprofile()
        {
            InitializeComponent();
            s.readIni();
        }

        private void manageprofile_Load(object sender, EventArgs e)
        {
            bunifuFormFadeTransition1.ShowAsyc(this);
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                sql = "SELECT * FROM [user] WHERE username = '" + s.username + "' ";
                SqlCommand cmd = new SqlCommand(sql,con);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                guna2TextBox1.Text = dr["fullname"].ToString();
                guna2TextBox2.Text = dr["email"].ToString();
                guna2TextBox3.Text = dr["Gender"].ToString();
                guna2TextBox4.Text = dr["phonenumber"].ToString();
                guna2TextBox5.Text = dr["address"].ToString();
                guna2TextBox6.Text = dr["username"].ToString();
                con.Close();
            }
            catch (SqlException se)
            {
                AlertBox.ShowMessage(se.ToString(), "Barcode App Error Center", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            if (menustatus == false)
            {
                guna2ShadowPanel1.Width = 225;
                menustatus = true;
            }
            else
            {
                guna2ShadowPanel1.Width = 76;
                menustatus = false;
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            userdashboard ud = new userdashboard();
            this.Close();
            ud.Show();
        }

        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            userdashboard ud = new userdashboard();
            this.Close();
            ud.Show();
        }

        private void guna2TextBox6_TextChanged(object sender, EventArgs e)
        {
            string username = guna2TextBox6.Text;
            s.writeIni("SECTION", "username", username);
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            try
            {
                con = new SqlConnection(constr);
                con.Open();
                sql = "UPDATE [user] SET fullname = '" + guna2TextBox1.Text + "',email = '" + guna2TextBox2.Text + "',gender = '" + guna2TextBox3.Text + "',phonenumber = '" + guna2TextBox4.Text + "',address = '" + guna2TextBox5.Text + "',username = '" + guna2TextBox6.Text + "' WHERE username = '" + s.username + "' ";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                AlertBox.ShowMessage("Updated Data Sucessfully", "Barcode App Data Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
                con.Close();
            }
            catch (SqlException se)
            {
                AlertBox.ShowMessage(se.ToString(), "Barcode App Error Center", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            try
            {
                if(AlertBox.ShowMessage("Are you sure you want Delete Account", "Barcode App Data Center", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con = new SqlConnection(constr);
                    con.Open();
                    sql = "DELETE FROM [user] WHERE username = '" + s.username + "' ";
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    if(AlertBox.ShowMessage("Deleted Account Sucessfully","Barcode App Data Center",MessageBoxButtons.OK,MessageBoxIcon.None)==DialogResult.OK)
                    {
                        s.writeIni("SECTION", "username", "");
                    }
                }
            }
            catch (SqlException se)
            {
                AlertBox.ShowMessage(se.ToString(), "Barcode App Error Center", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ImageButton5_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            if (AlertBox.ShowMessage("Do you want to Logout ?", "Barcode App Data Center", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String activityname = "Logged Out";
                DateTime activitytime = DateTime.Now;
                con.Open();
                sql = "INSERT INTO [UserActivity] (username,activity_name,activity_time) VALUES('" + guna2TextBox1.Text + "','" + activityname + "','" + activitytime + "')";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                s.writeIni("SECTION", "username", "");
                login l = new login();
                this.Close();
                l.Show();
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            if (AlertBox.ShowMessage("Do you want to Logout ?", "Barcode App Data Center", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String activityname = "Logged Out";
                DateTime activitytime = DateTime.Now;
                con.Open();
                sql = "INSERT INTO [UserActivity] (username,activity_name,activity_time) VALUES('" + guna2TextBox1.Text + "','" + activityname + "','" + activitytime + "')";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                s.writeIni("SECTION", "username", "");
                login l = new login();
                this.Close();
                l.Show();
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }
    }
}
