namespace UI
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.PardisoCheck = new System.Windows.Forms.CheckBox();
            this.SeidelCheck = new System.Windows.Forms.CheckBox();
            this.JacobyCheck = new System.Windows.Forms.CheckBox();
            this.BiCGStabCheck = new System.Windows.Forms.CheckBox();
            this.GMResCheck = new System.Windows.Forms.CheckBox();
            this.LOScheck = new System.Windows.Forms.CheckBox();
            this.CGMcheck = new System.Windows.Forms.CheckBox();
            this.inputData = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.formatBox = new System.Windows.Forms.ComboBox();
            this.ManualEntry = new System.Windows.Forms.Button();
            this.fileInput = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Start = new System.Windows.Forms.Button();
            this.ChoseOutput = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuOpenOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.label4 = new System.Windows.Forms.Label();
            this.sim = new System.Windows.Forms.RadioButton();
            this.Notsim = new System.Windows.Forms.RadioButton();
            this.groupBox2.SuspendLayout();
            this.inputData.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.textBox14);
            this.groupBox2.Controls.Add(this.textBox13);
            this.groupBox2.Controls.Add(this.textBox12);
            this.groupBox2.Controls.Add(this.textBox11);
            this.groupBox2.Controls.Add(this.textBox10);
            this.groupBox2.Controls.Add(this.textBox9);
            this.groupBox2.Controls.Add(this.textBox8);
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.textBox6);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.textBox4);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.PardisoCheck);
            this.groupBox2.Controls.Add(this.SeidelCheck);
            this.groupBox2.Controls.Add(this.JacobyCheck);
            this.groupBox2.Controls.Add(this.BiCGStabCheck);
            this.groupBox2.Controls.Add(this.GMResCheck);
            this.groupBox2.Controls.Add(this.LOScheck);
            this.groupBox2.Controls.Add(this.CGMcheck);
            this.groupBox2.Font = new System.Drawing.Font("Book Antiqua", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(274, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 239);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Выбор методов решения";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(166, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 17);
            this.label3.TabIndex = 22;
            this.label3.Text = "Итерации";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "eps";
            // 
            // textBox14
            // 
            this.textBox14.Enabled = false;
            this.textBox14.Location = new System.Drawing.Point(169, 179);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(68, 24);
            this.textBox14.TabIndex = 20;
            // 
            // textBox13
            // 
            this.textBox13.Enabled = false;
            this.textBox13.Location = new System.Drawing.Point(169, 152);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(68, 24);
            this.textBox13.TabIndex = 19;
            // 
            // textBox12
            // 
            this.textBox12.Enabled = false;
            this.textBox12.Location = new System.Drawing.Point(169, 125);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(68, 24);
            this.textBox12.TabIndex = 18;
            // 
            // textBox11
            // 
            this.textBox11.Enabled = false;
            this.textBox11.Location = new System.Drawing.Point(169, 97);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(68, 24);
            this.textBox11.TabIndex = 17;
            // 
            // textBox10
            // 
            this.textBox10.Enabled = false;
            this.textBox10.Location = new System.Drawing.Point(169, 70);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(68, 24);
            this.textBox10.TabIndex = 16;
            // 
            // textBox9
            // 
            this.textBox9.Enabled = false;
            this.textBox9.Location = new System.Drawing.Point(169, 43);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(68, 24);
            this.textBox9.TabIndex = 15;
            // 
            // textBox8
            // 
            this.textBox8.Enabled = false;
            this.textBox8.Location = new System.Drawing.Point(169, 206);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(68, 24);
            this.textBox8.TabIndex = 14;
            // 
            // textBox7
            // 
            this.textBox7.Enabled = false;
            this.textBox7.Location = new System.Drawing.Point(95, 179);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(68, 24);
            this.textBox7.TabIndex = 13;
            // 
            // textBox6
            // 
            this.textBox6.Enabled = false;
            this.textBox6.Location = new System.Drawing.Point(95, 152);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(68, 24);
            this.textBox6.TabIndex = 12;
            // 
            // textBox5
            // 
            this.textBox5.Enabled = false;
            this.textBox5.Location = new System.Drawing.Point(95, 125);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(68, 24);
            this.textBox5.TabIndex = 11;
            // 
            // textBox4
            // 
            this.textBox4.Enabled = false;
            this.textBox4.Location = new System.Drawing.Point(95, 97);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(68, 24);
            this.textBox4.TabIndex = 10;
            // 
            // textBox3
            // 
            this.textBox3.Enabled = false;
            this.textBox3.Location = new System.Drawing.Point(95, 70);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(68, 24);
            this.textBox3.TabIndex = 9;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(95, 43);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(68, 24);
            this.textBox2.TabIndex = 8;
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(95, 206);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(68, 24);
            this.textBox1.TabIndex = 7;
            // 
            // PardisoCheck
            // 
            this.PardisoCheck.AutoSize = true;
            this.PardisoCheck.Location = new System.Drawing.Point(6, 182);
            this.PardisoCheck.Name = "PardisoCheck";
            this.PardisoCheck.Size = new System.Drawing.Size(72, 21);
            this.PardisoCheck.TabIndex = 6;
            this.PardisoCheck.Text = "Pardiso";
            this.PardisoCheck.UseVisualStyleBackColor = true;
            // 
            // SeidelCheck
            // 
            this.SeidelCheck.AutoSize = true;
            this.SeidelCheck.Location = new System.Drawing.Point(6, 155);
            this.SeidelCheck.Name = "SeidelCheck";
            this.SeidelCheck.Size = new System.Drawing.Size(62, 21);
            this.SeidelCheck.TabIndex = 5;
            this.SeidelCheck.Text = "Seidel";
            this.SeidelCheck.UseVisualStyleBackColor = true;
            // 
            // JacobyCheck
            // 
            this.JacobyCheck.AutoSize = true;
            this.JacobyCheck.Location = new System.Drawing.Point(6, 128);
            this.JacobyCheck.Name = "JacobyCheck";
            this.JacobyCheck.Size = new System.Drawing.Size(65, 21);
            this.JacobyCheck.TabIndex = 4;
            this.JacobyCheck.Text = "Jacoby";
            this.JacobyCheck.UseVisualStyleBackColor = true;
            // 
            // BiCGStabCheck
            // 
            this.BiCGStabCheck.AutoSize = true;
            this.BiCGStabCheck.Location = new System.Drawing.Point(6, 100);
            this.BiCGStabCheck.Name = "BiCGStabCheck";
            this.BiCGStabCheck.Size = new System.Drawing.Size(83, 21);
            this.BiCGStabCheck.TabIndex = 3;
            this.BiCGStabCheck.Text = "BiCGStab";
            this.BiCGStabCheck.UseVisualStyleBackColor = true;
            // 
            // GMResCheck
            // 
            this.GMResCheck.AutoSize = true;
            this.GMResCheck.Location = new System.Drawing.Point(6, 73);
            this.GMResCheck.Name = "GMResCheck";
            this.GMResCheck.Size = new System.Drawing.Size(70, 21);
            this.GMResCheck.TabIndex = 2;
            this.GMResCheck.Text = "GMRes";
            this.GMResCheck.UseVisualStyleBackColor = true;
            this.GMResCheck.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // LOScheck
            // 
            this.LOScheck.AutoSize = true;
            this.LOScheck.Location = new System.Drawing.Point(6, 46);
            this.LOScheck.Name = "LOScheck";
            this.LOScheck.Size = new System.Drawing.Size(52, 21);
            this.LOScheck.TabIndex = 1;
            this.LOScheck.Text = "LOS";
            this.LOScheck.UseVisualStyleBackColor = true;
            // 
            // CGMcheck
            // 
            this.CGMcheck.AutoSize = true;
            this.CGMcheck.Location = new System.Drawing.Point(6, 209);
            this.CGMcheck.Name = "CGMcheck";
            this.CGMcheck.Size = new System.Drawing.Size(58, 21);
            this.CGMcheck.TabIndex = 0;
            this.CGMcheck.Text = "CGM";
            this.CGMcheck.UseVisualStyleBackColor = true;
            this.CGMcheck.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // inputData
            // 
            this.inputData.Controls.Add(this.Notsim);
            this.inputData.Controls.Add(this.sim);
            this.inputData.Controls.Add(this.label4);
            this.inputData.Controls.Add(this.label1);
            this.inputData.Controls.Add(this.formatBox);
            this.inputData.Controls.Add(this.ManualEntry);
            this.inputData.Controls.Add(this.fileInput);
            this.inputData.Font = new System.Drawing.Font("Book Antiqua", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputData.Location = new System.Drawing.Point(12, 12);
            this.inputData.Name = "inputData";
            this.inputData.Size = new System.Drawing.Size(237, 239);
            this.inputData.TabIndex = 3;
            this.inputData.TabStop = false;
            this.inputData.Text = "Входные данные";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(168, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Выбор формата хранения";
            // 
            // formatBox
            // 
            this.formatBox.FormattingEnabled = true;
            this.formatBox.Location = new System.Drawing.Point(9, 100);
            this.formatBox.Name = "formatBox";
            this.formatBox.Size = new System.Drawing.Size(215, 25);
            this.formatBox.TabIndex = 3;
            // 
            // ManualEntry
            // 
            this.ManualEntry.Enabled = false;
            this.ManualEntry.Location = new System.Drawing.Point(11, 182);
            this.ManualEntry.Name = "ManualEntry";
            this.ManualEntry.Size = new System.Drawing.Size(215, 33);
            this.ManualEntry.TabIndex = 1;
            this.ManualEntry.Text = "Ручной ввод";
            this.ManualEntry.UseVisualStyleBackColor = true;
            // 
            // fileInput
            // 
            this.fileInput.Enabled = false;
            this.fileInput.ForeColor = System.Drawing.Color.Black;
            this.fileInput.Location = new System.Drawing.Point(9, 143);
            this.fileInput.Name = "fileInput";
            this.fileInput.Size = new System.Drawing.Size(215, 33);
            this.fileInput.TabIndex = 0;
            this.fileInput.Text = "Загрузить из файла";
            this.fileInput.UseVisualStyleBackColor = true;
            this.fileInput.Click += new System.EventHandler(this.fileInput_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 310);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(508, 27);
            this.progressBar1.TabIndex = 4;
            // 
            // Start
            // 
            this.Start.Enabled = false;
            this.Start.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Start.Location = new System.Drawing.Point(273, 257);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(246, 47);
            this.Start.TabIndex = 5;
            this.Start.Text = "Найти решение";
            this.Start.UseVisualStyleBackColor = true;
            // 
            // ChoseOutput
            // 
            this.ChoseOutput.Font = new System.Drawing.Font("Book Antiqua", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ChoseOutput.Location = new System.Drawing.Point(12, 257);
            this.ChoseOutput.Name = "ChoseOutput";
            this.ChoseOutput.Size = new System.Drawing.Size(237, 47);
            this.ChoseOutput.TabIndex = 6;
            this.ChoseOutput.Text = "Выбрать файл результата";
            this.ChoseOutput.UseVisualStyleBackColor = true;
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuOpenOutput});
            this.menuStrip2.Location = new System.Drawing.Point(0, 343);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(531, 24);
            this.menuStrip2.TabIndex = 15;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // toolStripMenuOpenOutput
            // 
            this.toolStripMenuOpenOutput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuOpenOutput.Enabled = false;
            this.toolStripMenuOpenOutput.Font = new System.Drawing.Font("Book Antiqua", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuOpenOutput.Name = "toolStripMenuOpenOutput";
            this.toolStripMenuOpenOutput.Size = new System.Drawing.Size(186, 20);
            this.toolStripMenuOpenOutput.Text = "Открыть файл с результатом";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(156, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Симметричная матрица";
            // 
            // sim
            // 
            this.sim.AutoSize = true;
            this.sim.Location = new System.Drawing.Point(9, 46);
            this.sim.Name = "sim";
            this.sim.Size = new System.Drawing.Size(43, 21);
            this.sim.TabIndex = 8;
            this.sim.TabStop = true;
            this.sim.Text = "Да";
            this.sim.UseVisualStyleBackColor = true;
            // 
            // Notsim
            // 
            this.Notsim.AutoSize = true;
            this.Notsim.Location = new System.Drawing.Point(177, 46);
            this.Notsim.Name = "Notsim";
            this.Notsim.Size = new System.Drawing.Size(49, 21);
            this.Notsim.TabIndex = 9;
            this.Notsim.TabStop = true;
            this.Notsim.Text = "Нет";
            this.Notsim.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 367);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.ChoseOutput);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.inputData);
            this.Controls.Add(this.groupBox2);
            this.Name = "MainForm";
            this.Text = "Решение СЛАУ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.inputData.ResumeLayout(false);
            this.inputData.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox simmetry;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button Hand;
        private System.Windows.Forms.Button File_enter;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox GMResCheck;
        private System.Windows.Forms.CheckBox LOScheck;
        private System.Windows.Forms.CheckBox CGMcheck;
        private System.Windows.Forms.CheckBox SeidelCheck;
        private System.Windows.Forms.CheckBox JacobyCheck;
        private System.Windows.Forms.CheckBox BiCGStabCheck;
        private System.Windows.Forms.CheckBox PardisoCheck;
        private System.Windows.Forms.GroupBox inputData;
        private System.Windows.Forms.ComboBox formatBox;
        private System.Windows.Forms.Button ManualEntry;
        private System.Windows.Forms.Button fileInput;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button ChoseOutput;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuOpenOutput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton Notsim;
        private System.Windows.Forms.RadioButton sim;
        private System.Windows.Forms.Label label4;
    }
}

