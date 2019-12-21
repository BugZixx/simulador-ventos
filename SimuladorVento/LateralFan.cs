using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            this.angle = newForce;
            this.number = n;
            points = new Point[] { new Point(0, 22), new Point(-6, 20), new Point(-7, 17), new Point(-6, 14), new Point(-5, 10), new Point(-4, 7), new Point(-3, 4), new Point(-6, -3), new Point(-6, -6), new Point(-5, -8), new Point(0, -11), new Point(0, -14), new Point(2, -22), new Point(6, -18), new Point(7, -14), new Point(7, -2), new Point(5, 4), new Point(3, 7), new Point(5, 12), new Point(5, 17), new Point(2, 20) };
            windBox = new WindBox(pos, points[1].X, points[1].Y);
            dim = new SizeF(6, 6);
            pincel = new SolidBrush(Color.White);
            myPen = new Pen(Color.White);
            force = -0.1f;
        }

        public override Vector2 getNewPosition(float r, float x, float y)
        {
            // metodo igual usado em FrontalFan.cs
            // explicado lá
            
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
            g.TranslateTransform(pos.X - (points[1].X / 2), pos.Y - (points[1].Y / 2), MatrixOrder.Append);
            g.ScaleTransform(2, 2);
            g.RotateTransform(rotation);
            g.DrawImage(fanImg, fanRect);
            //g.DrawPolygon(myPen, points);
            RectangleF rect = new RectangleF(
                new Point((int)-dim.Width / 2, (int)-dim.Height / 2),
                dim);
            g.FillEllipse(pincel, rect);

            Vector2 newPos = getNewPosition(rotation, 10, 0);
            windBox.Pos = new Vector2(pos.X + newPos.X + 5, pos.Y + newPos.Y - 10);
            windBox.Rotation = rotation;
            windBox.draw(g);
        }
    }
}
