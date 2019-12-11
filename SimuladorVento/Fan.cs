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
        public Point[] points;
        protected int number;
        protected float rotation;
        
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
        public bool IsInPolygon(Point[] p, float X, float Y)
        {
            for (int k = 0; k < p.Length; k++)
            {
                p[k].X += (int)pos.X;
                p[k].Y += (int)pos.Y;
            }
            // Get the angle between the point and the
            // first and last vertices.
            int max_point = p.Length - 1;
            float total_angle = GetAngle(
                p[max_point].X, p[max_point].Y,
                X, Y,
                p[0].X, p[0].Y);

            // Add the angles from the point
            // to each other pair of vertices.
            for (int i = 0; i < max_point; i++)
            {
                total_angle += GetAngle(
                    p[i].X, p[i].Y,
                    X, Y,
                    p[i + 1].X, p[i + 1].Y);
            }

            // The total angle should be 2 * PI or -2 * PI if
            // the point is in the polygon and close to zero
            // if the point is outside the polygon.
            return (Math.Abs(total_angle) > 1);
        }

        public static float GetAngle(float Ax, float Ay, float Bx, float By, float Cx, float Cy)
        {
            // Get the dot product.
            float dot_product = DotProduct(Ax, Ay, Bx, By, Cx, Cy);

            // Get the cross product.
            float cross_product = CrossProductLength(Ax, Ay, Bx, By, Cx, Cy);

            // Calculate the angle.
            return (float)Math.Atan2(cross_product, dot_product);
        }

        public static float CrossProductLength(float Ax, float Ay,
            float Bx, float By, float Cx, float Cy)
        {
            // Get the vectors' coordinates.
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            // Calculate the Z coordinate of the cross product.
            return (BAx * BCy - BAy * BCx);
        }

        private static float DotProduct(float Ax, float Ay,
            float Bx, float By, float Cx, float Cy)
        {
            // Get the vectors' coordinates.
            float BAx = Ax - Bx;
            float BAy = Ay - By;
            float BCx = Cx - Bx;
            float BCy = Cy - By;

            // Calculate the dot product.
            return (BAx * BCx + BAy * BCy);
        }

        public abstract void draw(Graphics g);
    }
}
