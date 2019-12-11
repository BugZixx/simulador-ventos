using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace SimuladorVento
{
    class Objective
    {
        int i = 0;
        private Vector2 pos;
        private float rotation;
        private Random rndPos;
        private Bitmap targetImg;
        private Rectangle targetRect;

        private Pen myPen;
        private Point[] points;

        private List<Bullet> bullets;

        public Objective(int width, int height)
        {
            targetImg = new Bitmap(Properties.Resources.target);
            targetRect = new Rectangle(0 - targetImg.Width / 20, 0 - targetImg.Height / 20, targetImg.Width / 10, targetImg.Height / 10);
            rndPos = new Random();
            pos = new Vector2(rndPos.Next(width / 2, width - 30), height-10);
            rotation = 0;
            points = new Point[] { new Point(-5, 0), new Point(0, -10), new Point(5, 0) };
            myPen = new Pen(Color.White, 1);
            bullets = new List<Bullet>();
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public void draw(Graphics g)
        {
            g.ResetTransform();
            g.RotateTransform(rotation, MatrixOrder.Append);
            g.TranslateTransform(pos.X, pos.Y, MatrixOrder.Append);
            g.ScaleTransform(2, 2);
            g.DrawImage(targetImg, targetRect);
            g.DrawPolygon(myPen, points);
        }
    }
}
