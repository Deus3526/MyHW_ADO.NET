using MyHW.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHW
{
    public partial class FrmMyAlbum : Form
    {
        SqlConnection connection;
        SqlCommand command;
        SqlDataReader dataReader;
        SqlDataAdapter dataAdapter;
        DataSet ds;
        public FrmMyAlbum()
        {
            InitializeComponent();
            connection = new SqlConnection(Settings.Default.CityPhotoConnectionString);
            command = new SqlCommand("null", connection);
            LoadInitialData();


        }
        private void LoadInitialData()
        {
            command.CommandText = "select CityID,CityName from City";
            connection.Open();
            dataReader = command.ExecuteReader();
            int count = 0;
            while(dataReader.Read())
            {
                LinkLabel linkLabel = new LinkLabel();
                linkLabel.Size = new Size(100, 50);
                linkLabel.Font = new Font("微軟正黑體", 20);
                linkLabel.Text = dataReader["CityName"].ToString();
                linkLabel.Left = 100;
                linkLabel.Top = count * 50;
                linkLabel.Tag = dataReader["CityID"];
                splitContainer1.Panel1.Controls.Add(linkLabel);
                count++;
                linkLabel.Click += LinkLabel_Click;

                comboBox1.Items.Add(dataReader["CityName"]);



            }
            connection.Close();
         }

        private void LinkLabel_Click(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            LinkLabel linkLabel = (LinkLabel)sender;
            connection.Open();
            command.CommandText =$"select PhotoPicture from Photo where CityID ={linkLabel.Tag}";
            dataAdapter = new SqlDataAdapter(command.CommandText, connection);
            ds = new DataSet();
            dataAdapter.Fill(ds,"Photo");
            //MessageBox.Show(ds.Tables[0].TableName);
            foreach(DataRow row in ds.Tables["Photo"].Rows)
            {
                PictureBox pictureBox = new PictureBox();
                pictureBox.Size = new Size(200, 150);
                pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
                flowLayoutPanel1.Controls.Add(pictureBox);
                Byte[] bytes =(Byte[])row["PhotoPicture"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                pictureBox.Image = Image.FromStream(ms);
            }
            connection.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowLayoutPanel2.Controls.Clear();

        }
    }
}
