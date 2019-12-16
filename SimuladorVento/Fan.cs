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
    public abstract class Fan
    {
        protected Vector2 pos, force;
        protected Bitmap fanImg;
        protected Rectangle fanRect;
        protected WindBox windBox;
        protected Pen myPen;
        protected SizeF dim;
        protected SolidBrush pincel;
        public Point[] points;
        protected int number;
        protected float rotation;
        public Rectangle wBR;

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }
        public Vector2 Force
        {
            get { return force; }
            set { force = value; }
        }
        public Rectangle FanRect
        {
            get { return fanRect; }
            set { fanRect = value; }
        }
        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public int Number
        {
            get { return number; }
            set { number = value; }
        }
        public WindBox WindBox
        {
            get { return windBox; }
            set { windBox = value; }
        }

        public bool IsInPolygon(Point[] p, float X, float Y)
        {
            for (int k = 0; k < p.Length; k++)
            {
                p[k].X += (int)pos.X - points[6].X;
                p[k].Y += (int)pos.Y;
            }

            int max_point = p.Length - 1;
            float total_angle = GetAngle(
                p[max_point].X, p[max_point].Y,
                X, Y,
                p[0].X, p[0].Y);
            
            for (int i = 0; i < max_point; i++)
            {
                total_angle += GetAngle(
                    p[i].X, p[i].Y,
                    X, Y,
                    p[i + 1].X, p[i + 1].Y);
            }
            
            return (Math.Abs(total_angle) > 1);
        }

        public static float GetAngle(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
        {
            float dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);
            
            float cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);
            
            return (float)Math.Atan2(cross_product, dot_product);
        }

        public static float CrossProductLength(float Ax, float Ay,
            float Bx, float By, float Cx, float Cy)
        {
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;
            
            return (BAx * BCy - BAy * BCx);
        }

        private static float DotProduct(float Ax, float Ay,
            float Bx, float By, float Cx, float Cy)
        {
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;
            
            return (BAx * BCx + BAy * BCy);
        }
        public abstract Vector2 getNewPosition(float r, float x, float y);

        public abstract void draw(Graphics g);
    }
}
