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

namespace SimuladorVento
{
    public partial class Simulador : Form
    {
        Arena arena;

        public Simulador()
        {
            InitializeComponent();
            arena = Arena.GetInstancia(panelArea.Size);
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
            statusLabelInfo.Text = arena.x;
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
            buttonCreateFrontal.BackgroundImage = Properties.Resources.buttonCreateFrontalInverted;
            arena.Action = "createFrontal";
        }

        private void buttonCreateLateral_Click(object sender, EventArgs e)
        {
            resetButtons();
            buttonCreateLateral.BackgroundImage = Properties.Resources.buttonCreateLateralInverted;
            arena.Action = "createLateral";
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            resetButtons();
            buttonMove.BackgroundImage = Properties.Resources.buttonMoveInverted;
            arena.Action = "move";
        }

        private void buttonRotate_Click(object sender, EventArgs e)
        {
            resetButtons();
            buttonRotate.BackgroundImage = Properties.Resources.buttonRotateInverted;
            arena.Action = "rotate";
        }

        private void buttonForce_Click(object sender, EventArgs e)
        {
            resetButtons();
            buttonForce.BackgroundImage = Properties.Resources.buttonForceInverted;
            arena.Action = "force";
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            resetButtons();
            buttonRemove.BackgroundImage = Properties.Resources.buttonRemoveInverted;
            arena.Action = "remove";
        }

        private void resetButtons()
        {
            buttonCreateFrontal.BackgroundImage = Properties.Resources.buttonCreateFrontal;
            buttonCreateLateral.BackgroundImage = Properties.Resources.buttonCreateLateral;
            buttonForce.BackgroundImage = Properties.Resources.buttonForce;
            buttonMove.BackgroundImage = Properties.Resources.buttonMove;
            buttonRemove.BackgroundImage = Properties.Resources.buttonRemove;
            buttonRotate.BackgroundImage = Properties.Resources.buttonRotate;
        }
    }
}
