namespace UI
{
    partial class Factorizer
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.FactorizerName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(12, 41);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(153, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // FactorizerName
            // 
            this.FactorizerName.AutoSize = true;
            this.FactorizerName.Location = new System.Drawing.Point(28, 9);
            this.FactorizerName.Name = "FactorizerName";
            this.FactorizerName.Size = new System.Drawing.Size(137, 13);
            this.FactorizerName.TabIndex = 1;
            this.FactorizerName.Text = "Выбор предобуславителя";
            // 
            // Factorizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(177, 112);
            this.Controls.Add(this.FactorizerName);
            this.Controls.Add(this.comboBox1);
            this.Name = "Factorizer";
            this.Text = "Factorizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label FactorizerName;
    }
}