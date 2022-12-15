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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(108, 202);
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
            this.verticalOffsetCheck.Location = new System.Drawing.Point(6, 43);
            this.verticalOffsetCheck.Name = "verticalOffsetCheck";
            this.verticalOffsetCheck.Size = new System.Drawing.Size(142, 29);
            this.verticalOffsetCheck.TabIndex = 2;
            this.verticalOffsetCheck.Text = "Вертикально";
            this.verticalOffsetCheck.UseVisualStyleBackColor = true;
            this.verticalOffsetCheck.CheckedChanged += new System.EventHandler(this.verticalOffsetCheck_CheckedChanged);
            // 
            // horizontalOffsetCheck
            // 
            this.horizontalOffsetCheck.AutoSize = true;
            this.horizontalOffsetCheck.Location = new System.Drawing.Point(6, 78);
            this.horizontalOffsetCheck.Name = "horizontalOffsetCheck";
            this.horizontalOffsetCheck.Size = new System.Drawing.Size(162, 29);
            this.horizontalOffsetCheck.TabIndex = 3;
            this.horizontalOffsetCheck.Text = "Горизонтально";
            this.horizontalOffsetCheck.UseVisualStyleBackColor = true;
            this.horizontalOffsetCheck.CheckedChanged += new System.EventHandler(this.horizontalOffsetCheck_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.verticalOffsetCheck);
            this.groupBox1.Controls.Add(this.horizontalOffsetCheck);
            this.groupBox1.Location = new System.Drawing.Point(23, 23);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 123);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Поиск изображения";
            // 
            // BitmapVisualizer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.timeLabel);
            this.Name = "BitmapVisualizer";
            this.Text = "BitmapVisualizer";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label timeLabel;
        private System.Windows.Forms.Timer timer1;
        private CheckBox verticalOffsetCheck;
        private CheckBox horizontalOffsetCheck;
        private GroupBox groupBox1;
    }
}