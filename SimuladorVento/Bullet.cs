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
    public class Bullet
    {
        private Vector2 pos, velo, acel;
        private Size dim;
        private SolidBrush pincel;
        private int number;
        private Rectangle rect;
        public Rectangle bR;
        private bool rem;
        public Vector2 remAcel;
        private bool goalAchieved;

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
        public Rectangle Rect
        {
            get { return rect; }
            set { rect = value; }
        }
        public Vector2 RemAcel
        {
            get { return remAcel; }
            set { remAcel = value; }
        }
        public bool Rem
        {
            get { return rem; }
            set { rem = value; }
        }
        public bool GoalAchieved
        {
            get { return goalAchieved; }
            set { goalAchieved = value; }
        }

        public Bullet(Vector2 position, int n)
        {
            pos = position;
            number = n;
            velo = new Vector2(2,0);
            acel = new Vector2(0);
            dim = new Size(8, 8);
            Color c = ColorTranslator.FromHtml("#FFFA9D");
            pincel = new SolidBrush(c);
            rect = new Rectangle(
                new Point((int)-dim.Width / 2, (int)-dim.Height / 2),
                dim);
        }

        public void move()
        {
            velo += acel;
            pos += velo;
        }

        public void draw(Graphics g)
        {
            g.ResetTransform();
            g.TranslateTransform(pos.X, pos.Y, MatrixOrder.Append);
            g.FillEllipse(pincel, rect);
        }
    }
}
