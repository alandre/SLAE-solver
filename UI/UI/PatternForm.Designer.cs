namespace UI
{
    partial class PatternForm
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
            this.A = new System.Windows.Forms.DataGridView();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.backwardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forwardToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.infoTextBox = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.A)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.A.Location = new System.Drawing.Point(12, 67);
            this.A.Name = "A";
            this.A.ReadOnly = true;
            this.A.RowHeadersVisible = false;
            this.A.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.A.Size = new System.Drawing.Size(71, 45);
            this.A.TabIndex = 13;
            this.A.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.A_CellClick);
            this.A.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.A_RowsAdded);
            this.A.SelectionChanged += new System.EventHandler(this.A_SelectionChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backwardToolStripMenuItem,
            this.forwardToolStripMenuItem1});
            this.menuStrip1.Location = new System.Drawing.Point(0, 127);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(282, 24);
            this.menuStrip1.TabIndex = 15;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // backwardToolStripMenuItem
            // 
            this.backwardToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.backwardToolStripMenuItem.Name = "backwardToolStripMenuItem";
            this.backwardToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.backwardToolStripMenuItem.Text = "Назад";
            this.backwardToolStripMenuItem.Click += new System.EventHandler(this.backwardToolStripMenuItem_Click);
            // 
            // forwardToolStripMenuItem1
            // 
            this.forwardToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.forwardToolStripMenuItem1.Name = "forwardToolStripMenuItem1";
            this.forwardToolStripMenuItem1.Size = new System.Drawing.Size(55, 20);
            this.forwardToolStripMenuItem1.Text = "Далее";
            this.forwardToolStripMenuItem1.Click += new System.EventHandler(this.forwardToolStripMenuItem1_Click);
            // 
            // infoTextBox
            // 
            this.infoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.infoTextBox.Location = new System.Drawing.Point(14, 12);
            this.infoTextBox.Name = "infoTextBox";
            this.infoTextBox.ReadOnly = true;
            this.infoTextBox.Size = new System.Drawing.Size(256, 44);
            this.infoTextBox.TabIndex = 16;
            this.infoTextBox.Text = "Портрет матрицы был определен автоматически. Для добавления в него нулевых элемен" +
    "тов, щелкните по ним мышью.";
            // 
            // PatternForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 151);
            this.Controls.Add(this.infoTextBox);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.A);
            this.Name = "PatternForm";
            this.Text = "Портрет матрицы";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PatternForm_FormClosing);
            this.Load += new System.EventHandler(this.PatternForm_Load);
            this.Shown += new System.EventHandler(this.PatternForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.A)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView A;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem backwardToolStripMenuItem;
        private System.Windows.Forms.RichTextBox infoTextBox;
        private System.Windows.Forms.ToolStripMenuItem forwardToolStripMenuItem1;
    }
}