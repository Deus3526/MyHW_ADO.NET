using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class FrmProducts : Form
    {
        public FrmProducts()
        {
            InitializeComponent();
            //test
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(textBox1.Text) > int.Parse(textBox2.Text)) throw new Exception("左數字小右數字大");
                productsTableAdapter1.FillByUnitPrice(northWindDataSet1.Products, int.Parse(textBox1.Text), int.Parse(textBox2.Text));
                SimpifyUIControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                productsTableAdapter1.FillByProductName(northWindDataSet1.Products,textBox3.Text);
                SimpifyUIControl();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveFirst();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveLast();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            label2.Text = $"{bindingSource1.Position + 1}/{bindingSource1.Count}";
        }
        private void SimpifyUIControl()
        {
            bindingSource1.DataSource = northWindDataSet1.Products;
            dataGridView1.DataSource = bindingSource1;
            bindingNavigator1.BindingSource = bindingSource1;
            lblResult.Text = $"結果有{bindingSource1.Count}筆";
        }
    }
}

