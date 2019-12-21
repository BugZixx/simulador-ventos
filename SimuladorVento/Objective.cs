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
        private int posWidth;
        private bool goalAchieved;

        private Pen myPen;
        private Point[] points;

        private List<Bullet> bullets;

        public Objective(int width, int height)
        {
            rndPos = new Random();
            posWidth = rndPos.Next(0, width / 2 - 30);
            pos = new Vector2(width - posWidth, height-10);
            rotation = 0;
            points = new Point[] { new Point(-14, 0), new Point(-8, -5), new Point(0, -6), new Point(8, -5), new Point(14, 0), new Point(8, 5), new Point(-8, 5) };
            myPen = new Pen(Color.White, 1);
            bullets = new List<Bullet>();
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public int PosWidth
        {
            get { return posWidth; }
            set { posWidth = value; }
        }

        public Point[] Points
        {
            get { return points; }
            set { points = value; }
        }
        public bool GoalAchieved
        {
            get { return goalAchieved; }
            set { goalAchieved = value; }
        }

        public void draw(Graphics g)
        {
            // define se desenha alvo atingido (versão verde) ou não atingido (versão vermelha)
            if(!goalAchieved)
                targetImg = new Bitmap(Properties.Resources.target);
            else
                targetImg = new Bitmap(Properties.Resources.targetAchieved);
            targetRect = new Rectangle(0 - targetImg.Width / 20, 0 - targetImg.Height / 20, targetImg.Width / 10, targetImg.Height / 10);
            g.ResetTransform();
            g.RotateTransform(rotation, MatrixOrder.Append);
            g.TranslateTransform(pos.X, pos.Y, MatrixOrder.Append);
            g.ScaleTransform(2, 2);
            g.DrawImage(targetImg, targetRect);
            g.DrawPolygon(myPen, points);
        }
    }
}
