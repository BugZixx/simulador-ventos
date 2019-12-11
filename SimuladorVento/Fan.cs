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

        public abstract bool IsInPolygon(Point[] poly, Point point);

        public abstract void draw(Graphics g);
    }
}
