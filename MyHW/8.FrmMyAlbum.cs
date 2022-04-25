using MyHW.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        PictureBox picture_ContextOpen;
        public FrmMyAlbum()
        {
            InitializeComponent();
            connection = new SqlConnection(Settings.Default.CityPhotoConnectionString);
            command = new SqlCommand("null", connection);
            LoadInitialData();
            tabControl1.SelectedIndex = 0;

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
            command.CommandText =$"select * from Photo where CityID ={linkLabel.Tag}";
            dataAdapter = new SqlDataAdapter(command.CommandText, connection);
            ds = new DataSet();
            dataAdapter.Fill(ds,"Photo");
            //MessageBox.Show(ds.Tables[0].TableName);
            foreach(DataRow row in ds.Tables["Photo"].Rows)
            {
                PictureBox pictureBox = BuildPictureBox();
                object[] picture=new object[2]
                  {
                    linkLabel.Tag,
                    (int)row["PhotoID"]
                };
                pictureBox.Tag =picture;
                pictureBox.Click += PictureBox_Click;
                flowLayoutPanel1.Controls.Add(pictureBox);
                Byte[] bytes =(Byte[])row["PhotoPicture"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                pictureBox.Image = Image.FromStream(ms);
            }
            connection.Close();
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBox pictureBox= (PictureBox)sender;
            MyAlbum_Viewer form = new MyAlbum_Viewer((object[])pictureBox.Tag);
            form.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            flowLayoutPanel2.Controls.Clear();
            connection.Open();
            command.CommandText = $"select PhotoPicture,Photo.CityID,PhotoID from Photo join City on City.CityID=Photo.CityID where CityName ='{comboBox1.Text}'";
            dataAdapter = new SqlDataAdapter(command.CommandText, connection);
            ds = new DataSet();
            dataAdapter.Fill(ds, "Photo");
            //MessageBox.Show(ds.Tables[0].TableName);
            foreach (DataRow row in ds.Tables["Photo"].Rows)
            {
                PictureBox pictureBox = BuildPictureBox();
                pictureBox.Tag = row["PhotoID"];
                flowLayoutPanel2.Controls.Add(pictureBox);
                Byte[] bytes = (Byte[])row["PhotoPicture"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
                pictureBox.Image = Image.FromStream(ms);

                pictureBox.ContextMenuStrip = contextMenuStrip1;

                connection.Close();
            }

        }

        private void flowLayoutPanel2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void flowLayoutPanel2_DragDrop(object sender, DragEventArgs e)
        {
            connection.Open();

            string[] temp = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach (string item in temp)
            {
                PictureBox pictureBox = BuildPictureBox();
                pictureBox.Image = Image.FromFile(item);
                flowLayoutPanel2.Controls.Add(pictureBox);

                InsertPicture(pictureBox);
            }
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                connection.Open();
                string[] dirs = System.IO.Directory.GetFiles(folderBrowserDialog1.SelectedPath);
                foreach(string item in dirs)
                {
                    if (Regex.IsMatch(item, @"\.jpg$|\.png$|\.gif$") == false) continue;
                    PictureBox pictureBox = BuildPictureBox();
                    pictureBox.Image = Image.FromFile(item);
                    flowLayoutPanel2.Controls.Add(pictureBox);
                    InsertPicture(pictureBox);
                }
                connection.Close();
            }
        }
        private int ReturnCityIDInComboBox()
        {
            command.CommandText = $"select CityID from City where CityName='{comboBox1.Text}'";
            dataReader = command.ExecuteReader();
            dataReader.Read();
            int CityID= (int)dataReader[0];
            dataReader.Close();
            return CityID;
        }
        private void InsertPicture(PictureBox pictureBox)
        {

            int CityID = ReturnCityIDInComboBox();
            command.CommandText = $"Insert into Photo(CityID,PhotoPicture)  values({CityID},@image)";
            byte[] bytes;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            pictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            bytes = ms.GetBuffer();
            command.Parameters.Add("@image", SqlDbType.Image).Value = bytes;
            command.ExecuteNonQuery();
            command.Parameters.Clear();
        }
        private PictureBox BuildPictureBox()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Size = new Size(200, 150);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.BorderStyle = BorderStyle.Fixed3D;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseLeave += PictureBox_MouseLeave;
            pictureBox.Padding = new Padding(5, 5, 5, 5);
            return pictureBox;
        }


        private void PictureBox_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            pictureBox.Refresh();
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox=(PictureBox)sender;
            System.Drawing.Graphics g=pictureBox.CreateGraphics();
            Pen pen = new Pen(Color.Red,5);
            pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
            pictureBox.CreateGraphics().DrawRectangle(pen,pictureBox.DisplayRectangle);
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {
        }

        private void photoBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.photoBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.cityPhotoDataSet);

        }

        private void FrmMyAlbum_Load_City(object sender, EventArgs e)
        {
            // TODO: 這行程式碼會將資料載入 'cityPhotoDataSet.City' 資料表。您可以視需要進行移動或移除。
            this.cityTableAdapter.Fill(this.cityPhotoDataSet.City);
            // TODO: 這行程式碼會將資料載入 'cityPhotoDataSet.Photo' 資料表。您可以視需要進行移動或移除。

            cityBindingSource.Position = 0;
        }
        private void FrmMyAlbum_Load_Photo()
        {
            //MessageBox.Show(cityIDTextBox.Text);
            photoTableAdapter.FillByCityID(cityPhotoDataSet.Photo, int.Parse(cityIDTextBox.Text));
            //this.photoTableAdapter.Fill(this.cityPhotoDataSet.Photo);
        }

    private void button2_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                photoPicturePictureBox.Image = Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void cityIDTextBox_TextChanged(object sender, EventArgs e)
        {
            FrmMyAlbum_Load_Photo();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            command.CommandText = $"Delete Photo where PhotoID ={picture_ContextOpen.Tag}";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            picture_ContextOpen.Dispose();
        }

        //再刪除時，我要知道是哪個PictureBox開啟這個選單
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            picture_ContextOpen = (PictureBox)((ContextMenuStrip)sender).SourceControl;
        }
    }
}
