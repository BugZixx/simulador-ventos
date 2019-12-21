using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Numerics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace SimuladorVento
{
    public partial class Simulador : Form
    {
        Arena arena;

        public Simulador()
        {
            InitializeComponent();
            arena = Arena.GetInstancia(panelArea.Size);
            txt.Name = "Path para ficheiro de obstáculos";
        }

        private void panelArea_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            drawArea(g);
        }

        private void drawArea(Graphics g)
        {
            g.Clear(panelArea.BackColor);
            arena.draw(g);
        }

        private void redrawArea()
        {
            BufferedGraphicsContext currentContext;
            BufferedGraphics myBuffer;
            currentContext = BufferedGraphicsManager.Current;
            myBuffer = currentContext.Allocate(this.panelArea.CreateGraphics(),
                this.panelArea.DisplayRectangle);
            Graphics g = myBuffer.Graphics;

            drawArea(g);

            myBuffer.Render();
            myBuffer.Dispose();
        }

        private void panelArea_SizeChanged(object sender, EventArgs e)
        {
            arena.Area = panelArea.Size;
            panelArea.Invalidate();
        }

        private void timerAnimation_Tick(object sender, EventArgs e)
        {
            arena.move();
            redrawArea();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panelArea_MouseDown(object sender, MouseEventArgs e)
        {
            arena.mouseClick(sender, e);
            // statusLabelInfo.Text = arena.x;
        }

        private void panelArea_MouseMove(object sender, MouseEventArgs e)
        {
            arena.mouseMove(sender, e);
        }

        private void panelArea_MouseUp(object sender, MouseEventArgs e)
        {
            arena.mouseUp(sender, e);
        }

        private void buttonCreateFrontal_Click(object sender, EventArgs e)
        {
            resetButtons();
            buttonCreateFrontal.BackgroundImage = Properties.Resources.buttonCreateFrontalInverted; // muda o botao para ativo
            arena.Action = "createFrontal";
        }

        private void buttonCreateLateral_Click(object sender, EventArgs e)
        {
            resetButtons();
            buttonCreateLateral.BackgroundImage = Properties.Resources.buttonCreateLateralInverted; // muda o botao para ativo
            arena.Action = "createLateral";
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            resetButtons();
            buttonMove.BackgroundImage = Properties.Resources.buttonMoveInverted; // muda o botao para ativo
            arena.Action = "move";
        }

        private void buttonRotate_Click(object sender, EventArgs e)
        {
            resetButtons();
            buttonRotate.BackgroundImage = Properties.Resources.buttonRotateInverted; // muda o botao para ativo
            arena.Action = "rotate";
        }

        private void buttonForce_Click(object sender, EventArgs e)
        {
            resetButtons();
            buttonForce.BackgroundImage = Properties.Resources.buttonForceInverted; // muda o botao para ativo
            forceBar.Visible = true;
            arena.BarValue = forceBar.Value;
            arena.Action = "force";
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            resetButtons();
            buttonRemove.BackgroundImage = Properties.Resources.buttonRemoveInverted; // muda o botao para ativo
            arena.Action = "remove";
        }

        private void resetButtons() // serve para deixar todos os butoes no seu estado normal
        {
            buttonCreateFrontal.BackgroundImage = Properties.Resources.buttonCreateFrontal;
            buttonCreateLateral.BackgroundImage = Properties.Resources.buttonCreateLateral;
            buttonForce.BackgroundImage = Properties.Resources.buttonForce;
            buttonMove.BackgroundImage = Properties.Resources.buttonMove;
            buttonRemove.BackgroundImage = Properties.Resources.buttonRemove;
            buttonRotate.BackgroundImage = Properties.Resources.buttonRotate;
            forceBar.Visible = false;
        }

        private void forceBar_ValueChanged(object sender, EventArgs e)
        {
            arena.BarValue = forceBar.Value; // ajusta a força que deve dar a uma ventoinha conforme a trackbar
        }

        private void Simulador_Resize(object sender, EventArgs e)
        {
            arena.objective.Pos = new Vector2(arena.Area.Width - arena.objective.PosWidth, arena.Area.Height - 10); // certifica-se que o objetivo final fica junto ao lado direito da arena, depois do resize
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            // quando o utilizador clica no enter, ou se usa o path introduzido por ele, ou usa o path predefinido
            // usa-se o metodo ReadAllLines para converter as linhas do ficheiro para um array de strings
            if(e.KeyCode == Keys.Enter)
            {
                arena.txtText = txt.Text;
                txt.Visible = false;
                text1.Visible = false;
                if(txt.Text.Length > 0)
                {
                    arena.obsRect = System.IO.File.ReadAllLines(arena.txtText);
                    arena.createObstacles();
                }
                else
                {
                    String path = Directory.GetParent(Directory.GetParent(Directory.GetCurrentDirectory()).ToString()).ToString();
                    path = path + "\\obsRect.txt";
                    arena.obsRect = System.IO.File.ReadAllLines(path);
                    arena.createObstacles();
                }
            }
        }
    }
}
