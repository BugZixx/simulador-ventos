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
        private Rectangle rect;

        public WindBox(Vector2 pos, float x, float y)
        {
            this.pos = pos + (new Vector2(x, y));
            rect = new Rectangle(15, -25, 85, 50);
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

        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        public void draw(Graphics g)
        {
            g.ResetTransform();
            g.TranslateTransform(pos.X, pos.Y, MatrixOrder.Append);
            g.RotateTransform(rotation);
            g.DrawRectangle(myPen, rect);
        }
    }
}
