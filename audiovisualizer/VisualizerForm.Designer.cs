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
            components = new System.ComponentModel.Container();
            timer1 = new System.Windows.Forms.Timer(components);
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            connectButton = new Button();
            schemeBox = new ComboBox();
            schemeLabel = new Label();
            highGain = new TextBox();
            midGain = new TextBox();
            lowGain = new TextBox();
            gainLabel = new Label();
            smoothBox = new TextBox();
            smoothLabel = new Label();
            highLabel = new Label();
            midLabel = new Label();
            lowLabel = new Label();
            signalPlot = new ScottPlot.FormsPlot();
            peakLabel = new Label();
            inputBox = new ComboBox();
            tabPage2 = new TabPage();
            useThumbnailCheckBox = new CheckBox();
            label1 = new Label();
            manualControlBox = new CheckBox();
            synchronizeCheck = new CheckBox();
            tableBox = new GroupBox();
            tableOverflowRadio = new RadioButton();
            tablePulseRadio = new RadioButton();
            tableStaticRadio = new RadioButton();
            bedBox = new GroupBox();
            bedOverflowRadio = new RadioButton();
            bedPulseRadio = new RadioButton();
            bedStaticRadio = new RadioButton();
            BitmapForm = new Button();
            timeLabel = new Label();
            colorDialog1 = new ColorDialog();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tableBox.SuspendLayout();
            bedBox.SuspendLayout();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 20;
            timer1.Tick += timer1_Tick;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(20, 20);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(797, 516);
            tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(connectButton);
            tabPage1.Controls.Add(schemeBox);
            tabPage1.Controls.Add(schemeLabel);
            tabPage1.Controls.Add(highGain);
            tabPage1.Controls.Add(midGain);
            tabPage1.Controls.Add(lowGain);
            tabPage1.Controls.Add(gainLabel);
            tabPage1.Controls.Add(smoothBox);
            tabPage1.Controls.Add(smoothLabel);
            tabPage1.Controls.Add(highLabel);
            tabPage1.Controls.Add(midLabel);
            tabPage1.Controls.Add(lowLabel);
            tabPage1.Controls.Add(signalPlot);
            tabPage1.Controls.Add(peakLabel);
            tabPage1.Controls.Add(inputBox);
            tabPage1.Location = new Point(4, 34);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(789, 478);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Automatic";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // connectButton
            // 
            connectButton.Location = new Point(270, 12);
            connectButton.Name = "connectButton";
            connectButton.Size = new Size(112, 34);
            connectButton.TabIndex = 25;
            connectButton.Text = "Connect";
            connectButton.UseVisualStyleBackColor = true;
            connectButton.Click += connectButton_Click;
            // 
            // schemeBox
            // 
            schemeBox.FormattingEnabled = true;
            schemeBox.Items.AddRange(new object[] { "RGB(оранжевый)", "RBG(пурпурный)", "GRB(жёлтый)", "GBR(фиолетовый)", "BRG(голубой)", "BGR(синий)" });
            schemeBox.Location = new Point(586, 427);
            schemeBox.Name = "schemeBox";
            schemeBox.Size = new Size(182, 33);
            schemeBox.TabIndex = 24;
            schemeBox.Text = "RGB(оранжевый)";
            schemeBox.SelectedIndexChanged += schemeBox_SelectedIndexChanged;
            // 
            // schemeLabel
            // 
            schemeLabel.AutoSize = true;
            schemeLabel.Location = new Point(456, 430);
            schemeLabel.Name = "schemeLabel";
            schemeLabel.Size = new Size(124, 25);
            schemeLabel.TabIndex = 23;
            schemeLabel.Text = "Color scheme:";
            // 
            // highGain
            // 
            highGain.BackColor = Color.FromArgb(192, 255, 255);
            highGain.Location = new Point(376, 426);
            highGain.Name = "highGain";
            highGain.Size = new Size(31, 31);
            highGain.TabIndex = 22;
            highGain.Text = "4";
            highGain.TextAlign = HorizontalAlignment.Center;
            // 
            // midGain
            // 
            midGain.BackColor = Color.FromArgb(192, 255, 192);
            midGain.Location = new Point(339, 426);
            midGain.Name = "midGain";
            midGain.Size = new Size(31, 31);
            midGain.TabIndex = 21;
            midGain.Text = "4";
            midGain.TextAlign = HorizontalAlignment.Center;
            // 
            // lowGain
            // 
            lowGain.BackColor = Color.FromArgb(255, 192, 192);
            lowGain.Location = new Point(303, 426);
            lowGain.Name = "lowGain";
            lowGain.Size = new Size(31, 31);
            lowGain.TabIndex = 20;
            lowGain.Text = "3";
            lowGain.TextAlign = HorizontalAlignment.Center;
            // 
            // gainLabel
            // 
            gainLabel.AutoSize = true;
            gainLabel.Location = new Point(246, 430);
            gainLabel.Name = "gainLabel";
            gainLabel.Size = new Size(51, 25);
            gainLabel.TabIndex = 19;
            gainLabel.Text = "Gain:";
            // 
            // smoothBox
            // 
            smoothBox.Location = new Point(162, 426);
            smoothBox.Name = "smoothBox";
            smoothBox.Size = new Size(31, 31);
            smoothBox.TabIndex = 18;
            smoothBox.Text = "5";
            smoothBox.TextAlign = HorizontalAlignment.Center;
            // 
            // smoothLabel
            // 
            smoothLabel.AutoSize = true;
            smoothLabel.Location = new Point(51, 429);
            smoothLabel.Name = "smoothLabel";
            smoothLabel.Size = new Size(105, 25);
            smoothLabel.TabIndex = 17;
            smoothLabel.Text = "Smoothing:";
            // 
            // highLabel
            // 
            highLabel.AutoSize = true;
            highLabel.Location = new Point(709, 16);
            highLabel.Name = "highLabel";
            highLabel.Size = new Size(50, 25);
            highLabel.TabIndex = 16;
            highLabel.Text = "High";
            // 
            // midLabel
            // 
            midLabel.AutoSize = true;
            midLabel.Location = new Point(644, 16);
            midLabel.Name = "midLabel";
            midLabel.Size = new Size(43, 25);
            midLabel.TabIndex = 15;
            midLabel.Text = "Mid";
            // 
            // lowLabel
            // 
            lowLabel.AutoSize = true;
            lowLabel.Location = new Point(586, 16);
            lowLabel.Name = "lowLabel";
            lowLabel.Size = new Size(44, 25);
            lowLabel.TabIndex = 14;
            lowLabel.Text = "Low";
            // 
            // signalPlot
            // 
            signalPlot.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            signalPlot.Location = new Point(0, 46);
            signalPlot.Margin = new Padding(6, 5, 6, 5);
            signalPlot.Name = "signalPlot";
            signalPlot.Size = new Size(783, 378);
            signalPlot.TabIndex = 13;
            // 
            // peakLabel
            // 
            peakLabel.AutoSize = true;
            peakLabel.Location = new Point(387, 16);
            peakLabel.Margin = new Padding(4, 0, 4, 0);
            peakLabel.Name = "peakLabel";
            peakLabel.Size = new Size(138, 25);
            peakLabel.TabIndex = 12;
            peakLabel.Text = "Peak Frequency:";
            // 
            // inputBox
            // 
            inputBox.FormattingEnabled = true;
            inputBox.Location = new Point(23, 13);
            inputBox.Margin = new Padding(4, 5, 4, 5);
            inputBox.Name = "inputBox";
            inputBox.Size = new Size(235, 33);
            inputBox.TabIndex = 11;
            inputBox.SelectedIndexChanged += inputBox_SelectedIndexChanged;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(useThumbnailCheckBox);
            tabPage2.Controls.Add(label1);
            tabPage2.Controls.Add(manualControlBox);
            tabPage2.Controls.Add(synchronizeCheck);
            tabPage2.Controls.Add(tableBox);
            tabPage2.Controls.Add(bedBox);
            tabPage2.Controls.Add(BitmapForm);
            tabPage2.Controls.Add(timeLabel);
            tabPage2.Location = new Point(4, 34);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(789, 478);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Manual";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // useThumbnailCheckBox
            // 
            useThumbnailCheckBox.AutoSize = true;
            useThumbnailCheckBox.Location = new Point(437, 279);
            useThumbnailCheckBox.Name = "useThumbnailCheckBox";
            useThumbnailCheckBox.Size = new Size(197, 29);
            useThumbnailCheckBox.TabIndex = 13;
            useThumbnailCheckBox.Text = "Use song thumbnail";
            useThumbnailCheckBox.UseVisualStyleBackColor = true;
            useThumbnailCheckBox.CheckedChanged += useThumbnailCheckBox_CheckedChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 433);
            label1.Name = "label1";
            label1.Size = new Size(120, 25);
            label1.TabIndex = 12;
            label1.Text = "Time elapsed:";
            // 
            // manualControlBox
            // 
            manualControlBox.AutoSize = true;
            manualControlBox.Location = new Point(147, 279);
            manualControlBox.Name = "manualControlBox";
            manualControlBox.Size = new Size(96, 29);
            manualControlBox.TabIndex = 11;
            manualControlBox.Text = "Manual";
            manualControlBox.UseVisualStyleBackColor = true;
            manualControlBox.CheckedChanged += manualControlBox_CheckedChanged;
            // 
            // synchronizeCheck
            // 
            synchronizeCheck.AutoSize = true;
            synchronizeCheck.Location = new Point(274, 279);
            synchronizeCheck.Name = "synchronizeCheck";
            synchronizeCheck.Size = new Size(132, 29);
            synchronizeCheck.TabIndex = 10;
            synchronizeCheck.Text = "Synchronize";
            synchronizeCheck.UseVisualStyleBackColor = true;
            synchronizeCheck.CheckedChanged += synchronizeCheck_CheckedChanged;
            // 
            // tableBox
            // 
            tableBox.Controls.Add(tableOverflowRadio);
            tableBox.Controls.Add(tablePulseRadio);
            tableBox.Controls.Add(tableStaticRadio);
            tableBox.Enabled = false;
            tableBox.Location = new Point(400, 88);
            tableBox.Name = "tableBox";
            tableBox.Size = new Size(370, 167);
            tableBox.TabIndex = 9;
            tableBox.TabStop = false;
            tableBox.Text = "Table";
            // 
            // tableOverflowRadio
            // 
            tableOverflowRadio.AutoSize = true;
            tableOverflowRadio.CheckAlign = ContentAlignment.MiddleRight;
            tableOverflowRadio.Checked = true;
            tableOverflowRadio.Location = new Point(23, 41);
            tableOverflowRadio.Name = "tableOverflowRadio";
            tableOverflowRadio.Size = new Size(109, 29);
            tableOverflowRadio.TabIndex = 6;
            tableOverflowRadio.TabStop = true;
            tableOverflowRadio.Text = "Overflow";
            tableOverflowRadio.UseVisualStyleBackColor = true;
            tableOverflowRadio.CheckedChanged += tableOverflowRadio_CheckedChanged;
            // 
            // tablePulseRadio
            // 
            tablePulseRadio.AutoSize = true;
            tablePulseRadio.CheckAlign = ContentAlignment.MiddleRight;
            tablePulseRadio.Location = new Point(23, 76);
            tablePulseRadio.Name = "tablePulseRadio";
            tablePulseRadio.Size = new Size(78, 29);
            tablePulseRadio.TabIndex = 7;
            tablePulseRadio.Text = "Pulse";
            tablePulseRadio.UseVisualStyleBackColor = true;
            tablePulseRadio.CheckedChanged += tablePulseRadio_CheckedChanged;
            // 
            // tableStaticRadio
            // 
            tableStaticRadio.AutoSize = true;
            tableStaticRadio.CheckAlign = ContentAlignment.MiddleRight;
            tableStaticRadio.Location = new Point(23, 111);
            tableStaticRadio.Name = "tableStaticRadio";
            tableStaticRadio.Size = new Size(79, 29);
            tableStaticRadio.TabIndex = 8;
            tableStaticRadio.Text = "Static";
            tableStaticRadio.UseVisualStyleBackColor = true;
            tableStaticRadio.CheckedChanged += tableStaticRadio_CheckedChanged;
            // 
            // bedBox
            // 
            bedBox.Controls.Add(bedOverflowRadio);
            bedBox.Controls.Add(bedPulseRadio);
            bedBox.Controls.Add(bedStaticRadio);
            bedBox.Enabled = false;
            bedBox.Location = new Point(19, 88);
            bedBox.Name = "bedBox";
            bedBox.Size = new Size(350, 167);
            bedBox.TabIndex = 8;
            bedBox.TabStop = false;
            bedBox.Text = "Bed";
            // 
            // bedOverflowRadio
            // 
            bedOverflowRadio.AutoSize = true;
            bedOverflowRadio.CheckAlign = ContentAlignment.MiddleRight;
            bedOverflowRadio.Checked = true;
            bedOverflowRadio.Location = new Point(18, 41);
            bedOverflowRadio.Name = "bedOverflowRadio";
            bedOverflowRadio.Size = new Size(109, 29);
            bedOverflowRadio.TabIndex = 3;
            bedOverflowRadio.TabStop = true;
            bedOverflowRadio.Text = "Overflow";
            bedOverflowRadio.UseVisualStyleBackColor = true;
            bedOverflowRadio.CheckedChanged += bedOverflowRadio_CheckedChanged;
            // 
            // bedPulseRadio
            // 
            bedPulseRadio.AutoSize = true;
            bedPulseRadio.CheckAlign = ContentAlignment.MiddleRight;
            bedPulseRadio.Location = new Point(18, 76);
            bedPulseRadio.Name = "bedPulseRadio";
            bedPulseRadio.Size = new Size(78, 29);
            bedPulseRadio.TabIndex = 4;
            bedPulseRadio.Text = "Pulse";
            bedPulseRadio.UseVisualStyleBackColor = true;
            bedPulseRadio.CheckedChanged += bedPulseRadio_CheckedChanged;
            // 
            // bedStaticRadio
            // 
            bedStaticRadio.AutoSize = true;
            bedStaticRadio.CheckAlign = ContentAlignment.MiddleRight;
            bedStaticRadio.Location = new Point(18, 111);
            bedStaticRadio.Name = "bedStaticRadio";
            bedStaticRadio.Size = new Size(79, 29);
            bedStaticRadio.TabIndex = 5;
            bedStaticRadio.Text = "Static";
            bedStaticRadio.UseVisualStyleBackColor = true;
            bedStaticRadio.CheckedChanged += bedStaticRadio_CheckedChanged;
            // 
            // BitmapForm
            // 
            BitmapForm.Location = new Point(615, 429);
            BitmapForm.Name = "BitmapForm";
            BitmapForm.Size = new Size(155, 34);
            BitmapForm.TabIndex = 7;
            BitmapForm.Text = "Screen capture";
            BitmapForm.UseVisualStyleBackColor = true;
            BitmapForm.Click += BitmapForm_Click;
            // 
            // timeLabel
            // 
            timeLabel.AutoSize = true;
            timeLabel.Location = new Point(136, 434);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(59, 25);
            timeLabel.TabIndex = 6;
            timeLabel.Text = "label1";
            // 
            // VisualizerForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(829, 548);
            Controls.Add(tabControl1);
            Margin = new Padding(4, 5, 4, 5);
            Name = "VisualizerForm";
            Text = "Visualizer";
            Load += VisualizerForm_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tableBox.ResumeLayout(false);
            tableBox.PerformLayout();
            bedBox.ResumeLayout(false);
            bedBox.PerformLayout();
            ResumeLayout(false);
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
        private Label label1;
        private RadioButton tableOverflowRadio;
        private RadioButton tablePulseRadio;
        private RadioButton tableStaticRadio;
        private CheckBox useThumbnailCheckBox;
    }
}