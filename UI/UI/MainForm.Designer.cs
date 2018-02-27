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
            this.label2 = new System.Windows.Forms.Label();
            this.epsBox = new System.Windows.Forms.TextBox();
            this.inputData = new System.Windows.Forms.GroupBox();
            this.Notsim = new System.Windows.Forms.RadioButton();
            this.sim = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.formatBox = new System.Windows.Forms.ComboBox();
            this.ManualEntry = new System.Windows.Forms.Button();
            this.fileInput = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Start = new System.Windows.Forms.Button();
            this.ChoseOutput = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuOpenOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label5 = new System.Windows.Forms.Label();
            this.timeBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.inputData.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.timeBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.listBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.epsBox);
            this.groupBox2.Font = new System.Drawing.Font("Book Antiqua", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(274, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 239);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Выбор методов решения";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(199, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(13, 17);
            this.label2.TabIndex = 21;
            this.label2.Text = "ε";
            // 
            // epsBox
            // 
            this.epsBox.Enabled = false;
            this.epsBox.Location = new System.Drawing.Point(172, 63);
            this.epsBox.Name = "epsBox";
            this.epsBox.Size = new System.Drawing.Size(68, 24);
            this.epsBox.TabIndex = 8;
            this.epsBox.Text = "1e-10";
            this.epsBox.TextChanged += new System.EventHandler(this.epsBox_TextChanged);
            this.epsBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.epsBox_KeyPress);
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
            // Notsim
            // 
            this.Notsim.AutoSize = true;
            this.Notsim.Location = new System.Drawing.Point(177, 46);
            this.Notsim.Name = "Notsim";
            this.Notsim.Size = new System.Drawing.Size(49, 21);
            this.Notsim.TabIndex = 9;
            this.Notsim.Text = "Нет";
            this.Notsim.UseVisualStyleBackColor = true;
            this.Notsim.Click += new System.EventHandler(this.Notsim_Click);
            // 
            // sim
            // 
            this.sim.AutoSize = true;
            this.sim.Checked = true;
            this.sim.Location = new System.Drawing.Point(9, 46);
            this.sim.Name = "sim";
            this.sim.Size = new System.Drawing.Size(43, 21);
            this.sim.TabIndex = 8;
            this.sim.TabStop = true;
            this.sim.Text = "Да";
            this.sim.UseVisualStyleBackColor = true;
            this.sim.Click += new System.EventHandler(this.sim_Click);
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
            this.formatBox.Items.AddRange(new object[] {
            "Плотный ",
            "Строчный разреженный без выделенной диагонали",
            "Строчно-столбцовый",
            "Координатный"});
            this.formatBox.Location = new System.Drawing.Point(9, 100);
            this.formatBox.Name = "formatBox";
            this.formatBox.Size = new System.Drawing.Size(215, 25);
            this.formatBox.TabIndex = 3;
            this.formatBox.Text = "Координатный";
            this.formatBox.SelectedIndexChanged += new System.EventHandler(this.formatBox_SelectedIndexChanged);
            // 
            // ManualEntry
            // 
            this.ManualEntry.Location = new System.Drawing.Point(11, 182);
            this.ManualEntry.Name = "ManualEntry";
            this.ManualEntry.Size = new System.Drawing.Size(215, 33);
            this.ManualEntry.TabIndex = 1;
            this.ManualEntry.Text = "Ручной ввод";
            this.ManualEntry.UseVisualStyleBackColor = true;
            this.ManualEntry.Click += new System.EventHandler(this.ManualEntry_Click);
            // 
            // fileInput
            // 
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
            this.ChoseOutput.Click += new System.EventHandler(this.ChoseOutput_Click);
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
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 17;
            this.listBox1.Items.AddRange(new object[] {
            "LOS",
            "GMRes",
            "BiCGStab",
            "Jacoby",
            "Seidel",
            "Pardiso",
            "CGM"});
            this.listBox1.Location = new System.Drawing.Point(18, 44);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(129, 140);
            this.listBox1.TabIndex = 23;
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(182, 106);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 17);
            this.label5.TabIndex = 24;
            this.label5.Text = "Время";
            // 
            // timeBox
            // 
            this.timeBox.Enabled = false;
            this.timeBox.Location = new System.Drawing.Point(172, 145);
            this.timeBox.Name = "timeBox";
            this.timeBox.Size = new System.Drawing.Size(68, 24);
            this.timeBox.TabIndex = 25;
            this.timeBox.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(166, 125);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "(в секундах)";
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
        private System.Windows.Forms.GroupBox inputData;
        private System.Windows.Forms.ComboBox formatBox;
        private System.Windows.Forms.Button ManualEntry;
        private System.Windows.Forms.Button fileInput;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox epsBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button ChoseOutput;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuOpenOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton Notsim;
        private System.Windows.Forms.RadioButton sim;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox timeBox;
        private System.Windows.Forms.Label label5;
    }
}

