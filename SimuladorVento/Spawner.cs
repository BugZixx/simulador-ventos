using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SimuladorVento
{
    public class Spawner
    {
        int i = 0;
        private Vector2 pos;
        private float rotation;
        private Random rndPos;
        protected Bitmap spawnerImg;
        protected Rectangle spawnerRect;

        private Pen myPen;
        private Point[] points;

        private List<Bullet> bullets;

        public Spawner(int height)
        {
            spawnerImg = new Bitmap(Properties.Resources.spawner);
            spawnerRect = new Rectangle(0 - spawnerImg.Width / 20, 0 - spawnerImg.Height / 20, spawnerImg.Width / 10, spawnerImg.Height / 10);
            rndPos = new Random();
            pos = new Vector2(10, rndPos.Next(60, height / 2));
            rotation = 0;
            points = new Point[] { new Point(0, 5), new Point(10, 0), new Point(0, -5)};
            myPen = new Pen(Color.White, 1);
            bullets = new List<Bullet>();
        }

        public List<Bullet> Bullets
        {
            get { return bullets; }
            set { bullets = value; }
        }

        public Vector2 Pos
        {
            get { return pos; }
            set { pos = value; }
        }

        public void SpawnBullet()
        {
            //metodo para criar/disparar bullets
            if (i == 6)
            {
                Bullet e = new Bullet(new Vector2(pos.X + 18, pos.Y), 1);
                bullets.Add(e);
                i = 0;
            }
            i++;
        }

        public void moveBullets()
        {
            // metodo para mover as bullets
            foreach (Bullet e in bullets)
            {
                e.move();
            }
        }

        public void draw(Graphics g)
        {
            foreach (Bullet e in bullets)
                e.draw(g);

            g.ResetTransform();
            g.RotateTransform(rotation, MatrixOrder.Append);
            g.TranslateTransform(pos.X, pos.Y, MatrixOrder.Append);
            g.ScaleTransform(2, 2);
            g.DrawImage(spawnerImg, spawnerRect);
            //g.DrawPolygon(myPen, points);
        }
    }
}
