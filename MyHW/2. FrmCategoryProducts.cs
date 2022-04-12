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

namespace MyHomeWork
{
    public partial class FrmCategoryProducts : Form
    {
        public FrmCategoryProducts()
        {
            InitializeComponent();
            SqlConnection connect = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            connect.Open();
            SqlCommand command = new SqlCommand("select CategoryName from categories", connect);
            SqlDataReader dataReader = command.ExecuteReader();
            while(dataReader.Read())
            {
                comboBox1.Items.Add(dataReader["CategoryName"]);
            }
            connect.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            SqlConnection connect = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            SqlCommand command = null;
            try
            { 
                connect.Open();
                command = new SqlCommand($"select * from products p join Categories c on c.CategoryID = p.CategoryID where c.CategoryName ='{comboBox1.Text}'", connect);
                SqlDataReader dataReader = command.ExecuteReader();
                string result = $"{"ProductName",-40}{"UnitPrice",-10}{"CategoryID"}";
                listBox1.Items.Add(result);
                listBox1.Items.Add("\n");
                while(dataReader.Read())
                {
                    result = $"{dataReader["productname"],-40}{dataReader["UnitPrice"],-10:c2}{dataReader["CategoryID"]}";
                    listBox1.Items.Add(result);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }
    }
}
