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

    class Obstacle
    {
        private Rectangle area;
        private SolidBrush pencil;
        private Random rnd;
        private Vector2 pos;
        private int s1, s2;

        public Obstacle(String size1, String size2, int H, int W, Random rnd)
        {
            s1 = Int32.Parse(size1);
            s2 = Int32.Parse(size2);
            pos.X = rnd.Next(50, W - s1 - 10);
            pos.Y = rnd.Next(50, H - s2 - 15);
            area = new Rectangle((int)pos.X, (int)pos.Y, s1, s2);
            pencil = new SolidBrush(Color.Bisque);
        }

        public Rectangle Area
        {
            get { return area; }
            set { area = value; }
        }

        public void draw(Graphics g)
        {
            g.ResetTransform();
            g.FillRectangle(pencil, area);
        }
    }
}
