using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LissajousCurveTable
{
    public partial class Form1 : Form
    {

        public Color clearColor = Color.Black;
        public Graphics gfx, g;
        public SolidBrush brush = new SolidBrush(Color.White);
        public Pen pen = new Pen(Color.White);

        public Thread thread;

        public Bitmap img;

        public int r = 50;

        public int cols;
        public int rows;

        public float space;
        public float angle = 0;
        public float speed = 1;

        Curve[,] curves;
        

        public Form1()
        {
            InitializeComponent();
            

      

            g = this.CreateGraphics();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;


            
            

            img = new Bitmap(Width, Height);
            gfx = Graphics.FromImage(img);

            cols = Width / (2 * r);
            rows = Height / (2 * r);

            space = (2 * r) / 5;

            curves = new Curve[cols, cols];
            for (int i = 1; i < cols; i++)
            {
                for (int j = 1; j < cols; j++)
                {
                    curves[i, j] = new Curve();
                }
            }
        }

        private void draw()
        {
            gfx.Clear(clearColor);
            for (int i = 1; i < cols; i++)
            {
                float x = (r - space / 2) * sin(angle * (speed  / 10f));
                float y = (r - space / 2) * cos(angle * (speed / 10f));

                gfx.DrawEllipse(pen, new RectangleF(new PointF(2 * i * r, space / 2), new SizeF(r * 2 - space, r * 2 - space)));
                point(2 * i * r + r - 15 + x, space / 2 + r - 15 + y);
                line(new Vector2(2 * i * r + r - 15 + x, 0), new Vector2(2 * i * r + r - 15 + x, Height));

                if (!(i >= Height / (2 * r)))
                {
                    gfx.DrawEllipse(pen, new RectangleF(new PointF(space / 2, 2 * i * r), new SizeF(r * 2 - space, r * 2 - space)));
                    point(space / 2 + r - 15 + x, 2 * i * r + r - 15 + y);
                    line(new Vector2(0, 2 * i * r + r - 15 + y), new Vector2(Width, 2 * i * r + r - 15 + y));
                }


                int speedt = 1;
                for (int j = 1; j < rows; j++)
                {
                    
                    float yt = (r - space / 2) * cos(angle * (speedt / 10f));
                    point(2 * i * r + r - 15 + x, 2 * j * r + r - 15 + yt, 10);

                    curves[i, j].AddPoint(2 * i * r + r - 15 + x, 2 * j * r + r - 15 + yt);
                    curves[i, j].Draw(gfx, brush);

                    speedt++;
                }

                speed++;

            }
            if (angle >= 360)
            {
                for (int i = 1; i < cols; i++)
                {
                    for (int j = 1; j < cols; j++)
                    {
                        curves[i, j].Clear();
                    }
                }
                angle = 0;
            }
            angle += 0.18f;
            speed = 1;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
            gfx.Dispose();
            g.Dispose();
            img.Dispose();
        }
        private void line(Vector2 v1, Vector2 v2)
        {
            gfx.DrawLine(pen, v1.x, v1.y, v2.x, v2.y);
        }
        private static float cos(double angle)
        {
            return (float)Math.Cos(angle);
        }

        private static float sin(double angle)
        {
            return (float)Math.Sin(angle);
        }
        private void point(float x, float y)
        {
            gfx.FillEllipse(brush, new RectangleF(x - 10, y - 10, 20, 20));
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
                Close();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (thread != null)
                thread.Abort();
            thread = new Thread(() =>
            {
                while (true)
                {
                    draw();

                    g.DrawImage(img, 0, 0);
                }
            });
            thread.Start();
        }

        private void point(float x, float y, int size)
        {
            gfx.FillEllipse(brush, new RectangleF(x - size / 2, y - size / 2, size, size));
        }
    }
}
