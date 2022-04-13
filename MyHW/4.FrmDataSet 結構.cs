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
    public partial class FrmDataSet_結構 : Form
    {
        public FrmDataSet_結構()
        {
            InitializeComponent();
        }
        private void button13_Click(object sender, EventArgs e)
        {
            productsTableAdapter1.Fill(northWindDataSet1.Products);
            categoriesTableAdapter1.Fill(northWindDataSet1.Categories);
            customersTableAdapter1.Fill(northWindDataSet1.Customers);
            dataGridView4.DataSource = northWindDataSet1.Products;
            dataGridView5.DataSource = northWindDataSet1.Categories;
            dataGridView6.DataSource = northWindDataSet1.Customers;

            listBox2.Items.Clear();
            foreach (DataTable table in northWindDataSet1.Tables)
            {
                listBox2.Items.Add(table.TableName);

                string result = "";
                foreach (DataColumn column in table.Columns)
                {
                    result += $"{column.ColumnName,-20}";
                }
                listBox2.Items.Add(result);

                foreach (DataRow row in table.Rows)
                {
                    result = "";
                    foreach (object content in row.ItemArray)
                    {
                       if(content.ToString().Length>15) result+= $"{content.ToString().PadRight(15).Substring(0, 15)+"...",-20}";
                        else result += $"{content.ToString().PadRight(15).Substring(0, 15),-20}";
                    }
                    listBox2.Items.Add(result);
                }

                listBox2.Items.Add("=========================================");
            }
        }
    }
}
