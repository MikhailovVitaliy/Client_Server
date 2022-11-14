using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace CSA_2S
{
    class Program
    {
        static int port = 8005; // порт для приема входящих запросов

        static int[,] map = { { 0, 0, 0, 0, 0, 0, 0, 0 },
                              { 0, 0, 0, 0, 0, 0, 0, 0 },
                              { 0, 0, 0, 0, 0, 0, 0, 0 },
                              { 0, 0, 0, 0, 0, 0, 0, 0 },
                              { 0, 0, 0, 0, 0, 0, 0, 0 },
                              { 0, 0, 0, 0, 0, 0, 0, 0 },
                              { 0, 0, 0, 0, 0, 0, 0, 0 },
                              { 0, 0, 0, 0, 0, 0, 0, 0 }};

        static int bCount = 12;     //счетчики количества фигур на старте для каждого из цветов
        static int wCount = 12;     //не забывать менять обратно после тестов; шашек в IRL игре на доске на старте 12

        static int m1 = 0;
        static int n1 = 0;
        static int m2 = 0;
        static int n2 = 0;
        
        static int hod = 0;
        static int flagOk = 0;
        static int gameMode = 0;

        static string mapStr;
        static string TransMess = "";
        

        static public void messageUncoder(string mess)                  //расшифровывает строку mess полученную от клиента о действиях игрока
        {
            n1 = Convert.ToInt32(new string(mess[0], 1));
            m1 = Convert.ToInt32(new string(mess[1], 1));
            n2 = Convert.ToInt32(new string(mess[2], 1));
            m2 = Convert.ToInt32(new string(mess[3], 1));
            hod = Convert.ToInt32(new string(mess[4], 1));
            gameMode = Convert.ToInt32(new string(mess[73], 1));

            string bufCount;
            bufCount = mess[69].ToString() + mess[70].ToString();
            bCount = Convert.ToInt32(bufCount);
            bufCount = mess[71].ToString() + mess[72].ToString();
            wCount = Convert.ToInt32(bufCount);

            Console.WriteLine(n1 + " " + m1 + " " + n2 + " " + m2 + " " + hod + " " + bCount + " " + wCount);

            for (int i = 5; i <= 68; i++)
            {
                double k = Math.Floor((double)(i - 5) / 8);
                map[(int)k, ((i - 5)) % 8] = Convert.ToInt32(new string(mess[i], 1)); 
                //Console.WriteLine(map[(int)k, ((i - 5)) % 8]);
            }
        }

        static public string mapConverter(int[,] map)                           //преобразовывает массив 8х8 в строку длиной 64 символа
        {
            mapStr = "";
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                { mapStr += map[i, j].ToString(); }
            }
            return mapStr;
        }

        static public void transform(int n2, int m2)                       //преобразование пешек в дамки
        {
            if (map[n2, m2] == 2)
            {
                if (n2 == 0)
                {
                    map[n2, m2] = 4;
                }
            }
            else
            if (map[n2, m2] == 1)
            {
                if (n2 == 7)
                {
                    map[n2, m2] = 3;
                }
            }
        }
        
        static public void killer(int hod)                          //понижает счетик фигур на столе с каждой съеденной фигурой 
        {
            if (hod == 0)
            { bCount--; }
            else
                if (hod == 1)
            { wCount--; }
        }
        static public void hodChange()                              //смена хода
        {
            if (hod == 1) { hod = 0; }                              //0 - белые; 1 - черные
            else { hod = 1; }
        }

        static private void pomethque()                          //снятие промежуточной пометки при успешном ходе дамки
        {
            for (int l = 0; l < 8; l++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (map[l, j] == 5 || map[l, j] == 6 || map[l, j] == 7 || map[l, j] == 8)
                    {
                        map[l, j] = 0;
                    }
                }
            }
        }

        static public void damkee_kostyl(int n2, int m2, int nn, int mm, int hod)      //говорит само за себя
        {
            map[n2, m2] = map[nn, mm];
            map[nn, mm] = 0;
            pomethque();
            flagOk++;
            hodChange();
        }

        static public void damkee(int n1, int m1, int n2, int m2)      //атака и перемещение дамок
        {
            if (map[n1, m1] == 3 || map[n1, m1] == 4)                       //проверка на дамок
            {
                if (map[n1, m1] != 0 && Math.Abs(m2 - m1) == Math.Abs(n2 - n1) && map[n2, m2] == 0)   //проверка что выбрана не пустая клетка 
                {                                                                                     //&& ход по диаг. на люб. расст. и в люб. напр.
                    if (map[n1, m1] % 2 == hod)                                 //проверка на допустимость хода
                    {
                        /*
                         *  ↓ пошаговая обработка передвижения/атаки дамки в любую из сторон ↓
                        */

                        int nn = n1; int mm = m1;

                        for (int i = 0; i < Math.Abs(n2 - nn); i++)
                        {
                            if (nn < n2 && mm < m2)     //вправо-вниз
                            {
                                if (n1 == n2 && m1 == m2)
                                {
                                    damkee_kostyl(n2, m2, nn, mm, hod);
                                    break;
                                }

                                if (map[n1 + 1, m1 + 1] == 0)
                                {
                                    n1 = n1 + 1;
                                    m1 = m1 + 1;

                                    if (n1 == n2 && m1 == m2)
                                    {
                                        damkee_kostyl(n2, m2, nn, mm, hod);
                                        break;
                                    }
                                    continue;
                                }
                                if (map[n1 + 1, m1 + 1] % 2 != hod && map[n1 + 2, m1 + 2] == 0)
                                {
                                    if (map[n1 + 1, m1 + 1] == 1) { map[n1 + 1, m1 + 1] = 5; }          //пометка атакованной клетки  
                                    if (map[n1 + 1, m1 + 1] == 2) { map[n1 + 1, m1 + 1] = 6; }
                                    if (map[n1 + 1, m1 + 1] == 3) { map[n1 + 1, m1 + 1] = 7; }
                                    if (map[n1 + 1, m1 + 1] == 4) { map[n1 + 1, m1 + 1] = 8; }
                                    killer(hod);
                                    n1 = n1 + 2;
                                    m1 = m1 + 2;
                                    continue;
                                }
                            }

                            else if (nn > n2 && mm < m2)        //вправо-вверх
                            {
                                if (n1 == n2 && m1 == m2)       //прибыли в пункт назначения?
                                {
                                    damkee_kostyl(n2, m2, nn, mm, hod);
                                    break;
                                }

                                if (map[n1 - 1, m1 + 1] == 0)       //клетка на пути следования пуста
                                {
                                    n1 = n1 - 1;
                                    m1 = m1 + 1;

                                    if (n1 == n2 && m1 == m2)       //костыль - прибыли в пункт назначения?
                                    {
                                        damkee_kostyl(n2, m2, nn, mm, hod);
                                        break;
                                    }
                                    continue;
                                }
                                else
                                    if (map[n1 - 1, m1 + 1] % 2 != hod && map[n1 - 2, m1 + 2] == 0)     //на клетке на пути следования враг
                                {                                                                       //а за ней клетка пуста

                                    if (map[n1 - 1, m1 + 1] == 1) { map[n1 - 1, m1 + 1] = 5; }          //пометка атакованной клетки  
                                    if (map[n1 - 1, m1 + 1] == 2) { map[n1 - 1, m1 + 1] = 6; }
                                    if (map[n1 - 1, m1 + 1] == 3) { map[n1 - 1, m1 + 1] = 7; }
                                    if (map[n1 - 1, m1 + 1] == 4) { map[n1 - 1, m1 + 1] = 8; }
                                    killer(hod);
                                    n1 = n1 - 2;
                                    m1 = m1 + 2;
                                    continue;
                                }
                            }

                            else if (nn > n2 && mm > m2)        //влево-вверх
                            {
                                if (n1 == n2 && m1 == m2)
                                {
                                    damkee_kostyl(n2, m2, nn, mm, hod);
                                    break;
                                }

                                if (map[n1 - 1, m1 - 1] == 0)
                                {
                                    n1 = n1 - 1;
                                    m1 = m1 - 1;

                                    if (n1 == n2 && m1 == m2)
                                    {
                                        damkee_kostyl(n2, m2, nn, mm, hod);
                                        break;
                                    }
                                    continue;
                                }
                                else
                                    if (map[n1 - 1, m1 - 1] % 2 != hod && map[n1 - 2, m1 - 2] == 0)
                                {
                                    if (map[n1 - 1, m1 - 1] == 1) { map[n1 - 1, m1 - 1] = 5; }          //пометка атакованной клетки  
                                    if (map[n1 - 1, m1 - 1] == 2) { map[n1 - 1, m1 - 1] = 6; }
                                    if (map[n1 - 1, m1 - 1] == 3) { map[n1 - 1, m1 - 1] = 7; }
                                    if (map[n1 - 1, m1 - 1] == 4) { map[n1 - 1, m1 - 1] = 8; }
                                    killer(hod);
                                    n1 = n1 - 2;
                                    m1 = m1 - 2;
                                    continue;
                                }
                            }

                            else if (nn < n2 && mm > m2)        //влево-вниз
                            {
                                if (n1 == n2 && m1 == m2)
                                {
                                    damkee_kostyl(n2, m2, nn, mm, hod);
                                    break;
                                }

                                if (map[n1 + 1, m1 - 1] == 0)
                                {
                                    n1 = n1 + 1;
                                    m1 = m1 - 1;

                                    if (n1 == n2 && m1 == m2)
                                    {
                                        damkee_kostyl(n2, m2, nn, mm, hod);
                                        break;
                                    }
                                    continue;
                                }
                                else
                                    if (map[n1 + 1, m1 - 1] % 2 != hod && map[n1 + 2, m1 - 2] == 0)
                                {
                                    if (map[n1 + 1, m1 - 1] == 1) { map[n1 + 1, m1 - 1] = 5; }          //пометка атакованной клетки  
                                    if (map[n1 + 1, m1 - 1] == 2) { map[n1 + 1, m1 - 1] = 6; }
                                    if (map[n1 + 1, m1 - 1] == 3) { map[n1 + 1, m1 - 1] = 7; }
                                    if (map[n1 + 1, m1 - 1] == 4) { map[n1 + 1, m1 - 1] = 8; }
                                    killer(hod);
                                    n1 = n1 + 2;
                                    m1 = m1 - 2;
                                    continue;
                                }
                            }
                        }
                    }
                }
            }
            for (int l = 0; l < 8; l++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (map[l, j] == 5) { map[l, j] = 1; }
                    if (map[l, j] == 6) { map[l, j] = 2; }
                    if (map[l, j] == 7) { map[l, j] = 3; }
                    if (map[l, j] == 8) { map[l, j] = 4; }
                }
            }
        }

        static private void attack(int n1, int m1, int n2, int m2)     //атака пешек
        {
            if (map[n1, m1] == 1 || map[n1, m1] == 2)                       //для пешек
            {
                if (map[n1, m1] != 0 && Math.Abs(m2 - m1) == 2 && Math.Abs(n2 - n1) == 2 && map[n2, m2] == 0)   //проверка что выбрана не пустая клетка 
                {                                                                                               //&& что ход по диагонали по m
                    if (map[n1, m1] % 2 == hod)              //проверка на допустимость хода 
                    {
                        //проверка что в ячейке n2-1, m2-1 
                        //т.е. (по середине между n1, m1 и n2, m2)
                        //есть враг, а не пустота или друг
                        //при атаке в любую из 4ех сторон
                        if ((map[(n2 + n1) / 2, (m2 + m1) / 2] == 1 && (hod == 0))
                         || (map[(n2 + n1) / 2, (m2 + m1) / 2] == 2 && (hod == 1))
                         || (map[(n2 + n1) / 2, (m2 + m1) / 2] == 3 && (hod == 0))
                         || (map[(n2 + n1) / 2, (m2 + m1) / 2] == 4 && (hod == 1)))
                        {
                            killer(hod);
                            map[n2, m2] = map[n1, m1];
                            map[n1, m1] = 0;
                            map[(Math.Abs(n2 + n1) / 2), (Math.Abs(m2 + m1) / 2)] = 0;
                            flagOk++;
                            hodChange();
                        }
                    }
                }
            }
        }

        static private void move(int n1, int m1, int n2, int m2)       //движение пешек
        {
            if (map[n1, m1] == 1 || map[n1, m1] == 2)                       //для пешек
            {
                if (map[n1, m1] != 0 && Math.Abs(m2 - m1) == 1 && map[n2, m2] == 0)            //проверка что выбрана не пустая клетка && что ход по диагонали по m
                {
                    if (map[n1, m1] % 2 == hod)                               //проверка на допустимость хода 
                    {
                        if ((n2 - n1 == -1 && hod == 0) || (n2 - n1 == 1 && hod == 1))      //проверка хода по диагонали по n только вперед 
                        {                                                                      //в зависимости от цвета
                            map[n2, m2] = map[n1, m1];
                            map[n1, m1] = 0;
                            flagOk++;
                            hodChange();
                        }
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            TcpListener server = null;
            try
            {
                //IPAddress localAddr = IPAddress.Parse("127.0.0.1");
                IPAddress localAddr = IPAddress.Parse("192.168.0.1");
                server = new TcpListener(IPAddress.Any, port);
                server.Start();
                
                while (true)
                {
                    Console.WriteLine("Сервер запущен. Ожидание подключений...");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Подключен клиент. Выполнение запроса...");
                    NetworkStream stream = client.GetStream();

                    int bytes = 0; // количество полученных байт
                    byte[] data = new byte[256]; // буфер для получаемых данных

                    do
                    {
                        bytes = stream.Read(data, 0, data.Length); 
                        TransMess += (Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (stream.DataAvailable);


                    //производим действия с полученными данными
                    Console.WriteLine(DateTime.Now.ToLongTimeString() + ": " + TransMess);  //выводим полученное сообщение как оно есть и время
                    messageUncoder(TransMess);                                              //вывод переменных с присвоенными значениями

                    damkee(n1, m1, n2, m2);
                    attack(n1, m1, n2, m2);
                    move(n1, m1, n2, m2);                                   
                    transform(n2, m2);
                    //bot(gameMode);

                    string strbCount;
                    string strwCount;

                    if (bCount < 10)
                    { strbCount = "0" + bCount.ToString(); }
                    else { strbCount = bCount.ToString(); }

                    if (wCount < 10)
                    { strwCount = "0" + wCount.ToString(); }
                    else { strwCount = wCount.ToString(); }
                    
                    // отправляем ответ
                    string message = n1.ToString() + m1.ToString() + n2.ToString() + m2.ToString() + hod.ToString() + mapConverter(map) + strbCount + strwCount;
                    
                    // преобразуем сообщение в массив байтов
                    byte[] data2 = Encoding.Unicode.GetBytes(message);

                    // отправка сообщения
                    stream.Write(data2, 0, data2.Length);
                    // закрываем поток
                    stream.Close();
                    // закрываем подключение
                    client.Close();

                    
                    TransMess = "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}
