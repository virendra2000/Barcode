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
using AForge.Video.DirectShow;
using AForge.Video;
using ZXing;
namespace Barcode
{
    public partial class barcodescanner : Form
    {
        bool menustatus = false;
        String constr = @"Data Source=.;Initial Catalog=Barcodeappuser;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        string sql;
        int count;
        string name;
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        public barcodescanner()
        {
            InitializeComponent();
        }

        private void barcodescanner_Load(object sender, EventArgs e)
        {
            guna2Button9.Hide();
            bunifuFormFadeTransition1.ShowAsyc(this);
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo device in filterInfoCollection)
                cboCamera.Items.Add(device.Name);
            cboCamera.SelectedIndex = 0;
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

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            userdashboard ud = new userdashboard();
            this.Close();
            ud.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            userdashboard ud = new userdashboard();
            this.Close();
            ud.Show();
        }

        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            manageprofile mp = new manageprofile();
            this.Close();
            mp.Show();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            manageprofile mp = new manageprofile();
            this.Close();
            mp.Show();
        }

        private void guna2ImageButton4_Click(object sender, EventArgs e)
        {

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

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    guna2PictureBox1.Image = Image.FromFile(ofd.FileName);
                    BarcodeReader reader = new BarcodeReader();
                    var result = reader.Decode((Bitmap)guna2PictureBox1.Image);
                    String activityname = "Scanned Barcode";
                    if (result != null)
                    {
                        guna2TextBox1.Text = result.ToString();
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
                        count = int.Parse(dr["barcodescanned"].ToString());
                        con.Close();
                        con.Open();
                        count = count + 1;
                        sql = "UPDATE [UserServiceActivity] SET barcodescanned = '" + count + "' WHERE username = '" + name + "' ";
                        cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    guna2PictureBox1.Image = Image.FromFile(ofd.FileName);
                    BarcodeReader reader = new BarcodeReader();
                    var result = reader.Decode((Bitmap)guna2PictureBox1.Image);
                    String activityname = "Scanned Barcode";
                    if (result != null)
                    {
                        guna2TextBox1.Text = result.ToString();
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
                        count = int.Parse(dr["barcodescanned"].ToString());
                        con.Close();
                        con.Open();
                        count = count + 1;
                        sql = "UPDATE [UserServiceActivity] SET barcodescanned = '" + count + "' WHERE username = '" + name + "' ";
                        cmd = new SqlCommand(sql, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }

                }
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[cboCamera.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += VideoCaptureDevice_NewFrame;
            videoCaptureDevice.Start();
            guna2Button6.Enabled = false;
            guna2Button9.Show();
        }

        private void VideoCaptureDevice_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();
            BarcodeReader reader = new BarcodeReader();
            var result = reader.Decode(bitmap);
            if (result != null)
            {
                guna2TextBox1.Invoke(new MethodInvoker(delegate ()
                {
                    guna2TextBox1.Text = result.ToString();
                    guna2PictureBox1.Image = Properties.Resources.browsewhite;
                    String activityname = "Scanned Barcode";
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
                    count = int.Parse(dr["barcodescanned"].ToString());
                    con.Close();
                    con.Open();
                    count = count + 1;
                    sql = "UPDATE [UserServiceActivity] SET barcodescanned = '" + count + "' WHERE username = '" + name + "' ";
                    cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                }));
            }
            guna2PictureBox1.Image = bitmap;
        }

        private void barcodescanner_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice != null)
            {
                if (videoCaptureDevice.IsRunning)
                    videoCaptureDevice.Stop();
            }
        }

        private void cboCamera_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            userdashboard ud = new userdashboard();
            this.Close();
            ud.Show();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            videoCaptureDevice.Stop();
            guna2Button6.Enabled = true;
            guna2PictureBox1.Image = Properties.Resources.browsewhite;
        }
    }
}
