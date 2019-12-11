using System;
using System.Collections.Generic;
using System.Drawing;
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

        private static Arena instancia = null;
        public List<Fan> fans;

        public Arena(Size d)
        {
            area = d;
            spawner = new Spawner(area.Height);
            objective = new Objective(area.Width, area.Height);
            fans = new List<Fan>();
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

        public void rotateFan(Vector2 newPosition, int fanNumber)
        {
            Vector2 d = newPosition - savedPos;
            float x, y;
            float angle = (float)Math.Atan2(d.X, d.Y);

            x = (float)Math.Cos(angle);
            y = (float)Math.Sin(angle);

            for (int k = 0; k < fans.Count; k++)
            {
                if (fanNumber == fans[k].Number)
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
            }
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
