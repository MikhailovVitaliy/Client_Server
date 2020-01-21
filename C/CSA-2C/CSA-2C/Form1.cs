using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace CSA_2C
{
    public partial class Form1 : Form
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        int[,] map = { { 9, 1, 9, 1, 9, 1, 9, 1 },
                       { 1, 9, 1, 9, 1, 9, 1, 9 },
                       { 9, 1, 9, 1, 9, 1, 9, 1 },
                       { 0, 9, 0, 9, 0, 9, 0, 9 },
                       { 9, 0, 9, 0, 9, 0, 9, 0 },
                       { 2, 9, 2, 9, 2, 9, 2, 9 },
                       { 9, 2, 9, 2, 9, 2, 9, 2 },
                       { 2, 9, 2, 9, 2, 9, 2, 9 }};

        //int[,] map = { { 9, 0, 9, 0, 9, 0, 9, 0 },
        //               { 0, 9, 0, 9, 3, 9, 3, 9 },
        //               { 9, 0, 9, 0, 9, 2, 9, 0 },
        //               { 0, 9, 0, 9, 0, 9, 4, 9 },
        //               { 9, 0, 9, 2, 9, 0, 9, 0 },
        //               { 0, 9, 0, 9, 2, 9, 0, 9 },
        //               { 9, 2, 9, 0, 9, 0, 9, 0 },
        //               { 0, 9, 0, 9, 0, 9, 0, 9 }};

        int bCount = 12;     //счетчики количества фигур на старте для каждого из цветов
        int wCount = 12;     //не забывать менять обратно после тестов; шашек в IRL игре на доске на старте 12
        int prebCount = 12;     
        int prewCount = 12;

        public IPAddress address2 = IPAddress.Parse("192.168.0.2");


        int I1 = 500; int J1 = 500;
        int I2 = 500; int J2 = 500;

        int m1 = 0;
        int n1 = 0;
        int m2 = 0;
        int n2 = 0;

        int W = 45;
        int H = 49;
        
        int hod = 0;

        private Form parent;
        string mapStr;
        string TransMess;

        public Form1()
        {
            InitializeComponent();
        }
        public Form1(Form form)
        {
            InitializeComponent();
            parent = form;
        }
        public void gameModeForm(int gameMode)
        {
            if (Form2.gameMode == 1)
            {
                turn.Enabled = false;
            }
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            gameModeForm(Form2.gameMode);
        }
        public string mapConverter(int [,] map)                     //преобразовывает массив 8х8 в строку длиной 64 символа
        {
            mapStr = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    mapStr += map[i, j].ToString();

                }
            }
            return mapStr;
        }
        public void drawer(int[,] mas)                              //отрисовка фигур
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (mas[i, j] == 2)        //отрисовка пешек
                    {
                        g.FillEllipse(new SolidBrush(Color.Goldenrod), 15 + j * W, 15 + i * H, 45, 50);
                        g.DrawEllipse(new Pen(Color.OldLace, 5), 20 + j * W, 20 + i * H, 35, 40);
                    }

                    else
                        if (mas[i, j] == 1)
                    {
                        g.FillEllipse(new SolidBrush(Color.DarkGray), 15 + j * W, 15 + i * H, 45, 50);
                        g.DrawEllipse(new Pen(Color.Black, 5), 20 + j * W, 20 + i * H, 35, 40);
                    }

                    else
                        if (mas[i, j] == 4)         //отрисовка дамок
                    {
                        g.FillEllipse(new SolidBrush(Color.Goldenrod), 15 + j * W, 15 + i * H, 45, 50);
                        g.DrawEllipse(new Pen(Color.OldLace, 5), 20 + j * W, 20 + i * H, 35, 40);
                        g.DrawLine(new Pen(Color.OldLace, 3), 15 + j * W + W / 3, 15 + i * H + H / 2, 15 + j * W + W - (W / 3), 15 + i * H + H / 2);
                        g.DrawLine(new Pen(Color.OldLace, 3), 15 + j * W + W / 2, 15 + i * H + H - (H / 3), 15 + j * W + W / 2, 15 + i * H + H / 3);
                    }

                    else
                        if (mas[i, j] == 3)
                    {
                        g.FillEllipse(new SolidBrush(Color.DarkGray), 15 + j * W, 15 + i * H, 45, 50);
                        g.DrawEllipse(new Pen(Color.Black, 5), 20 + j * W, 20 + i * H, 35, 40);
                        g.DrawLine(new Pen(Color.Black, 3), 15 + j * W + W / 3, 15 + i * H + H / 2, 15 + j * W + W - (W / 3), 15 + i * H + H / 2);
                        g.DrawLine(new Pen(Color.Black, 3), 15 + j * W + W / 2, 15 + i * H + H - (H / 3), 15 + j * W + W / 2, 15 + i * H + H / 3);
                    }
                    else
                        if (mas[i, j] == 0)
                    {
                        g.FillRectangle(new SolidBrush(Color.Black), 15 + j * W, 15 + i * H, 45, 50);     //заливка пустых клеток
                    }
                }
            }
        }
        public void messageUncoder(string mess)
        {
            n1 = Convert.ToInt32(new string(mess[0], 1));
            m1 = Convert.ToInt32(new string(mess[1], 1));
            n2 = Convert.ToInt32(new string(mess[2], 1));
            m2 = Convert.ToInt32(new string(mess[3], 1));
            hod = Convert.ToInt32(new string(mess[4], 1));

            string bufCount;
            bufCount = mess[69].ToString() + mess[70].ToString();
            bCount = Convert.ToInt32(bufCount);
            bufCount = mess[71].ToString() + mess[72].ToString();
            wCount = Convert.ToInt32(bufCount);
            
            //textBox1.Text = (n1 + " " + m1 + " " + n2 + " " + m2 + " " + hod + " " + bCount + " " + wCount);

            for (int i = 5; i <= 68; i++)
            {
                double k = Math.Floor((double)(i - 5) / 8);
                map[(int)k, ((i - 5)) % 8] = Convert.ToInt32(new string(mess[i], 1));
                //Console.WriteLine(map[(int)k, ((i - 5)) % 8]);
            }
        }
        public void translation(string TransMess)                   //передача на сервер
        {
            int port = 8005;                                    // порт сервера
            IPAddress server = IPAddress.Parse("192.168.0.2");
            //string server = "192.168.0.2";                       // адрес сервера
            //string server = "127.0.0.1";
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(server, port);

                byte[] data = new byte[256];
                StringBuilder response = new StringBuilder();
                NetworkStream stream = client.GetStream();

                byte[] data2 = Encoding.Unicode.GetBytes(TransMess);
                stream.Write(data2, 0, data2.Length);

                do
                {
                    int bytes = stream.Read(data, 0, data.Length);
                    response.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (stream.DataAvailable); // пока данные есть в потоке

                //do
                //{
                //    bytes = socket.Receive(data, data.Length, 0);
                //    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                //}
                //while (socket.Available > 0);

                textBox1.Text = response.ToString();             // ← ← ← запись в сообщение
                mapStr = response.ToString();
                prebCount = bCount; 
                prewCount = wCount;
                messageUncoder(mapStr);                         //"раскодируем" сообщение с сервера, присваиваем переменным обновленные значения
                drawer(map);                                    //отрисовываем
                victoryCheck(hod);                              //проверяем, не победил ли кто-то
               // textBox1.Text = "1";
                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            {
                //textBox1.Text = ex.Message;

            }
        }
        public void button1_Click(object sender, EventArgs e)       //пересылка из textBox1 на сервер
        {
            turn.Enabled = false;
            translation(TransMess);
        }
        public void hodChange()                                     //смена хода
        {
            if (hod == 1) { hod = 0; }                              //0 - белые; 1 - черные
            else { hod = 1; }
        }
        public void victoryCheck(int hod)                           //отслеживание победных условий
        {
            if (prebCount != bCount || prewCount != wCount)
            {
                if (bCount == 0) { victoryLabel.Text = "Победа белых"; pictureBox1.Enabled = false; return; }
                if (wCount == 0) { victoryLabel.Text = "Победа чёрных"; pictureBox1.Enabled = false; return; }
            }
        }
        private void start_Click(object sender, EventArgs e)        //начать игру
        {
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            drawer(map);
            pictureBox1.Enabled = (pictureBox1.Enabled)?false:true;
            if (Form2.gameMode == 4) { turn.Enabled = true; }             //сюда можно добавить лок кнопки на совершение чего-то страшного
        }
        private void close_Click(object sender, EventArgs e)        //обработчик кнопки завершить
        {
            this.Close();
        }
        private void turn_Click(object sender, EventArgs e)         //обработчик кнопки ход
        {
            hodChange();
            //textBox1.Text = Form2.gameMode.ToString();
        } 
        private void pictureBox1_Click(object sender, EventArgs e)          //обрабатываем клики по доске 
        {
            if (pictureBox1.Enabled) { 
            MouseEventArgs mousePos = (MouseEventArgs)e;

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            drawer(map);

            if (I1 == 500 && J1 == 500)
            {
                //textBox1.Text = mousePos.Location.ToString();
                I1 = (int)mousePos.Location.X;
                J1 = (int)mousePos.Location.Y;

                m1 = (I1 - 15) / W;
                n1 = (J1 - 15) / H;

                if (map[n1, m1] != 0 && map[n1, m1] != 9 && map[n1, m1] % 2 == hod)         //проверка индикации клика короч
                {
                    g.DrawRectangle(new Pen(Color.LawnGreen, 2), 15 + m1 * W, 15 + n1 * H, 45, 50);
                }
                else { I1 = 500; J1 = 500; }                            //сброс если выбрана неправильная клетка
            }
            else
            {
                    I2 = (int)mousePos.Location.X;
                    J2 = (int)mousePos.Location.Y;

                    m2 = (I2 - 15) / W;
                    n2 = (J2 - 15) / H;

                    I1 = 500;                                               //сброс первых координат по второму клику
                    J1 = 500;

                    string strbCount;
                    string strwCount;

                    if (bCount < 10)
                    { strbCount = "0" + bCount.ToString(); }
                    else { strbCount = bCount.ToString(); }

                    if (wCount < 10)
                    { strwCount = "0" + wCount.ToString(); }
                    else { strwCount = wCount.ToString(); }

                    TransMess = n1.ToString() + m1.ToString() + n2.ToString() + m2.ToString() 
                              + hod.ToString() + mapConverter(map) + strbCount + strwCount + Form2.gameMode.ToString();
                    translation(TransMess);
                    TransMess = "";
                }
            }
        }

        private void Form1_FormClosing(object sender, EventArgs e)          //закрывает приложение
        {
            parent.Show();
        }
    }
}
//Попытка установить соединение была безуспешной, 
//т.к. от другого компьютера за требуемое время не получен нужный отклик, 
//или было разорвано уже установленное соединение из-за неверного отклика уже подключенного компьютера 192.168.0.2:8005




//Подключение не установлено, т.к. конечный компьютер отверг запрос на подключение 192.168.0.2:8005






//сервак
//требуемый адрес для своего контекста неверен
