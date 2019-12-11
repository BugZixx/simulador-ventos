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
    public class WindBox
    {
        private Vector2 pos, force;
        private float rotation;
        private Pen myPen;
        private Point[] points;

        public WindBox(Vector2 pos, float x, float y)
        {
            this.pos = pos + (new Vector2(x, y));
            points = new Point[] { new Point(15, 5), new Point(35, 0), new Point(15, -5) };
            myPen = new Pen(Color.Red, 1);

        }

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

        public float Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }

        public void draw(Graphics g)
        {
            g.ResetTransform();
            g.TranslateTransform(pos.X, pos.Y, MatrixOrder.Append);
            g.RotateTransform(rotation);
            g.DrawPolygon(myPen, points);
        }
    }
}
