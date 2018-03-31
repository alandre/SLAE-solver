namespace UI
{
    partial class Krylov
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
            this.iterBox = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.iterBox)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // iterBox
            // 
            this.iterBox.Location = new System.Drawing.Point(92, 29);
            this.iterBox.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.iterBox.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.iterBox.Name = "iterBox";
            this.iterBox.Size = new System.Drawing.Size(50, 20);
            this.iterBox.TabIndex = 31;
            this.iterBox.ThousandsSeparator = true;
            this.iterBox.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(214, 13);
            this.label1.TabIndex = 32;
            this.label1.Text = "Размерность подпространства Крылова";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelToolStripMenuItem,
            this.forwardToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 57);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(234, 24);
            this.menuStrip1.TabIndex = 33;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.cancelToolStripMenuItem.Text = "Отмена";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // forwardToolStripMenuItem1
            // 
            this.forwardToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.forwardToolStripMenuItem1.Name = "forwardToolStripMenuItem1";
            this.forwardToolStripMenuItem1.Size = new System.Drawing.Size(55, 20);
            this.forwardToolStripMenuItem1.Text = "Далее";
            this.forwardToolStripMenuItem1.Click += new System.EventHandler(this.forwardToolStripMenuItem1_Click);
            // 
            // Krylov
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 81);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.iterBox);
            this.MaximumSize = new System.Drawing.Size(250, 120);
            this.MinimumSize = new System.Drawing.Size(250, 120);
            this.Name = "Krylov";
            this.Text = "Krylov";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Krylov_FormClosing);
            this.Load += new System.EventHandler(this.Krylov_Load);
            ((System.ComponentModel.ISupportInitialize)(this.iterBox)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown iterBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem forwardToolStripMenuItem1;
    }
}