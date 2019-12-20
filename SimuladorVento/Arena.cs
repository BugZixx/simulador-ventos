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

        public static Arena GetInstancia(Size d) // Singleton
        {
            if (instancia == null) instancia = new Arena(d);
            return instancia;
        }

        public static Arena Instancia // Singleton
        {
            get
            {
                if (instancia == null)
                    throw new InvalidOperationException("Objeto não criado. Criar com GetInstancia(.)");
                return instancia;
            }
        }

        // getters e setters

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
            spawner.SpawnBullet(); // cria novas bullets
            spawner.moveBullets(); // faz com que as bullets se movam
            collisionFan();        // ve as colisoes com ventoinhas
            collisionObjective();  // ve as colisoes com o objetivo
        }

        public void collisionFan()
        {
            foreach (Fan f in fans)
            {
                // cria vetores conforme o retangulo de windbox (vento)
                f.wBR = f.WindBox.Rect;
                Vector2 tl = new Vector2(f.wBR.Left, f.wBR.Top);
                Vector2 bl = new Vector2(f.wBR.Left, f.wBR.Bottom);
                Vector2 tr = new Vector2(f.wBR.Right, f.wBR.Top);
                Vector2 br = new Vector2(f.wBR.Right, f.wBR.Bottom);

                // ajusta para a posiçao correta (para igualar ao que é desenhado)
                f.wBR = new Rectangle(f.wBR.X + (int)f.Pos.X + 5, f.wBR.Y + (int)f.Pos.Y, f.wBR.Width, f.wBR.Height);
                tl += new Vector2((int)f.Pos.X + 5, (int)f.Pos.Y);
                bl += new Vector2((int)f.Pos.X + 5, (int)f.Pos.Y);
                tr += new Vector2((int)f.Pos.X + 5, (int)f.Pos.Y);
                br += new Vector2((int)f.Pos.X + 5, (int)f.Pos.Y);

                // cria uma regiao e adiciona a uma lista
                // a regiao contem pontos iguais ao do windbox da ventoinha f
                // cria rotaçao igual à da ventoinha, e usa o metodo rotateAroundPoint para rodar em torno da ventoinha
                Point[] p = { ToPoint(bl), ToPoint(tl), ToPoint(tr), ToPoint(br) };
                grapPth = new GraphicsPath();
                grapPth.AddLines(p);
                r[f.Number] = new Region(grapPth);
                grapPth.Transform(rotateAroundPoint(f.Rotation, new Point((int)f.Pos.X - 5, (int)f.Pos.Y)));
                r[f.Number].Transform(rotateAroundPoint(f.Rotation, new Point((int)f.Pos.X - 5, (int)f.Pos.Y)));
                rotationAngle(f.Number, new Vector2((int)grapPth.PathPoints[1].X, (int)grapPth.PathPoints[1].Y), new Vector2((int)grapPth.PathPoints[2].X, (int)grapPth.PathPoints[2].Y));

                foreach (Bullet b in spawner.Bullets)
                {
                    // tal como acima, cria um retangulo que corresponde à hitbox das bullets
                    // usa o metodo IsVisible para ver se cada bullet está dentro da region
                    // ambos os booleans b.Rem e f.Rem, e o Vector2 b.remAcel servem para, quando a bullet deixa de estar em contacto com a ventoinha, perde a aceleração causada pela mesma
                    // ficando assim apenas com a velocity na direção correspondida
                    // sem estes dois booleans e o Vector2, após sair da ventoinha, as bullets continuavam a acelerar
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
            // usa um algoritmo igual ao metodo collisionFans, apenas é retirado o sistema de rotação, porque o objetivo não roda
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

            // os if's abaixo servem para, se o alvo estiver em contacto com bullets, e deixar de estar, apenas 16 ticks depois volta a ficar vermelho
            // desta forma evitamos que o alvo fique apenas verde por 1 tick, caso apenas uma bullet tenha acertado, e tenha saido logo do alcance do alvo
            // serve apenas para efeitos esteticos
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
            // devolve uma matriz que roda algo à volta do ponto 'center' com o angulo 'angle'
            Matrix result = new Matrix();
            result.RotateAt(angle, center);
            return result;
        }

        private Matrix scaleMatrix(float sX, float sY)
        {
            // devolve uma matriz que dá scale com sX, sY
            Matrix m = new Matrix();
            m.Scale(sX, sY);
            return m;
        }

        public void rotationAngle(int fN, Vector2 alvo, Vector2 pos)
        {
            // este metodo é usado para definir o angulo que as bullets devem seguir depois de entrarem em contacto com a ventoinha
            Vector2 d = alvo - pos;
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
            // adiciona uma ventoinha frontal
            // adiciona tambem uma nova region, para ser comparada às bullets mais tarde
            savedPos = position;
            FrontalFan newFan = new FrontalFan(position, new Vector2(0, 0), fanNumber);
            r.Add(new Region());
            fans.Add(newFan);
        }

        public void addLateralFan(Vector2 position, int fanNumber)
        {
            // adiciona uma ventoinha lateral
            // adiciona tambem uma nova region, para ser comparada às bullets mais tarde
            savedPos = position;
            LateralFan newFan = new LateralFan(position, new Vector2(0, 0), fanNumber);
            r.Add(new Region());
            fans.Add(newFan);
        }

        public void rotateFan(Vector2 newPosition, int fN)
        {
            // serve para rodar uma ventoinha
            // pode ser uma ventoinha acabada de criar, ou alguma ja existente
            // compara a posição da ventoinha (savedPos) e a posição atual do rato (newPosition) e calcula o angulo entre elas
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
            // metodo invocado quando há um click do rato
            switch (action)
            {
                case "createFrontal": // serve para criar uma ventoinha frontal
                    rotation = true;
                    addFrontalFan(new Vector2(e.X, e.Y), fanNumber);
                    break;
                case "createLateral": // serve para criar uma ventoinha lateral
                    rotation = true;
                    addLateralFan(new Vector2(e.X, e.Y), fanNumber);
                    break;
                case "move": // serve para mover uma ventoinha existente
                    for (int k = 0; k < fans.Count; k++)
                    {
                        if (fans[k].IsInPolygon(fans[k].points.ToArray(), e.X, e.Y))
                        {
                            selectedFan = k;
                            motion = true;
                        }
                    }
                    break;
                case "rotate": // serve para rodar uma ventoinha existente
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
                case "remove": // serve para remover uma ventoinha
                    for (int k = 0; k < fans.Count; k++)
                    {
                        if (fans[k].IsInPolygon(fans[k].points.ToArray(), e.X, e.Y))
                            fans.Remove(fans[k]);
                    }
                    break;
                case "force": // serve para ajustar a força de uma ventoinha, funciona através de uma trackbar
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
            // este metodo é invocado quando o rato se mexe
            // as explicações são iguais ao metodo mouseClick
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
            // este metodo é invocado depois de clicarem no botao do rato, e param de clicar
            // as explicações são iguais ao metodo mouseClick
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
            // metodo que serve apenas para converter um Vector2 num Point
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
