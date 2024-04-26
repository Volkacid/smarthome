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
            interceptBox = new GroupBox();
            settingsNoneBtn = new RadioButton();
            settingsThumbnailBtn = new RadioButton();
            settingsDominantBtn = new RadioButton();
            BitmapForm = new Button();
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
            colorDialog1 = new ColorDialog();
            interceptBox.SuspendLayout();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 15;
            timer1.Tick += timer1_Tick;
            // 
            // interceptBox
            // 
            interceptBox.Controls.Add(settingsNoneBtn);
            interceptBox.Controls.Add(settingsThumbnailBtn);
            interceptBox.Controls.Add(settingsDominantBtn);
            interceptBox.Location = new Point(494, 470);
            interceptBox.Name = "interceptBox";
            interceptBox.Size = new Size(276, 126);
            interceptBox.TabIndex = 28;
            interceptBox.TabStop = false;
            interceptBox.Text = "Other settings";
            // 
            // settingsNoneBtn
            // 
            settingsNoneBtn.AutoSize = true;
            settingsNoneBtn.Checked = true;
            settingsNoneBtn.Location = new Point(18, 41);
            settingsNoneBtn.Name = "settingsNoneBtn";
            settingsNoneBtn.Size = new Size(77, 29);
            settingsNoneBtn.TabIndex = 3;
            settingsNoneBtn.TabStop = true;
            settingsNoneBtn.Text = "none";
            settingsNoneBtn.UseVisualStyleBackColor = true;
            settingsNoneBtn.CheckedChanged += settingsNoneBtn_CheckedChanged;
            // 
            // settingsThumbnailBtn
            // 
            settingsThumbnailBtn.AutoSize = true;
            settingsThumbnailBtn.Location = new Point(115, 44);
            settingsThumbnailBtn.Name = "settingsThumbnailBtn";
            settingsThumbnailBtn.Size = new Size(120, 29);
            settingsThumbnailBtn.TabIndex = 4;
            settingsThumbnailBtn.Text = "Thumbnail";
            settingsThumbnailBtn.UseVisualStyleBackColor = true;
            settingsThumbnailBtn.CheckedChanged += settingsThumbnailBtn_CheckedChanged;
            // 
            // settingsDominantBtn
            // 
            settingsDominantBtn.AutoSize = true;
            settingsDominantBtn.Location = new Point(115, 79);
            settingsDominantBtn.Name = "settingsDominantBtn";
            settingsDominantBtn.Size = new Size(161, 29);
            settingsDominantBtn.TabIndex = 5;
            settingsDominantBtn.Text = "Dominant color";
            settingsDominantBtn.UseVisualStyleBackColor = true;
            settingsDominantBtn.CheckedChanged += settingsDominantBtn_CheckedChanged;
            // 
            // BitmapForm
            // 
            BitmapForm.Location = new Point(281, 550);
            BitmapForm.Name = "BitmapForm";
            BitmapForm.Size = new Size(155, 34);
            BitmapForm.TabIndex = 27;
            BitmapForm.Text = "Screen capture";
            BitmapForm.UseVisualStyleBackColor = true;
            BitmapForm.Click += BitmapForm_Click;
            // 
            // connectButton
            // 
            connectButton.Location = new Point(281, 23);
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
            schemeBox.Items.AddRange(new object[] { "RGB", "RBG", "GRB", "GBR", "BRG", "BGR" });
            schemeBox.Location = new Point(370, 494);
            schemeBox.Name = "schemeBox";
            schemeBox.Size = new Size(66, 33);
            schemeBox.TabIndex = 24;
            schemeBox.Text = "RGB";
            schemeBox.SelectedIndexChanged += schemeBox_SelectedIndexChanged;
            // 
            // schemeLabel
            // 
            schemeLabel.AutoSize = true;
            schemeLabel.Location = new Point(249, 498);
            schemeLabel.Name = "schemeLabel";
            schemeLabel.Size = new Size(124, 25);
            schemeLabel.TabIndex = 23;
            schemeLabel.Text = "Color scheme:";
            // 
            // highGain
            // 
            highGain.BackColor = Color.FromArgb(192, 255, 255);
            highGain.Location = new Point(192, 551);
            highGain.Name = "highGain";
            highGain.Size = new Size(31, 31);
            highGain.TabIndex = 22;
            highGain.Text = "4";
            highGain.TextAlign = HorizontalAlignment.Center;
            // 
            // midGain
            // 
            midGain.BackColor = Color.FromArgb(192, 255, 192);
            midGain.Location = new Point(155, 551);
            midGain.Name = "midGain";
            midGain.Size = new Size(31, 31);
            midGain.TabIndex = 21;
            midGain.Text = "4";
            midGain.TextAlign = HorizontalAlignment.Center;
            // 
            // lowGain
            // 
            lowGain.BackColor = Color.FromArgb(255, 192, 192);
            lowGain.Location = new Point(119, 551);
            lowGain.Name = "lowGain";
            lowGain.Size = new Size(31, 31);
            lowGain.TabIndex = 20;
            lowGain.Text = "3";
            lowGain.TextAlign = HorizontalAlignment.Center;
            // 
            // gainLabel
            // 
            gainLabel.AutoSize = true;
            gainLabel.Location = new Point(62, 555);
            gainLabel.Name = "gainLabel";
            gainLabel.Size = new Size(51, 25);
            gainLabel.TabIndex = 19;
            gainLabel.Text = "Gain:";
            // 
            // smoothBox
            // 
            smoothBox.Location = new Point(173, 497);
            smoothBox.Name = "smoothBox";
            smoothBox.Size = new Size(31, 31);
            smoothBox.TabIndex = 18;
            smoothBox.Text = "5";
            smoothBox.TextAlign = HorizontalAlignment.Center;
            // 
            // smoothLabel
            // 
            smoothLabel.AutoSize = true;
            smoothLabel.Location = new Point(62, 500);
            smoothLabel.Name = "smoothLabel";
            smoothLabel.Size = new Size(105, 25);
            smoothLabel.TabIndex = 17;
            smoothLabel.Text = "Smoothing:";
            // 
            // highLabel
            // 
            highLabel.AutoSize = true;
            highLabel.Location = new Point(720, 27);
            highLabel.Name = "highLabel";
            highLabel.Size = new Size(50, 25);
            highLabel.TabIndex = 16;
            highLabel.Text = "High";
            // 
            // midLabel
            // 
            midLabel.AutoSize = true;
            midLabel.Location = new Point(655, 27);
            midLabel.Name = "midLabel";
            midLabel.Size = new Size(43, 25);
            midLabel.TabIndex = 15;
            midLabel.Text = "Mid";
            // 
            // lowLabel
            // 
            lowLabel.AutoSize = true;
            lowLabel.Location = new Point(597, 27);
            lowLabel.Name = "lowLabel";
            lowLabel.Size = new Size(44, 25);
            lowLabel.TabIndex = 14;
            lowLabel.Text = "Low";
            // 
            // signalPlot
            // 
            signalPlot.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            signalPlot.Location = new Point(16, 57);
            signalPlot.Margin = new Padding(6, 5, 6, 5);
            signalPlot.Name = "signalPlot";
            signalPlot.Size = new Size(771, 430);
            signalPlot.TabIndex = 13;
            // 
            // peakLabel
            // 
            peakLabel.AutoSize = true;
            peakLabel.Location = new Point(398, 27);
            peakLabel.Margin = new Padding(4, 0, 4, 0);
            peakLabel.Name = "peakLabel";
            peakLabel.Size = new Size(138, 25);
            peakLabel.TabIndex = 12;
            peakLabel.Text = "Peak Frequency:";
            // 
            // inputBox
            // 
            inputBox.FormattingEnabled = true;
            inputBox.Location = new Point(34, 24);
            inputBox.Margin = new Padding(4, 5, 4, 5);
            inputBox.Name = "inputBox";
            inputBox.Size = new Size(235, 33);
            inputBox.TabIndex = 11;
            inputBox.SelectedIndexChanged += inputBox_SelectedIndexChanged;
            // 
            // VisualizerForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(801, 605);
            Controls.Add(interceptBox);
            Controls.Add(BitmapForm);
            Controls.Add(connectButton);
            Controls.Add(inputBox);
            Controls.Add(schemeBox);
            Controls.Add(peakLabel);
            Controls.Add(schemeLabel);
            Controls.Add(signalPlot);
            Controls.Add(highGain);
            Controls.Add(lowLabel);
            Controls.Add(midGain);
            Controls.Add(midLabel);
            Controls.Add(lowGain);
            Controls.Add(highLabel);
            Controls.Add(gainLabel);
            Controls.Add(smoothLabel);
            Controls.Add(smoothBox);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Margin = new Padding(4, 5, 4, 5);
            Name = "VisualizerForm";
            Text = "Visualizer";
            Load += VisualizerForm_Load;
            interceptBox.ResumeLayout(false);
            interceptBox.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private Label highLabel;
        private Label midLabel;
        private Label lowLabel;
        private ScottPlot.FormsPlot signalPlot;
        private Label peakLabel;
        private ComboBox inputBox;
        private TextBox smoothBox;
        private Label smoothLabel;
        private TextBox highGain;
        private TextBox midGain;
        private TextBox lowGain;
        private Label gainLabel;
        private ComboBox schemeBox;
        private Label schemeLabel;
        private Button connectButton;
        private Button BitmapForm;
        private ColorDialog colorDialog1;
        private GroupBox interceptBox;
        private RadioButton settingsNoneBtn;
        private RadioButton settingsThumbnailBtn;
        private RadioButton settingsDominantBtn;
    }
}