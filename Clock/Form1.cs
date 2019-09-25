using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clock
{
    public partial class Form1 : Form
    {
        Timer t = new Timer();
        int W = 300, H = 300, s = 140, m = 130, h = 120;// chiều dài đồng hồ, và chiều dài các kim

        int cx, cy;// tọa độ tâm đồng hồ
        Bitmap bmp;
        Graphics g;

        public Form1()
        {
            InitializeComponent();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void PictureBox1_LoadCompleted(object sender, AsyncCompletedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bmp = new Bitmap(W + 1, H + 1);
            cx = H / 2;
            cy = W / 2;
            t.Interval = 1000;
            t.Tick += new EventHandler(this.t_Tick);
            t.Start();
            BackColor = Color.White;

        }
        private void t_Tick(object sender, EventArgs e)
        {
            
            g = Graphics.FromImage(bmp);

            // lấy thời gian
            int ss = DateTime.Now.Second;
            int mm = DateTime.Now.Minute;
            int hh = DateTime.Now.Hour;

            int[] handCoord = new int[2];

            //clear
            g.Clear(Color.White);

            //vẽ hình tròn
            g.DrawEllipse(new Pen(Color.Black, 1f), 0, 0, W, H);

            //vẽ số giờ
            g.DrawString("12", new Font("Arial", 12), Brushes.Black, new PointF(140, 2));
            g.DrawString("3", new Font("Arial",12), Brushes.Black, new PointF(286, 140));
            g.DrawString("6", new Font("Arial", 12), Brushes.Black, new PointF(142, 282));
            g.DrawString("9", new Font("Arial",12), Brushes.Black, new PointF(0,140));

            //kim giây
            handCoord = msCoord(ss, s);
            g.DrawLine(new Pen(Color.Red, 1f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //kim phút
            handCoord = msCoord(mm, m);
            g.DrawLine(new Pen(Color.Green, 2f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //kim giờ
            handCoord = hrCoord(hh % 12, mm, h);
            g.DrawLine(new Pen(Color.Black, 3f), new Point(cx, cy), new Point(handCoord[0], handCoord[1]));

            //đưa bmp vào pictureBox;
            pictureBox1.Image = bmp;

            // hiển thị thời gian
         
            DateTime now = DateTime.Now;
            string a = now.ToString("hh");
            string b = now.ToString("mm");
            string c = now.ToString("ss");

            label1.Text = "Hour :" + a + " Minute :" + b + " Second :" + c;

            //  dispose
            g.Dispose();




        }
        // hàm xử lí kim phut và kim giây
        private int[] msCoord(int val, int hlen)
        {
            //hlen là chiều dài kim phut hoặc giây
            int[] coord = new int[2];
            val *= 6;// cứ mỗi phut hoặc 1 giây sẽ tăng 360/60=6 độ;

            if (val >= 0 && val < 180) 
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));

            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;

           
        }
        //hàm xử lí chuyển động kim  giờ
        private int[] hrCoord(int hval,int mval, int hlen)
        {
            //hlen là chiêu dài kim giờ
            //hval là số giờ, mval là số phút
            int[] coord = new int[2];
            //mỗi giờ sẽ tăng thêm 30 độ=360/12;
            //mỗi phút sẽ tăng thêm 0.5 độ=30/60;

            int val = (int)((hval * 30) + (mval * 0.5));

            if (val >= 0 && val < 180)
            {
                coord[0] = cx + (int)(hlen * Math.Sin(Math.PI * val / 180));// 
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));

            }
            else
            {
                coord[0] = cx - (int)(hlen * -Math.Sin(Math.PI * val / 180));
                coord[1] = cy - (int)(hlen * Math.Cos(Math.PI * val / 180));
            }
            return coord;


        }

    }
}
