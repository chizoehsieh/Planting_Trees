using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace final_project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        Form2 f2 = new Form2();
        Form5 f5 = new Form5();
        Form9 f9 = new Form9();

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            f9.Show();
            f9.username(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            f5.Show();
            f5.username(textBox1.Text);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            f2.Show();
            f2.username(textBox1.Text);
        }
        Random rd = new Random();
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] name = new string[] { "Jame", "Mark", "Carol", "Brian", "Leo", "Angel", "Tom", "Melody" };
            int r = rd.Next(0, name.Length - 1);
            textBox1.Text = name[r];
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            int ch = Convert.ToInt32(e.KeyChar);
            if((ch > 64 && ch < 91) || (ch > 96 && ch < 123))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
