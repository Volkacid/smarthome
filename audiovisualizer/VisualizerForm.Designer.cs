namespace AudioMonitor
{
    partial class VisualizerForm
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
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.connectButton = new System.Windows.Forms.Button();
            this.schemeBox = new System.Windows.Forms.ComboBox();
            this.schemeLabel = new System.Windows.Forms.Label();
            this.highGain = new System.Windows.Forms.TextBox();
            this.midGain = new System.Windows.Forms.TextBox();
            this.lowGain = new System.Windows.Forms.TextBox();
            this.gainLabel = new System.Windows.Forms.Label();
            this.smoothBox = new System.Windows.Forms.TextBox();
            this.smoothLabel = new System.Windows.Forms.Label();
            this.highLabel = new System.Windows.Forms.Label();
            this.midLabel = new System.Windows.Forms.Label();
            this.lowLabel = new System.Windows.Forms.Label();
            this.signalPlot = new ScottPlot.FormsPlot();
            this.peakLabel = new System.Windows.Forms.Label();
            this.inputBox = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.manualControlBox = new System.Windows.Forms.CheckBox();
            this.synchronizeCheck = new System.Windows.Forms.CheckBox();
            this.tableBox = new System.Windows.Forms.GroupBox();
            this.bedBox = new System.Windows.Forms.GroupBox();
            this.bedOverflowRadio = new System.Windows.Forms.RadioButton();
            this.bedPulseRadio = new System.Windows.Forms.RadioButton();
            this.bedStaticRadio = new System.Windows.Forms.RadioButton();
            this.BitmapForm = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.bedBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(17, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(797, 516);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.connectButton);
            this.tabPage1.Controls.Add(this.schemeBox);
            this.tabPage1.Controls.Add(this.schemeLabel);
            this.tabPage1.Controls.Add(this.highGain);
            this.tabPage1.Controls.Add(this.midGain);
            this.tabPage1.Controls.Add(this.lowGain);
            this.tabPage1.Controls.Add(this.gainLabel);
            this.tabPage1.Controls.Add(this.smoothBox);
            this.tabPage1.Controls.Add(this.smoothLabel);
            this.tabPage1.Controls.Add(this.highLabel);
            this.tabPage1.Controls.Add(this.midLabel);
            this.tabPage1.Controls.Add(this.lowLabel);
            this.tabPage1.Controls.Add(this.signalPlot);
            this.tabPage1.Controls.Add(this.peakLabel);
            this.tabPage1.Controls.Add(this.inputBox);
            this.tabPage1.Location = new System.Drawing.Point(4, 34);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(789, 478);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Automatic";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // connectButton
            // 
            this.connectButton.Location = new System.Drawing.Point(270, 12);
            this.connectButton.Name = "connectButton";
            this.connectButton.Size = new System.Drawing.Size(112, 34);
            this.connectButton.TabIndex = 25;
            this.connectButton.Text = "Connect";
            this.connectButton.UseVisualStyleBackColor = true;
            this.connectButton.Click += new System.EventHandler(this.connectButton_Click);
            // 
            // schemeBox
            // 
            this.schemeBox.FormattingEnabled = true;
            this.schemeBox.Items.AddRange(new object[] {
            "RGB(оранжевый)",
            "RBG(пурпурный)",
            "GRB(жёлтый)",
            "GBR(фиолетовый)",
            "BRG(голубой)",
            "BGR(синий)"});
            this.schemeBox.Location = new System.Drawing.Point(586, 427);
            this.schemeBox.Name = "schemeBox";
            this.schemeBox.Size = new System.Drawing.Size(182, 33);
            this.schemeBox.TabIndex = 24;
            this.schemeBox.Text = "RGB(оранжевый)";
            this.schemeBox.SelectedIndexChanged += new System.EventHandler(this.schemeBox_SelectedIndexChanged);
            // 
            // schemeLabel
            // 
            this.schemeLabel.AutoSize = true;
            this.schemeLabel.Location = new System.Drawing.Point(456, 430);
            this.schemeLabel.Name = "schemeLabel";
            this.schemeLabel.Size = new System.Drawing.Size(124, 25);
            this.schemeLabel.TabIndex = 23;
            this.schemeLabel.Text = "Color scheme:";
            // 
            // highGain
            // 
            this.highGain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.highGain.Location = new System.Drawing.Point(364, 427);
            this.highGain.Name = "highGain";
            this.highGain.Size = new System.Drawing.Size(31, 31);
            this.highGain.TabIndex = 22;
            this.highGain.Text = "4";
            this.highGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // midGain
            // 
            this.midGain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.midGain.Location = new System.Drawing.Point(327, 427);
            this.midGain.Name = "midGain";
            this.midGain.Size = new System.Drawing.Size(31, 31);
            this.midGain.TabIndex = 21;
            this.midGain.Text = "4";
            this.midGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lowGain
            // 
            this.lowGain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.lowGain.Location = new System.Drawing.Point(291, 427);
            this.lowGain.Name = "lowGain";
            this.lowGain.Size = new System.Drawing.Size(31, 31);
            this.lowGain.TabIndex = 20;
            this.lowGain.Text = "3";
            this.lowGain.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gainLabel
            // 
            this.gainLabel.AutoSize = true;
            this.gainLabel.Location = new System.Drawing.Point(234, 431);
            this.gainLabel.Name = "gainLabel";
            this.gainLabel.Size = new System.Drawing.Size(51, 25);
            this.gainLabel.TabIndex = 19;
            this.gainLabel.Text = "Gain:";
            // 
            // smoothBox
            // 
            this.smoothBox.Location = new System.Drawing.Point(134, 429);
            this.smoothBox.Name = "smoothBox";
            this.smoothBox.Size = new System.Drawing.Size(31, 31);
            this.smoothBox.TabIndex = 18;
            this.smoothBox.Text = "5";
            this.smoothBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // smoothLabel
            // 
            this.smoothLabel.AutoSize = true;
            this.smoothLabel.Location = new System.Drawing.Point(23, 432);
            this.smoothLabel.Name = "smoothLabel";
            this.smoothLabel.Size = new System.Drawing.Size(105, 25);
            this.smoothLabel.TabIndex = 17;
            this.smoothLabel.Text = "Smoothing:";
            // 
            // highLabel
            // 
            this.highLabel.AutoSize = true;
            this.highLabel.Location = new System.Drawing.Point(709, 16);
            this.highLabel.Name = "highLabel";
            this.highLabel.Size = new System.Drawing.Size(50, 25);
            this.highLabel.TabIndex = 16;
            this.highLabel.Text = "High";
            // 
            // midLabel
            // 
            this.midLabel.AutoSize = true;
            this.midLabel.Location = new System.Drawing.Point(644, 16);
            this.midLabel.Name = "midLabel";
            this.midLabel.Size = new System.Drawing.Size(43, 25);
            this.midLabel.TabIndex = 15;
            this.midLabel.Text = "Mid";
            // 
            // lowLabel
            // 
            this.lowLabel.AutoSize = true;
            this.lowLabel.Location = new System.Drawing.Point(586, 16);
            this.lowLabel.Name = "lowLabel";
            this.lowLabel.Size = new System.Drawing.Size(44, 25);
            this.lowLabel.TabIndex = 14;
            this.lowLabel.Text = "Low";
            // 
            // signalPlot
            // 
            this.signalPlot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.signalPlot.Location = new System.Drawing.Point(0, 46);
            this.signalPlot.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.signalPlot.Name = "signalPlot";
            this.signalPlot.Size = new System.Drawing.Size(783, 378);
            this.signalPlot.TabIndex = 13;
            // 
            // peakLabel
            // 
            this.peakLabel.AutoSize = true;
            this.peakLabel.Location = new System.Drawing.Point(387, 16);
            this.peakLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.peakLabel.Name = "peakLabel";
            this.peakLabel.Size = new System.Drawing.Size(138, 25);
            this.peakLabel.TabIndex = 12;
            this.peakLabel.Text = "Peak Frequency:";
            // 
            // inputBox
            // 
            this.inputBox.FormattingEnabled = true;
            this.inputBox.Location = new System.Drawing.Point(23, 13);
            this.inputBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.inputBox.Name = "inputBox";
            this.inputBox.Size = new System.Drawing.Size(235, 33);
            this.inputBox.TabIndex = 11;
            this.inputBox.SelectedIndexChanged += new System.EventHandler(this.inputBox_SelectedIndexChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.manualControlBox);
            this.tabPage2.Controls.Add(this.synchronizeCheck);
            this.tabPage2.Controls.Add(this.tableBox);
            this.tabPage2.Controls.Add(this.bedBox);
            this.tabPage2.Controls.Add(this.BitmapForm);
            this.tabPage2.Controls.Add(this.timeLabel);
            this.tabPage2.Location = new System.Drawing.Point(4, 34);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(789, 478);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Manual";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // manualControlBox
            // 
            this.manualControlBox.AutoSize = true;
            this.manualControlBox.Location = new System.Drawing.Point(164, 434);
            this.manualControlBox.Name = "manualControlBox";
            this.manualControlBox.Size = new System.Drawing.Size(96, 29);
            this.manualControlBox.TabIndex = 11;
            this.manualControlBox.Text = "Manual";
            this.manualControlBox.UseVisualStyleBackColor = true;
            this.manualControlBox.CheckedChanged += new System.EventHandler(this.manualControlBox_CheckedChanged);
            // 
            // synchronizeCheck
            // 
            this.synchronizeCheck.AutoSize = true;
            this.synchronizeCheck.Location = new System.Drawing.Point(304, 433);
            this.synchronizeCheck.Name = "synchronizeCheck";
            this.synchronizeCheck.Size = new System.Drawing.Size(132, 29);
            this.synchronizeCheck.TabIndex = 10;
            this.synchronizeCheck.Text = "Synchronize";
            this.synchronizeCheck.UseVisualStyleBackColor = true;
            this.synchronizeCheck.CheckedChanged += new System.EventHandler(this.synchronizeCheck_CheckedChanged);
            // 
            // tableBox
            // 
            this.tableBox.Location = new System.Drawing.Point(400, 16);
            this.tableBox.Name = "tableBox";
            this.tableBox.Size = new System.Drawing.Size(370, 398);
            this.tableBox.TabIndex = 9;
            this.tableBox.TabStop = false;
            this.tableBox.Text = "Table";
            // 
            // bedBox
            // 
            this.bedBox.Controls.Add(this.bedOverflowRadio);
            this.bedBox.Controls.Add(this.bedPulseRadio);
            this.bedBox.Controls.Add(this.bedStaticRadio);
            this.bedBox.Enabled = false;
            this.bedBox.Location = new System.Drawing.Point(19, 16);
            this.bedBox.Name = "bedBox";
            this.bedBox.Size = new System.Drawing.Size(350, 398);
            this.bedBox.TabIndex = 8;
            this.bedBox.TabStop = false;
            this.bedBox.Text = "Bed";
            // 
            // bedOverflowRadio
            // 
            this.bedOverflowRadio.AutoSize = true;
            this.bedOverflowRadio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bedOverflowRadio.Checked = true;
            this.bedOverflowRadio.Location = new System.Drawing.Point(18, 41);
            this.bedOverflowRadio.Name = "bedOverflowRadio";
            this.bedOverflowRadio.Size = new System.Drawing.Size(109, 29);
            this.bedOverflowRadio.TabIndex = 3;
            this.bedOverflowRadio.TabStop = true;
            this.bedOverflowRadio.Text = "Overflow";
            this.bedOverflowRadio.UseVisualStyleBackColor = true;
            this.bedOverflowRadio.CheckedChanged += new System.EventHandler(this.bedOverflowRadio_CheckedChanged);
            // 
            // bedPulseRadio
            // 
            this.bedPulseRadio.AutoSize = true;
            this.bedPulseRadio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bedPulseRadio.Location = new System.Drawing.Point(18, 76);
            this.bedPulseRadio.Name = "bedPulseRadio";
            this.bedPulseRadio.Size = new System.Drawing.Size(78, 29);
            this.bedPulseRadio.TabIndex = 4;
            this.bedPulseRadio.Text = "Pulse";
            this.bedPulseRadio.UseVisualStyleBackColor = true;
            // 
            // bedStaticRadio
            // 
            this.bedStaticRadio.AutoSize = true;
            this.bedStaticRadio.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.bedStaticRadio.Location = new System.Drawing.Point(18, 111);
            this.bedStaticRadio.Name = "bedStaticRadio";
            this.bedStaticRadio.Size = new System.Drawing.Size(79, 29);
            this.bedStaticRadio.TabIndex = 5;
            this.bedStaticRadio.Text = "Static";
            this.bedStaticRadio.UseVisualStyleBackColor = true;
            this.bedStaticRadio.CheckedChanged += new System.EventHandler(this.bedStaticRadio_CheckedChanged);
            // 
            // BitmapForm
            // 
            this.BitmapForm.Location = new System.Drawing.Point(615, 429);
            this.BitmapForm.Name = "BitmapForm";
            this.BitmapForm.Size = new System.Drawing.Size(155, 34);
            this.BitmapForm.TabIndex = 7;
            this.BitmapForm.Text = "Screen capture";
            this.BitmapForm.UseVisualStyleBackColor = true;
            this.BitmapForm.Click += new System.EventHandler(this.BitmapForm_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(19, 429);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(59, 25);
            this.timeLabel.TabIndex = 6;
            this.timeLabel.Text = "label1";
            // 
            // VisualizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(829, 548);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "VisualizerForm";
            this.Text = "Visualizer";
            this.Load += new System.EventHandler(this.VisualizerForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.bedBox.ResumeLayout(false);
            this.bedBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Label highLabel;
        private Label midLabel;
        private Label lowLabel;
        private ScottPlot.FormsPlot signalPlot;
        private Label peakLabel;
        private ComboBox inputBox;
        private TabPage tabPage2;
        private TextBox smoothBox;
        private Label smoothLabel;
        private TextBox highGain;
        private TextBox midGain;
        private TextBox lowGain;
        private Label gainLabel;
        private RadioButton bedStaticRadio;
        private RadioButton bedPulseRadio;
        private RadioButton bedOverflowRadio;
        private ComboBox schemeBox;
        private Label schemeLabel;
        private Button connectButton;
        private Label timeLabel;
        private Button BitmapForm;
        private GroupBox bedBox;
        private GroupBox tableBox;
        private CheckBox synchronizeCheck;
        private ColorDialog colorDialog1;
        private CheckBox manualControlBox;
    }
}