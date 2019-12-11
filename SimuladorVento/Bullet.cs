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
    class Bullet
    {
        private Vector2 pos, velo, acel;
        private SizeF dim;
        private SolidBrush pincel;
        private int number;

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public Vector2 Velo
        {
            get { return velo; }
            set { velo = value; }
        }

        public Vector2 Acel
        {
            get { return acel; }
            set { acel = value; }
        }

        public float Radius
        {
            get { return dim.Width / 2; }
        }

        public Bullet(Vector2 position, int n)
        {
            pos = position;
            number = n;
            velo = new Vector2(2,0);
            acel = new Vector2(0);
            dim = new SizeF(8, 8);
            Color c = ColorTranslator.FromHtml("#FFFA9D");
            pincel = new SolidBrush(c);
        }

        public void move()
        {
            velo += acel;
            pos += velo;
        }

        public void draw(Graphics g)
        {
            RectangleF rect = new RectangleF(
                new Point((int)-dim.Width / 2, (int)-dim.Height / 2),
                dim);
            g.ResetTransform();
            g.TranslateTransform(pos.X, pos.Y, MatrixOrder.Append);
            g.FillEllipse(pincel, rect);
        }
    }
}
