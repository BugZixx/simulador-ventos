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
        private int l;
        private Size area;
        private Spawner spawner;
        public Objective objective;
        private Vector2 savedPos;
        private string action;
        private int fanNumber = 0;
        private bool rotation = false;
        private bool motion = false;
        private bool changeForce = false;
        private int selectedFan;
        private SolidBrush pincel;
        public String x = "";
        private Region objectiveR;
        private List<Region> r;
        private GraphicsPath grapPth, objPath;
        private float barValue;

        private static Arena instancia = null;
        public List<Fan> fans;

        public Arena(Size d)
        {
            l = 6;
            area = d;
            spawner = new Spawner(area.Height);
            objective = new Objective(area.Width, area.Height);
            fans = new List<Fan>();
            pincel = new SolidBrush(Color.White);
            r = new List<Region>();
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
        public float BarValue
        {
            get { return barValue; }
            set { barValue = value; }
        }

        public string Action
        {
            get { return action; }
            set { action = value; }
        }

        public void move()
        {
            spawner.SpawnBullet();
            spawner.moveBullets();
            collisionFan();
            collisionObjective();
        }

        public void collisionFan()
        {
            foreach (Fan f in fans)
            {
                f.wBR = f.WindBox.Rect;
                Vector2 tl = new Vector2(f.wBR.Left, f.wBR.Top);
                Vector2 bl = new Vector2(f.wBR.Left, f.wBR.Bottom);
                Vector2 tr = new Vector2(f.wBR.Right, f.wBR.Top);
                Vector2 br = new Vector2(f.wBR.Right, f.wBR.Bottom);

                f.wBR = new Rectangle(f.wBR.X + (int)f.Pos.X + 5, f.wBR.Y + (int)f.Pos.Y, f.wBR.Width, f.wBR.Height);
                tl += new Vector2((int)f.Pos.X + 5, (int)f.Pos.Y);
                bl += new Vector2((int)f.Pos.X + 5, (int)f.Pos.Y);
                tr += new Vector2((int)f.Pos.X + 5, (int)f.Pos.Y);
                br += new Vector2((int)f.Pos.X + 5, (int)f.Pos.Y);

                Point[] p = { ToPoint(bl), ToPoint(tl), ToPoint(tr), ToPoint(br) };
                grapPth = new GraphicsPath();
                grapPth.AddLines(p);
                r[f.Number] = new Region(grapPth);
                grapPth.Transform(rotateAroundPoint(f.Rotation, new Point((int)f.Pos.X - 5, (int)f.Pos.Y)));
                r[f.Number].Transform(rotateAroundPoint(f.Rotation, new Point((int)f.Pos.X - 5, (int)f.Pos.Y)));
                rotationAngle(f.Number, new Vector2((int)grapPth.PathPoints[1].X, (int)grapPth.PathPoints[1].Y), new Vector2((int)grapPth.PathPoints[2].X, (int)grapPth.PathPoints[2].Y));

                foreach (Bullet b in spawner.Bullets)
                {
                    b.bR = b.Rect;
                    b.bR = new Rectangle(b.bR.X + (int)b.Pos.X, b.bR.Y + (int)b.Pos.Y, b.bR.Width, b.bR.Height);
                    if (r[f.Number].IsVisible(b.bR))
                    {
                        b.Rem = true;
                        f.Rem = true;
                        b.Acel += f.Angle * f.Force * 0.005f;
                        b.remAcel = b.Acel;
                    }
                    if (!r[f.Number].IsVisible(b.bR) && b.Rem == true && f.Rem == true)
                    {
                        b.Acel -= b.remAcel;
                        b.Rem = false;
                        f.Rem = false;
                    }
                }
            }
        }

        public void collisionObjective()
        {
            objPath = new GraphicsPath();
            objPath.AddLines(objective.Points);
            objectiveR = new Region(objPath);
            objectiveR.Transform(scaleMatrix(2, 2));
            objectiveR.Translate(objective.Pos.X, objective.Pos.Y);
            foreach(Bullet b in spawner.Bullets)
            {
                b.bR = b.Rect;
                b.bR = new Rectangle(b.bR.X + (int)b.Pos.X, b.bR.Y + (int)b.Pos.Y, b.bR.Width, b.bR.Height);

                if (objectiveR.IsVisible(b.bR))
                    b.GoalAchieved = true;
                else
                    b.GoalAchieved = false;

            }
            if (spawner.Bullets.All(Bullet => Bullet.GoalAchieved.Equals(false)) && l == 16)
            {
                objective.GoalAchieved = false;
                l = 0;
            }
            else if (spawner.Bullets.All(Bullet => Bullet.GoalAchieved.Equals(false)) && l < 16)
            {
                l++;
            }
            else
            {
                objective.GoalAchieved = true;
                l = 0;
            }
        }

        private Matrix rotateAroundPoint(float angle, Point center)
        {
            Matrix result = new Matrix();
            result.RotateAt(angle, center);
            return result;
        }

        private Matrix scaleMatrix(float sX, float sY)
        {
            Matrix m = new Matrix();
            m.Scale(sX, sY);
            return m;
        }

        public void rotationAngle(int fN, Vector2 alvo, Vector2 pos)
        {
            Vector2 d = alvo - pos;
            this.x = alvo.ToString() + pos.ToString();
            float x, y;
            float angle = (float)Math.Atan2(d.X, d.Y);

            x = (float)Math.Cos(angle);
            y = (float)Math.Sin(angle);

            for (int k = 0; k < fans.Count; k++)
            {
                if (fN == fans[k].Number)
                {
                    fans[k].Angle = new Vector2(y, x);
                }
            }
        }

        public void addFrontalFan(Vector2 position, int fanNumber)
        {
            savedPos = position;
            FrontalFan newFan = new FrontalFan(position, new Vector2(0, 0), fanNumber);
            r.Add(new Region());
            fans.Add(newFan);
        }

        public void addLateralFan(Vector2 position, int fanNumber)
        {
            savedPos = position;
            LateralFan newFan = new LateralFan(position, new Vector2(0, 0), fanNumber);
            r.Add(new Region());
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
                    fans[k].Angle = new Vector2(x, y);

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
                    for (int k = 0; k < fans.Count; k++)
                    {
                        if (fans[k].IsInPolygon(fans[k].points.ToArray(), e.X, e.Y))
                            fans.Remove(fans[k]);
                    }
                    break;
                case "force":
                    for (int k = 0; k < fans.Count; k++)
                    {
                        if (fans[k].IsInPolygon(fans[k].points.ToArray(), e.X, e.Y))
                            selectedFan = k;
                        changeForce = true;
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
                case "force":
                    if (changeForce)
                        fans[selectedFan].force = barValue * -0.03f;
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

        public Point ToPoint(Vector2 v2)
        {
            return (new Point((int)v2.X, (int)v2.Y));
        }

        public void draw(Graphics g)
        {
            spawner.draw(g);
            objective.draw(g);
            foreach (Fan f in fans)
                f.draw(g);
        }
    }
}
