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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        Random rd = new Random();
        public void difficulty(int d)
        {
            switch(d)
            {
                case 1:
                    dc = 1;
                    break;
                case 2:
                    dc = 2;
                    break;
                case 3:
                    dc = 3;
                    break;
                default:
                    break;
            }
        }
        string name = "";
        public void username(string s)
        {
            name = s;
        }
        int dc = 0;
        int[,] text = new int[3, 3];
        string cnstr = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database2.mdf;Initial Catalog = Database2;Integrated Security=True";
        private void Form2_Load(object sender, EventArgs e)
        {
            pi1 = new PictureBox[3,3] { {pictureBox1,pictureBox2,pictureBox3 },
                                        {pictureBox5,pictureBox4,pictureBox6 },
                                        {pictureBox7,pictureBox8,pictureBox9 } };
            foreach(PictureBox p in pi1)
            {
                p.Size = new Size(121, 121);
            }
            baseform();
            baseform();
            baseform();
            label2.Text = "0";
            
            
            try
            {
                SqlConnection cn = new SqlConnection(cnstr);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = $"select top 1 Score from 歷史成績 where grade = 2 order by Score desc";
                cmd.Connection = cn;
                SqlDataReader sread = cmd.ExecuteReader();

                if(sread.Read())
                {
                    label4.Text = sread["Score"].ToString();
                }
                cn.Close();
                 
            }
            catch
            {
                label4.Text = "0";
            }
            axWindowsMediaPlayer1.Visible = false;
            playback();

            
        }
        ArrayList content = new ArrayList();
        private void baseform()
        {
            foreach(PictureBox p in pi1)
            {
                p.Image = null;
            }
            content.Clear();
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<3;j++)
                {
                    if(text[i,j] == 0)
                    {
                        content.Add(i * 3 + j + 1);
                    }
                }
            }
            if(content.Count>0)
            {
                int place = int.Parse(content[rd.Next(0, content.Count - 1)].ToString());
                int i0 = (place - 1) / 3;
                int j0 = (place - 1) - i0 * 3;
                int ran = rd.Next(1, 10);
                if(Enumerable.Range(1,5).Contains(ran))
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
            for(int i=0;i<3;i++)
            {
                for(int j=0;j<3;j++)
                {
                    switch(text[i,j])
                    {
                        case 0:
                            pi1[i, j].BackColor = Color.LightYellow;
                            
                            break;
                        case 1:
                            pi1[i, j].BackColor = Color.LemonChiffon;
                            pi1[i, j].Image = imageList2.Images[0];
                            break;
                        case 2:
                            pi1[i, j].BackColor = Color.MediumSlateBlue;
                            pi1[i, j].Image = imageList2.Images[1];
                            break;
                        case 4:
                            pi1[i, j].BackColor = Color.Green;
                            pi1[i, j].Image = imageList2.Images[2];
                            break;
                        case 8:
                            pi1[i, j].BackColor = Color.LightGreen;
                            pi1[i, j].Image = imageList2.Images[3];
                            break;
                        case 16:
                            pi1[i, j].BackColor = Color.DarkOliveGreen;
                            pi1[i, j].Image = imageList2.Images[4];
                            break;
                        case 32:
                            pi1[i, j].BackColor = Color.Yellow;
                            pi1[i, j].Image = imageList2.Images[5];
                            break;
                        case 64:
                            pi1[i, j].BackColor = Color.Cyan;
                            pi1[i, j].Image = imageList2.Images[6];
                            break;
                        case 128:
                            pi1[i, j].BackColor = Color.Blue;
                            pi1[i, j].Image = imageList2.Images[7];
                            break;
                        case 256:
                            pi1[i, j].BackColor = Color.DarkViolet;
                            pi1[i, j].Image = imageList2.Images[8];
                            break;
                        case 512:
                            this.Hide();
                            setdata();
                            Form4 f4 = new Form4();
                            f4.Show();
                            f4.score(label2.Text, label4.Text, 2);
                            player.Stop();
                            break;
                    }
                    pi1[i, j].Size = new Size(90, 90);
                }
            }
        }
        private int combine()
        {
            int a = 0;
            int b = 0;
            int c = 0;
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(text[i,j] == 0)
                    {
                        a++;
                    }
                    else
                    {
                        for(int k = j+1; k < 3; k++)
                        {
                            if(text[i,k] != 0)
                            {
                                int r = text[i, j];
                                int e = text[i, k];
                                if(text[i,j] == text[i,k])
                                {
                                    b = 1;
                                    
                                }
                                break;
                            }                         
                        }
                        
                    }
                }
            }
            for(int j = 0; j < 3; j++)
            {
                for(int i = 0; i < 3; i++)
                {
                    if(text[i,j] == 0)
                    {

                    }
                    else
                    {
                        for (int k = i + 1; k < 3; k++)
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

            if(a == 1 && c == 0 && b == 0)
            {
                return 2;
            }
            else if(a == 1 && b == 1 && c == 1)
            {
                return 4;
            }
            else if(a == 1 && b == 1)
            {
                return 1;
            }
            else if(a == 1 && c == 1)
            {
                return 3;
            }
            else
            {
                return 4;
            }
        }
        PictureBox[,] pi1 = new PictureBox[3,3];
        PictureBox[] pi2 = new PictureBox[16];
        PictureBox[] pi3 = new PictureBox[36];
        private void button4_Click(object sender, EventArgs e)
        {
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
                player.Stop();
                f3.score(label2.Text,label4.Text,2);
                f3.Show();
            }
            else if(c == 3)
            {
                switch(keyData)
                {
                    case Keys.Up:
                        up();
                        break;
                    case Keys.Down:
                        down();
                        break;
                }
            }
            else if(c == 4)
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
        private void up()
        {
            bool same = false;
            bool white = false;
            for(int j = 0; j < 3; j++)
            {
                int whitecount = 0;
                for(int i = 0; i < 3; i++)
                {
                    if(text[i,j] == 0)
                    {
                        whitecount++;
                    }
                    else
                    {
                        if(i+1 < 4)
                        {
                            bool c = false;
                            for(int k = i+1; k < 3; k++)
                            {
                                if(text[i,j] == text[k,j])
                                {
                                    text[i, j] = 2 * text[i, j];
                                    text[k, j] = 0;
                                    label2.Text = (int.Parse(label2.Text) + text[i, j]).ToString();
                                    c = true;
                                    if(whitecount != 0)
                                    {
                                        text[i- whitecount, j] = text[i, j];
                                        text[i, j] = 0;
                                    }
                                    break;
                                }
                                else if(text[k,j] == 0)
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
                            if(whitecount != 0)
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
            for(int j = 0; j < 3; j++)
            {
                int whitecount = 0;
                for(int i = 2; i >= 0; i--)
                {
                    if(text[i,j] == 0)
                    {
                        whitecount++;
                    }
                    else
                    {
                        if(i - 1 >= 0)
                        {
                            bool c = false;
                            for(int k = i-1; k >= 0; k--)
                            {
                                if(text[i,j] == text[k,j])
                                {
                                    text[i, j] = 2 * text[i, j];
                                    text[k, j] = 0;
                                    label2.Text = (int.Parse(label2.Text) + text[i, j]).ToString();
                                    c = true;
                                    if(whitecount != 0)
                                    {
                                        text[i + whitecount, j] = text[i, j];
                                        text[i, j] = 0;
                                    }
                                    break;
                                }
                                else if(text[k,j] == 0)
                                {
                                    continue;
                                }
                                break;
                            }
                            if(whitecount != 0 && c == false)
                            {
                                text[i + whitecount, j] = text[i, j];
                                text[i, j] = 0;
                            }
                        }
                        else
                        {
                            if(whitecount != 0)
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
            for(int i = 0; i < 3; i++)
            {
                int whitecount = 0;
                for(int j =0; j < 3; j++)
                {
                    if(text[i,j] == 0)
                    {
                        whitecount++;
                    }
                    else
                    {
                        
                        if (j + 1 < 4)
                        {
                            bool c = false;
                            for (int k = j + 1; k < 3; k++)
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
                            if (whitecount != 0 )
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
            for(int i = 0; i < 3; i++)
            {
                int whitecount = 0;
                for(int j = 2; j >= 0; j--)
                {
                    if(text[i,j] == 0)
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
                            if (whitecount != 0 )
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
            if(int.Parse(label2.Text) > int.Parse(label4.Text))
            {
                label4.Text = label2.Text;
            }
        }
        private void setdata()
        {
            try
            {
                SqlConnection cn = new SqlConnection(cnstr);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                string time = DateTime.Now.ToString("s");
                string s = name;

                cmd.CommandText = $"INSERT INTO 歷史成績(Time,Score,Player,grade)VALUES('{time.Replace("'", "''")}',{label2.Text},'{name}',2)";
                //cmd.CommandText = $"INSERT INTO 歷史成績(Time,Score,grade)VALUES('{time.Replace("'", "''")}',{label2.Text},2)";

                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
                cn.Close();
            }
            catch
            {
                MessageBox.Show("新增資料失敗");
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

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }
        int t = 0;
        private void pictureBox10_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_TextChanged(object sender, EventArgs e)
        {
            playplus();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            t++;
            toolStripButton1.Image = imageList1.Images[t % 2];
            switch (t % 2)
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
            player.Stop();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void 結束遊戲ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void 遊戲說明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("使用↑、↓、←、→鍵控制所有方塊向同一個方向移動，兩個相同圖片的方塊撞在一起之後，就會合併成為下一個階段植物的圖片，當成功合併兩張”枯萎的樹”即為過關。");

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            player.Stop();
            Form2 f2 = new Form2();
            f2.Show();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            this.Hide();
            player.Stop();
            Form1 f1 = new Form1();
            f1.Show();
        }

        private void 排行榜ToolStripMenuItem_Click(object sender, EventArgs e)
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
                    score += t + "\t" + sread["Player"].ToString() + "\t" + sread["Score"].ToString() + "\r\n";
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
