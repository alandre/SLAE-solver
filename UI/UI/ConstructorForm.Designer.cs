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
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.sizePanel = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.A = new System.Windows.Forms.DataGridView();
            this.F = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.сохранитьКакToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вПлотномФорматеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вСтрочномФорматеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вСтрочностолбцовомФорматеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.вКоординатномФорматеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.далееToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x0 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
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
            this.numericUpDown1.Location = new System.Drawing.Point(93, 10);
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
            this.numericUpDown1.TabIndex = 0;
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
            this.sizePanel.Controls.Add(this.checkBox1);
            this.sizePanel.Controls.Add(this.label1);
            this.sizePanel.Controls.Add(this.numericUpDown1);
            this.sizePanel.Location = new System.Drawing.Point(3, 3);
            this.sizePanel.Name = "sizePanel";
            this.sizePanel.Size = new System.Drawing.Size(171, 65);
            this.sizePanel.TabIndex = 2;
            this.sizePanel.Paint += new System.Windows.Forms.PaintEventHandler(this.sizePanel_Paint);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 40);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(147, 17);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Симметричная матрица";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(96, 89);
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
            this.A.Location = new System.Drawing.Point(15, 74);
            this.A.Name = "A";
            this.A.RowHeadersVisible = false;
            this.A.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.A.Size = new System.Drawing.Size(71, 45);
            this.A.TabIndex = 12;
            this.A.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.A_CellEndEdit);
            this.A.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.A_CellEnter);
            this.A.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.A_CellValueChanged);
            // 
            // F
            // 
            this.F.AllowUserToAddRows = false;
            this.F.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.F.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.F.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.F.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.F.ColumnHeadersVisible = false;
            this.F.Location = new System.Drawing.Point(138, 74);
            this.F.Name = "F";
            this.F.RowHeadersVisible = false;
            this.F.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.F.Size = new System.Drawing.Size(36, 45);
            this.F.TabIndex = 13;
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сохранитьКакToolStripMenuItem,
            this.далееToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 186);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(186, 24);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // сохранитьКакToolStripMenuItem
            // 
            this.сохранитьКакToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.сохранитьКакToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вПлотномФорматеToolStripMenuItem,
            this.вСтрочномФорматеToolStripMenuItem,
            this.вСтрочностолбцовомФорматеToolStripMenuItem,
            this.вКоординатномФорматеToolStripMenuItem});
            this.сохранитьКакToolStripMenuItem.Name = "сохранитьКакToolStripMenuItem";
            this.сохранитьКакToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.сохранитьКакToolStripMenuItem.Text = "Сохранить в файл";
            this.сохранитьКакToolStripMenuItem.Click += new System.EventHandler(this.сохранитьКакToolStripMenuItem_Click);
            // 
            // вПлотномФорматеToolStripMenuItem
            // 
            this.вПлотномФорматеToolStripMenuItem.Name = "вПлотномФорматеToolStripMenuItem";
            this.вПлотномФорматеToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.вПлотномФорматеToolStripMenuItem.Text = "В плотном формате";
            // 
            // вСтрочномФорматеToolStripMenuItem
            // 
            this.вСтрочномФорматеToolStripMenuItem.Name = "вСтрочномФорматеToolStripMenuItem";
            this.вСтрочномФорматеToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.вСтрочномФорматеToolStripMenuItem.Text = "В строчном формате";
            // 
            // вСтрочностолбцовомФорматеToolStripMenuItem
            // 
            this.вСтрочностолбцовомФорматеToolStripMenuItem.Name = "вСтрочностолбцовомФорматеToolStripMenuItem";
            this.вСтрочностолбцовомФорматеToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.вСтрочностолбцовомФорматеToolStripMenuItem.Text = "В строчно-столбцовом формате";
            // 
            // вКоординатномФорматеToolStripMenuItem
            // 
            this.вКоординатномФорматеToolStripMenuItem.Name = "вКоординатномФорматеToolStripMenuItem";
            this.вКоординатномФорматеToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.вКоординатномФорматеToolStripMenuItem.Text = "В координатном формате";
            // 
            // далееToolStripMenuItem
            // 
            this.далееToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.далееToolStripMenuItem.Name = "далееToolStripMenuItem";
            this.далееToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.далееToolStripMenuItem.Text = "Далее";
            // 
            // x0
            // 
            this.x0.AllowUserToAddRows = false;
            this.x0.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.x0.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.x0.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.x0.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.x0.ColumnHeadersVisible = false;
            this.x0.Location = new System.Drawing.Point(15, 148);
            this.x0.Name = "x0";
            this.x0.RowHeadersVisible = false;
            this.x0.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.x0.Size = new System.Drawing.Size(71, 23);
            this.x0.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(12, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Начальное приближение:";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // ConstructorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(186, 210);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.x0);
            this.Controls.Add(this.F);
            this.Controls.Add(this.A);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sizePanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "ConstructorForm";
            this.Text = "Ввод матрицы";
            this.Load += new System.EventHandler(this.ConstructorForm_Load);
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
        private System.Windows.Forms.ToolStripMenuItem сохранитьКакToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вПлотномФорматеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вСтрочномФорматеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вСтрочностолбцовомФорматеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem вКоординатномФорматеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem далееToolStripMenuItem;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.DataGridView x0;
        private System.Windows.Forms.Label label3;
    }
}