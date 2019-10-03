namespace _5Daddy_Landing_Monitor
{
    partial class UserControl1
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.PitchLabel = new MetroFramework.Controls.MetroLabel();
            this.BankLabel = new MetroFramework.Controls.MetroLabel();
            this.SpeedLabel = new MetroFramework.Controls.MetroLabel();
            this.WiDiLabel = new MetroFramework.Controls.MetroLabel();
            this.WiSpLabel = new MetroFramework.Controls.MetroLabel();
            this.VSRate = new MetroFramework.Controls.MetroLabel();
            this.Rate = new MetroFramework.Controls.MetroLabel();
            this.label8 = new MetroFramework.Controls.MetroLabel();
            this.label10 = new MetroFramework.Controls.MetroLabel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.checkBox1.Location = new System.Drawing.Point(30, 309);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(344, 33);
            this.checkBox1.TabIndex = 16;
            this.checkBox1.Text = "Send to (server) LRM Server";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // PitchLabel
            // 
            this.PitchLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.PitchLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.PitchLabel.Location = new System.Drawing.Point(26, 55);
            this.PitchLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.PitchLabel.Name = "PitchLabel";
            this.PitchLabel.Size = new System.Drawing.Size(224, 45);
            this.PitchLabel.TabIndex = 18;
            this.PitchLabel.Text = "Pitch: 0";
            // 
            // BankLabel
            // 
            this.BankLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.BankLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.BankLabel.Location = new System.Drawing.Point(26, 100);
            this.BankLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.BankLabel.Name = "BankLabel";
            this.BankLabel.Size = new System.Drawing.Size(224, 45);
            this.BankLabel.TabIndex = 19;
            this.BankLabel.Text = "Bank: 0";
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.SpeedLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.SpeedLabel.Location = new System.Drawing.Point(30, 149);
            this.SpeedLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(256, 45);
            this.SpeedLabel.TabIndex = 20;
            this.SpeedLabel.Text = "Speed: 0";
            // 
            // WiDiLabel
            // 
            this.WiDiLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.WiDiLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.WiDiLabel.Location = new System.Drawing.Point(322, 105);
            this.WiDiLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.WiDiLabel.Name = "WiDiLabel";
            this.WiDiLabel.Size = new System.Drawing.Size(286, 45);
            this.WiDiLabel.TabIndex = 21;
            this.WiDiLabel.Text = "Wind Direction: 0";
            // 
            // WiSpLabel
            // 
            this.WiSpLabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.WiSpLabel.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.WiSpLabel.Location = new System.Drawing.Point(322, 55);
            this.WiSpLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.WiSpLabel.Name = "WiSpLabel";
            this.WiSpLabel.Size = new System.Drawing.Size(286, 45);
            this.WiSpLabel.TabIndex = 22;
            this.WiSpLabel.Text = "Wind Speed: 0";
            // 
            // VSRate
            // 
            this.VSRate.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.VSRate.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.VSRate.Location = new System.Drawing.Point(588, 55);
            this.VSRate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.VSRate.Name = "VSRate";
            this.VSRate.Size = new System.Drawing.Size(286, 45);
            this.VSRate.TabIndex = 23;
            this.VSRate.Text = "Feet Per Minute: 0";
            // 
            // Rate
            // 
            this.Rate.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.Rate.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.Rate.Location = new System.Drawing.Point(588, 100);
            this.Rate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.Rate.Name = "Rate";
            this.Rate.Size = new System.Drawing.Size(256, 45);
            this.Rate.TabIndex = 24;
            this.Rate.Click += new System.EventHandler(this.Rate_Click);
            // 
            // label8
            // 
            this.label8.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.label8.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label8.Location = new System.Drawing.Point(588, 100);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(189, 63);
            this.label8.TabIndex = 25;
            // 
            // label10
            // 
            this.label10.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.label10.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.label10.Location = new System.Drawing.Point(26, 11);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(969, 45);
            this.label10.TabIndex = 26;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(588, 149);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(286, 192);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Rate);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.VSRate);
            this.Controls.Add(this.WiSpLabel);
            this.Controls.Add(this.WiDiLabel);
            this.Controls.Add(this.SpeedLabel);
            this.Controls.Add(this.BankLabel);
            this.Controls.Add(this.PitchLabel);
            this.Controls.Add(this.checkBox1);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1028, 378);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.CheckBox checkBox1;
        private MetroFramework.Controls.MetroLabel PitchLabel;
        private MetroFramework.Controls.MetroLabel BankLabel;
        private MetroFramework.Controls.MetroLabel SpeedLabel;
        private MetroFramework.Controls.MetroLabel WiDiLabel;
        private MetroFramework.Controls.MetroLabel WiSpLabel;
        private MetroFramework.Controls.MetroLabel VSRate;
        private MetroFramework.Controls.MetroLabel Rate;
        private MetroFramework.Controls.MetroLabel label8;
        private MetroFramework.Controls.MetroLabel label10;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
