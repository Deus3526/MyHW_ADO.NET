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
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Beverages
            //Condiments
            //Confections
            //Dairy Products
            //Grains / Cereals
            //Meat / Poultry
            //Produce
            //Seafood
            listBox1.Items.Clear();
            SqlConnection connect = new SqlConnection("Data Source=.;Initial Catalog=Northwind;Integrated Security=True");
            SqlCommand command = null;
            try
            { 
                connect.Open();
                switch (this.comboBox1.Text)
                {
                    case "Beverages":
                        command = new SqlCommand("select * from products p join Categories c on c.CategoryID = p.CategoryID where c.CategoryName = 'Beverages'",connect);
                        break;
                    case "Condiments":
                        command = new SqlCommand("select * from products p join Categories c on c.CategoryID = p.CategoryID where c.CategoryName = 'Condiments'", connect);
                        break;
                    case "Confections":
                        command = new SqlCommand("select * from products p join Categories c on c.CategoryID = p.CategoryID where c.CategoryName = 'Confections'", connect);
                        break;
                    case "Dairy Products":
                        command = new SqlCommand("select * from products p join Categories c on c.CategoryID = p.CategoryID where c.CategoryName = 'Dairy Products'", connect);
                        break;
                    case "Grains/Cereals":
                        command = new SqlCommand("select * from products p join Categories c on c.CategoryID = p.CategoryID where c.CategoryName = 'Grains/Cereals'", connect);
                        break;
                    case "Meat/Poultry":
                        command = new SqlCommand("select * from products p join Categories c on c.CategoryID = p.CategoryID where c.CategoryName = 'Meat/Poultry'", connect);
                        break;
                    case "Produce":
                        command = new SqlCommand("select * from products p join Categories c on c.CategoryID = p.CategoryID where c.CategoryName = 'Produce'", connect);
                        break;
                    case "Seafood":
                        command = new SqlCommand("select * from products p join Categories c on c.CategoryID = p.CategoryID where c.CategoryName = 'Seafood'", connect);
                        break;

                }
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
