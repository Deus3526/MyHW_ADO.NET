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

namespace MyHomeWork
{
    public partial class FrmLogon : Form
    {
        public FrmLogon()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                using (SqlConnection connect = new SqlConnection(Settings.Default.HwMember))
                {
                    connect.Open();
                    SqlCommand command = new SqlCommand("Insert into Member(UserName, Password) values (@UserName,@Password)",connect);
                    command.Parameters.Add("UserName", SqlDbType.NVarChar, 16).Value = UsernameTextBox.Text;
                    command.Parameters.Add("@Password", SqlDbType.NVarChar, 40).Value = PasswordTextBox.Text;
                    command.ExecuteNonQuery();
                    MessageBox.Show("建立成功");

                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            

        }

        private void OK_Click(object sender, EventArgs e)
        {

            //try
            //{
            //    using (SqlConnection connect = new SqlConnection(Settings.Default.HwMember))
            //    {
            //        connect.Open();
            //        SqlCommand command = new SqlCommand("select * from  Member where username=@UserName and password=@Password", connect);
            //        command.Parameters.Add("UserName", SqlDbType.NVarChar, 16).Value = UsernameTextBox.Text;
            //        command.Parameters.Add("@Password", SqlDbType.NVarChar, 40).Value = PasswordTextBox.Text;
            //        SqlDataReader dataReader = command.ExecuteReader();
            //        if(dataReader.HasRows)
            //        {
            //            MessageBox.Show("登入成功");
            //        }
            //        else
            //        {
            //            MessageBox.Show("登入失敗");
            //        }

            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }
    }
}
