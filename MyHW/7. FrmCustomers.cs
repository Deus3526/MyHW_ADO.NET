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
using MyHW.Properties;

namespace MyHW
{
    enum Status_Order { No, Asc, Desc };
    public partial class FrmCustomers : Form
    {
        bool status_Group = false;
       Status_Order status_Order = Status_Order.No;
        public FrmCustomers()
        {
            InitializeComponent();
            LoadCountryToCombobox();//包含group也一起建立
            LoadColumnToListview();
        }


        //TODO HW

        //1. All Country
        private void LoadCountryToCombobox()
        {
            try
            {
                using (SqlConnection connect = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand("select distinct country from Customers order by country ", connect);
                    connect.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    //comboBox1.Items.Add("All Country");
                    while (dataReader.Read())
                    {
                        comboBox1.Items.Add(dataReader[0].ToString());
                        ListViewGroup group = new ListViewGroup(dataReader[0].ToString(), dataReader[0].ToString());
                        listView1.Groups.Add(group);
                    }
                    comboBox1.Items.Insert(0, "All Country");
                    //MessageBox.Show( comboBox1.Items[0].ToString());

                    comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //將All Country的Font繪製成藍色，其他的預設黑色
        private void comboBox1_DrawItem(object sender, DrawItemEventArgs e) 
        {

            //初始化字体和背景色
            Pen fontColor = new Pen(Color.Black);
            Pen backColor = new Pen(Color.White);

            switch (e.Index)
            {

                case 0:
                    {
                        fontColor = new Pen(Color.Blue);
                        break;
                    }
            }
            e.Graphics.FillRectangle(backColor.Brush, e.Bounds);
            e.Graphics.DrawString((string)comboBox1.Items[e.Index], this.Font, fontColor.Brush, e.Bounds);
        }

        private void LoadColumnToListview()
        {
            try
            {
                listView1.View = View.Details;
                using (SqlConnection connect = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand("select * from Customers", connect);
                    connect.Open();
                    SqlDataReader dataReader = command.ExecuteReader();
                    DataTable table = dataReader.GetSchemaTable();
                    foreach(DataRow row in table.Rows)
                    {
                        listView1.Columns.Add(row[0].ToString());
                    }
                    listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "All Country") LoadDataIntoListview(true);
            else LoadDataIntoListview(false);
        }
        private void LoadDataIntoListview(bool allCountry)
        {
            
            listView1.Visible= false;
            listView1.Items.Clear();
            listView1.ShowGroups = true;
            using (SqlConnection connect = new SqlConnection(Settings.Default.NorthwindConnectionString))
            {
                connect.Open();
                SqlCommand command = null;
                if (allCountry == true)
                {
                    command = new SqlCommand("Select * from Customers", connect);
                }
                else
                {
                    command = new SqlCommand($"select * from Customers where country='{comboBox1.Text}'", connect);
                }

                if (status_Order == Status_Order.Asc)
                {
                    command.CommandText += " order by customerid asc";
                }
                else if(status_Order == Status_Order.Desc)
                {
                    command.CommandText += " order by customerid desc";
                }

                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string country=dataReader["country"].ToString();
                    ListViewItem lvi = listView1.Items.Add(dataReader[0].ToString());
                    listView1.Groups[country].Items.Add(lvi);


                    for (int i = 1; i < dataReader.FieldCount; i++)
                    {
                        if (dataReader.IsDBNull(i)) lvi.SubItems.Add("空值");

                        else lvi.SubItems.Add(dataReader[i].ToString());
                        //listView1.Items[listView1.Items.Count-1].SubItems.Add(dataReader[i].ToString());
                    }
                }
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            foreach(ListViewGroup group in listView1.Groups)
            {
                group.Header = group.Name + $"--({group.Items.Count})";
            }
            listView1.Visible = true;
            if (status_Group == false) listView1.ShowGroups = false;
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
        }

        private void customerIDAscToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status_Order = Status_Order.Asc;
            if (comboBox1.Text == "All Country") LoadDataIntoListview(true);
            else LoadDataIntoListview(false);
        }

        private void customerIDDescToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status_Order = Status_Order.Desc;
            if (comboBox1.Text == "All Country") LoadDataIntoListview(true);
            else LoadDataIntoListview(false);
        }

        private void countryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status_Group = true;
            listView1.ShowGroups = true;
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status_Group= false;
            listView1.ShowGroups = false;
        }

        private void noOrederToolStripMenuItem_Click(object sender, EventArgs e)
        {
            status_Order = Status_Order.No;
            if (comboBox1.Text == "All Country") LoadDataIntoListview(true);
            else LoadDataIntoListview(false);
        }



        //================================
        //2. ContextMenuStrip2
        //選擇性作業
        //Groups
        //USA (100) 
        //UK (20)

        //this.listview1.visible = false;
        //ListViewItem lvi = this.listView1.Items.Add(dataReader[0].ToString());

        //if (this.listView1.Groups["USA"] == null)
        //{                       {
        //    ListViewGroup group = this.listView1.Groups.Add("USA", "USA"); //Add(string key, string headerText);
        //    group.Tag = 0;
        //    lvi.Group = group; 
        //}
        //else
        //{
        //    ListViewGroup group = this.listView1.Groups["USA"]; 
        //    lvi.Group = group;
        //}

        //this.listView1.Groups["USA"].Tag = 
        //this.listView1.Groups["USA"].Header = 
    }
}