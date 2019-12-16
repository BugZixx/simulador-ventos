﻿namespace SimuladorVento
{
    partial class Simulador
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelArea = new System.Windows.Forms.Panel();
            this.timerAnimation = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonCreateFrontal = new System.Windows.Forms.Button();
            this.buttonCreateLateral = new System.Windows.Forms.Button();
            this.buttonMove = new System.Windows.Forms.Button();
            this.buttonRotate = new System.Windows.Forms.Button();
            this.buttonForce = new System.Windows.Forms.Button();
            this.buttonRemove = new System.Windows.Forms.Button();
            this.statusLabelInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelArea
            // 
            this.panelArea.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelArea.BackColor = System.Drawing.SystemColors.MenuText;
            this.panelArea.Location = new System.Drawing.Point(12, 12);
            this.panelArea.Name = "panelArea";
            this.panelArea.Size = new System.Drawing.Size(862, 458);
            this.panelArea.TabIndex = 0;
            this.panelArea.SizeChanged += new System.EventHandler(this.panelArea_SizeChanged);
            this.panelArea.Paint += new System.Windows.Forms.PaintEventHandler(this.panelArea_Paint);
            this.panelArea.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panelArea_MouseDown);
            this.panelArea.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panelArea_MouseMove);
            this.panelArea.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panelArea_MouseUp);
            // 
            // timerAnimation
            // 
            this.timerAnimation.Enabled = true;
            this.timerAnimation.Interval = 30;
            this.timerAnimation.Tick += new System.EventHandler(this.timerAnimation_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabelInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 517);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(886, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(886, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sairToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.sairToolStripMenuItem_Click);
            // 
            // buttonCreateFrontal
            // 
            this.buttonCreateFrontal.BackColor = System.Drawing.Color.DarkRed;
            this.buttonCreateFrontal.BackgroundImage = global::SimuladorVento.Properties.Resources.buttonCreateFrontal;
            this.buttonCreateFrontal.FlatAppearance.BorderSize = 0;
            this.buttonCreateFrontal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateFrontal.Location = new System.Drawing.Point(13, 476);
            this.buttonCreateFrontal.Name = "buttonCreateFrontal";
            this.buttonCreateFrontal.Size = new System.Drawing.Size(156, 38);
            this.buttonCreateFrontal.TabIndex = 3;
            this.buttonCreateFrontal.Text = " \r\n";
            this.buttonCreateFrontal.UseVisualStyleBackColor = false;
            this.buttonCreateFrontal.Click += new System.EventHandler(this.buttonCreateFrontal_Click);
            // 
            // buttonCreateLateral
            // 
            this.buttonCreateLateral.BackColor = System.Drawing.Color.DarkRed;
            this.buttonCreateLateral.BackgroundImage = global::SimuladorVento.Properties.Resources.buttonCreateLateral;
            this.buttonCreateLateral.FlatAppearance.BorderSize = 0;
            this.buttonCreateLateral.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonCreateLateral.Location = new System.Drawing.Point(175, 476);
            this.buttonCreateLateral.Name = "buttonCreateLateral";
            this.buttonCreateLateral.Size = new System.Drawing.Size(156, 38);
            this.buttonCreateLateral.TabIndex = 4;
            this.buttonCreateLateral.Text = " \r\n";
            this.buttonCreateLateral.UseVisualStyleBackColor = false;
            this.buttonCreateLateral.Click += new System.EventHandler(this.buttonCreateLateral_Click);
            // 
            // buttonMove
            // 
            this.buttonMove.BackColor = System.Drawing.Color.DarkRed;
            this.buttonMove.BackgroundImage = global::SimuladorVento.Properties.Resources.buttonMove;
            this.buttonMove.FlatAppearance.BorderSize = 0;
            this.buttonMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonMove.Location = new System.Drawing.Point(337, 476);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(103, 38);
            this.buttonMove.TabIndex = 5;
            this.buttonMove.Text = " \r\n";
            this.buttonMove.UseVisualStyleBackColor = false;
            this.buttonMove.Click += new System.EventHandler(this.buttonMove_Click);
            // 
            // buttonRotate
            // 
            this.buttonRotate.BackColor = System.Drawing.Color.DarkRed;
            this.buttonRotate.BackgroundImage = global::SimuladorVento.Properties.Resources.buttonRotate;
            this.buttonRotate.FlatAppearance.BorderSize = 0;
            this.buttonRotate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRotate.Location = new System.Drawing.Point(446, 476);
            this.buttonRotate.Name = "buttonRotate";
            this.buttonRotate.Size = new System.Drawing.Size(103, 38);
            this.buttonRotate.TabIndex = 6;
            this.buttonRotate.Text = " \r\n";
            this.buttonRotate.UseVisualStyleBackColor = false;
            this.buttonRotate.Click += new System.EventHandler(this.buttonRotate_Click);
            // 
            // buttonForce
            // 
            this.buttonForce.BackColor = System.Drawing.Color.DarkRed;
            this.buttonForce.BackgroundImage = global::SimuladorVento.Properties.Resources.buttonForce;
            this.buttonForce.FlatAppearance.BorderSize = 0;
            this.buttonForce.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonForce.Location = new System.Drawing.Point(555, 476);
            this.buttonForce.Name = "buttonForce";
            this.buttonForce.Size = new System.Drawing.Size(94, 38);
            this.buttonForce.TabIndex = 7;
            this.buttonForce.Text = " \r\n";
            this.buttonForce.UseVisualStyleBackColor = false;
            this.buttonForce.Click += new System.EventHandler(this.buttonForce_Click);
            // 
            // buttonRemove
            // 
            this.buttonRemove.BackColor = System.Drawing.Color.DarkRed;
            this.buttonRemove.BackgroundImage = global::SimuladorVento.Properties.Resources.buttonRemove;
            this.buttonRemove.FlatAppearance.BorderSize = 0;
            this.buttonRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRemove.Location = new System.Drawing.Point(655, 476);
            this.buttonRemove.Name = "buttonRemove";
            this.buttonRemove.Size = new System.Drawing.Size(117, 38);
            this.buttonRemove.TabIndex = 8;
            this.buttonRemove.Text = " \r\n";
            this.buttonRemove.UseVisualStyleBackColor = false;
            this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
            // 
            // statusLabelInfo
            // 
            this.statusLabelInfo.Name = "statusLabelInfo";
            this.statusLabelInfo.Size = new System.Drawing.Size(57, 17);
            this.statusLabelInfo.Text = "Info = 0,0";
            // 
            // Simulador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 539);
            this.Controls.Add(this.buttonRemove);
            this.Controls.Add(this.buttonForce);
            this.Controls.Add(this.buttonRotate);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.buttonCreateLateral);
            this.Controls.Add(this.buttonCreateFrontal);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panelArea);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Simulador";
            this.Text = "Form1";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelArea;
        private System.Windows.Forms.Timer timerAnimation;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.Button buttonCreateFrontal;
        private System.Windows.Forms.Button buttonCreateLateral;
        private System.Windows.Forms.Button buttonMove;
        private System.Windows.Forms.Button buttonRotate;
        private System.Windows.Forms.Button buttonForce;
        private System.Windows.Forms.Button buttonRemove;
        private System.Windows.Forms.ToolStripStatusLabel statusLabelInfo;
    }
}
