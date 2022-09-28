using System.Drawing;
namespace tic_tac_toe
{
    public partial class Form1 : Form
    {
        static int n = 10;
        static int nn = 5;
        int k = 0;
        int[,] tictak = new int[n, n];
        int check = 0;
        int player1 = 0;
        int player2 = 0;
        string log;
        string path = "logs.txt";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
         
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, 0, 0, 0, 600);
            e.Graphics.DrawLine(Pens.Black, 0, 600, 600, 600);
            e.Graphics.DrawLine(Pens.Black, 600, 600, 600, 0);
            e.Graphics.DrawLine(Pens.Black, 600, 0, 0, 0);
            int sizeX = 600 / n;
            int sizeY = 600 / n;
            int temp = 0;
            for (int i = 0; i < (n-1); i++) 
            {
                e.Graphics.DrawLine(Pens.Black, temp + sizeX, 0, temp + sizeX, 600);
                temp += sizeX;
            }
            temp = 0;
            for (int i = 0; i < (n - 1); i++)
            {
                e.Graphics.DrawLine(Pens.Black, 0, temp + sizeY, 600, temp + sizeY);
                temp += sizeY;
            }
            string[] ReadingFile = File.ReadAllLines(path);
            string[] ArrayFile;
            int centerX;
            int centrY;
            for (int i = 0; (i < ReadingFile.Length); i++)
            {
                ArrayFile = ReadingFile[i].Split(' '); // 0 - player1, 1 - player2, 2 - krest / nol, 3 - centerX, 4 - centrY, 5 - nX, 6 - nY
                player1 = int.Parse(ArrayFile[0]);
                player2 = int.Parse(ArrayFile[1]);
                if (ArrayFile[2] == "krest")
                {
                    centerX = int.Parse(ArrayFile[3]);
                    centrY = int.Parse(ArrayFile[4]);
                    e.Graphics.DrawLine(Pens.Blue, centerX, centrY, centerX + (600 / n / 2 - 10), centrY + (600 / n / 2 - 10));
                    e.Graphics.DrawLine(Pens.Blue, centerX, centrY, centerX + (600 / n / 2 - 10), centrY - (600 / n / 2 - 10));
                    e.Graphics.DrawLine(Pens.Blue, centerX, centrY, centerX - (600 / n / 2 - 10), centrY + (600 / n / 2 - 10));
                    e.Graphics.DrawLine(Pens.Blue, centerX, centrY, centerX - (600 / n / 2 - 10), centrY - (600 / n / 2 - 10));
                    k++;
                    tictak[int.Parse(ArrayFile[5]), int.Parse(ArrayFile[6])] = 1;
                }

                if (ArrayFile[2] == "nol")
                {
                    centerX = int.Parse(ArrayFile[3]);
                    centrY = int.Parse(ArrayFile[4]);
                    e.Graphics.DrawEllipse(Pens.Green, centerX - 600 / n / 2 + 10, centrY - 600 / n / 2 + 10, 600 / n - 20, 600 / n - 20);
                    k++;
                    tictak[int.Parse(ArrayFile[5]), int.Parse(ArrayFile[6])] = 2;

                }

            }
        }

        Point click;
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            int flag = 1;
            click = e.Location;
            int nX = e.X / (600 / n);
            int nY = e.Y / (600 / n);
            if (tictak[nX, nY] == 1 || tictak[nX, nY] == 2)
            {
                flag = 0;
            }
            if (flag == 1)
            {
                var pic = (PictureBox)sender;
                var g = pic.CreateGraphics();
                k += 1;
                if (k % 2 == 1)
                    tictak[nX, nY] = 1;
                else
                    tictak[nX, nY] =  2;
                int centerX = nX * (600 / n) + (600 / n) / 2;
                int centrY = nY * (600 / n) + (600 / n) / 2;

                if (k % 2 == 1)
                {
                    g.DrawLine(Pens.Blue, centerX, centrY, centerX + (600 / n / 2 - 10), centrY + (600 / n / 2 - 10));
                    g.DrawLine(Pens.Blue, centerX, centrY, centerX + (600 / n / 2 - 10), centrY - (600 / n / 2 - 10));
                    g.DrawLine(Pens.Blue, centerX, centrY, centerX - (600 / n / 2 - 10), centrY + (600 / n / 2 - 10));
                    g.DrawLine(Pens.Blue, centerX, centrY, centerX - (600 / n / 2 - 10), centrY - (600 / n / 2 - 10));
                    log = player1.ToString() + ' ' + player2.ToString() + ' ' + "krest" + ' ' + centerX.ToString() + ' ' + centrY.ToString() + ' ' + nX.ToString() + ' ' + nY.ToString() + '\n';
                    File.AppendAllText(path, log);
                }

                else
                {
                    g.DrawEllipse(Pens.Green, centerX - 600 / n / 2 + 10, centrY - 600 / n / 2 + 10, 600 / n - 20, 600 / n - 20);
                    log = player1.ToString() + ' ' + player2.ToString() + ' ' + "nol" + ' ' + centerX.ToString() + ' ' + centrY.ToString() + ' ' + nX.ToString() + ' ' + nY.ToString() + '\n';
                    File.AppendAllText(path, log);
                }

                int krestcount;
                int nolcount;

                if (check == 0)
                {
                    // по вертикали
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j <= n - nn; j++)
                        {
                            krestcount = 0;
                            nolcount = 0;
                            for (int k = j; k < j + nn; k++)
                            {
                                //g.DrawLine(Pens.Orange, 100, 200, 300, 400);
                                if (tictak[i, k] == 1) krestcount++;
                                if (tictak[i, k] == 2) nolcount++;
                            }
                            if (krestcount == nn || nolcount == nn)
                            {
                                g.DrawLine(Pens.Red, 600 / n * nX + 600 / n / 2, 600 / n * j, 600 / n * nX + 600 / n / 2, 600 / n * j + nn * 600 / n);
                                check = 1;
                                if (krestcount == nn)
                                {
                                    MessageBox.Show("Победил Игрок 1", "Сообщение");
                                    player1++;
                                    textBox5.Text = player1.ToString();
                                }
                                if (nolcount == nn)
                                {
                                    MessageBox.Show("Победил Игрок 2", "Сообщение");
                                    player2++;
                                    textBox4.Text = player2.ToString();
                                }
                            }
                        }
                    }
                }
                
                
                if (check == 0)
                {
                    // по горизонтали
                    for (int i = 0; i < n; i++)
                    {
                        for (int j = 0; j <= n - nn; j++)
                        {
                            krestcount = 0;
                            nolcount = 0;
                            for (int k = j; k < j + nn; k++)
                            {
                                if (tictak[k, i] == 1) krestcount++;
                                if (tictak[k, i] == 2) nolcount++;
                            }
                            if (krestcount == nn || nolcount == nn)
                            {
                                g.DrawLine(Pens.Red, 600 / n * j, 600 / n * nY + 600 / n / 2, 600 / n * j + nn * 600 / n, 600 / n * nY + 600 / n / 2);
                                check = 1;
                                if (krestcount == nn)
                                {
                                    MessageBox.Show("Победил Игрок 1", "Сообщение");
                                    player1++;
                                    textBox5.Text = player1.ToString();
                                }
                                if (nolcount == nn)
                                {
                                    MessageBox.Show("Победил Игрок 2", "Сообщение");
                                    player2++;
                                    textBox4.Text = player2.ToString();
                                }

                            }
                        }
                    }
                }
                
                if (check == 0)
                {
                    // диагональ вправо вниз
                    for (int i = 0; i <= n - nn; i++)
                    {
                        for (int x = i, y = 0; x <= n - nn; x++, y++)
                        {
                            krestcount = 0;
                            nolcount = 0;
                            for (int k = x, t = y; k < x + nn; k++, t++)
                            {
                                if (tictak[k, t] == 1) krestcount++;
                                if ((tictak[k, t] == 2)) nolcount++;
                            }
                            if (krestcount == nn || nolcount == nn)
                            {
                                g.DrawLine(Pens.Red, 600 / n * x, 600 / n * y, 600 / n * x + 600 / n * nn, 600 / n * y + 600 / n * nn);
                                check = 1;
                                if (krestcount == nn)
                                {
                                    MessageBox.Show("Победил Игрок 1", "Сообщение");
                                    player1++;
                                    textBox5.Text = player1.ToString();
                                }
                                if (nolcount == nn)
                                {
                                    MessageBox.Show("Победил Игрок 2", "Сообщение");
                                    player2++;
                                    textBox4.Text = player2.ToString();
                                }
                            }
                        }
                    }
                }
                
                if (check == 0)
                {
                    // по вертикали вправо вниз
                    for (int i = 0; i <= n - nn; i++)
                    {
                        for (int x = i, y = 0; x <= n - nn; x++, y++)
                        {
                            krestcount = 0;
                            nolcount = 0;
                            for (int k = x, t = y; k < x + nn; k++, t++)
                            {
                                if (tictak[t, k] == 1) krestcount++;
                                if ((tictak[t, k] == 2)) nolcount++;
                            }
                            if (krestcount == nn || nolcount == nn)
                            {
                                g.DrawLine(Pens.Red, 600 / n * y, 600 / n * x, 600 / n * y + 600 / n * nn, 600 / n * x + 600 / n * nn);
                                check = 1;
                                if (krestcount == nn)
                                {
                                    MessageBox.Show("Победил Игрок 1", "Сообщение");
                                    player1++;
                                    textBox5.Text = player1.ToString();
                                }
                                if (nolcount == nn)
                                {
                                    MessageBox.Show("Победил Игрок 2", "Сообщение");
                                    player2++;
                                    textBox4.Text = player2.ToString();
                                }
                            }
                        }
                    }
                }
                
                if (check == 0)
                {
                    // диагональ по горизонтали вниз
                    for (int i = n - 1; i >= 0; i--)
                    {
                        for (int x = i, y = 0; x + 1 >= nn; x--, y++)
                        {
                            krestcount = 0;
                            nolcount = 0;
                            for (int k = x, t = y; k > x - nn; k--, t++)
                            {
                                if (tictak[k, t] == 1) krestcount++;
                                if ((tictak[k, t] == 2)) nolcount++;
                            }
                            if (krestcount == nn || nolcount == nn)
                            {
                                g.DrawLine(Pens.Red, 600 / n * x + 600 / n, 600 / n * y, 600 / n * x + 600 / n - 600 / n * nn, 600 / n * y + 600 / n * nn);
                                check = 1;
                                if (krestcount == nn)
                                {
                                    MessageBox.Show("Победил Игрок 1", "Сообщение");
                                    player1++;
                                    textBox5.Text = player1.ToString();
                                }
                                if (nolcount == nn)
                                {
                                    MessageBox.Show("Победил Игрок 2", "Сообщение");
                                    player2++;
                                    textBox4.Text = player2.ToString();
                                }
                            }
                        }
                    }
                }
                
                if (check == 0)
                {
                    for (int i = 0; i <= n - nn; i++)
                    {
                        for (int x = i, y = n - 1; x <= n - nn; x++, y--)
                        {
                            krestcount = 0;
                            nolcount = 0;
                            for (int k = x, t = y; k < x + nn; k++, t--)
                            {
                                if (tictak[t, k] == 1) krestcount++;
                                if ((tictak[t, k] == 2)) nolcount++;
                            }
                            if (krestcount == nn || nolcount == nn)
                            {
                                g.DrawLine(Pens.Red, 600 / n * y + 600 / n, 600 / n * x, 600 / n * y + 600 / n - 600 / n * nn, 600 / n * x + 600 / n * nn);
                                check = 1;
                                if (krestcount == nn)
                                {
                                    MessageBox.Show("Победил Игрок 1", "Сообщение");
                                    player1++;
                                    textBox5.Text = player1.ToString();
                                }
                                if (nolcount == nn)
                                {
                                    MessageBox.Show("Победил Игрок 2", "Сообщение");
                                    player2++;
                                    textBox4.Text = player2.ToString();
                                }
                            }
                        }
                    }
                }
                int countdraw = 0;
                for (int i = 0; i < tictak.Length / n; i++)
                {
                    for (int j = 0; j < tictak.Length / n; j++)
                    {
                        if(tictak[i, j] == 0) countdraw++;
                    }
                }
                if (countdraw == 0)
                    MessageBox.Show("Ничья", "Сообщение");


            }
        }

        
        //Игрок 1
        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
       // Игрок 2
        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox1.Invalidate();
            k = 0;
            check = 0;
            for (int i = 0; i < tictak.Length / n; i++)
            {
                for (int j = 0; j < tictak.Length / n; j++)
                {
                    tictak[i, j] = 0;
                }
            }
            File.WriteAllText(path, String.Empty);
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
            pictureBox1.Invalidate();
            for (int i = 0; i < tictak.Length / n; i++)
            {
                for (int j = 0; j < tictak.Length / n; j++)
                {
                    tictak[i, j] = 0;
                }
            }
            player1 = 0;
            player2 = 0;
            textBox5.Text = "0";
            textBox4.Text = "0";
            k = 0;
            check = 0;
            File.WriteAllText(path, String.Empty);
        }
    }
}