using MyHW.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHW
{
    public partial class MyAlbum_Viewer : Form
    {
        int PhotoID;
        int CityID;
        DataSet ds;
        bool status_Size=false;
        Size size;
        public MyAlbum_Viewer( object[] picture)
        {
            InitializeComponent();
            PhotoID = (int)picture[1];
            CityID = (int)picture[0];


            using (SqlConnection connection = new SqlConnection(Settings.Default.CityPhotoConnectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter($"Select * from Photo where CityID='{CityID}' ", connection);
                ds = new DataSet();
                adapter.Fill(ds);

                bindingSource1.DataSource = ds.Tables[0];
                bindingSource1.Position= bindingSource1.Find("PhotoID",PhotoID);
    
                pictureBox1.DataBindings.Add("Image", bindingSource1, "PhotoPicture",true);


                pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
                status_Size = false;
                //MessageBox.Show("" + pictureBox1.Width + " " + pictureBox1.Height);
                //MessageBox.Show("" + pictureBox1.ClientSize.Width + " " + pictureBox1.ClientSize.Height);
                //Size size1 = new Size(pictureBox1.Width, pictureBox1.Height);
                //MessageBox.Show("" + size1.Width + "      " + size1.Height);
            }


        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            SetSize();
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.Size = new Size(size.Width*trackBar1.Value,size.Height*trackBar1.Value);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            pictureBox1.Size = size;
            trackBar1.Value = 1;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(bindingSource1.Position==bindingSource1.Count-1)
            {
                bindingSource1.MoveFirst();
            }
            else bindingSource1.MoveNext();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }
        private void SetSize()
        {
            if (status_Size == true) return;
            else if(status_Size==false)
            {
                size = new Size(pictureBox1.Width, pictureBox1.Height);
                status_Size = true;
            }
        }

        private void bindingSource1_PositionChanged(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            status_Size = false;
        }
    }
}
