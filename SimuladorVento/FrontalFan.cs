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
    public class FrontalFan : Fan
    {

        public FrontalFan(Vector2 newPos, Vector2 newForce, int n)
        {
            fanImg = new Bitmap(Properties.Resources.fan);
            fanRect = new Rectangle(0 - fanImg.Width / 30, 0 - fanImg.Height / 30, fanImg.Width / 15, fanImg.Height / 15);
            this.pos = newPos;
            this.rotation = 0;
            this.angle = newForce;
            this.number = n;
            points = new Point[] { new Point(-10, 0), new Point(-6, 6), new Point(-1, 7), new Point(3, 4), new Point(6, 12), new Point(8, 13), new Point(9, 0), new Point(8, -13), new Point(6, -12), new Point(3, -4), new Point(-1, -7), new Point(-6, -6) };
            windBox = new WindBox(pos, points[6].X, points[6].Y);
            dim = new SizeF(6, 6);
            pincel = new SolidBrush(Color.White);
            force = -0.1f;
        }

        public override Vector2 getNewPosition(float r, float x, float y)
        {
            Vector2 newPos;
            float x1, y1;
            r *= (float)(Math.PI / 180);
            x1 = (float)(x * Math.Cos(r) - y * Math.Sin(r));
            y1 = (float)(y * Math.Cos(r) + x * Math.Sin(r));

            newPos = new Vector2(x1, y1);

            return newPos;
        }

        public override void draw(Graphics g)
        {
            g.ResetTransform();
            g.TranslateTransform(pos.X - (points[6].X / 2), pos.Y - (points[6].Y / 2), MatrixOrder.Append);
            g.ScaleTransform(2, 2);
            g.RotateTransform(rotation);
            g.DrawImage(fanImg, fanRect);
            RectangleF rect = new RectangleF(
                new Point((int)-dim.Width / 2, (int)-dim.Height / 2),
                dim);
            g.FillEllipse(pincel, rect);

            Vector2 newPos = getNewPosition(rotation, 10, 0);
            Vector2 newHitBoxPos = getNewPosition(rotation, windBox.Rect.X, windBox.Rect.Y);

            windBox.Pos = new Vector2(pos.X + newPos.X - 5, pos.Y + newPos.Y);
            windBox.Rotation = rotation;
            windBox.draw(g);
        }
    }
}
