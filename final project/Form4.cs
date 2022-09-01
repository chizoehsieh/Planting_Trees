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

namespace final_project
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form4_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
        }
        int f = 0;
        public void score(string s, string b, int form)
        {
            label2.Text = s;
            label4.Text = b;
            f = form;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            switch (f)
            {
                case 2:
                    hard();
                    break;
                case 5:
                    normal();
                    break;
                case 9:
                    easy();
                    break;
                    
            }
        }
        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database2.mdf;Initial Catalog = Database2;Integrated Security=True";
        private void hard()
        {
            try
            {
                SqlConnection cn = new SqlConnection(cnstr);
                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cn;
                cmd.CommandText = $"select top 10 * from 歷史成績 where grade = 2 order by Score desc";
                SqlDataReader sread = cmd.ExecuteReader();

                string score = "名次\t玩家\t分數\r\n";
                int t = 0;
                while (sread.Read())
                {
                    t++;
                    score += t + "\t" + sread["Player"] + "\t" + sread["Score"].ToString() + "\r\n";
                }
                MessageBox.Show(score);
                cn.Close();
            }
            catch
            {
                MessageBox.Show("無法讀取資料");
            }
        }

        private void normal()
        {
            try
            {
                SqlConnection cn = new SqlConnection(cnstr);
                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cn;
                cmd.CommandText = $"select top 10 * from 歷史成績 where grade = 5 order by Score desc";
                SqlDataReader sread = cmd.ExecuteReader();

                string score = "名次\t玩家\t分數\r\n";
                int t = 0;
                while (sread.Read())
                {
                    t++;
                    score += t + "\t" + sread["Player"] + "\t" + sread["Score"].ToString() + "\r\n";
                }
                MessageBox.Show(score);
                cn.Close();
            }
            catch
            {
                MessageBox.Show("無法讀取資料");
            }
        }

        private void easy()
        {
            try
            {
                SqlConnection cn = new SqlConnection(cnstr);
                cn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cn;
                cmd.CommandText = $"select top 10 * from 歷史成績 where grade = 9 order by Score desc";
                SqlDataReader sread = cmd.ExecuteReader();

                string score = "名次\t玩家\t分數\r\n";
                int t = 0;
                while (sread.Read())
                {
                    t++;
                    score += t + "\t" + sread["Player"] + "\t" + sread["Score"].ToString() + "\r\n";
                }
                MessageBox.Show(score);
                cn.Close();
            }
            catch
            {
                MessageBox.Show("無法讀取資料");
            }
        }
    }
}
