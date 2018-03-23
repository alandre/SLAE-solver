namespace UI
{
    partial class ConstructorForm
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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.sizePanel = new System.Windows.Forms.Panel();
            this.symCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.A = new System.Windows.Forms.DataGridView();
            this.F = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.x0 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolTip3 = new System.Windows.Forms.ToolTip(this.components);
            this.CleanF_Btn = new System.Windows.Forms.Button();
            this.CleanMatrix_Btn = new System.Windows.Forms.Button();
            this.CleanX0_Btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.sizePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.A)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.F)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.x0)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(87, 10);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(39, 20);
            this.numericUpDown1.TabIndex = 1;
            this.numericUpDown1.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Размерность:";
            // 
            // sizePanel
            // 
            this.sizePanel.Controls.Add(this.symCheckBox);
            this.sizePanel.Controls.Add(this.label1);
            this.sizePanel.Controls.Add(this.numericUpDown1);
            this.sizePanel.Location = new System.Drawing.Point(3, 3);
            this.sizePanel.Name = "sizePanel";
            this.sizePanel.Size = new System.Drawing.Size(183, 65);
            this.sizePanel.TabIndex = 2;
            // 
            // symCheckBox
            // 
            this.symCheckBox.AutoSize = true;
            this.symCheckBox.Location = new System.Drawing.Point(12, 40);
            this.symCheckBox.Name = "symCheckBox";
            this.symCheckBox.Size = new System.Drawing.Size(147, 17);
            this.symCheckBox.TabIndex = 15;
            this.symCheckBox.Text = "Симметричная матрица";
            this.symCheckBox.UseVisualStyleBackColor = true;
            this.symCheckBox.CheckedChanged += new System.EventHandler(this.symCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(116, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "• x = ";
            // 
            // A
            // 
            this.A.AllowUserToAddRows = false;
            this.A.AllowUserToDeleteRows = false;
            this.A.AllowUserToResizeColumns = false;
            this.A.AllowUserToResizeRows = false;
            this.A.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.A.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.A.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.A.ColumnHeadersVisible = false;
            this.A.Location = new System.Drawing.Point(15, 83);
            this.A.Name = "A";
            this.A.RowHeadersVisible = false;
            this.A.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.A.Size = new System.Drawing.Size(71, 45);
            this.A.TabIndex = 0;
            this.A.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.CellBeginEdit);
            this.A.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.CellEndEdit);
            this.A.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.A_CellEnter);
            this.A.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.CellValidating);
            // 
            // F
            // 
            this.F.AllowUserToAddRows = false;
            this.F.AllowUserToDeleteRows = false;
            this.F.AllowUserToResizeColumns = false;
            this.F.AllowUserToResizeRows = false;
            this.F.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.F.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.F.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.F.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.F.ColumnHeadersVisible = false;
            this.F.Location = new System.Drawing.Point(154, 83);
            this.F.Name = "F";
            this.F.RowHeadersVisible = false;
            this.F.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.F.Size = new System.Drawing.Size(36, 45);
            this.F.TabIndex = 13;
            this.F.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.CellBeginEdit);
            this.F.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.CellEndEdit);
            this.F.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.CellValidating);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cancelToolStripMenuItem,
            this.forwardToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 212);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(227, 24);
            this.menuStrip1.TabIndex = 14;
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
            // x0
            // 
            this.x0.AllowUserToAddRows = false;
            this.x0.AllowUserToDeleteRows = false;
            this.x0.AllowUserToResizeColumns = false;
            this.x0.AllowUserToResizeRows = false;
            this.x0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.x0.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.x0.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.x0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.x0.ColumnHeadersVisible = false;
            this.x0.Location = new System.Drawing.Point(15, 163);
            this.x0.Name = "x0";
            this.x0.RowHeadersVisible = false;
            this.x0.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.x0.Size = new System.Drawing.Size(71, 23);
            this.x0.TabIndex = 15;
            this.x0.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.CellBeginEdit);
            this.x0.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.CellEndEdit);
            this.x0.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.CellValidating);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Начальное приближение:";
            // 
            // CleanF_Btn
            // 
            this.CleanF_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CleanF_Btn.BackgroundImage = global::UI.Properties.Resources.CleanIcon;
            this.CleanF_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CleanF_Btn.Location = new System.Drawing.Point(195, 83);
            this.CleanF_Btn.Name = "CleanF_Btn";
            this.CleanF_Btn.Size = new System.Drawing.Size(20, 20);
            this.CleanF_Btn.TabIndex = 18;
            this.CleanF_Btn.UseVisualStyleBackColor = true;
            this.CleanF_Btn.Click += new System.EventHandler(this.CleanF_Btn_Click);
            // 
            // CleanMatrix_Btn
            // 
            this.CleanMatrix_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CleanMatrix_Btn.BackgroundImage = global::UI.Properties.Resources.CleanIcon;
            this.CleanMatrix_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CleanMatrix_Btn.Location = new System.Drawing.Point(90, 83);
            this.CleanMatrix_Btn.Name = "CleanMatrix_Btn";
            this.CleanMatrix_Btn.Size = new System.Drawing.Size(20, 20);
            this.CleanMatrix_Btn.TabIndex = 17;
            this.CleanMatrix_Btn.UseVisualStyleBackColor = true;
            this.CleanMatrix_Btn.Click += new System.EventHandler(this.CleanMatrix_Btn_Click);
            // 
            // CleanX0_Btn
            // 
            this.CleanX0_Btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CleanX0_Btn.BackgroundImage = global::UI.Properties.Resources.CleanIcon;
            this.CleanX0_Btn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CleanX0_Btn.Location = new System.Drawing.Point(90, 163);
            this.CleanX0_Btn.Name = "CleanX0_Btn";
            this.CleanX0_Btn.Size = new System.Drawing.Size(20, 20);
            this.CleanX0_Btn.TabIndex = 16;
            this.CleanX0_Btn.UseVisualStyleBackColor = true;
            this.CleanX0_Btn.Click += new System.EventHandler(this.CleanX0_Btn_Click);
            // 
            // ConstructorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 236);
            this.Controls.Add(this.CleanF_Btn);
            this.Controls.Add(this.CleanMatrix_Btn);
            this.Controls.Add(this.CleanX0_Btn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.x0);
            this.Controls.Add(this.F);
            this.Controls.Add(this.A);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sizePanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(243, 275);
            this.MinimumSize = new System.Drawing.Size(243, 275);
            this.Name = "ConstructorForm";
            this.Text = "Конструктор СЛАУ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConstructorForm_FormClosing);
            this.Load += new System.EventHandler(this.ConstructorForm_Load);
            this.Shown += new System.EventHandler(this.ConstructorForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.sizePanel.ResumeLayout(false);
            this.sizePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.A)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.F)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.x0)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel sizePanel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView A;
        private System.Windows.Forms.DataGridView F;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        private System.Windows.Forms.CheckBox symCheckBox;
        private System.Windows.Forms.DataGridView x0;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button CleanX0_Btn;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button CleanMatrix_Btn;
        private System.Windows.Forms.Button CleanF_Btn;
        private System.Windows.Forms.ToolTip toolTip2;
        private System.Windows.Forms.ToolTip toolTip3;
        private System.Windows.Forms.ToolStripMenuItem forwardToolStripMenuItem1;
    }
}