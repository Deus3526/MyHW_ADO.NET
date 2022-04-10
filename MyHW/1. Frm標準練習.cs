
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class FrmHomeWork : Form
    {
        public FrmHomeWork()
        {
            InitializeComponent();
        }
        int[] arr0711 = { 1, 5, 6, 8, 7, 97, 54, 887, 65, 578 };
        string[] arr0711_Str = { "mother張", "emma", "迪克蕭", "J40", "Candy", "Cindy", "Coconut", "Motherfacker" };

        private void button1_Click(object sender, EventArgs e)
        {
            int a = 100;
            int b = 66;
            int c = 77;
            int max= Math.Max(a, b);
            max = Math.Max(max, c);
            lblResult.Text = max.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int[] nums = { 33, 4, 5, 11, 222, 34 };
            lblResult.Text = "陣列int[] nums:";
            foreach (int item in nums) lblResult.Text += item + " ";
            lblResult.Text += "\n";
            int even=0;
            int odd=0;
            foreach(int item in nums)
            {
                if (item % 2 == 0) even++;
                if(item % 2 == 1) odd++;
            }
            lblResult.Text += "奇數有:" + odd + "個\n" + "偶數有:" + even + "個";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string[] names = { "aaa", "ksdkfjsdk"};
            int MaxLength = 0;
            string LongestName = "";
            foreach (string item in names)
            {
                if (item.Length > MaxLength)
                {
                    MaxLength = item.Length;
                    LongestName= item;
                }
            }
            lblResult.Text = "陣列string[]:";
            foreach (string item in names)
            {
                lblResult.Text += item + " ";
            }
            lblResult.Text += "\n";
            lblResult.Text += "最長的名字是:" + LongestName + "\n有" + MaxLength + "個字";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool flag = int.TryParse(textBox4.Text, out int input);
            if (flag == false)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            else lblResult.Text = EvenOrOdd(input);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int[] scores = { 2, 3, 46, 33, 22, 100,150, 33,55};

            int max =  scores.Max();
            int min = scores.Min();
            lblResult.Text = "int[]陣列 scores = { 2, 3, 46, 33, 22, 100,150, 33,55}";
            lblResult.Text+= "\n最大值為:" + max;
            lblResult.Text += "\n最小值為:" + min;
            //MessageBox.Show("Max = " + max);

            //Array.Sort(scores);
            //MessageBox.Show("Max =" + scores[scores.Length - 1]);

            //================================

            //Point[] points = new Point[3];
            //points[0].X = 3;
            //points[0].Y = 4;
            ////System.InvalidOperationException: '無法比較陣列中的兩個元素。'

            //Array.Sort(points);

            //=================================


        }

        int MyMinScore(int[] nums)
        {
            return 10;
        }

        private void button19_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";
            List<int> a = new List<int>();
            for (int i = 0; i < 49; i++) a.Add(i + 1);
            for (int i = 0; i < 6; i++)
            {
                Random random = new Random(DateTime.Now.Millisecond + i);
                int b = random.Next(49 - i);
                lblResult.Text += a[b] + " ";
                a.RemoveAt(b);
                // MessageBox.Show("" + a[b]);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            lblResult.Text = "arr0711_Str陣列 : mother張 emma 迪克蕭 J40 Candy Cindy Coconut Motherfacker\n";
            int count_str = 0;
            string result1 = "\"C\"及c的名字共有:";
            string result2 = "";
            foreach (string item in arr0711_Str)
            {
                if (item.IndexOf("C") != -1 || item.IndexOf("c") != -1)
                {
                    result2 += item + " ";
                    count_str++;
                }
            }
            result1 += count_str + "個:\n";
            lblResult.Text += result1 + result2;
            count_str = 0;
        }
        public string EvenOrOdd(int num)
        {
            string result;
            switch (num % 2)
            {
                case 0:
                    result = "輸入的數 " + num + " 為偶數";
                    break;
                case 1:
                    result = "輸入的數 " + num + " 為奇數";
                    break;
                case -1:
                    result = "輸入的數 " + num + " 為奇數";
                    break;
                default:
                    result = "錯誤";
                    break;
            }
            return result;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            lblResult.Text = "結果";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int sum = 0;
            if (int.TryParse(textBox1.Text, out int from) == false)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            if (int.TryParse(textBox2.Text, out int to) == false)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            if (to < from)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            if (int.TryParse(textBox3.Text, out int step) == false)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            if (step <= 0)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            lblResult.Text = "";
            lblResult.Text += from + " 到 " + to + " 相隔 " + (step - 1) + "\n";
            for (int i = from; i <= to; i += step) sum += i;
            lblResult.Text += "加總為 " + sum;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            int sum = 0;
            if (int.TryParse(textBox1.Text, out int from) == false)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            if (int.TryParse(textBox2.Text, out int to) == false)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            if (to < from)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            if (int.TryParse(textBox3.Text, out int step) == false)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            if (step <= 0)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            lblResult.Text = "";
            lblResult.Text += from + " 到 " + to + " 相隔 " + (step - 1) + "\n";
            //for (int i = from; i <= to; i += step) sum += i;
            int i = from;
            while (i <= to)
            {
                sum += i;
                i += step;
            }
            lblResult.Text += "加總為 " + sum;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int sum = 0;
            if (int.TryParse(textBox1.Text, out int from) == false)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            if (int.TryParse(textBox2.Text, out int to) == false)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            if (to < from)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            if (int.TryParse(textBox3.Text, out int step) == false)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            if (step <= 0)
            {
                MessageBox.Show("格式輸入錯誤");
                return;
            }
            lblResult.Text = "";
            lblResult.Text += from + " 到 " + to + " 相隔 " + (step - 1) + "\n";
            //while (i <= to)
            //{
            //    sum += i;
            //    i += step;
            //}
            int i = from;
            do
            {
                sum += i;
                i += step;
            }
            while (i <= to);
            lblResult.Text += "加總為 " + sum;

        }

        private void button12_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";
            for (int i = 1; i <= 9; i++)
            {
                for (int j = 1; j <= 9; j++)
                {
                    lblResult.Text += i + "x" + j + "=" + (i * j).ToString().PadLeft(2, '0') + " \t ";// .ToString("000")=.ToString().PadLeft(3,'0')
                    if (j == 9) lblResult.Text += "\n";
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            lblResult.Text = "";
            lblResult.Text = DecimalIntoBinary(100).ToString();
        }
        public int DecimalIntoBinary(int num)
        {
            int result = 0;
            int count = 0;
            while (num > 2)
            {
                int temp = num;
                while (temp / 2 > 0)
                {
                    count++;
                    temp = temp / 2;
                }
                result = (int)(result + Math.Pow(10, count));
                num = (int)(num - Math.Pow(2, count));
                count = 0;
            }
            return result;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int max = arr0711.Max();
            lblResult.Text = "int[]陣列 arr0711 = { 1, 5, 6, 8, 7, 97, 54, 887, 65, 578 }";
            lblResult.Text += "\n最大值為" + max;

        }
    }
}
