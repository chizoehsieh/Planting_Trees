using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using System.Data.SqlClient;

namespace final_project
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }
        int[,] text = new int[4, 4];
        PictureBox[,] pi = new PictureBox[4, 4];
        Random rd = new Random();
        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database2.mdf;Initial Catalog = Database2;Integrated Security=True";

        string name = "";
        public void username(string s)
        {
            name = s;
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            pi = new PictureBox[4, 4] { { pictureBox1,pictureBox2,pictureBox3,pictureBox4},
                                      {pictureBox5,pictureBox6,pictureBox7,pictureBox8 },
                                      {pictureBox9,pictureBox11,pictureBox12,pictureBox13 },
                                      {pictureBox14,pictureBox15,pictureBox16,pictureBox17 }
            };
            baseform();
            baseform();
            baseform();
            label2.Text = "0";
            try
            {
                SqlConnection cn = new SqlConnection(cnstr);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $"select top 1 Score from 歷史成績 where grade = 5 order by Score desc";
                cmd.Connection = cn;
                SqlDataReader sread = cmd.ExecuteReader();

                if (sread.Read())
                {
                    label4.Text = sread["Score"].ToString();
                }
                cn.Close();

            }
            catch
            {
                label4.Text = "0";
            }
            playback();
            axWindowsMediaPlayer1.Visible = false;
        }

        ArrayList content = new ArrayList();
        private void baseform()
        {
            foreach (PictureBox p in pi)
            {
                p.Image = null;
            }
            content.Clear();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (text[i, j] == 0)
                    {
                        content.Add(i * 4 + j + 1);
                    }
                }
            }
            if (content.Count > 0)
            {
                int place = int.Parse(content[rd.Next(0, content.Count - 1)].ToString());
                int i0 = (place - 1) / 4;
                int j0 = (place - 1) - i0 * 4;
                int ran = rd.Next(1, 10);
                if (Enumerable.Range(1, 5).Contains(ran))
                {
                    text[i0, j0] = 1;
                }
                else
                {
                    text[i0, j0] = 2;
                }
            }
            setpicture();
        }

        private void setpicture()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    switch (text[i, j])
                    {
                        case 0:
                            pi[i, j].BackColor = Color.LightYellow;

                            break;
                        case 1:
                            pi[i, j].BackColor = Color.LemonChiffon;
                            pi[i, j].Image = imageList2.Images[0];
                            break;
                        case 2:
                            pi[i, j].BackColor = Color.MediumSlateBlue;
                            pi[i, j].Image = imageList2.Images[1];
                            break;
                        case 4:
                            pi[i, j].BackColor = Color.Green;
                            pi[i, j].Image = imageList2.Images[2];
                            break;
                        case 8:
                            pi[i, j].BackColor = Color.LightGreen;
                            pi[i, j].Image = imageList2.Images[3];
                            break;
                        case 16:
                            pi[i, j].BackColor = Color.DarkOliveGreen;
                            pi[i, j].Image = imageList2.Images[4];
                            break;
                        case 32:
                            pi[i, j].BackColor = Color.Yellow;
                            pi[i, j].Image = imageList2.Images[5];
                            break;
                        case 64:
                            pi[i, j].BackColor = Color.Cyan;
                            pi[i, j].Image = imageList2.Images[6];
                            break;
                        case 128:
                            pi[i, j].BackColor = Color.Blue;
                            pi[i, j].Image = imageList2.Images[7];
                            break;
                        case 256:
                            pi[i, j].BackColor = Color.DarkViolet;
                            pi[i, j].Image = imageList2.Images[8];
                            break;
                        case 512:
                            this.Hide();
                            setdata();
                            Form4 f4 = new Form4();
                            f4.Show();
                            f4.score(label2.Text, label4.Text, 5);
                            axWindowsMediaPlayer1.settings.mute = true;
                            player.Stop();
                            break;
                    }
                    //pi[i, j].Size = new Size(90, 90);
                    pi[i, j].SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
        int t = 0;

        private void setdata()
        {
            try
            {
                SqlConnection cn = new SqlConnection(cnstr);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                string time = DateTime.Now.ToString("s");
                string s = name;
                
                cmd.CommandText = $"INSERT INTO 歷史成績(Time,Score,Player,grade)VALUES('{time.Replace("'", "''")}',{label2.Text},'{name}',5)";
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {
                MessageBox.Show("新增資料失敗");
            }
        }
        Form3 f3 = new Form3();
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int c = combine();
            if (c == 1)
            {
                switch (keyData)
                {

                    case Keys.Left:
                        left();
                        break;
                    case Keys.Right:
                        right();
                        break;
                }
            }
            else if (c == 2)
            {
                this.Hide();
                setdata();
                f3.score(label2.Text, label4.Text, 5);
                f3.Show();
                axWindowsMediaPlayer1.settings.mute = true;
                player.Stop();
            }
            else if (c == 3)
            {
                switch (keyData)
                {
                    case Keys.Up:
                        up();
                        break;
                    case Keys.Down:
                        down();
                        break;
                }
            }
            else if (c == 4)
            {
                switch (keyData)
                {
                    case Keys.Up:
                        up();
                        break;
                    case Keys.Down:
                        down();
                        break;
                    case Keys.Left:
                        left();
                        break;
                    case Keys.Right:
                        right();
                        break;
                }
            }
            orderscore();
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private int combine()
        {
            int a = 0;
            int b = 0;
            int c = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (text[i, j] == 0)
                    {
                        a++;
                    }
                    else
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (text[i, k] != 0)
                            {
                                int r = text[i, j];
                                int e = text[i, k];
                                if (text[i, j] == text[i, k])
                                {
                                    b = 1;

                                }
                                break;
                            }
                        }

                    }
                }
            }
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (text[i, j] == 0)
                    {

                    }
                    else
                    {
                        for (int k = i + 1; k < 4; k++)
                        {
                            if (text[k, j] != 0)
                            {
                                if (text[i, j] == text[k, j])
                                {
                                    c = 1;
                                }
                                break;
                            }
                        }
                    }
                }
            }

            if (a == 1 && c == 0 && b == 0)
            {
                return 2;
            }
            else if (a == 1 && b == 1 && c == 1)
            {
                return 4;
            }
            else if (a == 1 && b == 1)
            {
                return 1;
            }
            else if (a == 1 && c == 1)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }

        private void up()
        {
            for (int j = 0; j < 4; j++)
            {
                int whitecount = 0;
                for (int i = 0; i < 4; i++)
                {
                    if (text[i, j] == 0)
                    {
                        whitecount++;
                    }
                    else
                    {
                        if (i + 1 < 5)
                        {
                            bool c = false;
                            for (int k = i + 1; k < 4; k++)
                            {
                                if (text[i, j] == text[k, j])
                                {
                                    text[i, j] = 2 * text[i, j];
                                    text[k, j] = 0;
                                    label2.Text = (int.Parse(label2.Text) + text[i, j]).ToString();
                                    c = true;
                                    if (whitecount != 0)
                                    {
                                        text[i - whitecount, j] = text[i, j];
                                        text[i, j] = 0;
                                    }
                                    break;
                                }
                                else if (text[k, j] == 0)
                                {
                                    continue;
                                }
                                break;
                            }
                            if (whitecount != 0 && c == false)
                            {
                                text[i - whitecount, j] = text[i, j];
                                text[i, j] = 0;
                            }
                        }
                        else
                        {
                            if (whitecount != 0)
                            {
                                int a = i;
                                int b = j;
                                int d = whitecount;
                                text[i - whitecount, j] = text[i, j];
                                text[i, j] = 0;

                            }
                        }
                    }
                }
            }
            baseform();
        }

        private void down()
        {
            for (int j = 0; j < 4; j++)
            {
                int whitecount = 0;
                for (int i = 3; i >= 0; i--)
                {
                    if (text[i, j] == 0)
                    {
                        whitecount++;
                    }
                    else
                    {
                        if (i - 1 >= 0)
                        {
                            bool c = false;
                            for (int k = i - 1; k >= 0; k--)
                            {
                                if (text[i, j] == text[k, j])
                                {
                                    text[i, j] = 2 * text[i, j];
                                    text[k, j] = 0;
                                    label2.Text = (int.Parse(label2.Text) + text[i, j]).ToString();
                                    c = true;
                                    if (whitecount != 0)
                                    {
                                        text[i + whitecount, j] = text[i, j];
                                        text[i, j] = 0;
                                    }
                                    break;
                                }
                                else if (text[k, j] == 0)
                                {
                                    continue;
                                }
                                break;
                            }
                            if (whitecount != 0 && c == false)
                            {
                                text[i + whitecount, j] = text[i, j];
                                text[i, j] = 0;
                            }
                        }
                        else
                        {
                            if (whitecount != 0)
                            {
                                text[i + whitecount, j] = text[i, j];
                                text[i, j] = 0;
                            }

                        }
                    }
                }
            }
            baseform();
        }

        private void left()
        {
            for (int i = 0; i < 4; i++)
            {
                int whitecount = 0;
                for (int j = 0; j < 4; j++)
                {
                    if (text[i, j] == 0)
                    {
                        whitecount++;
                    }
                    else
                    {

                        if (j + 1 < 5)
                        {
                            bool c = false;
                            for (int k = j + 1; k < 4; k++)
                            {
                                if (text[i, j] == text[i, k])
                                {
                                    text[i, j] = 2 * text[i, j];
                                    text[i, k] = 0;
                                    label2.Text = (int.Parse(label2.Text) + text[i, j]).ToString();
                                    c = true;
                                    if (whitecount != 0)
                                    {
                                        text[i, j - whitecount] = text[i, j];
                                        text[i, j] = 0;
                                    }
                                    break;
                                }
                                else if (text[i, k] == 0)
                                {
                                    continue;
                                }
                                break;
                            }
                            if (whitecount != 0 && c == false)
                            {
                                text[i, j - whitecount] = text[i, j];
                                text[i, j] = 0;
                            }

                        }
                        else
                        {
                            if (whitecount != 0)
                            {
                                text[i, j - whitecount] = text[i, j];
                                text[i, j] = 0;
                            }
                        }

                    }
                }
            }
            baseform();
        }

        private void right()
        {
            for (int i = 0; i < 4; i++)
            {
                int whitecount = 0;
                for (int j = 3; j >= 0; j--)
                {
                    if (text[i, j] == 0)
                    {
                        whitecount++;
                    }
                    else
                    {

                        if (j - 1 >= 0)
                        {
                            bool c = false;
                            for (int k = j - 1; k >= 0; k--)
                            {
                                if (text[i, j] == text[i, k])
                                {
                                    text[i, j] = 2 * text[i, j];
                                    text[i, k] = 0;
                                    label2.Text = (int.Parse(label2.Text) + text[i, j]).ToString();
                                    c = true;
                                    if (whitecount != 0)
                                    {
                                        text[i, j + whitecount] = text[i, j];
                                        text[i, j] = 0;
                                    }
                                    break;

                                }
                                else if (text[i, k] == 0)
                                {
                                    continue;
                                }
                                break;
                            }
                            if (whitecount != 0 && c == false)
                            {
                                text[i, j + whitecount] = text[i, j];
                                text[i, j] = 0;
                            }

                        }
                        else
                        {
                            if (whitecount != 0)
                            {
                                text[i, j + whitecount] = text[i, j];
                                text[i, j] = 0;
                            }
                        }

                    }
                }
            }
            baseform();
        }

        private void orderscore()
        {
            if (int.Parse(label2.Text) > int.Parse(label4.Text))
            {
                label4.Text = label2.Text;
            }
        }
        System.Media.SoundPlayer player = new System.Media.SoundPlayer("7874.wav");

        private void playback()
        {
            
            player.PlayLooping();
        }

        private void playplus()
        {
            axWindowsMediaPlayer1.URL = "12907.wav";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            t++;
            toolStripButton1.Image = imageList1.Images[t % 2];
            switch(t % 2)
            {
                case 0:
                    axWindowsMediaPlayer1.settings.mute = false;
                    player.PlayLooping();
                    break;
                case 1:
                    axWindowsMediaPlayer1.settings.mute = true;
                    player.Stop();
                    break;
            }
        }

        private void 新遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
            axWindowsMediaPlayer1.settings.mute = true;
            player.Stop();
        }

        private void 結束遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
            playplus();
        }

        private void 遊戲說明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("使用↑、↓、←、→鍵控制所有方塊向同一個方向移動，兩個相同圖片的方塊撞在一起之後，就會合併成為下一個階段植物的圖片，當成功合併兩張”枯萎的樹”即為過關。");
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            player.Stop();
            Form5 f5 = new Form5();
            f5.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.Show();
            axWindowsMediaPlayer1.settings.mute = true;
            player.Stop();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void 排行榜ToolStripMenuItem_Click(object sender, EventArgs e)
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
    }
}
