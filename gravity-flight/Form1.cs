using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _1_ая_лабораторная
{
    public partial class Form1 : Form
    {
        public double v0;                                    //  переменная нач. скорости(задаётся)
        double alpha;                                        //  переменная угла броска(задаётся)
        public double h0;                                    //  переменная нач. высоты(задаётся)

        public double sin;                                   //  переменная угла sin(находится)
        public double cos;                                   //  переменная угла cos(находится)
        double D;                                            //  переменная дискриминанта(находится)
        public const double g = 9.81;                        //  константа ускорения свободного падения

        public double L;                                     //  дальность полета тела(находится)
        public double T;                                     //  переменная времени полёта(находится)
        public double H;                                     //  переменная макс. высоты(находится)

        Graphics graph1;                                     //  графический инструмент
        
        Bitmap bm1 = new Bitmap(1920, 1080);                    //  объект для хранения рисунка в ОЗУ
                         
        Pen pen1 = new Pen(Color.Red, 3);                       //  карандаш(цвет траектори - красный) толщиной 3
        Pen pen2 = new Pen(Color.Blue, 2);                      //  карандаш(цвет контура объекта - синий) толщиной 2
        Pen pen3 = new Pen(Color.Gray, 2);                      //  карандаш(цвет полос на графике - серый) толщиной 2

        SolidBrush br1 = new SolidBrush(Color.Blue);            //  кисть(цвет заполняется внутри контура объекта)

        double t;                                                //  счетчик времени 
        public double dt;                                        //  шаг времени
        double x;                                                //  математическая координата X тела           
        double y;                                                //  математическая координата У тела
        double kx;                                               //  масштабный коэффициент по горизонтали (вдоль оси X)
        double ky;                                               //  масштабный коэффициент по вертикали (вдоль оси Y)
        double K;                                                //  итоговый масштабный коэффициент
        double k_max;                                            //  максимальное значение масштабного коэффициента
        double k_min;                                            //  минимальное значение масштабного коэффициента
        public double Vmax;                                      //  максимальная скорость

        int x_begin;                                             //  графическая координата X (в пикселях) для начала линии
        int y_begin;                                             //  графическая координата Y (в пикселях) для начала
        int x_end;                                               //  графическая координата X (в пикселях) для конца линии       
        int y_end;                                               //  графическая координата Y (в пикселях) для конца линии
        bool flag = false;                                       //  логическая переменная, показывающая был ли произведен запуск движения и выполнены все расчеты

        public static Form1 Fr1;                                    //  Объект для обращения к главному окну (форме Form1)
        public static Form2 Fr2;                                    //  Объект для обращения к дополнительному окну (форме Form2)


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Fr1 = this;                                                 //  В Fr1 записывается ссылка на первое (главное) окно приложения. После этого к нему можно обращаться по имени Fr1.

            graph1 = Graphics.FromImage(bm1);                           //  Определение объекта Gr1 (инструмента для рисования), чтобы он работал на объекте BM1

            pictureBox1.Image = bm1;                                    //  Рисунок на pictureBox1 будет взят из BM1
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            v0 = double.Parse(textBox1.Text);                               // Начальная скорость
            alpha = double.Parse(textBox2.Text) * Math.PI / 180;            // Начальный угол направления скорости к горизонту (переводится в радианы)
            h0 = double.Parse(textBox3.Text);                               // Начальная высота

            sin = Math.Sin(alpha);                                          // Синус начального угла
            cos = Math.Cos(alpha);                                          // Косинус начального угла
            D = v0 * v0 * sin * sin + 2 * g * h0;                           // Дискриминант квадратного уравнения

            T = (v0 * sin + Math.Sqrt(D)) / g;                              // Время движения
            L = v0 * cos * T;                                               // Дальность полета
            H = D / (2 * g);                                                // Максимальная высота
            Vmax = Math.Sqrt(v0 * v0 + 2 * g * h0);                         // Максимальная скорость

            // Округление значений(четыре знака после запятой):
            L = Math.Round(L, 4);                                            
            T = Math.Round(T, 4);                                                
            H = Math.Round(H, 4);                                              

            // Вывод результатов:
            textBox4.Text = L.ToString();
            textBox5.Text = T.ToString();
            textBox6.Text = H.ToString();


          

            for (int k = 0; k <= 10; k++)               
            {
                graph1.DrawLine(pen3, 20, 20 + k * (pictureBox1.Height - 40) / 10, pictureBox1.Width - 20, 20 + k * (pictureBox1.Height - 40) / 10);            // горизонтальная линия
                graph1.DrawLine(pen3, 20 + k * (pictureBox1.Width - 40) / 10, 20, 20 + k * (pictureBox1.Width - 40) / 10, pictureBox1.Height - 20);             // вертикальная линия

            }

            t = 0;                                                     //  Обнулить счетчик времени 
            dt = T / 1000;                                              //  Определить временной шаг

            kx = (pictureBox1.Width - 40) / L;                         //  Определить масштабный коэффициент по горизонтали
            ky = (pictureBox1.Height - 40) / H;                        //  Определить масштабный коэффициент по вертикали

            if (kx < ky) K = kx; else K = ky;                          //  Определить итоговый масштабный коэффициент
            k_min = K / 3;                                             //  Определить минимальное значение итогового масштабного коэффициента
            k_max = 3 * K;                                             //  Определить максимальное значение итогового масштабного коэффициента
            flag = true;                                               //  Вычисления выполнены, можно изображать метку label8, которая перемещается с курсором мыши по pictureBox1

            DrawPicture();                                                                 //  Нарисовать рисунок


            timer1.Enabled = true;                                                         //  Включить таймер (с этого момента начнет происходить моделирование движения тела)
            button1.Enabled = false;                                                       //  Заблокировать кнопку button1 (ПУСК)
            button2.Enabled = true;                                                        //  Разблокировать кнопку button2 (СТОП)

            listBox1.Items.Clear();                                                        //  Очистить listBox1               
            listBox1.Items.Add("Текущее время, с:\tКоордината X и Y, м:");                 //  Вывод в listBox1 заглавной строки
            listBox1.Items.Add("");                                                        //  Вывод в listBox1 пустой строки                     



        }

        private void button2_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;                   //  Изменение состояния таймера на противоположное (т.е. если таймер включен, то выключить, или наоборот)
            if (timer1.Enabled)
            {
                //  если таймер включен, то выполнить команды:
                button1.Enabled = false;
                button2.Text = "Стоп";
                пускToolStripMenuItem.Enabled = false;
                стопToolStripMenuItem.Text = "Стоп";
            }
            else
            {
                //  если таймер выключен, то выполнить команды:
                button1.Enabled = true;
                button2.Text = "Продолжить";
                пускToolStripMenuItem.Enabled = true;
                стопToolStripMenuItem.Text = "Продолжить";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();                               //  Завершение работы приложения
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            t ++;                                       // Изменение счетчика времени



            textBox7.Text = (t * dt).ToString();                                                                  //  Вывод текущего времени t*dt в textBox7                                                              
            listBox1.Items.Add(string.Format("t = {0:f12}\tx = {1:f12}\ty = {2:f12}", t * dt, x, y));             //  Добавление в listBox1 строки с данными текущего положения тела




            DrawPicture();                          //  Перерисовать рисунок

            if (t >= 1000)                           //  Если движение закончилось (тело упало на землю, т.е. y = 0)
            {
                timer1.Enabled = false;             //  Выключить таймер
                button1.Enabled = true;             //  Разблокировать кнопку button1 (ПУСК)
                button2.Enabled = false;            //  Заблокировать кнопку button2 (СТОП)
                MessageBox.Show("Движение завершенно", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Stop);       //  Окно-сообщение MessageBox
            }
            else    
            {
                x_begin = x_end;                    //  координата X конца текущего кусочка траектории будет координатой X начала следующего кусочка траектории
                y_begin = y_end;                         //  координата Y конца текущего кусочка траектории будет координатой Y начала следующего кусочка траектории
            }

        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            if (!flag) return;              // Выход из процедуры, если не выполнены вычисления (flag = false)
            label10.Visible = true;          // Метка label10 становится видимой

        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            if (!flag) return;              // Выход из процедуры, если не выполнены вычисления (flag = false)
            label10.Visible = false;          // Метка label10 становится невидимой
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!flag) return;                                          // Выход из процедуры, если не выполнены вычисления (flag = false)
            label10.Left = pictureBox1.Left + (e.X + 10);                //  Изменение координаты X метки label10
            label10.Top = pictureBox1.Top + (e.Y + 10);                  //  Изменение координаты Y метки label10
            label10.Text = string.Format("( {0:f3}; {1:f3})", (e.X - 20) / K, (pictureBox1.Height - 20 - e.Y) / K);   //  Изменение текста метки label10
        }

       

     
        private void показатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Fr2 = new Form2();                              //  Создание объекта Fr2 (дополнительного окна)
            Fr2.Visible = true;                             //  Дополнительное окно сделать видимым на дисплее
            показатьToolStripMenuItem.Enabled = false;      //  Заблокировать кнопку "Показать" в меню, чтобы нельзя создавать доп. окно несколько раз

            Fr2.DrawGraphic1();                             // Нарисовать график 1 на Form 2
            Fr2.DrawGraphic2();                             // Нарисовать график 2 на Form 2
            Fr2.DrawGraphic3();                             // Нарисовать график 3 на Form 2    
            Fr2.DrawGraphic4();                             // Нарисовать график 4 на Form 2        
            Fr2.DrawGraphic5();                             // Нарисовать график 5 на Form 2
            Fr2.DrawGraphic6();                             // Нарисовать график 6 на Form 2            
            Fr2.DrawGraphic7();                             // Нарисовать график 7 на Form 2
            Fr2.DrawGraphic8();                             // Нарисовать график 8 на Form 2

            Fr2.SizeChange();                               // Изменение размера формы
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            if (!flag) return;                      // Выход из процедуры, если не выполнены вычисления (flag = false)
            kx = (pictureBox1.Width - 40) / L;      //  Определить масштабный коэффициент по горизонтали          
            ky = (pictureBox1.Height - 40) / H;     //  Определить масштабный коэффициент по вертикали  
            if (kx < ky) K = kx; else K = ky;       //  Определить итоговый масштабный коэффициент       
            k_min = K / 3;                          //  Определить минимальное значение итогового масштабного коэффициента              
            k_max = 3 * K;                          //  Определить максимальное значение итогового масштабного коэффициента

            DrawPicture();
        }

        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (!flag) return;                      // Выход из процедуры, если не выполнены вычисления (flag = false)
            K *= Math.Pow(1.1, e.Delta / 100);      // Изменение масштабного коэффициента при вращении колеса мыши
            if (K < k_min) K = k_min;               // Проверка значения масштабного коэффициента, чтобы не вышло за нижнюю границу установленного диапозона  
            if (K > k_max) K = k_max;               // Проверка значения масштабного коэффициента, чтобы не вышло за верхнюю границу установленного диапозона


            label10.Text = string.Format("( {0:f3}, {1:f3})", (e.X - 20) / K, (pictureBox1.Height - 20 - e.Y) / K);              //  Изменение текста метки label10

            DrawPicture();              //  Перерисовать рисунок
        }

        private void DrawPicture() 
        {
            graph1.Clear(pictureBox1.BackColor);            //  Стереть старый рисунок         
            for (int k = 0; k <= 10; k++)                   //  Нарисовать координатную сетку 10х10
            {
                graph1.DrawLine(pen3, 20, 20 + k * (pictureBox1.Height - 40) / 10, pictureBox1.Width - 20, 20 + k * (pictureBox1.Height - 40) / 10);        // горизонтальная линия
                graph1.DrawLine(pen3, 20 + k * (pictureBox1.Width - 40) / 10, 20, 20 + k * (pictureBox1.Width - 40) / 10, pictureBox1.Height - 20);         // вертикальная линия
            }

            int i = 0;                  // номер шага для траетории движения(начальное значение - ноль)
            x_begin = 20;               //  Графическая координата X для начальной точки траектории         
            y_begin = (int)(pictureBox1.Height - 20 - K * h0);          //  Графическая координата Y для начальной точки траектории

            while (i <= t)    //  счетчик i считает от 0 до текущего положения t, где находится тело в данный момент
            {
                i++;                                                               // Изменение счетчика 
                x = v0 * cos * (i * dt);                                           // Вычисление математической координаты X           
                y = h0 + v0 * sin * (i * dt) - g * (i * dt) * (i * dt) / 2;        // Вычисление математической координаты Y
                x_end = (int)(20 + K * x);                                         // Вычисление графической координаты X
                y_end = (int)(pictureBox1.Height - 20 - K * y);                    // Вычисление графической координаты Y    
                graph1.DrawLine(pen1, x_begin, y_begin, x_end, y_end);             // Рисование линии - очередного маленького кусочка траектории движения тела на BM1   
                x_begin = x_end;                                                   // Координата X конца текущего кусочка траектории будет координатой X начала следующего кусочка траектории           
                y_begin = y_end;                                                   // Координата Y конца текущего кусочка траектории будет координатой Y начала следующего кусочка траектории   
            }

            graph1.FillEllipse(br1, x_end - 10, y_end - 10, 21, 21);               //  Рисование тела в виде закрашенного круга 
            graph1.DrawEllipse(pen2, x_end - 10, y_end - 10, 21, 21);              //  Рисование тела в виде окружности
            pictureBox1.Refresh();                                                 //  Обновление рисунка BM1 на pictureBox1
        }

        //  Процедура выполняется при нажатии кнопки "Пуск" в меню
        private void пускToolStripMenuItem_Click(object sender, EventArgs e)
        {
            v0 = double.Parse(textBox1.Text);
            alpha = double.Parse(textBox2.Text) * Math.PI / 180;
            h0 = double.Parse(textBox3.Text);

            sin = Math.Sin(alpha);
            cos = Math.Cos(alpha);
            D = v0 * v0 * sin * sin + 2 * g * h0;

            T = (v0 * sin + Math.Sqrt(D)) / g;
            L = v0 * cos * T;
            H = D / (2 * g);
            Vmax = Math.Sqrt(v0 * v0 + 2 * g * h0);

            L = Math.Round(L, 4);
            T = Math.Round(T, 4);
            H = Math.Round(H, 4);

            textBox4.Text = L.ToString();
            textBox5.Text = T.ToString();
            textBox6.Text = H.ToString();




            for (int k = 0; k <= 10; k++)
            {
                graph1.DrawLine(pen3, 20, 20 + k * (pictureBox1.Height - 40) / 10, pictureBox1.Width - 20, 20 + k * (pictureBox1.Height - 40) / 10);
                graph1.DrawLine(pen3, 20 + k * (pictureBox1.Width - 40) / 10, 20, 20 + k * (pictureBox1.Width - 40) / 10, pictureBox1.Height - 20);

            }

            t = 0;
            dt = T / 100;

            kx = (pictureBox1.Width - 40) / L;
            ky = (pictureBox1.Height - 40) / H;

            if (kx < ky) K = kx; else K = ky;
            k_min = K / 3;
            k_max = 3 * K;
            flag = true;

            DrawPicture();


            timer1.Enabled = true;
            button1.Enabled = false;
            button2.Enabled = true;

            listBox1.Items.Clear();
            listBox1.Items.Add("Текущее время, с:\tКоордината X и Y, м:");
            listBox1.Items.Add("");

        }

        //  Процедура выполняется при нажатии кнопки "Стоп" в меню
        private void стопToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;                   //  Изменение состояния таймера на противоположное (т.е. если таймер включен, то выключить, или наоборот)
            if (timer1.Enabled)
            {
                //  если таймер включен, то выполнить команды:
                button1.Enabled = false;
                button2.Text = "Стоп";
                пускToolStripMenuItem.Enabled = false;
                стопToolStripMenuItem.Text = "Стоп";
            }
            else
            {
                //  если таймер выключен, то выполнить команды:
                button1.Enabled = true;
                button2.Text = "Продолжить";
                пускToolStripMenuItem.Enabled = true;
                стопToolStripMenuItem.Text = "Продолжить";
            }
        }
        //  Процедура выполняется при нажатии кнопки "Выход" в меню
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}
