namespace LEDVisualizer
{
    partial class BitmapVisualizer
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
            this.timeLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.verticalOffsetCheck = new System.Windows.Forms.CheckBox();
            this.horizontalOffsetCheck = new System.Windows.Forms.CheckBox();
            this.autoOffsetBox = new System.Windows.Forms.GroupBox();
            this.timeLabelText = new System.Windows.Forms.Label();
            this.manualOffsetBox = new System.Windows.Forms.GroupBox();
            this.manHorOffsetBox = new System.Windows.Forms.TextBox();
            this.manVertOffsetBox = new System.Windows.Forms.TextBox();
            this.autoOffsetBox.SuspendLayout();
            this.manualOffsetBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(216, 167);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(59, 25);
            this.timeLabel.TabIndex = 0;
            this.timeLabel.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 20;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // verticalOffsetCheck
            // 
            this.verticalOffsetCheck.AutoSize = true;
            this.verticalOffsetCheck.Location = new System.Drawing.Point(6, 32);
            this.verticalOffsetCheck.Name = "verticalOffsetCheck";
            this.verticalOffsetCheck.Size = new System.Drawing.Size(94, 29);
            this.verticalOffsetCheck.TabIndex = 2;
            this.verticalOffsetCheck.Text = "Vertical";
            this.verticalOffsetCheck.UseVisualStyleBackColor = true;
            this.verticalOffsetCheck.CheckedChanged += new System.EventHandler(this.verticalOffsetCheck_CheckedChanged);
            // 
            // horizontalOffsetCheck
            // 
            this.horizontalOffsetCheck.AutoSize = true;
            this.horizontalOffsetCheck.Location = new System.Drawing.Point(6, 78);
            this.horizontalOffsetCheck.Name = "horizontalOffsetCheck";
            this.horizontalOffsetCheck.Size = new System.Drawing.Size(120, 29);
            this.horizontalOffsetCheck.TabIndex = 3;
            this.horizontalOffsetCheck.Text = "Horizontal";
            this.horizontalOffsetCheck.UseVisualStyleBackColor = true;
            this.horizontalOffsetCheck.CheckedChanged += new System.EventHandler(this.horizontalOffsetCheck_CheckedChanged);
            // 
            // autoOffsetBox
            // 
            this.autoOffsetBox.Controls.Add(this.verticalOffsetCheck);
            this.autoOffsetBox.Controls.Add(this.horizontalOffsetCheck);
            this.autoOffsetBox.Location = new System.Drawing.Point(23, 23);
            this.autoOffsetBox.Name = "autoOffsetBox";
            this.autoOffsetBox.Size = new System.Drawing.Size(155, 123);
            this.autoOffsetBox.TabIndex = 4;
            this.autoOffsetBox.TabStop = false;
            this.autoOffsetBox.Text = "Auto offset";
            // 
            // timeLabelText
            // 
            this.timeLabelText.AutoSize = true;
            this.timeLabelText.Location = new System.Drawing.Point(90, 167);
            this.timeLabelText.Name = "timeLabelText";
            this.timeLabelText.Size = new System.Drawing.Size(120, 25);
            this.timeLabelText.TabIndex = 5;
            this.timeLabelText.Text = "Time elapsed:";
            // 
            // manualOffsetBox
            // 
            this.manualOffsetBox.Controls.Add(this.manHorOffsetBox);
            this.manualOffsetBox.Controls.Add(this.manVertOffsetBox);
            this.manualOffsetBox.Location = new System.Drawing.Point(197, 23);
            this.manualOffsetBox.Name = "manualOffsetBox";
            this.manualOffsetBox.Size = new System.Drawing.Size(155, 123);
            this.manualOffsetBox.TabIndex = 6;
            this.manualOffsetBox.TabStop = false;
            this.manualOffsetBox.Text = "Manual offset";
            // 
            // manHorOffsetBox
            // 
            this.manHorOffsetBox.Location = new System.Drawing.Point(19, 76);
            this.manHorOffsetBox.Name = "manHorOffsetBox";
            this.manHorOffsetBox.Size = new System.Drawing.Size(122, 31);
            this.manHorOffsetBox.TabIndex = 20;
            this.manHorOffsetBox.Text = "Horizontal";
            this.manHorOffsetBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.manHorOffsetBox.TextChanged += new System.EventHandler(this.horManOffsetBox_TextChanged);
            // 
            // manVertOffsetBox
            // 
            this.manVertOffsetBox.Location = new System.Drawing.Point(19, 30);
            this.manVertOffsetBox.Name = "manVertOffsetBox";
            this.manVertOffsetBox.Size = new System.Drawing.Size(122, 31);
            this.manVertOffsetBox.TabIndex = 19;
            this.manVertOffsetBox.Text = "Vertical";
            this.manVertOffsetBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.manVertOffsetBox.TextChanged += new System.EventHandler(this.manVertOffsetBox_TextChanged);
            // 
            // BitmapVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(369, 213);
            this.Controls.Add(this.manualOffsetBox);
            this.Controls.Add(this.timeLabelText);
            this.Controls.Add(this.autoOffsetBox);
            this.Controls.Add(this.timeLabel);
            this.Name = "BitmapVisualizer";
            this.Text = "BitmapVisualizer";
            this.autoOffsetBox.ResumeLayout(false);
            this.autoOffsetBox.PerformLayout();
            this.manualOffsetBox.ResumeLayout(false);
            this.manualOffsetBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label timeLabel;
        private System.Windows.Forms.Timer timer1;
        private CheckBox verticalOffsetCheck;
        private CheckBox horizontalOffsetCheck;
        private GroupBox autoOffsetBox;
        private Label timeLabelText;
        private GroupBox manualOffsetBox;
        private TextBox manHorOffsetBox;
        private TextBox manVertOffsetBox;
    }
}