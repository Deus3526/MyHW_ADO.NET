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
    public partial class FrmTreeview : Form
    {
        public FrmTreeview()
        {
            InitializeComponent();
            LoadDataIntoTreeviewByForeach("Country", "City", "CustomerID");
            // LoadDataIntoTreeview();

            AddCountOfSubNodeForEveryNode();

        }
        private void LoadDataIntoTreeviewByForeach(params string[] columnNode)
        {
            string connectString = Settings.Default.NorthwindConnectionString;
            try
            {
                using (SqlConnection connect = new SqlConnection(connectString))//syntax sugar 區塊執行結束會自動執行該物件的Dispose();
                {
                    connect.Open();

                    SqlCommand comman = new SqlCommand("select * from  Customers order by country", connect);
                    SqlDataReader datareader = comman.ExecuteReader();
                    while (datareader.Read())
                    {

                        TreeNode tempNode = null;
                        foreach (string item in columnNode)
                        {
                            string columnNodeValue = datareader[item].ToString();


                            if (tempNode == null)
                            {
                                if (treeView1.Nodes[columnNodeValue] == null)
                                {
                                    tempNode=treeView1.Nodes.Add(columnNodeValue,columnNodeValue);
                                    tempNode.Tag=item;
                                }
                                else tempNode = treeView1.Nodes[columnNodeValue];
                            }


                            else if (tempNode.Nodes[columnNodeValue] == null)
                            {
                                tempNode=tempNode.Nodes.Add(columnNodeValue,columnNodeValue);
                                tempNode.Tag=item;
                            }
                            else tempNode = tempNode.Nodes[columnNodeValue];

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  Error1");
            }
        }

        private void LoadDataIntoTreeview()
        {
            string connectString = "Data Source=.;Initial Catalog=Northwind;Integrated Security=True";
            try
            {
                using (SqlConnection connect = new SqlConnection(connectString))//syntax sugar 區塊執行結束會自動執行該物件的Dispose();
                {
                    connect.Open();

                    SqlCommand comman = new SqlCommand("select * from  Customers order by country", connect);
                    SqlDataReader datareader = comman.ExecuteReader();
                    while (datareader.Read())
                    {
                        string country = datareader["Country"].ToString();
                        string city = datareader["City"].ToString();
                        string customerID = datareader["CustomerID"].ToString();
                        TreeNode tempNode = null;
                        if (treeView1.Nodes[country] == null)
                        {
                            TreeNode newNode = new TreeNode(country);
                            newNode.Name = country;
                            newNode.Tag = "Country";
                            treeView1.Nodes.Add(newNode);
                            tempNode = newNode;

                        }
                        else tempNode = treeView1.Nodes[country];

                        if (tempNode.Nodes[city] == null)
                        {
                            TreeNode newNode = new TreeNode(city);
                            newNode.Name = city;
                            newNode.Tag = "City";
                            tempNode.Nodes.Add(newNode);
                            tempNode = newNode;
                        }
                        else tempNode = tempNode.Nodes[city];

                        if (tempNode.Nodes[customerID] == null)
                        {
                            TreeNode newNode = new TreeNode(customerID);
                            newNode.Name = customerID;
                            newNode.Tag = "CustomerID";
                            tempNode.Nodes.Add(newNode);
                            tempNode = newNode;
                        }
                        else tempNode = tempNode.Nodes[customerID];
                    }
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  Error2");
            }
        }

        private void AddCountOfSubNodeForEveryNode()
        {
            foreach (TreeNode node in treeView1.Nodes)
            {
                AddCountOfEverySubNode(node);
            }
        }
        private void AddCountOfEverySubNode(TreeNode node)
        {
            if (node.Nodes.Count==0) return;
            
            else
            {
                node.Text = $"{node.Name,-20}-----({node.Nodes.Count})";
                foreach (TreeNode subnode in node.Nodes)
                {
                    AddCountOfEverySubNode(subnode);
                }
            }

            
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode treeNode = (TreeNode)e.Node;
            string connectString = "Data Source=.;Initial Catalog=Northwind;Integrated Security=True";
            try
            {
                using (SqlConnection connect = new SqlConnection(connectString))//syntax sugar 區塊執行結束會自動執行該物件的Dispose();
                {
                    connect.Open();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter($"select * from  Customers where {treeNode.Tag} = '{treeNode.Name}' ", connect);
                    DataSet ds = new DataSet();
                    dataAdapter.Fill(ds);
                    dataGridView1.DataSource = ds.Tables[0];

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "  Error3");
            }
        }
    }
 }
    

