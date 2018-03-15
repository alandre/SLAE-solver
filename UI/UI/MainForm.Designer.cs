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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.iterBox = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.methodCheckedImg = new System.Windows.Forms.PictureBox();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.epsBox = new System.Windows.Forms.TextBox();
            this.inputData = new System.Windows.Forms.GroupBox();
            this.inputCheckedImg = new System.Windows.Forms.PictureBox();
            this.manualInputBtn = new System.Windows.Forms.LinkLabel();
            this.manualInpitRadioBtn = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.fileInputRadioBtn = new System.Windows.Forms.RadioButton();
            this.fileInputPanel = new System.Windows.Forms.Panel();
            this.fileInputBtn = new System.Windows.Forms.LinkLabel();
            this.sim = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.formatBox = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Start = new System.Windows.Forms.Button();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuOpenOutput = new System.Windows.Forms.ToolStripMenuItem();
            this.resultsFormToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.outputCheckedImg = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.timerHightlight = new System.Windows.Forms.Timer(this.components);
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iterBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.methodCheckedImg)).BeginInit();
            this.inputData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputCheckedImg)).BeginInit();
            this.fileInputPanel.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputCheckedImg)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.iterBox);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.methodCheckedImg);
            this.groupBox2.Controls.Add(this.checkedListBox1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.epsBox);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(260, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 128);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Выбор методов решения";
            // 
            // iterBox
            // 
            this.iterBox.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.iterBox.Location = new System.Drawing.Point(49, 22);
            this.iterBox.Maximum = new decimal(new int[] {
            1000000000,
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
            this.iterBox.TabIndex = 30;
            this.iterBox.ThousandsSeparator = true;
            this.iterBox.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 29;
            this.label7.Text = "maxiter:";
            // 
            // methodCheckedImg
            // 
            this.methodCheckedImg.Enabled = false;
            this.methodCheckedImg.Image = ((System.Drawing.Image)(resources.GetObject("methodCheckedImg.Image")));
            this.methodCheckedImg.Location = new System.Drawing.Point(220, 11);
            this.methodCheckedImg.Name = "methodCheckedImg";
            this.methodCheckedImg.Size = new System.Drawing.Size(20, 20);
            this.methodCheckedImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.methodCheckedImg.TabIndex = 21;
            this.methodCheckedImg.TabStop = false;
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.ColumnWidth = 115;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Items.AddRange(new object[] {
            "BiCGStab",
            "CGM",
            "GMRes",
            "Jacoby",
            "LOS",
            "Pardiso",
            "Seidel"});
            this.checkedListBox1.Location = new System.Drawing.Point(8, 53);
            this.checkedListBox1.MultiColumn = true;
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(230, 60);
            this.checkedListBox1.Sorted = true;
            this.checkedListBox1.TabIndex = 27;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(106, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "ε:";
            // 
            // epsBox
            // 
            this.epsBox.Location = new System.Drawing.Point(124, 22);
            this.epsBox.Name = "epsBox";
            this.epsBox.Size = new System.Drawing.Size(55, 20);
            this.epsBox.TabIndex = 8;
            this.epsBox.Text = "1e-10";
            this.epsBox.Validating += new System.ComponentModel.CancelEventHandler(this.epsBox_Validating);
            // 
            // inputData
            // 
            this.inputData.Controls.Add(this.inputCheckedImg);
            this.inputData.Controls.Add(this.manualInputBtn);
            this.inputData.Controls.Add(this.manualInpitRadioBtn);
            this.inputData.Controls.Add(this.label3);
            this.inputData.Controls.Add(this.fileInputRadioBtn);
            this.inputData.Controls.Add(this.fileInputPanel);
            this.inputData.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.inputData.Location = new System.Drawing.Point(12, 12);
            this.inputData.Name = "inputData";
            this.inputData.Size = new System.Drawing.Size(237, 188);
            this.inputData.TabIndex = 3;
            this.inputData.TabStop = false;
            this.inputData.Text = "Входные данные";
            // 
            // inputCheckedImg
            // 
            this.inputCheckedImg.Enabled = false;
            this.inputCheckedImg.Image = global::UI.Properties.Resources.UnabledCheckMark;
            this.inputCheckedImg.Location = new System.Drawing.Point(211, 11);
            this.inputCheckedImg.Name = "inputCheckedImg";
            this.inputCheckedImg.Size = new System.Drawing.Size(20, 20);
            this.inputCheckedImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.inputCheckedImg.TabIndex = 20;
            this.inputCheckedImg.TabStop = false;
            // 
            // manualInputBtn
            // 
            this.manualInputBtn.ActiveLinkColor = System.Drawing.Color.Maroon;
            this.manualInputBtn.AutoSize = true;
            this.manualInputBtn.Enabled = false;
            this.manualInputBtn.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.manualInputBtn.Location = new System.Drawing.Point(23, 160);
            this.manualInputBtn.Name = "manualInputBtn";
            this.manualInputBtn.Size = new System.Drawing.Size(126, 13);
            this.manualInputBtn.TabIndex = 19;
            this.manualInputBtn.TabStop = true;
            this.manualInputBtn.Text = "Открыть конструктор...";
            this.manualInputBtn.VisitedLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.manualInputBtn.Click += new System.EventHandler(this.ManualEntry_Click);
            // 
            // manualInpitRadioBtn
            // 
            this.manualInpitRadioBtn.AutoSize = true;
            this.manualInpitRadioBtn.Location = new System.Drawing.Point(7, 139);
            this.manualInpitRadioBtn.Name = "manualInpitRadioBtn";
            this.manualInpitRadioBtn.Size = new System.Drawing.Size(87, 17);
            this.manualInpitRadioBtn.TabIndex = 18;
            this.manualInpitRadioBtn.Text = "Ручной ввод";
            this.manualInpitRadioBtn.UseVisualStyleBackColor = true;
            this.manualInpitRadioBtn.CheckedChanged += new System.EventHandler(this.manualInpitRadioBtn_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(80, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Способ ввода:";
            // 
            // fileInputRadioBtn
            // 
            this.fileInputRadioBtn.AutoSize = true;
            this.fileInputRadioBtn.Checked = true;
            this.fileInputRadioBtn.Location = new System.Drawing.Point(7, 47);
            this.fileInputRadioBtn.Name = "fileInputRadioBtn";
            this.fileInputRadioBtn.Size = new System.Drawing.Size(74, 17);
            this.fileInputRadioBtn.TabIndex = 10;
            this.fileInputRadioBtn.TabStop = true;
            this.fileInputRadioBtn.Text = "Из файла";
            this.fileInputRadioBtn.UseVisualStyleBackColor = true;
            this.fileInputRadioBtn.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // fileInputPanel
            // 
            this.fileInputPanel.Controls.Add(this.fileInputBtn);
            this.fileInputPanel.Controls.Add(this.sim);
            this.fileInputPanel.Controls.Add(this.label1);
            this.fileInputPanel.Controls.Add(this.formatBox);
            this.fileInputPanel.Location = new System.Drawing.Point(12, 46);
            this.fileInputPanel.Name = "fileInputPanel";
            this.fileInputPanel.Size = new System.Drawing.Size(224, 93);
            this.fileInputPanel.TabIndex = 19;
            // 
            // fileInputBtn
            // 
            this.fileInputBtn.ActiveLinkColor = System.Drawing.Color.Maroon;
            this.fileInputBtn.AutoSize = true;
            this.fileInputBtn.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.fileInputBtn.Location = new System.Drawing.Point(75, 3);
            this.fileInputBtn.Name = "fileInputBtn";
            this.fileInputBtn.Size = new System.Drawing.Size(48, 13);
            this.fileInputBtn.TabIndex = 18;
            this.fileInputBtn.TabStop = true;
            this.fileInputBtn.Text = "Обзор...";
            this.fileInputBtn.VisitedLinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.fileInputBtn.Click += new System.EventHandler(this.fileInput_Click);
            // 
            // sim
            // 
            this.sim.AutoSize = true;
            this.sim.Location = new System.Drawing.Point(14, 67);
            this.sim.Name = "sim";
            this.sim.Size = new System.Drawing.Size(147, 17);
            this.sim.TabIndex = 16;
            this.sim.Text = "Симметричная матрица";
            this.sim.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Формат хранения:";
            // 
            // formatBox
            // 
            this.formatBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.formatBox.FormattingEnabled = true;
            this.formatBox.Location = new System.Drawing.Point(14, 41);
            this.formatBox.Name = "formatBox";
            this.formatBox.Size = new System.Drawing.Size(197, 21);
            this.formatBox.TabIndex = 3;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 287);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(494, 19);
            this.progressBar1.TabIndex = 4;
            // 
            // Start
            // 
            this.Start.Enabled = false;
            this.Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Start.Location = new System.Drawing.Point(12, 206);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(494, 26);
            this.Start.TabIndex = 5;
            this.Start.Text = "Найти решение";
            this.Start.UseVisualStyleBackColor = true;
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuOpenOutput,
            this.resultsFormToolStripMenuItem});
            this.menuStrip2.Location = new System.Drawing.Point(0, 324);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(519, 24);
            this.menuStrip2.TabIndex = 15;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // toolStripMenuOpenOutput
            // 
            this.toolStripMenuOpenOutput.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuOpenOutput.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toolStripMenuOpenOutput.Name = "toolStripMenuOpenOutput";
            this.toolStripMenuOpenOutput.Size = new System.Drawing.Size(188, 20);
            this.toolStripMenuOpenOutput.Text = "Открыть папку с результатами";
            this.toolStripMenuOpenOutput.Click += new System.EventHandler(this.toolStripMenuOpenOutput_Click);
            // 
            // resultsFormToolStripMenuItem
            // 
            this.resultsFormToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.resultsFormToolStripMenuItem.Name = "resultsFormToolStripMenuItem";
            this.resultsFormToolStripMenuItem.Size = new System.Drawing.Size(112, 20);
            this.resultsFormToolStripMenuItem.Text = "Сводные данные";
            this.resultsFormToolStripMenuItem.Click += new System.EventHandler(this.resultsFormToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.outputCheckedImg);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Controls.Add(this.linkLabel1);
            this.groupBox3.Location = new System.Drawing.Point(260, 147);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(246, 53);
            this.groupBox3.TabIndex = 20;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Выходные данные";
            // 
            // outputCheckedImg
            // 
            this.outputCheckedImg.Enabled = false;
            this.outputCheckedImg.Image = global::UI.Properties.Resources.UnabledCheckMark;
            this.outputCheckedImg.Location = new System.Drawing.Point(220, 12);
            this.outputCheckedImg.Name = "outputCheckedImg";
            this.outputCheckedImg.Size = new System.Drawing.Size(20, 20);
            this.outputCheckedImg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.outputCheckedImg.TabIndex = 28;
            this.outputCheckedImg.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(150, 20);
            this.textBox1.TabIndex = 20;
            // 
            // linkLabel1
            // 
            this.linkLabel1.ActiveLinkColor = System.Drawing.Color.Maroon;
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkColor = System.Drawing.SystemColors.MenuHighlight;
            this.linkLabel1.Location = new System.Drawing.Point(164, 25);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(48, 13);
            this.linkLabel1.TabIndex = 19;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Обзор...";
            this.linkLabel1.VisitedLinkColor = System.Drawing.SystemColors.MenuHighlight;
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(12, 262);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(494, 19);
            this.progressBar2.TabIndex = 21;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 244);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Текущая невязка:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(472, 244);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "1 из 4";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // timerHightlight
            // 
            this.timerHightlight.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 348);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.menuStrip2);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.inputData);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.MaximumSize = new System.Drawing.Size(535, 387);
            this.MinimumSize = new System.Drawing.Size(535, 387);
            this.Name = "MainForm";
            this.Text = "Решение СЛАУ";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iterBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.methodCheckedImg)).EndInit();
            this.inputData.ResumeLayout(false);
            this.inputData.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.inputCheckedImg)).EndInit();
            this.fileInputPanel.ResumeLayout(false);
            this.fileInputPanel.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.outputCheckedImg)).EndInit();
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
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox epsBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuOpenOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton fileInputRadioBtn;
        private System.Windows.Forms.RadioButton manualInpitRadioBtn;
        private System.Windows.Forms.Panel fileInputPanel;
        private System.Windows.Forms.CheckBox sim;
        private System.Windows.Forms.LinkLabel fileInputBtn;
        private System.Windows.Forms.LinkLabel manualInputBtn;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.ToolStripMenuItem resultsFormToolStripMenuItem;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox inputCheckedImg;
        private System.Windows.Forms.PictureBox methodCheckedImg;
        private System.Windows.Forms.PictureBox outputCheckedImg;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown iterBox;
        private System.Windows.Forms.Timer timerHightlight;
    }
}

