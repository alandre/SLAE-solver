namespace UI
{
    partial class ResultsForm
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.method = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.res_res = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.itercount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.method,
            this.res_res,
            this.itercount,
            this.time});
            this.dataGridView1.Location = new System.Drawing.Point(499, 37);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(411, 310);
            this.dataGridView1.TabIndex = 0;
            // 
            // method
            // 
            this.method.HeaderText = "Имя метода";
            this.method.Name = "method";
            // 
            // res_res
            // 
            this.res_res.HeaderText = "Конечная невязка";
            this.res_res.Name = "res_res";
            // 
            // itercount
            // 
            this.itercount.HeaderText = "Количество итераций";
            this.itercount.Name = "itercount";
            // 
            // time
            // 
            this.time.HeaderText = "Время";
            this.time.Name = "time";
            // 
            // ResultsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 487);
            this.Controls.Add(this.dataGridView1);
            this.Name = "ResultsForm";
            this.Text = "Результаты";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn method;
        private System.Windows.Forms.DataGridViewTextBoxColumn res_res;
        private System.Windows.Forms.DataGridViewTextBoxColumn itercount;
        private System.Windows.Forms.DataGridViewTextBoxColumn time;
    }
}