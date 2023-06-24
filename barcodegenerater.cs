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
using ZXing;
namespace Barcode
{
    public partial class Barcodegenerater : Form
    {
        SqlConnection con;
        String constr = @"Data Source=.;Initial Catalog=Barcodeappuser;Integrated Security=True";
        SqlCommand cmd;
        String sql;
        string name;
        bool menustatus = false;
        int count;
        public Barcodegenerater()
        {
            InitializeComponent();
        }

        private void Barcodegenerater_Load(object sender, EventArgs e)
        {
            bunifuFormFadeTransition1.ShowAsyc(this);
            Settings s = new Settings();
            s.readIni();
            try
            {

                con = new SqlConnection(constr);
                con.Open();
                sql = "SELECT fullname FROM [user] WHERE username = @usern ";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@usern", s.username);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                name = dr["fullname"].ToString();
                con.Close();
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.ToString(), "Barcode Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            BarcodeWriter writer = new BarcodeWriter() { Format = BarcodeFormat.CODE_128 };
            guna2PictureBox1.Image = writer.Write(guna2TextBox1.Text);
            AlertBox.ShowMessage("Barcode Generated Successfully", "Barcode App Data Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
            String activityname = "Generated Barcode";
            DateTime activitytime = DateTime.Now;
            con.Open();
            sql = "INSERT INTO [UserActivity] (username,activity_name,activity_time) VALUES('" + name + "','" + activityname + "','" + activitytime + "')";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
            con.Open();
            sql = "SELECT * FROM [UserServiceActivity] WHERE username = '" + name + "'";
            cmd = new SqlCommand(sql, con);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            count = int.Parse(dr["barcodegenerated"].ToString());
            con.Close();
            con.Open();
            count = count + 1;
            sql = "UPDATE [UserServiceActivity] SET barcodegenerated = '" + count + "' WHERE username = '" + name + "' ";
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            userdashboard ud = new userdashboard();
            ud.Show();
            this.Close();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Image img = guna2PictureBox1.Image;
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PNG|*.png" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                    img.Save(sfd.FileName);
                AlertBox.ShowMessage("Barcode Saved Successfully", "Barcode App Data Center", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            manageprofile mp = new manageprofile();
            this.Close();
            mp.Show();
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

        private void guna2ImageButton5_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            if (AlertBox.ShowMessage("Do you want to Logout ?", "Barcode App Data Center", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String activityname = "Logged Out";
                DateTime activitytime = DateTime.Now;
                con.Open();
                sql = "INSERT INTO [UserActivity] (username,activity_name,activity_time) VALUES('" + name + "','" + activityname + "','" + activitytime + "')";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                s.writeIni("SECTION", "username", "");
                login l = new login();
                this.Close();
                l.Show();
            }
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            userdashboard ud = new userdashboard();
            this.Hide();
            ud.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            if (AlertBox.ShowMessage("Do you want to Logout ?", "Barcode App Data Center", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                String activityname = "Logged Out";
                DateTime activitytime = DateTime.Now;
                con.Open();
                sql = "INSERT INTO [UserActivity] (username,activity_name,activity_time) VALUES('" + name + "','" + activityname + "','" + activitytime + "')";
                cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
                con.Close();
                s.writeIni("SECTION", "username", "");
                login l = new login();
                this.Close();
                l.Show();
            }
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            manageprofile mp = new manageprofile();
            this.Close();
            mp.Show();
        }
    }
}
