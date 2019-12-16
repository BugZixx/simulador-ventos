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
    class Arena
    {
        private Size area;
        private Spawner spawner;
        private Objective objective;
        private Vector2 savedPos;
        private string action;
        private int fanNumber = 0;
        private bool rotation = false;
        private bool motion = false;
        private int selectedFan;
        private SolidBrush pincel;
        public String x = "";

        private static Arena instancia = null;
        public List<Fan> fans;

        public Arena(Size d)
        {
            area = d;
            spawner = new Spawner(area.Height);
            objective = new Objective(area.Width, area.Height);
            fans = new List<Fan>();
            pincel = new SolidBrush(Color.White);
        }

        public static Arena GetInstancia(Size d)
        {
            if (instancia == null) instancia = new Arena(d);
            return instancia;
        }

        public static Arena Instancia
        {
            get
            {
                if (instancia == null)
                    throw new InvalidOperationException("Objeto não criado. Criar com GetInstancia(.)");
                return instancia;
            }
        }

        public Size Area
        {
            get { return area; }
            set
            {
                if (value.Width > 20 && value.Height > 20)
                    area = value;
            }
        }

        public string Action
        {
            get { return action; }
            set
            {
                action = value;
            }
        }

        public void move()
        {
            spawner.SpawnBullet();
            spawner.moveBullets();
        }

        public void collision(Graphics g)
        {
            foreach (Fan f in fans)
            {
                f.wBR = f.WindBox.Rect;
                f.wBR = new Rectangle(f.wBR.X + (int)f.Pos.X + 5, f.wBR.Y + (int)f.Pos.Y, f.wBR.Width, f.wBR.Height);
                g.ResetTransform();
                g.Transform = rotateAroundPoint(f.Rotation, new Point((int)f.Pos.X - 5, (int)f.Pos.Y));
                foreach (Bullet b in spawner.Bullets)
                {
                    b.bR = b.Rect;
                    b.bR = new Rectangle(b.bR.X + (int)b.Pos.X, b.bR.Y + (int)b.Pos.Y, b.bR.Width, b.bR.Height);
                    if (true)
                        b.Acel += new Vector2(0.01f, 0.01f);
                    g.ResetTransform();
                    g.FillRectangle(pincel, b.bR);
                }
            }
        }

        private Matrix rotateAroundPoint(float angle, Point center)
        {
            Matrix result = new Matrix();
            result.RotateAt(angle, center);
            return result;
        }

        public Vector2 getNewPosition(float r, float x, float y)
        {
            Vector2 newPos;
            float x1, y1;
            r *= (float)(Math.PI / 180);
            x1 = (float)(x * Math.Cos(r) - y * Math.Sin(r));
            y1 = (float)(y * Math.Cos(r) + x * Math.Sin(r));

            newPos = new Vector2(x1, y1);

            return newPos;
        }

        public void addFrontalFan(Vector2 position, int fanNumber)
        {
            savedPos = position;
            FrontalFan newFan = new FrontalFan(position, new Vector2(0, 0), fanNumber);
            fans.Add(newFan);
        }

        public void addLateralFan(Vector2 position, int fanNumber)
        {
            savedPos = position;
            LateralFan newFan = new LateralFan(position, new Vector2(0, 0), fanNumber);
            fans.Add(newFan);
        }

        public void rotateFan(Vector2 newPosition, int fN)
        {
            Vector2 d = newPosition - savedPos;
            float x, y;
            float angle = (float)Math.Atan2(d.X, d.Y);

            x = (float)Math.Cos(angle);
            y = (float)Math.Sin(angle);

            for (int k = 0; k < fans.Count; k++)
            {
                if (fN == fans[k].Number)
                {
                    fans[k].Force = new Vector2(x, y);

                    fans[k].Rotation = (float)Math.Atan2(x, y);
                    fans[k].Rotation *= 180f / (float)Math.PI;
                }
            }
        }

        public void mouseClick(object sender, MouseEventArgs e)
        {
            switch (action)
            {
                case "createFrontal":
                    rotation = true;
                    addFrontalFan(new Vector2(e.X, e.Y), fanNumber);
                    break;
                case "createLateral":
                    rotation = true;
                    addLateralFan(new Vector2(e.X, e.Y), fanNumber);
                    break;
                case "move":
                    for (int k = 0; k < fans.Count; k++)
                    {
                        if (fans[k].IsInPolygon(fans[k].points.ToArray(), e.X, e.Y))
                        {
                            selectedFan = k;
                            motion = true;
                        }
                    }
                    break;
                case "rotate":
                    for (int k = 0; k < fans.Count; k++)
                    {
                        if (fans[k].IsInPolygon(fans[k].points.ToArray(), e.X, e.Y))
                        {
                            selectedFan = fans[k].Number;
                            rotation = true;
                            savedPos = new Vector2(e.X, e.Y);
                        }
                    }
                    break;
                case "remove":
                    for(int k = 0; k < fans.Count; k++)
                    {
                        if (fans[k].IsInPolygon(fans[k].points.ToArray(), e.X, e.Y))
                            fans.Remove(fans[k]);
                    }
                    break;
            }
        }

        public void mouseMove(object sender, MouseEventArgs e)
        {
            switch (action)
            {
                case "createFrontal":
                    if (rotation)
                        rotateFan(new Vector2(e.X, e.Y), fanNumber);
                    break;
                case "createLateral":
                    if (rotation)
                        rotateFan(new Vector2(e.X, e.Y), fanNumber);
                    break;
                case "move":
                    if (motion)
                        fans[selectedFan].Pos = new Vector2(e.X, e.Y);
                    break;
                case "rotate":
                    if (rotation)
                        rotateFan(new Vector2(e.X, e.Y), selectedFan);
                    break;
            }
        }

        public void mouseUp(object sender, MouseEventArgs e)
        {
            switch (action)
            {
                case "createFrontal":
                    rotation = false;
                    fanNumber++;
                    break;
                case "createLateral":
                    rotation = false;
                    fanNumber++;
                    break;
                case "move":
                    if (motion)
                        motion = false;
                    break;
                case "rotate":
                    if (rotation)
                        rotation = false;
                    break;
            }
        }

        public void draw(Graphics g)
        {
            spawner.draw(g);
            objective.draw(g);
            collision(g);
            foreach (Fan f in fans)
                f.draw(g);
        }
    }
}
