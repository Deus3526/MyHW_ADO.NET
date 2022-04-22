using MyHomeWork;
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
    public partial class HomePage : Form
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void OpenFormInPanle2(Form form)
        {
            
            form.TopLevel = false;
            splitContainer1.Panel2.Controls.Clear();
            splitContainer1.Panel2.Controls.Add(form);
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFormInPanle2(new Frm標準練習());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFormInPanle2(new FrmCategoryProducts());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFormInPanle2(new FrmProducts());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFormInPanle2(new FrmDataSet_結構());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFormInPanle2(new FrmAdventureWorks());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFormInPanle2(new FrmMyAlbum());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            OpenFormInPanle2(new FrmCustomers());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            OpenFormInPanle2(new FrmTreeview());
        }
    }
}
