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
    public partial class FrmCustomers : Form
    {
        public FrmCustomers()
        {
            InitializeComponent();
            LoadCountryToCombobox();
        }


        //TODO HW

        //1. All Country
        private void LoadCountryToCombobox()
        {
            try
            {
                using(SqlConnection connect = new SqlConnection(Settings.Default.NorthwindConnectionString))
                {
                    SqlCommand command = new SqlCommand("select distinct country from Customers",connect);
                    connect.Open();
                    SqlDataReader dataReader=command.ExecuteReader();
                    
                    while(dataReader.Read())
                    {
                        comboBox1.Items.Add(dataReader[0].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

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