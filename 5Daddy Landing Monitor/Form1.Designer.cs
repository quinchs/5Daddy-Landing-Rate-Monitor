namespace _5Daddy_Landing_Monitor
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button3 = new MetroFramework.Controls.MetroButton();
            this.button5 = new MetroFramework.Controls.MetroButton();
            this.button7 = new MetroFramework.Controls.MetroButton();
            this.button6 = new MetroFramework.Controls.MetroButton();
            this.button4 = new MetroFramework.Controls.MetroButton();
            this.label3 = new MetroFramework.Controls.MetroLabel();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.Connect = new MetroFramework.Controls.MetroButton();
            this.button2 = new MetroFramework.Controls.MetroButton();
            this.button1 = new MetroFramework.Controls.MetroButton();
            this.userControl11 = new _5Daddy_Landing_Monitor.UserControl1();
            this.atcComms1 = new _5Daddy_Landing_Monitor.ATCComms();
            this.signIn1 = new _5Daddy_Landing_Monitor.SignIn();
            this.lrmDatabase1 = new _5Daddy_Landing_Monitor.LRMDatabase();
            this.serverList1 = new _5Daddy_Landing_Monitor.ServerList();
            this.options1 = new _5Daddy_Landing_Monitor.Options();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(242, 61);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(609, 352);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(8, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(0, 0);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(0, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(0, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button3.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.button3.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(68, 505);
            this.button3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 69);
            this.button3.TabIndex = 18;
            this.button3.Text = "Disconnect";
            this.button3.UseCustomBackColor = true;
            this.button3.UseCustomForeColor = true;
            this.button3.UseSelectable = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button5.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.button5.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Location = new System.Drawing.Point(207, 505);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(111, 69);
            this.button5.TabIndex = 19;
            this.button5.Text = "Account";
            this.button5.UseCustomBackColor = true;
            this.button5.UseCustomForeColor = true;
            this.button5.UseSelectable = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button7
            // 
            this.button7.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button7.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.button7.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.button7.ForeColor = System.Drawing.Color.White;
            this.button7.Location = new System.Drawing.Point(327, 505);
            this.button7.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(111, 69);
            this.button7.TabIndex = 20;
            this.button7.Text = "Database";
            this.button7.UseCustomBackColor = true;
            this.button7.UseCustomForeColor = true;
            this.button7.UseSelectable = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button6
            // 
            this.button6.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button6.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.button6.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.button6.ForeColor = System.Drawing.Color.White;
            this.button6.Location = new System.Drawing.Point(447, 505);
            this.button6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(154, 69);
            this.button6.TabIndex = 21;
            this.button6.Text = "ATC Comms";
            this.button6.UseCustomBackColor = true;
            this.button6.UseCustomForeColor = true;
            this.button6.UseSelectable = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button4.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.button4.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(610, 505);
            this.button4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(120, 69);
            this.button4.TabIndex = 22;
            this.button4.Text = "Servers";
            this.button4.UseCustomBackColor = true;
            this.button4.UseCustomForeColor = true;
            this.button4.UseSelectable = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.label3.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(36, 438);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 25);
            this.label3.TabIndex = 24;
            this.label3.Text = "Not Connected!";
            this.label3.UseCustomForeColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(130, 579);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(564, 19);
            this.label1.TabIndex = 25;
            this.label1.Text = "Brought to you by 5Quin (quin#3017) in association with 5Daddy (5Daddy#6717) 5Dad" +
    "dy © FSX";
            // 
            // Connect
            // 
            this.Connect.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.Connect.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.Connect.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.Connect.ForeColor = System.Drawing.Color.White;
            this.Connect.Location = new System.Drawing.Point(327, 425);
            this.Connect.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(524, 118);
            this.Connect.TabIndex = 29;
            this.Connect.Text = "Connect To Flight Simulator";
            this.Connect.UseCustomBackColor = true;
            this.Connect.UseCustomForeColor = true;
            this.Connect.UseSelectable = true;
            this.Connect.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button2.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.button2.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(738, 505);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(120, 69);
            this.button2.TabIndex = 36;
            this.button2.Text = "LRM";
            this.button2.UseCustomBackColor = true;
            this.button2.UseCustomForeColor = true;
            this.button2.UseSelectable = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.button1.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(866, 505);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 69);
            this.button1.TabIndex = 37;
            this.button1.Text = "Options";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseCustomBackColor = true;
            this.button1.UseCustomForeColor = true;
            this.button1.UseSelectable = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // userControl11
            // 
            this.userControl11.BackColor = System.Drawing.Color.White;
            this.userControl11.Location = new System.Drawing.Point(38, 125);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(1028, 372);
            this.userControl11.TabIndex = 35;
            // 
            // atcComms1
            // 
            this.atcComms1.Location = new System.Drawing.Point(34, 125);
            this.atcComms1.Margin = new System.Windows.Forms.Padding(2);
            this.atcComms1.Name = "atcComms1";
            this.atcComms1.Size = new System.Drawing.Size(1028, 288);
            this.atcComms1.TabIndex = 15;
            this.atcComms1.Load += new System.EventHandler(this.atcComms1_Load);
            // 
            // signIn1
            // 
            this.signIn1.BackColor = System.Drawing.Color.White;
            this.signIn1.Location = new System.Drawing.Point(34, 125);
            this.signIn1.Margin = new System.Windows.Forms.Padding(2);
            this.signIn1.Name = "signIn1";
            this.signIn1.Size = new System.Drawing.Size(1036, 288);
            this.signIn1.TabIndex = 13;
            this.signIn1.Load += new System.EventHandler(this.signIn1_Load);
            // 
            // lrmDatabase1
            // 
            this.lrmDatabase1.Location = new System.Drawing.Point(32, 125);
            this.lrmDatabase1.Margin = new System.Windows.Forms.Padding(2);
            this.lrmDatabase1.Name = "lrmDatabase1";
            this.lrmDatabase1.Size = new System.Drawing.Size(1028, 288);
            this.lrmDatabase1.TabIndex = 16;
            this.lrmDatabase1.Load += new System.EventHandler(this.lrmDatabase1_Load);
            // 
            // serverList1
            // 
            this.serverList1.Location = new System.Drawing.Point(38, 129);
            this.serverList1.Name = "serverList1";
            this.serverList1.Size = new System.Drawing.Size(1028, 288);
            this.serverList1.TabIndex = 34;
            // 
            // options1
            // 
            this.options1.Location = new System.Drawing.Point(38, 129);
            this.options1.Name = "options1";
            this.options1.Size = new System.Drawing.Size(1028, 288);
            this.options1.TabIndex = 33;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 655);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.userControl11);
            this.Controls.Add(this.atcComms1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.signIn1);
            this.Controls.Add(this.lrmDatabase1);
            this.Controls.Add(this.serverList1);
            this.Controls.Add(this.options1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(30, 92, 30, 31);
            this.Resizable = false;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "5Daddy LRM";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private SignIn signIn1;
        private ATCComms atcComms1;
        private LRMDatabase lrmDatabase1;
        private MetroFramework.Controls.MetroButton button3;
        private MetroFramework.Controls.MetroButton button5;
        private MetroFramework.Controls.MetroButton button7;
        private MetroFramework.Controls.MetroButton button6;
        private MetroFramework.Controls.MetroButton button4;
        private MetroFramework.Controls.MetroLabel label3;
        private MetroFramework.Controls.MetroLabel label1;
        private MetroFramework.Controls.MetroButton Connect;
        private Options options1;
        private ServerList serverList1;
        private UserControl1 userControl11;
        private MetroFramework.Controls.MetroButton button2;
        private MetroFramework.Controls.MetroButton button1;
        
    }
}

