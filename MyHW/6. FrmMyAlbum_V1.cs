using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHW
{
    public partial class FrmMyAlbum_V1 : Form
    {
        public FrmMyAlbum_V1()
        {
            InitializeComponent();
            photoTableAdapter1.Fill(cityPhotoDataSet1.Photo);
            cityTableAdapter1.Fill(cityPhotoDataSet1.City);
            bindingSource1.DataSource = cityPhotoDataSet1.Photo;


            for(int i = 0; i < cityPhotoDataSet1.City.Rows.Count; i++)
            {
                LinkLabel x=new LinkLabel();
                panel1.Controls.Add(x);
                x.Top = 50 * i;
                x.Text = cityPhotoDataSet1.City[i].CityName.ToString();
                x.Click += X_Click;
                x.Tag = cityPhotoDataSet1.City[i].CityID;
            }


        }

        private void X_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            LinkLabel x = sender as LinkLabel;
            int count = 0;
            for(int i = 0; i < cityPhotoDataSet1.Photo.Rows.Count; i++)
            {
                if((int)x.Tag==cityPhotoDataSet1.Photo[i].CityID)
                {
                    PictureBox y = new PictureBox();
                    panel2.Controls.Add(y);
                    y.Top =200 * count;
                    y.SizeMode = PictureBoxSizeMode.StretchImage;
                    y.Width = 300;
                    y.Height = 200;
                    count++;


                    Byte[] byteBLOBData = (Byte[])(cityPhotoDataSet1.Photo[i].PhotoPicture);
                    MemoryStream stmBLOBData = new MemoryStream(byteBLOBData);
                    y.Image = Image.FromStream(stmBLOBData);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmMyAlbum_Table_Update newform=new FrmMyAlbum_Table_Update();
            newform.ShowDialog();
            photoTableAdapter1.Fill(cityPhotoDataSet1.Photo);
        }
    }
}
