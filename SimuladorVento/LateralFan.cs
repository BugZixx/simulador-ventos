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
    public class LateralFan : Fan
    {

        public LateralFan(Vector2 newPos, Vector2 newForce, int n)
        {
            fanImg = new Bitmap(Properties.Resources.fanLateral);
            fanRect = new Rectangle(0 - fanImg.Width / 22, 0 - fanImg.Height / 22, fanImg.Width / 11, fanImg.Height / 11);
            this.pos = newPos;
            this.rotation = 0;
            this.force = newForce;
            this.number = n;
            points = new Point[] { new Point(-10, 0), new Point(-6, 6), new Point(-1, 7), new Point(3, 4), new Point(6, 12), new Point(8, 13), new Point(9, 0), new Point(8, -13), new Point(6, -12), new Point(3, -4), new Point(-1, -7), new Point(-6, -6) };
            windBox = new WindBox(pos, points[1].X, points[1].Y);
            myPen = new Pen(Color.White, 1);
        }

        public override Vector2 getNewPosition(float r, float x, float y)
        {
            Vector2 newPos;
            float x1, y1;
            x = 16;
            y = 0;
            r *= (float)(Math.PI / 180);
            x1 = (float)(x * Math.Cos(r) - y * Math.Sin(r));
            y1 = (float)(y * Math.Cos(r) + x * Math.Sin(r));

            newPos = new Vector2(x1, y1);

            return newPos;
        }

        public override void draw(Graphics g)
        {
            g.ResetTransform();
            g.TranslateTransform(pos.X - (points[1].X / 2), pos.Y - (points[1].Y / 2), MatrixOrder.Append);
            g.ScaleTransform(2, 2);
            g.RotateTransform(rotation);
            g.DrawImage(fanImg, fanRect);
            g.DrawPolygon(myPen, points);

            Vector2 newPos = getNewPosition(rotation, 10, 0);

            windBox.Pos = new Vector2(pos.X + newPos.X - 8, pos.Y + newPos.Y);
            windBox.Rotation = rotation - 90;
            windBox.draw(g);
        }
    }
}
