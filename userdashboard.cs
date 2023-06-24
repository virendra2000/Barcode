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
    public partial class userdashboard : Form
    {
        string constr = @"Data Source=.;Initial Catalog=Barcodeappuser;Integrated Security=True";
        string sql;
        SqlConnection con;
        SqlCommand cmd;
        String name;
        bool menustatus = false;
        int count,cnt;

        public userdashboard()
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
                guna2ImageButton2.Image = Properties.Resources.night;
                guna2ImageButton2.CheckedState.Image = Properties.Resources.night;
                guna2ImageButton2.HoverState.Image = Properties.Resources.night;
                guna2ImageButton2.PressedState.Image = Properties.Resources.night;
                guna2ControlBox1.FillColor = Color.FromArgb(32, 33, 36);
                guna2ControlBox1.IconColor = Color.White;
                guna2ControlBox1.HoverState.FillColor = Color.White;
                guna2ControlBox1.HoverState.IconColor = Color.DodgerBlue;
                s.writeIni("SECTION", "key", "dark");
                this.BackColor = Color.FromArgb(32, 33, 36);
                guna2HtmlLabel1.ForeColor = Color.White;
                guna2HtmlLabel2.ForeColor = Color.Red;
                guna2HtmlLabel3.ForeColor = Color.White;
                guna2HtmlLabel4.ForeColor = Color.White;
                guna2HtmlLabel5.ForeColor = Color.White;
                guna2HtmlLabel6.ForeColor = Color.White;
                guna2Button1.FillColor = Color.White;
                guna2Button1.ForeColor = Color.DodgerBlue;
                guna2ShadowPanel1.FillColor = Color.FromArgb(32, 33, 36);
                guna2ShadowPanel2.FillColor = Color.FromArgb(32, 33, 36);
                guna2ShadowPanel3.FillColor = Color.FromArgb(32, 33, 36);
                guna2ShadowPanel4.FillColor = Color.FromArgb(32, 33, 36);
                guna2Button1.FillColor = Color.FromArgb(32, 33, 36);
                guna2Button5.FillColor = Color.FromArgb(32, 33, 36);
                guna2Button6.FillColor = Color.FromArgb(32, 33, 36);
                guna2Button5.ForeColor = Color.White;
                guna2Button6.ForeColor = Color.White;
                chart1.Legends["Legend1"].ForeColor = Color.White;
                chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(32, 33, 36);
            }
            else
            {
                guna2ImageButton1.Checked = false;
                guna2ImageButton2.Image = Properties.Resources.sun_removebg_preview;
                guna2ImageButton2.CheckedState.Image = Properties.Resources.sun_removebg_preview;
                guna2ImageButton2.HoverState.Image = Properties.Resources.sun_removebg_preview;
                guna2ImageButton2.PressedState.Image = Properties.Resources.sun_removebg_preview;
                guna2ControlBox1.FillColor = Color.White;
                guna2ControlBox1.IconColor = Color.DodgerBlue;
                guna2ControlBox1.HoverState.FillColor = Color.DodgerBlue;
                guna2ControlBox1.HoverState.IconColor = Color.White;
                s.writeIni("SECTION", "key", "light");
                this.BackColor = Color.White;
                guna2HtmlLabel1.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel2.ForeColor = Color.Red;
                guna2HtmlLabel3.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel4.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel5.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel6.ForeColor = Color.DodgerBlue;
                guna2Button1.FillColor = Color.White;
                guna2Button1.ForeColor = Color.DodgerBlue;
                guna2ShadowPanel1.FillColor = Color.White;
                guna2ShadowPanel2.FillColor = Color.White;
                guna2ShadowPanel3.FillColor = Color.White;
                guna2ShadowPanel4.FillColor = Color.White;
                guna2Button1.FillColor = Color.White;
                guna2Button5.FillColor = Color.White;
                guna2Button6.FillColor = Color.White;
                guna2Button5.ForeColor = Color.DodgerBlue;
                guna2Button6.ForeColor = Color.DodgerBlue;
                chart1.Legends["Legend1"].ForeColor = Color.DodgerBlue;
                chart1.ChartAreas["ChartArea1"].BackColor = Color.White;
            }
        }

        private void userdashboard_Load(object sender, EventArgs e)
        {
            guna2GradientButton1.Text = "Happy" + "\n" + "Customers";
            bunifuFormFadeTransition1.ShowAsyc(this);
            Settings s = new Settings();
            s.readIni();
            try
            {
                
                con = new SqlConnection(constr);
                con.Open();
                sql = "SELECT fullname FROM [user] WHERE username = @usern ";
                cmd = new SqlCommand(sql,con);
                cmd.Parameters.AddWithValue("@usern", s.username);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                name = dr["fullname"].ToString();
                con.Close();
            }
            catch(SqlException se)
            {
                MessageBox.Show(se.ToString(), "Barcode Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {

                con = new SqlConnection(constr);
                con.Open();
                sql = "SELECT COUNT(fullname) FROM [user]";
                cmd = new SqlCommand(sql, con);
                int hp = (int) cmd.ExecuteScalar();
                guna2GradientButton1.Text = hp.ToString() + "\n" + "Happy" + "\n" + "Customers";
                con.Close();
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.ToString(), "Barcode Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (DateTime.Now.Hour < 12)
            {
                guna2HtmlLabel1.Text = "Hey " + name ;
                guna2HtmlLabel2.Text = "Good Morning";
                guna2PictureBox1.Image = Properties.Resources.morning;

            }
            else if (DateTime.Now.Hour < 17)
            {
                guna2HtmlLabel1.Text = "Hey " + name;
                guna2HtmlLabel2.Text = "Good Afternoon";
                guna2PictureBox1.Image = Properties.Resources.afternoon;
            }
            else if (DateTime.Now.Hour < 20)
            {
                guna2HtmlLabel1.Text = "Hey " + name;
                guna2HtmlLabel2.Text = "Good Evening";
                guna2PictureBox1.Image = Properties.Resources.evening__1_;
            }
            else
            {
                guna2HtmlLabel1.Text = "Hey " + name;
                guna2HtmlLabel2.Text = "Good Night";
                guna2PictureBox1.Image = Properties.Resources.night;
            }
            try
            {

                con = new SqlConnection(constr);
                con.Open();
                sql = "SELECT * FROM [UserServiceActivity] WHERE username = @usern ";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@usern", name);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                count = int.Parse(dr["barcodegenerated"].ToString());
                guna2HtmlLabel6.Text = count.ToString();
                con.Close();
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.ToString(), "Barcode Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {

                con = new SqlConnection(constr);
                con.Open();
                sql = "SELECT * FROM [UserServiceActivity] WHERE username = @usern ";
                cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@usern", name);
                SqlDataReader dr = cmd.ExecuteReader();
                dr.Read();
                cnt = int.Parse(dr["barcodescanned"].ToString());
                guna2HtmlLabel4.Text = cnt.ToString();
                con.Close();
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.ToString(), "Barcode Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2ImageButton5_Click(object sender, EventArgs e)
        {
            Settings s = new Settings();
            if(AlertBox.ShowMessage("Do you want to Logout ?", "Barcode App Data Center", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            if(menustatus == false)
            {
                guna2ShadowPanel1.Width = 225;
                menustatus = true;
            }
            else {
                guna2ShadowPanel1.Width = 76;
                menustatus = false;
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

        private void guna2ImageButton3_Click(object sender, EventArgs e)
        {
            manageprofile mp = new manageprofile();
            this.Close();
            mp.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            manageprofile mp = new manageprofile();
            this.Close();
            mp.Show();
        }

        private void guna2PictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void guna2ShadowPanel6_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void guna2GradientButton1_Click(object sender, EventArgs e)
        {
            try
            {

                con = new SqlConnection(constr);
                con.Open();
                sql = "SELECT COUNT(fullname) FROM [user]";
                cmd = new SqlCommand(sql, con);
                int hp = (int)cmd.ExecuteScalar();
                guna2GradientButton1.Text = hp.ToString() + "\n" + "Happy" + "\n" + "Customers";
                con.Close();
            }
            catch (SqlException se)
            {
                MessageBox.Show(se.ToString(), "Barcode Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            barcodescanner bs = new barcodescanner();
            this.Close();
            bs.Show();
        }

        private void guna2ImageButton2_Click_1(object sender, EventArgs e)
        {
            Settings s = new Settings();
            if (guna2ImageButton1.Checked == false)
            {
                guna2ImageButton1.Checked = true;
                guna2ImageButton2.Image = Properties.Resources.night;
                guna2ImageButton2.CheckedState.Image = Properties.Resources.night;
                guna2ImageButton2.HoverState.Image = Properties.Resources.night;
                guna2ImageButton2.PressedState.Image = Properties.Resources.night;
                guna2ControlBox1.FillColor = Color.FromArgb(32, 33, 36);
                guna2ControlBox1.IconColor = Color.White;
                guna2ControlBox1.HoverState.FillColor = Color.White;
                guna2ControlBox1.HoverState.IconColor = Color.DodgerBlue;
                s.writeIni("SECTION", "key", "dark");
                this.BackColor = Color.FromArgb(32, 33, 36);
                guna2HtmlLabel1.ForeColor = Color.White;
                guna2HtmlLabel2.ForeColor = Color.Red;
                guna2HtmlLabel3.ForeColor = Color.White;
                guna2HtmlLabel4.ForeColor = Color.White;
                guna2HtmlLabel5.ForeColor = Color.White;
                guna2HtmlLabel6.ForeColor = Color.White;
                guna2Button1.FillColor = Color.White;
                guna2Button1.ForeColor = Color.DodgerBlue;
                guna2ShadowPanel1.FillColor = Color.FromArgb(32, 33, 36);
                guna2ShadowPanel2.FillColor = Color.FromArgb(32, 33, 36);
                guna2ShadowPanel3.FillColor = Color.FromArgb(32, 33, 36);
                guna2ShadowPanel4.FillColor = Color.FromArgb(32, 33, 36);
                guna2Button1.FillColor = Color.FromArgb(32, 33, 36);
                guna2Button5.FillColor = Color.FromArgb(32, 33, 36);
                guna2Button6.FillColor = Color.FromArgb(32, 33, 36);
                guna2Button5.ForeColor = Color.White;
                guna2Button6.ForeColor = Color.White;
                chart1.Legends["Legend1"].ForeColor = Color.White;
                chart1.ChartAreas["ChartArea1"].BackColor = Color.FromArgb(32, 33, 36);
            }
            else
            {
                guna2ImageButton1.Checked = false;
                guna2ImageButton2.Image = Properties.Resources.sun_removebg_preview;
                guna2ImageButton2.CheckedState.Image = Properties.Resources.sun_removebg_preview;
                guna2ImageButton2.HoverState.Image = Properties.Resources.sun_removebg_preview;
                guna2ImageButton2.PressedState.Image = Properties.Resources.sun_removebg_preview;
                guna2ControlBox1.FillColor = Color.White;
                guna2ControlBox1.IconColor = Color.DodgerBlue;
                guna2ControlBox1.HoverState.FillColor = Color.DodgerBlue;
                guna2ControlBox1.HoverState.IconColor = Color.White;
                s.writeIni("SECTION", "key", "light");
                this.BackColor = Color.White;
                guna2HtmlLabel1.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel2.ForeColor = Color.Red;
                guna2HtmlLabel3.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel4.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel5.ForeColor = Color.DodgerBlue;
                guna2HtmlLabel6.ForeColor = Color.DodgerBlue;
                guna2Button1.FillColor = Color.White;
                guna2Button1.ForeColor = Color.DodgerBlue;
                guna2ShadowPanel1.FillColor = Color.White;
                guna2ShadowPanel2.FillColor = Color.White;
                guna2ShadowPanel3.FillColor = Color.White;
                guna2ShadowPanel4.FillColor = Color.White;
                guna2Button1.FillColor = Color.White;
                guna2Button5.FillColor = Color.White;
                guna2Button6.FillColor = Color.White;
                guna2Button5.ForeColor = Color.DodgerBlue;
                guna2Button6.ForeColor = Color.DodgerBlue;
                chart1.Legends["Legend1"].ForeColor = Color.DodgerBlue;
                chart1.ChartAreas["ChartArea1"].BackColor = Color.White;
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Barcodegenerater bg = new Barcodegenerater();
            this.Close();
            bg.Show();
        }
    }
}
