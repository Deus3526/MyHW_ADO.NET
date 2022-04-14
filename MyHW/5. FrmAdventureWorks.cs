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

namespace MyHomeWork
{
    public partial class FrmAdventureWorks : Form
    {
        public FrmAdventureWorks()
        {
            InitializeComponent();
            //在combobox添加年份
            //方法1
            //adventureWorksDataSet1.EnforceConstraints = false;
            //productPhotoTableAdapter1.FillByYear(adventureWorksDataSet1.ProductPhoto);
            //foreach (DataRow row in adventureWorksDataSet1.ProductPhoto.Rows)
            //{
            //    comboBox1.Items.Add(row["Year"]);
            //}

            ////方法2
            SqlConnection connect = new SqlConnection("Data Source=.;Initial Catalog=AdventureWorks;Integrated Security=True");
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT year(ModifiedDate) as 'Year' FROM Production.ProductPhoto group by year(ModifiedDate) order by year(ModifiedDate)", connect);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            //for (int i = 0; i < ds.Tables[0].Rows.Count; i++) comboBox1.Items.Add(ds.Tables[0].Rows[i][0]);
            //or
            foreach (DataRow row in ds.Tables[0].Rows) comboBox1.Items.Add(row["Year"]);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            productPhotoTableAdapter1.FillByDate(adventureWorksDataSet1.ProductPhoto,dateTimePicker1.Value,dateTimePicker2.Value);
            SimpifyBindingUI();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveFirst();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            bindingSource1.MovePrevious();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveNext();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            bindingSource1.MoveLast();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {
            label4.Text = $"{ bindingSource1.Position + 1}/{bindingSource1.Count}";

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            productPhotoTableAdapter1.FillBySelectedYear(adventureWorksDataSet1.ProductPhoto, int.Parse(comboBox1.Text));
            SimpifyBindingUI();
        }
        private void SimpifyBindingUI()
        {
            bindingSource1.DataSource = adventureWorksDataSet1.ProductPhoto;
            bindingNavigator1.BindingSource = bindingSource1;
            dataGridView1.DataSource = bindingSource1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            productPhotoTableAdapter1.FillByOderByDate(adventureWorksDataSet1.ProductPhoto);
            SimpifyBindingUI();
        }
    }
}
