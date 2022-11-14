﻿using System;
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
    public partial class Form2 : Form
    {
        Bitmap fire;
        Point DrawHere;
        Rectangle InvalidRect;
        Form parent;
        public static int gameMode;
        public Form2()
        {
            InitializeComponent();
            fire = new Bitmap("F:/_Outchjoobeescthe/CSAD/CSA-4/5GpM.gif");
            DrawHere = new Point(10, 100);
            InvalidRect = new Rectangle(DrawHere, fire.Size);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
            if (ImageAnimator.CanAnimate(fire))
            {
                ImageAnimator.Animate(fire, new EventHandler(this.OnFrameChanged));
            }
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            ImageAnimator.UpdateFrames(fire);
            e.Graphics.DrawImage(fire, DrawHere);
        }

        private void OnFrameChanged(object o, EventArgs e)
        {   this.Invalidate(InvalidRect); }

        private void single_Click(object sender, EventArgs e)
        {
            Form1 Form1 = new Form1(this);
            Form1.Show();
            this.Hide();
            setGameMode(1);
        }

        private void multi_Click(object sender, EventArgs e)
        {
            int port = 8005;                                    // порт сервера
            string server = "127.0.0.1";
            try
            {
                TcpClient client = new TcpClient();
                client.Connect(server, port);

                StringBuilder response = new StringBuilder();
                NetworkStream stream = client.GetStream();

                byte[] data2 = Encoding.Unicode.GetBytes("9");
                stream.Write(data2, 0, data2.Length);

                stream.Close();
                client.Close();
            }
            catch (Exception ex)
            { }
            
            Form3 Form3 = new Form3(this);
            Form3.Show();
            this.Hide();
            setGameMode(4);
        }

        static void setGameMode(int i){ gameMode = i; }

        private void leaderboards_Click(object sender, EventArgs e)
        {

        }
    }
}
