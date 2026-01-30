using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1_ая_лабораторная
{
    public partial class Form2 : Form
    {

        //  графический инструмент
        Graphics graph1;                        
        Graphics graph2;                    
        Graphics graph3;                       
        Graphics graph4;                        
        Graphics graph5;                        
        Graphics graph6;                        
        Graphics graph7;                        
        Graphics graph8;

        //  объект для хранения рисунка в ОЗУ
        Bitmap bm1 = new Bitmap(540, 540);          
        Bitmap bm2 = new Bitmap(540, 540);          
        Bitmap bm3 = new Bitmap(540,540);           
        Bitmap bm4 = new Bitmap(540, 540);          
        Bitmap bm5 = new Bitmap(540, 540);          
        Bitmap bm6 = new Bitmap(540, 540);          
        Bitmap bm7 = new Bitmap(540, 540);          
        Bitmap bm8 = new Bitmap(540, 540);          


        Pen pen0 = new Pen(Color.Gray, 1);
        Pen pen1 = new Pen(Color.Red, 2);           
        Pen pen2 = new Pen(Color.Red, 2);           
        Pen pen3 = new Pen(Color.Red, 2);           
        Pen pen4 = new Pen(Color.Red, 2);          
        Pen pen5 = new Pen(Color.Red, 2);          
        Pen pen6 = new Pen(Color.Red, 2);         
        Pen pen7 = new Pen(Color.Red, 2);          
        Pen pen8 = new Pen(Color.Red, 2);           




        double kh1, kh2, kh3, kh4, kh5, kh6, kh7, kh8; //  масштабный коэффициент по вертикали (вдоль оси Y)
        double kv1, kv2, kv3, kv4, kv5, kv6, kv7, kv8; //  масштабный коэффициент по горизонтали (вдоль оси X)
        double t;                           // время 
        double x;                           // координат Х
        double y;                           // координата У
        double Vx, Vy;                      // проекции скорости на оси
        double V;                           // скорость
        double S_int;                       // путь
        double Ekin;                        // кинетическая энергия
        double Ep;                          // потенциальная энергия
        double k1 = 1, k2 = 1, k3 = 1, k4 = 1, k5 = 1, k6 = 1 , k7 = 1, k8 = 1;
        double k_min = 0.1, k_max = 10;

        int x_begin;                        //  графическая координата X (в пикселях) для начала линии
        int y_begin;                        //  графическая координата Y (в пикселях) для начала линии
        int x_end;                          //  графическая координата Х (в пикселях) для конца линии
        int y_end;                          //  графическая координата Y (в пикселях) для конца линии


        public Form2()
        {
            InitializeComponent();  
        }

       

        private void Form2_Load(object sender, EventArgs e)
        {
            //  Определение объекта Gr (инструмента для рисования), чтобы он работал на объекте BM
            graph1 = Graphics.FromImage(bm1);   
            graph2 = Graphics.FromImage(bm2);   
            graph3 = Graphics.FromImage(bm3);   
            graph4 = Graphics.FromImage(bm4);   
            graph5 = Graphics.FromImage(bm5);   
            graph6 = Graphics.FromImage(bm6);   
            graph7 = Graphics.FromImage(bm7);  
            graph8 = Graphics.FromImage(bm8);  
        }

        public void SizeChange()
        {
            groupBox1.Left = 20;                                // размещение groupBox1 по горизонтали
            groupBox1.Top = 20;                                 // размещение groupBox1 по вертикале
            groupBox1.Width = (this.Width - 200) / 4;           // изменение groupBox1 по ширине
            groupBox1.Height = (this.Height - 120) / 2;         // изменение groupBox1 по высоте 

            pictureBox1.Left = 10;                              // размещение pictureBox1 по горизонтали
            pictureBox1.Top = 20;                               // размещение pictureBox1 по вертикале
            pictureBox1.Width = groupBox1.Width - 70;           // изменение pictureBox1 по ширине
            pictureBox1.Height = groupBox1.Height - 80;         // изменение pictureBox1 по высоте

            button2.Left = 10;                                  // размещение button2 по горизонтали
            button2.Top = 20;                                   // размещение button2 по вертикале
            button2.Width = groupBox1.Width - 70;               // изменение button2 по ширине
            button2.Height = groupBox1.Height - 80;             // изменение button2 по высоте

            trackBar1.Left = 10;                                // размещение trackBar1 по горизонтали
            trackBar1.Top = groupBox1.Height - 50;              // размещение trackBar1 по вертикале 
            trackBar1.Width = pictureBox1.Width;                // ширина trackBar1 равная  pictureBox1

            trackBar2.Top = 20;                                 // размещение trackBar2 по вертикале
            trackBar2.Left = pictureBox1.Width + 20;            // размещение trackBar2 по горизонтали
            trackBar2.Height = pictureBox1.Height;              // высота trackBar2 равная  pictureBox1

            DrawGraphic1();
            
            groupBox2.Left = 40 + (this.Width - 200) / 4;       // размещение groupBox2 по горизонтали
            groupBox2.Top = 20;                                 // размещение groupBox2 по вертикале
            groupBox2.Width = (this.Width - 200) / 4;           // изменение groupBox2 по ширине
            groupBox2.Height = (this.Height - 120) / 2;         // изменение groupBox2 по высоте 

            button3.Left = 10;                                  // размещение button3 по горизонтали
            button3.Top = 20;                                   // размещение button3 по вертикале
            button3.Width = groupBox1.Width - 70;               // изменение button3 по ширине
            button3.Height = groupBox1.Height - 80;             // изменение button3 по высоте

            pictureBox2.Left = 10;                              // размещение pictureBox2 по горизонтали
            pictureBox2.Top = 20;                               // размещение pictureBox2 по вертикале
            pictureBox2.Width = groupBox1.Width - 70;           // изменение pictureBox2 по ширине
            pictureBox2.Height = groupBox1.Height - 80;         // изменение pictureBox2 по высоте

            trackBar4.Left = 10;                                // размещение trackBar4 по горизонтали
            trackBar4.Top = groupBox2.Height - 50;              // размещение trackBar4 по вертикале 
            trackBar4.Width = pictureBox2.Width;                // ширина trackBar4 равная  pictureBox2


            trackBar3.Top = 20;                                 // размещение trackBar3 по вертикале
            trackBar3.Left = pictureBox2.Width + 20;            // размещение trackBar3 по горизонтали   
            trackBar3.Height = pictureBox2.Height;              // высота trackBar3 равная  pictureBox2

            DrawGraphic2();
                
            groupBox3.Left = 10 + (this.Width - 100) / 2;       // размещение groupBox3 по горизонтали
            groupBox3.Top = 20;                                 // размещение groupBox3 по вертикале            
            groupBox3.Width = (this.Width - 200) / 4;           // изменение groupBox3 по ширине
            groupBox3.Height = (this.Height - 120) / 2;         // изменение groupBox3 по высоте
                
            button4.Left = 10;                                  // размещение button4 по горизонтали
            button4.Top = 20;                                   // размещение button4 по вертикале 
            button4.Width = groupBox1.Width - 70;               // изменение button4 по ширине
            button4.Height = groupBox1.Height - 80;             // изменение button4 по высоте

            pictureBox3.Left = 10;                              // размещение pictureBox3 по горизонтали
            pictureBox3.Top = 20;                               // размещение pictureBox3 по вертикале
            pictureBox3.Width = groupBox3.Width - 70;           // изменение pictureBox3 по ширине
            pictureBox3.Height = groupBox3.Height - 80;         // изменение pictureBox3 по высоте

            trackBar6.Left = 10;                                // размещение trackBar6 по горизонтали
            trackBar6.Top = groupBox3.Height - 50;              // размещение trackBar6 по вертикале
            trackBar6.Width = pictureBox3.Width;                // ширина trackBar6 равная  pictureBox3

            trackBar5.Top = 20;                                 // размещение trackBar5 по вертикале
            trackBar5.Left = pictureBox3.Width + 20;            // размещение trackBar5 по горизонтали
            trackBar5.Height = pictureBox3.Height;              // высота trackBar5 равная  pictureBox3

            DrawGraphic3();

            groupBox4.Left = 40 + 3 * (this.Width - 160) / 4;   // размещение groupBox4 по горизонтали
            groupBox4.Top = 20;                                 // размещение groupBox4 по вертикале
            groupBox4.Width = (this.Width - 200) / 4;           // изменение groupBox4 по ширине
            groupBox4.Height = (this.Height - 120) / 2;         // изменение groupBox4 по высоте

            button5.Left = 10;                                  // размещение button5 по горизонтали
            button5.Top = 20;                                   // размещение button5 по вертикале
            button5.Width = groupBox1.Width - 70;               // изменение button5 по ширине    
            button5.Height = groupBox1.Height - 80;             // изменение button5 по высоте

            pictureBox4.Left = 10;                              // размещение pictureBox4 по горизонтали
            pictureBox4.Top = 20;                               // размещение pictureBox4 по вертикале
            pictureBox4.Width = groupBox4.Width - 70;           // изменение pictureBox4 по ширине
            pictureBox4.Height = groupBox4.Height - 80;         // изменение pictureBox4 по высоте

            trackBar8.Left = 10;                                // размещение trackBar8 по горизонтали
            trackBar8.Top = groupBox4.Height - 50;              // размещение trackBar8 по вертикале
            trackBar8.Width = pictureBox4.Width;                // ширина trackBar8 равная  pictureBox4

            trackBar7.Top = 20;                                 // размещение trackBar7 по вертикале
            trackBar7.Left = pictureBox4.Width + 20;            // размещение trackBar7 по горизонтали
            trackBar7.Height = pictureBox4.Height;              // высота trackBar7 равная  pictureBox4

            DrawGraphic4();

            groupBox5.Left = 20;                                 // размещение groupBox5 по горизонтали
            groupBox5.Top = 20 + (this.Height - 120) / 2;        // размещение groupBox5 по вертикале
            groupBox5.Width = (this.Width - 200) / 4;            // изменение groupBox5 по ширине
            groupBox5.Height = (this.Height - 120) / 2;          // изменение groupBox5 по высоте

            button6.Left = 10;                                   // размещение button6 по горизонтали
            button6.Top = 20;                                    // размещение button6 по вертикале
            button6.Width = groupBox1.Width - 70;                // изменение button6 по ширине
            button6.Height = groupBox1.Height - 80;              // изменение button6 по высоте

            pictureBox5.Left = 10;                               // размещение pictureBox5 по горизонтали
            pictureBox5.Top = 20;                                // размещение pictureBox5 по вертикале
            pictureBox5.Width = groupBox1.Width - 70;            // изменение pictureBox5 по ширине
            pictureBox5.Height = groupBox1.Height - 80;          // изменение pictureBox5 по высоте

            trackBar10.Left = 10;                                // размещение trackBar10 по горизонтали
            trackBar10.Top = groupBox5.Height - 50;              // размещение trackBar10 по вертикале
            trackBar10.Width = pictureBox5.Width;                // ширина trackBar10 равная  pictureBox5

            trackBar9.Top = 20;                                  // размещение trackBar9 по вертикале               
            trackBar9.Left = pictureBox5.Width + 20;             // размещение trackBar9 по горизонтали
            trackBar9.Height = pictureBox5.Height;               // высота trackBar9 равная  pictureBox5

            DrawGraphic5();

            groupBox6.Left = 40 + (this.Width - 200) / 4;       // размещение groupBox6 по горизонтали
            groupBox6.Top = 20 + (this.Height - 120) / 2;       // размещение groupBox6 по вертикале
            groupBox6.Width = (this.Width - 200) / 4;           // изменение groupBox6 по ширине
            groupBox6.Height = (this.Height - 120) / 2;         // изменение groupBox6 по высоте

            button7.Left = 10;                                  // размещение button7 по горизонтали
            button7.Top = 20;                                   // размещение button7 по вертикале
            button7.Width = groupBox1.Width - 70;               // изменение button7 по ширине
            button7.Height = groupBox1.Height - 80;             // изменение button7 по высоте

            pictureBox6.Left = 10;                              // размещение pictureBox6 по горизонтали
            pictureBox6.Top = 20;                               // размещение pictureBox6 по вертикале
            pictureBox6.Width = groupBox1.Width - 70;           // изменение pictureBox6 по ширине
            pictureBox6.Height = groupBox1.Height - 80;         // изменение pictureBox6 по высоте

            trackBar12.Left = 10;                               // размещение trackBar12 по горизонтали
            trackBar12.Top = groupBox6.Height - 50;             // размещение trackBar12 по вертикале
            trackBar12.Width = pictureBox6.Width;               // ширина trackBar12 равная  pictureBox6

            trackBar11.Top = 20;                                // размещение trackBar11 по вертикале 
            trackBar11.Left = pictureBox6.Width + 20;           // размещение trackBar11 по горизонтали
            trackBar11.Height = pictureBox6.Height;             // высота trackBar11 равная  pictureBox6

            DrawGraphic6();

            groupBox7.Left = 10 + (this.Width - 100) / 2;       // размещение groupBox7 по горизонтали
            groupBox7.Top = 20 + (this.Height - 120) / 2;       // размещение groupBox7 по вертикале
            groupBox7.Width = (this.Width - 200) / 4;           // изменение groupBox7 по ширине
            groupBox7.Height = (this.Height - 120) / 2;         // изменение groupBox7 по высоте

            button8.Left = 10;                                  // размещение button8 по горизонтали
            button8.Top = 20;                                   // размещение button8 по вертикале
            button8.Width = groupBox1.Width - 70;               // изменение button8 по ширине
            button8.Height = groupBox1.Height - 80;             // изменение button8 по высоте

            pictureBox7.Left = 10;                              // размещение pictureBox7 по горизонтали
            pictureBox7.Top = 20;                               // размещение pictureBox7 по вертикале
            pictureBox7.Width = groupBox1.Width - 70;           // изменение pictureBox7 по ширине
            pictureBox7.Height = groupBox1.Height - 80;         // изменение pictureBox7 по высоте

            trackBar14.Left = 10;                               // размещение trackBar14 по горизонтали
            trackBar14.Top = groupBox7.Height - 50;             // размещение trackBar14 по вертикале
            trackBar14.Width = pictureBox7.Width;               // ширина trackBar14 равная  pictureBox7

            trackBar13.Top = 20;                                // размещение trackBar13 по вертикале
            trackBar13.Left = pictureBox7.Width + 20;           // размещение trackBar13 по горизонтали
            trackBar13.Height = pictureBox7.Height;             // высота trackBar13 равная  pictureBox7

            DrawGraphic7();

            groupBox8.Left = 40 + 3 * (this.Width - 160)/4;     // размещение groupBox8 по горизонтали
            groupBox8.Top = 20 + (this.Height - 120) / 2;       // размещение groupBox8 по вертикале
            groupBox8.Width = (this.Width - 200) / 4;           // изменение groupBox8 по ширине
            groupBox8.Height = (this.Height - 120) / 2;         // изменение groupBox8 по высоте

            button9.Left = 10;                                  // размещение button9 по горизонтали
            button9.Top = 20;                                   // размещение button9 по вертикале
            button9.Width = groupBox1.Width - 70;               // изменение button9 по ширине
            button9.Height = groupBox1.Height - 80;             // изменение button9 по высоте

            pictureBox8.Left = 10;                              // размещение pictureBox8 по горизонтали
            pictureBox8.Top = 20;                               // размещение pictureBox8 по вертикале
            pictureBox8.Width = groupBox8.Width - 70;           // изменение pictureBox8 по ширине
            pictureBox8.Height = groupBox8.Height - 80;         // изменение pictureBox8 по высоте

            trackBar16.Left = 10;                               // размещение trackBar16 по горизонтали
            trackBar16.Top = groupBox7.Height - 50;             // размещение trackBar16 по вертикале
            trackBar16.Width = pictureBox7.Width;               // ширина trackBar16 равная  pictureBox8

            trackBar15.Top = 20;                                // размещение trackBar15 по вертикале
            trackBar15.Left = pictureBox8.Width + 20;           // размещение trackBar15 по горизонтали
            trackBar15.Height = pictureBox8.Height;             // высота trackBar15 равная  pictureBox8

            DrawGraphic8();


            button1.Left = (Width - 300) / 2;                   // смещение по горизонтали 
        

        }
         
        // изменение размера Form 2
        private void Form2_Resize(object sender, EventArgs e)
        {
            SizeChange();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;    //скрыть рисунок
            button2.Visible = true;         //показать кнопку
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = true;      //показать рисунок
            button2.Visible = false;        //скрыть кнопку
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = false;        //скрыть рисунок
            button3.Visible = true;              //показать кнопку
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox2.Visible = true;          //показать рисунок
            button3.Visible = false;                //скрыть кнопку
        }

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = true;         //показать рисунок
            button4.Visible = false;            //скрыть кнопку
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pictureBox3.Visible = false;        //скрыть рисунок
            button4.Visible = true;             //показать кнопку
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = true;         //показать рисунок
            button5.Visible = false;            //скрыть кнопку
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pictureBox4.Visible = false;        //скрыть рисунок
            button5.Visible = true;             //показать кнопку
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox5.Visible = true;     //показать рисунок
            button6.Visible = false;        //скрыть кнопку
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pictureBox5.Visible = false;            //скрыть рисунок
            button6.Visible = true;                 //показать кнопку
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox6.Visible = true;         //показать рисунок
            button7.Visible = false;            //скрыть кнопку
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pictureBox6.Visible = false;        //скрыть рисунок
            button7.Visible = true;             //показать кнопку
        }

        private void button8_Click(object sender, EventArgs e)
        {
            pictureBox7.Visible = true;          //показать рисунок
            button8.Visible = false;            //скрыть кнопку
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            pictureBox7.Visible = false;        //скрыть рисунок
            button8.Visible = true;             //показать кнопку
        }

        private void button9_Click(object sender, EventArgs e)
        {
            pictureBox8.Visible = true;         //показать рисунок
            button9.Visible = false;            //скрыть кнопку
        }

      
        private void pictureBox8_Click(object sender, EventArgs e)
        {
            pictureBox8.Visible = false;        //скрыть рисунок
            button9.Visible = true;             //скрыть кнопку
        }



        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            label1.Visible = true;  // показать курсор
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            label1.Visible = false;         // скрыть курсор
        }

      

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            label1.Left = pictureBox1.Left + e.X + 5;               //  Изменение координаты X метки label1
            label1.Top = pictureBox1.Top + e.Y + 5;                 //  Изменение координаты Y метки label1
            label1.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh1, (pictureBox1.Height - 20 - e.Y) / kv1);           //  Изменение текста метки label1
        }

       

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
           
            k1 *= Math.Pow(1.1, e.Delta / 100);
            if (k1 < k_min) k1 = k_min;
            if (k1 > k_max) k1 = k_max;
            DrawGraphic1();
            label1.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh1, (pictureBox1.Height - 20 - e.Y) / kv1);          //  Изменение текста метки label1

        }


        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            label2.Visible = true;           // показать курсор
        }

 
        private void pictureBox2_MouseLeave(object sender, EventArgs e)
        {
            label2.Visible = false;             // скрыть курсор
        }

        

        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            label2.Left = pictureBox2.Left + e.X + 5;               //  Изменение координаты X метки label2
            label2.Top = pictureBox2.Top + e.Y + 5;                  //  Изменение координаты Y метки label2
            label2.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh2, (pictureBox2.Height - 20 - e.Y) / kv2);           //  Изменение текста метки label2
        }

        private void pictureBox2_MouseWheel(object sender, MouseEventArgs e)
        {
            k2 *= Math.Pow(1.1, e.Delta / 100);
            if (k2 < k_min) k2 = k_min;
            if (k2 > k_max) k2 = k_max;
            DrawGraphic2();
            label2.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh2, (pictureBox2.Height - 20 - e.Y) / kv2);           //  Изменение текста метки label2
        }


        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            label3.Visible = true;              // показать курсор
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            label3.Visible = false;             // скрыть курсор
        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            label3.Left = pictureBox3.Left + e.X + 5;                //  Изменение координаты X метки label3
            label3.Top = pictureBox3.Top + e.Y + 5;                  //  Изменение координаты Y метки label3
            label3.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh3, (pictureBox3.Height - 20 - e.Y) / kv3);          //  Изменение текста метки label3
        }

      
        private void pictureBox3_MouseWheel(object sender, MouseEventArgs e)
        {
            k3 *= Math.Pow(1.1, e.Delta / 100);
            if (k3 < k_min) k3 = k_min;
            if (k3 > k_max) k3 = k_max;
            DrawGraphic3();
            label3.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh3, (pictureBox3.Height - 20 - e.Y) / kv3);          //  Изменение текста метки label3
        }

        private void pictureBox4_MouseEnter(object sender, EventArgs e)
        {
            label4.Visible = true;          // показать курсор
        }

        private void pictureBox4_MouseLeave(object sender, EventArgs e)
        {
            label4.Visible = false;         // скрыть курсор
        }

        private void pictureBox4_MouseMove(object sender, MouseEventArgs e)
        {
            label4.Left = pictureBox4.Left + e.X + 5;           //  Изменение координаты X метки label4
            label4.Top = pictureBox4.Top + e.Y + 5;             //  Изменение координаты Y метки label4
            label4.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh4, (pictureBox4.Height - 20 - e.Y) / kv4);          //  Изменение текста метки label4
        }

        private void pictureBox4_MouseWheel(object sender, MouseEventArgs e)
        {
            k4 *= Math.Pow(1.1, e.Delta / 100);
            if (k4 < k_min) k4 = k_min;
            if (k4 > k_max) k4 = k_max;
            DrawGraphic4();
            label4.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh4, (pictureBox4.Height - 20 - e.Y) / kv4);           //  Изменение текста метки label4
        }

      

        private void pictureBox5_MouseEnter(object sender, EventArgs e)
        {
            label5.Visible = true;              // показать курсор
        }

        private void pictureBox5_MouseLeave(object sender, EventArgs e)
        {
            label5.Visible = false;              // скрыть курсор
        }

        private void pictureBox5_MouseMove(object sender, MouseEventArgs e)
        {
            label5.Left = pictureBox5.Left + e.X + 5;           //  Изменение координаты X метки label5
            label5.Top = pictureBox5.Top + e.Y + 5;             //  Изменение координаты Y метки label5
            label5.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh5, (pictureBox5.Height - 20 - e.Y) / kv5);           //  Изменение текста метки label5
        }

      
        private void pictureBox5_MouseWheel(object sender, MouseEventArgs e)
        {
            k5 *= Math.Pow(1.1, e.Delta / 100);
            if (k5 < k_min) k5 = k_min;
            if (k5 > k_max) k5 = k_max;
            DrawGraphic5();
            label5.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh5, (pictureBox5.Height - 20 - e.Y) / kv5);          //  Изменение текста метки label5
        }


        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            label6.Visible = true;              // показать курсор
        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            label6.Visible = false;         // скрыть курсор
        }

        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            label6.Left = pictureBox6.Left + e.X + 5;               //  Изменение координаты X метки label6
            label6.Top = pictureBox6.Top + e.Y + 5;                 //  Изменение координаты Y метки label6
            label6.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh6, (pictureBox6.Height - 20 - e.Y) / kv6);                    //  Изменение текста метки label6
        }
        private void pictureBox6_MouseWheel(object sender, MouseEventArgs e)
        {
            k6 *= Math.Pow(1.1, e.Delta / 100);
            if (k6 < k_min) k6 = k_min;
            if (k6 > k_max) k6 = k_max;
            DrawGraphic6();
            label6.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh6, (pictureBox6.Height - 20 - e.Y) / kv6);              //  Изменение текста метки label6
        }




        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            label7.Visible = true;              // показать курсор
        }
        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            label7.Visible = false;              // скрыть курсор
        }

        private void pictureBox7_MouseMove(object sender, MouseEventArgs e)
        {
            label7.Left = pictureBox7.Left + e.X + 5;              //  Изменение координаты X метки label7
            label7.Top = pictureBox7.Top + e.Y + 5;                 //  Изменение координаты Y метки label7
            label7.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh7, (pictureBox7.Height - 20 - e.Y) / kv7);              //  Изменение текста метки label7
        }
        private void pictureBox7_MouseWheel(object sender, MouseEventArgs e)
        {
            k7 *= Math.Pow(1.1, e.Delta / 100);
            if (k7 < k_min) k7 = k_min;
            if (k7 > k_max) k7 = k_max;
            DrawGraphic7();
            label7.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh7, (pictureBox7.Height - 20 - e.Y) / kv7);              //  Изменение текста метки label7
        }



        private void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
            label8.Visible = true;          // показать курсор
        }
        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            label8.Visible = false;         // скрыть курсор
        }

        private void pictureBox8_MouseMove(object sender, MouseEventArgs e)
        {
            label8.Left = pictureBox8.Left + e.X + 5;               //  Изменение координаты X метки label8
            label8.Top = pictureBox8.Top + e.Y + 5;                 //  Изменение координаты Y метки label8
            label8.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh8, (pictureBox8.Height - 20 - e.Y) / kv8);                  //  Изменение координаты Y метки label8
        }
        private void pictureBox8_MouseWheel(object sender, MouseEventArgs e)
        {
            k8 *= Math.Pow(1.1, e.Delta / 100);
            if (k8 < k_min) k8 = k_min;
            if (k8 > k_max) k8 = k_max;
            DrawGraphic8();
            label8.Text = string.Format("( {0:f2}; {1:f2})", (e.X - 20) / kh8, (pictureBox8.Height - 20 - e.Y) / kv8);                  //  Изменение координаты Y метки label8
        }




        private void button1_Click(object sender, EventArgs e)
        {
            Form1.Fr2.Dispose();                                         //  Уничножить объект Fr2 (дополнительное окно класс Form2)
            Form1.Fr1.показатьToolStripMenuItem.Enabled = true;          //  Разблокировать кнопку "Показать", чтобы можно было снова создать объект Fr2

        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            button1_Click(sender, e);           // закрыть Form2
        }

        public void DrawGraphic1() 
        {
            graph1.Clear(pictureBox1.BackColor);
             for (int k = 0; k <= 10; k++)               //  Нарисовать координатную сетку 10х10
            {
                graph1.DrawLine(pen0, 20, 20 + k * (pictureBox1.Height - 40) / 10, pictureBox1.Width - 20, 20 + k * (pictureBox1.Height - 40) / 10);        // горизонтальная линия
                graph1.DrawLine(pen0, 20 + k * (pictureBox1.Width - 40) / 10, 20, 20 + k * (pictureBox1.Width - 40) / 10, pictureBox1.Height - 20);         // вертикальная линия

            }

            kh1 = k1 * ((pictureBox1.Width - 40) / Form1.Fr1.T) * Math.Pow(1.5, trackBar1.Value);         //  Масштабный коэффициент по горизонтали для 1-го графика
            kv1 = k1 * ((pictureBox1.Height - 40) / Form1.Fr1.L) * Math.Pow(1.5, trackBar2.Value);        //  Масштабный коэффициент по вертикали для 1-го графика

            x_begin = 20;                                           //  Графическая координата X для начальной точки графика         
            y_begin = pictureBox1.Height - 20;                      //  Графическая координата Y для начальной точки графика
            for (int i = 21; i <= pictureBox1.Width - 20; i++)      //  вычисление зависимости Х от t
            {
                x_end = i;
                t = (x_end - 20) / kh1;
                if (t <= Form1.Fr1.T)
                {
                    x = Form1.Fr1.v0 * Form1.Fr1.cos * t;
                }
                else
                {
                    x = Form1.Fr1.L;
                }
                y_end = (int)(pictureBox1.Height - 20 - kv1 * x);       // вычисление конечной координаты У
                graph1.DrawLine(pen1, x_begin, y_begin, x_end, y_end);  // рисовать график
                x_begin = x_end;                                      // Координата X конца текущего кусочка траектории будет координатой X начала следующего кусочка графика
                y_begin = y_end;                                      // Координата Y конца текущего кусочка траектории будет координатой Y начала следующего кусочка графика
            }


            pictureBox1.Image = bm1;  //  Рисунок на pictureBox1 будет взят из BM1
            pictureBox1.Refresh();    // обновить pictureBox1


        }

        public void DrawGraphic2()
        {
            graph2.Clear(pictureBox2.BackColor);           
            for (int k = 0; k <= 10; k++)                //  Нарисовать координатную сетку 10х10
            {
                graph2.DrawLine(pen0, 20, 20 + k * (pictureBox2.Height - 40) / 10, pictureBox2.Width - 20, 20 + k * (pictureBox2.Height - 40) / 10);        // горизонтальная линия
                graph2.DrawLine(pen0, 20 + k * (pictureBox2.Width - 40) / 10, 20, 20 + k * (pictureBox2.Width - 40) / 10, pictureBox2.Height - 20);          // вертикальная линия
            }

            kh2 = k2 * ((pictureBox2.Width - 40) / Form1.Fr1.T) * Math.Pow(1.5, trackBar3.Value);                //  Масштабный коэффициент по горизонтали для 2-го графика
            kv2 = k2 * ((pictureBox2.Height - 40) / Form1.Fr1.H) * Math.Pow(1.5, trackBar4.Value);               //  Масштабный коэффициент по вертикали для 2-го графика

            x_begin = 20;                                                             //  Графическая координата X для начальной точки графика 
            y_begin = (int)(pictureBox2.Height - 20 - kv2 * Form1.Fr1.h0);            //  Графическая координата Y для начальной точки графика
            for (int i = 21; i <= pictureBox2.Width - 20; i++)                        //  вычисление зависимости Y от t
            {
                x_end = i;                                                               // вычисление конечной координаты Х
                t = (x_end - 20) / kh2;
                if (t <= Form1.Fr1.T)
                {
                    y = Form1.Fr1.h0 + Form1.Fr1.v0 * Form1.Fr1.sin * t - Form1.g * t * t / 2;
                }
                else
                {
                    y = 0;
                }
                y_end = (int)(pictureBox2.Height - 20 - kv2 * y);                   // вычисление конечной координаты У
                graph2.DrawLine(pen2, x_begin, y_begin, x_end, y_end);               // рисовать график
                x_begin = x_end;                                      // Координата X конца текущего кусочка траектории будет координатой X начала следующего кусочка графика
                y_begin = y_end;                                      // Координата Y конца текущего кусочка траектории будет координатой Y начала следующего кусочка графика                                     
            }


            pictureBox2.Image = bm2;            //  Рисунок на pictureBox2 будет взят из BM2
            pictureBox2.Refresh();              // обновить pictureBox2


        }


        public void DrawGraphic3() 
        {
            graph3.Clear(pictureBox3.BackColor);
            for (int k = 0; k <= 10; k++)               //  Нарисовать координатную сетку 10х10
            {
                graph3.DrawLine(pen0, 20, 20 + k * (pictureBox3.Height - 40) / 10, pictureBox3.Width - 20, 20 + k * (pictureBox3.Height - 40) / 10);        // горизонтальная линия
                graph3.DrawLine(pen0, 20 + k * (pictureBox3.Width - 40) / 10, 20, 20 + k * (pictureBox3.Width - 40) / 10, pictureBox3.Height - 20);          // вертикальная линия
            }

            kh3 = k3 * ((pictureBox3.Width - 40) / Form1.Fr1.T) * Math.Pow(1.5, trackBar5.Value);                       //  Масштабный коэффициент по горизонтали для 3-го графика
            kv3 = k3 * ((pictureBox3.Height - 40) / Form1.Fr1.Vmax) * Math.Pow(1.5, trackBar6.Value);                   //  Масштабный коэффициент по вертикали для 3-го графика

            x_begin = 20;                                                                               //  Графическая координата X для начальной точки графика 
            y_begin = (int)(pictureBox3.Height - 20 - kv3 * Form1.Fr1.v0 * Form1.Fr1.cos);              //  Графическая координата Y для начальной точки графика
            for (int i = 21; i <= pictureBox3.Width - 20; i++)              // вычисление зависимость проекции Vx скорости от времени t
            {
                x_end = i;                                                       // вычисление конечной координаты Х
                t = (x_end - 20) / kh3;
                if (t <= Form1.Fr1.T)
                {
                    Vx = Form1.Fr1.v0 * Form1.Fr1.cos;
                }
                else
                {
                    Vx = 0;
                }
                y_end = (int)(pictureBox3.Height - 20 - kv3 * Vx);              // вычисление конечной координаты У
                graph3.DrawLine(pen3, x_begin, y_begin, x_end, y_end);            // рисовать график
                x_begin = x_end;                                      // Координата X конца текущего кусочка траектории будет координатой X начала следующего кусочка графика
                y_begin = y_end;                                      // Координата Y конца текущего кусочка траектории будет координатой Y начала следующего кусочка графика                                                           
            }

            pictureBox3.Image = bm3;             //  Рисунок на pictureBox3 будет взят из BM3
            pictureBox3.Refresh();               // обновить pictureBox3

        }

        public void DrawGraphic4()
        {
            graph4.Clear(pictureBox4.BackColor);
            for (int k = 0; k <= 10; k++)               //  Нарисовать координатную сетку 10х10
            {
                graph4.DrawLine(pen0, 20, 20 + k * (pictureBox4.Height - 40) / 10, pictureBox4.Width - 20, 20 + k * (pictureBox4.Height - 40) / 10);    // горизонтальная линия
                graph4.DrawLine(pen0, 20 + k * (pictureBox4.Width - 40) / 10, 20, 20 + k * (pictureBox4.Width - 40) / 10, pictureBox4.Height - 20);     // вертикальная линия
            }

            kh4 = k4 * ((pictureBox4.Width - 40) / Form1.Fr1.T) * Math.Pow(1.5, trackBar7.Value);                   //  Масштабный коэффициент по горизонтали для 4-го графика
            kv4 = k4 * ((pictureBox4.Height - 40) / (2 * Form1.Fr1.Vmax)) * Math.Pow(1.5, trackBar8.Value);         //  Масштабный коэффициент по вертикали для 4-го графика


            x_begin = 20;                                                                                  //  Графическая координата X для начальной точки графика
            y_begin = (int)(pictureBox4.Height / 2 - kv4 * Form1.Fr1.v0 * Form1.Fr1.sin);                  //  Графическая координата Y для начальной точки графика
            for (int i = 21; i <= pictureBox4.Width - 20; i++)                                             // вычисление зависимость проекции Vу скорости от времени t
            {
                x_end = i;                                                                   // вычисление конечной координаты Х
                t = (x_end - 20) / kh4;
                if (t <= Form1.Fr1.T)
                {
                    Vy = Form1.Fr1.v0 * Form1.Fr1.sin - Form1.g * t;
                }
                else
                {
                    Vy = 0;
                }
                y_end = (int)(pictureBox4.Height / 2 - kv4 * Vy);                        // вычисление конечной координаты У
                graph4.DrawLine(pen4, x_begin, y_begin, x_end, y_end);                  // рисовать график
                x_begin = x_end;                                      // Координата X конца текущего кусочка траектории будет координатой X начала следующего кусочка графика
                y_begin = y_end;                                      // Координата Y конца текущего кусочка траектории будет координатой Y начала следующего кусочка графика                                                           

            }

            pictureBox4.Image = bm4;             //  Рисунок на pictureBox4 будет взят из BM4
            pictureBox4.Refresh();               // обновить pictureBox4


        }


        public void DrawGraphic5()
        {
            graph5.Clear(pictureBox5.BackColor);
            for (int k = 0; k <= 10; k++)              //  Нарисовать координатную сетку 10х10
            {
                graph5.DrawLine(pen0, 20, 20 + k * (pictureBox5.Height - 40) / 10, pictureBox5.Width - 20, 20 + k * (pictureBox5.Height - 40) / 10);    // горизонтальная линия
                graph5.DrawLine(pen0, 20 + k * (pictureBox5.Width - 40) / 10, 20, 20 + k * (pictureBox5.Width - 40) / 10, pictureBox5.Height - 20);     // вертикальная линия
            }

            kh5 = k5 * ((pictureBox5.Width - 40) / Form1.Fr1.T) * Math.Pow(1.5, trackBar9.Value);                //  Масштабный коэффициент по горизонтали для 5-го графика
            kv5 = k5 * ((pictureBox5.Height - 40) / Form1.Fr1.Vmax) * Math.Pow(1.5, trackBar10.Value);           //  Масштабный коэффициент по вертикали для 5-го графика

            x_begin = 20;                                                               //  Графическая координата X для начальной точки графика
            y_begin = (int)(pictureBox5.Height - 20 - kv5 * Form1.Fr1.v0);              //  Графическая координата Y для начальной точки графика
            for (int i = 21; i <= pictureBox5.Width - 20; i++)                          // вычисление зависимость проекции V скорости от времени t
            {
                x_end = i;                                                  // вычисление конечной координаты Х
                t = (x_end - 20) / kh5;
                if (t <= Form1.Fr1.T)
                {
                    Vx = Form1.Fr1.v0 * Form1.Fr1.cos;
                    Vy = Form1.Fr1.v0 * Form1.Fr1.sin - Form1.g * t;
                    V = Math.Sqrt(Vx * Vx + Vy * Vy);
                }
                else
                {
                    V = 0;
                }
                y_end = (int)(pictureBox5.Height - 20 - kv5 * V);               // вычисление конечной координаты У
                graph5.DrawLine(pen5, x_begin, y_begin, x_end, y_end);          // рисовать график
                x_begin = x_end;                                      // Координата X конца текущего кусочка траектории будет координатой X начала следующего кусочка графика
                y_begin = y_end;                                      // Координата Y конца текущего кусочка траектории будет координатой Y начала следующего кусочка графика                                                           

            }

            pictureBox5.Image = bm5;        //  Рисунок на pictureBox5 будет взят из BM5
            pictureBox5.Refresh();           // обновить pictureBox5

        }

        public void DrawGraphic6()
        {
            graph6.Clear(pictureBox6.BackColor);
            for (int k = 0; k <= 10; k++)           //  Нарисовать координатную сетку 10х10
            {
                graph6.DrawLine(pen0, 20, 20 + k * (pictureBox6.Height - 40) / 10, pictureBox6.Width - 20, 20 + k * (pictureBox6.Height - 40) / 10);         // горизонтальная линия
                graph6.DrawLine(pen0, 20 + k * (pictureBox6.Width - 40) / 10, 20, 20 + k * (pictureBox6.Width - 40) / 10, pictureBox6.Height - 20);          // вертикальная линия
            }

            kh6 = k6 * ((pictureBox6.Width - 40) / Form1.Fr1.T) * Math.Pow(1.5, trackBar11.Value);          //  Масштабный коэффициент по горизонтали для 6-го графика
            kv6 = k6 * ((pictureBox6.Height - 40) / Form1.Fr1.H) * Math.Pow(1.5, trackBar12.Value);         //  Масштабный коэффициент по вертикали для 6-го графика

            x_begin = 20;                               //  Графическая координата X для начальной точки графика
            y_begin = pictureBox6.Height - 20;          //  Графическая координата Y для начальной точки графика

            S_int = 0;              // значение нач. пути

            for (int i = 21; i <= pictureBox6.Width - 20; i++)       // вычисление пути по времени t
            {
                x_end = i;                      // вычисление конечной координаты Х
                t = (x_end - 20) / kh6;
                
                if (t <= Form1.Fr1.T)
                {
                    Vx = Form1.Fr1.v0 * Form1.Fr1.cos;
                    Vy = Form1.Fr1.v0 * Form1.Fr1.sin - Form1.g * t;
                    S_int += (Math.Sqrt((Vx * Vx) + (Vy * Vy))) * Form1.Fr1.dt;
                    y=S_int;

                }
              
                y_end = (int)(pictureBox6.Height - 20 - kv6 * y);                    // вычисление конечной координаты У
                graph6.DrawLine(pen6, x_begin, y_begin, x_end, y_end);                // рисовать график
                x_begin = x_end;                                      // Координата X конца текущего кусочка траектории будет координатой X начала следующего кусочка графика
                y_begin = y_end;                                      // Координата Y конца текущего кусочка траектории будет координатой Y начала следующего кусочка графика                                                           

            }


            pictureBox6.Image = bm6;            //  Рисунок на pictureBox6 будет взят из BM6
            pictureBox6.Refresh();               // обновить pictureBox6


        }

        public void DrawGraphic7()
        {
            graph7.Clear(pictureBox7.BackColor);
            for (int k = 0; k <= 10; k++)                       //  Нарисовать координатную сетку 10х10
            {
                graph7.DrawLine(pen0, 20, 20 + k * (pictureBox7.Height - 40) / 10, pictureBox7.Width - 20, 20 + k * (pictureBox7.Height - 40) / 10);          // горизонтальная линия
                graph7.DrawLine(pen0, 20 + k * (pictureBox7.Width - 40) / 10, 20, 20 + k * (pictureBox7.Width - 40) / 10, pictureBox7.Height - 20);           // вертикальная линия
            }

            kh7 = k7 * ((pictureBox7.Width - 40) / Form1.Fr1.T) * Math.Pow(1.5, trackBar13.Value);                                          //  Масштабный коэффициент по горизонтали для 7-го графика
            kv7 = k7 * ((pictureBox7.Height - 40) / (Form1.Fr1.Vmax * Form1.Fr1.Vmax / 2)) * Math.Pow(1.5, trackBar14.Value);                //  Масштабный коэффициент по вертикали для 7-го графика

            x_begin = 20;                                                                                   //  Графическая координата X для начальной точки графика
            y_begin = (int)(pictureBox7.Height - 20 - kv7 * Form1.Fr1.v0 * Form1.Fr1.v0 / 2);               //  Графическая координата Y для начальной точки графика
            for (int i = 21; i <= pictureBox7.Width - 20; i++)
            {
                x_end = i;                                   // вычисление конечной координаты Х
                t = (x_end - 20) / kh7;
                if (t <= Form1.Fr1.T)
                {
                    Vx = Form1.Fr1.v0 * Form1.Fr1.cos;
                    Vy = Form1.Fr1.v0 * Form1.Fr1.sin - Form1.g * t;
                    Ekin = (Vx * Vx + Vy * Vy) / 2;
                }
                else
                {
                    Ekin = 0;
                }
                y_end = (int)(pictureBox7.Height - 20 - kv7 * Ekin);                     // вычисление конечной координаты У
                graph7.DrawLine(pen7, x_begin, y_begin, x_end, y_end);                     // рисовать график
                x_begin = x_end;                                      // Координата X конца текущего кусочка траектории будет координатой X начала следующего кусочка графика
                y_begin = y_end;                                      // Координата Y конца текущего кусочка траектории будет координатой Y начала следующего кусочка графика                                                           

            }


            pictureBox7.Image = bm7;             //  Рисунок на pictureBox7 будет взят из BM7
            pictureBox7.Refresh();               // обновить pictureBox7
        }

        public void DrawGraphic8()
        {
            graph8.Clear(pictureBox8.BackColor);
            for (int k = 0; k <= 10; k++)               //  Нарисовать координатную сетку 10х10
            {
                graph8.DrawLine(pen0, 20, 20 + k * (pictureBox8.Height - 40) / 10, pictureBox8.Width - 20, 20 + k * (pictureBox8.Height - 40) / 10);            // горизонтальная линия
                graph8.DrawLine(pen0, 20 + k * (pictureBox8.Width - 40) / 10, 20, 20 + k * (pictureBox8.Width - 40) / 10, pictureBox8.Height - 20);             // вертикальная линия
            }

            kh8 = k8 * ((pictureBox8.Width - 40) / Form1.Fr1.T) * Math.Pow(1.5, trackBar15.Value);                               //  Масштабный коэффициент по горизонтали для 8-го графика
            kv8 = k8 * ((pictureBox8.Height - 40) / (Form1.g * Form1.Fr1.H)) * Math.Pow(1.5, trackBar16.Value);                   //  Масштабный коэффициент по вертикали для 8-го графика

            x_begin = 20;                                                                            //  Графическая координата X для начальной точки графика
            y_begin = (int)(pictureBox8.Height - 20 - kv8 * Form1.g * Form1.Fr1.h0);                  //  Графическая координата Y для начальной точки графика
            for (int i = 21; i <= pictureBox8.Width - 20; i++)
            {
                x_end = i;                          // вычисление конечной координаты Х
                t = (x_end - 20) / kh8;
                if (t <= Form1.Fr1.T)
                {
                    Ep = Form1.g * (Form1.Fr1.h0 + Form1.Fr1.v0 * Form1.Fr1.sin * t - Form1.g * t * t / 2);
                }
                else
                {
                    Ep = 0;
                }
                y_end = (int)(pictureBox8.Height - 20 - kv8 * Ep);                        // вычисление конечной координаты У
                graph8.DrawLine(pen8, x_begin, y_begin, x_end, y_end);                       // рисовать график
                x_begin = x_end;                                      // Координата X конца текущего кусочка траектории будет координатой X начала следующего кусочка графика
                y_begin = y_end;                                      // Координата Y конца текущего кусочка траектории будет координатой Y начала следующего кусочка графика                                                           

            }

            pictureBox8.Image = bm8;            //  Рисунок на pictureBox7 будет взят из BM8
            pictureBox8.Refresh();               // обновить pictureBox8
        }


        // перерисовать график при движении trackBar - ов для pictureBox1
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            DrawGraphic1();
        }
        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            DrawGraphic1();
        }

        // перерисовать график при движении trackBar - ов для pictureBox2
        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            DrawGraphic2();
        }
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            DrawGraphic2();
        }

        // перерисовать график при движении trackBar - ов для pictureBox3
        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            DrawGraphic3();
        }
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            DrawGraphic3();
        }

        // перерисовать график при движении trackBar - ов для pictureBox4
        private void trackBar8_Scroll(object sender, EventArgs e)
        {
            DrawGraphic4();

        }
        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            DrawGraphic4();
        }

        // перерисовать график при движении trackBar - ов для pictureBox5
        private void trackBar10_Scroll(object sender, EventArgs e)
        {
            DrawGraphic5();
        }
        private void trackBar9_Scroll(object sender, EventArgs e)
        {
            DrawGraphic5();
        }

        // перерисовать график при движении trackBar - ов для pictureBox6
        private void trackBar11_Scroll(object sender, EventArgs e)
        {
            DrawGraphic6();
        }
        private void trackBar12_Scroll(object sender, EventArgs e)
        {
            DrawGraphic6();
        }

        // перерисовать график при движении trackBar - ов для pictureBox7
        private void trackBar14_Scroll(object sender, EventArgs e)
        {
            DrawGraphic7();
        }
        private void trackBar13_Scroll(object sender, EventArgs e)
        {
            DrawGraphic7();
        }

        // перерисовать график при движении trackBar - ов для pictureBox8
        private void trackBar16_Scroll(object sender, EventArgs e)
        {
            DrawGraphic8();
        }
        private void trackBar15_Scroll(object sender, EventArgs e)
        {
            DrawGraphic8();
        }



    }
}
