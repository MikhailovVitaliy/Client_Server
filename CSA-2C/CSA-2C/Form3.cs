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
    public partial class Form3 : Form
    {
        public static string connIP;
        private Form parent;

        public Form3(Form form)
        {
            InitializeComponent();
            parent = form;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void confirm_Click(object sender, EventArgs e)
        {
            connIP = strIP.Text;

            int port = 8005;                                    // порт сервера
            string server = "127.0.0.1";
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(server, port);
                
                StringBuilder response = new StringBuilder();
                NetworkStream stream = client.GetStream();

                byte[] data2 = Encoding.Unicode.GetBytes(connIP);
                stream.Write(data2, 0, data2.Length);

                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            { }
           

            Form1 Form1 = new Form1();
            Form1.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void Form3_FormClosing(object sender, EventArgs e)          //закрывает приложение
        {
            parent.Show();
        }
    }
}
