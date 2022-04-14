using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHW
{
    public partial class FrmMyAlbum_Table_Update : Form
    {
        public FrmMyAlbum_Table_Update()
        {
            InitializeComponent();
            photoTableAdapter1.Fill(cityPhotoDataSet1.Photo);
            cityTableAdapter1.Fill(cityPhotoDataSet1.City);
            bindingSource1.DataSource = cityPhotoDataSet1.Photo;
            dataGridView1.DataSource = bindingSource1;
            pictureBox1.DataBindings.Add("Image",bindingSource1,"PhotoPicture",true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                pictureBox1.Image=Image.FromFile(openFileDialog1.FileName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Validate();
            bindingSource1.EndEdit();
            photoTableAdapter1.Update(cityPhotoDataSet1.Photo);
            cityTableAdapter1.Update(cityPhotoDataSet1.City);

        }
    }
}
