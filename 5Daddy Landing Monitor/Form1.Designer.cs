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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.atcComms1 = new _5Daddy_Landing_Monitor.ATCComms();
            this.signIn1 = new _5Daddy_Landing_Monitor.SignIn();
            this.lrmDatabase1 = new _5Daddy_Landing_Monitor.LRMDatabase();
            this.button3 = new MetroFramework.Controls.MetroButton();
            this.button5 = new MetroFramework.Controls.MetroButton();
            this.button7 = new MetroFramework.Controls.MetroButton();
            this.button6 = new MetroFramework.Controls.MetroButton();
            this.button4 = new MetroFramework.Controls.MetroButton();
            this.label3 = new MetroFramework.Controls.MetroLabel();
            this.label1 = new MetroFramework.Controls.MetroLabel();
            this.Connect = new MetroFramework.Controls.MetroButton();
            this.HR1 = new MetroFramework.Drawing.Html.HtmlPanel();
            this.HR = new MetroFramework.Drawing.Html.HtmlPanel();
            this.options1 = new _5Daddy_Landing_Monitor.Options();
            this.serverList1 = new _5Daddy_Landing_Monitor.ServerList();
            this.userControl11 = new _5Daddy_Landing_Monitor.UserControl1();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(181, 42);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(406, 229);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(5, 8);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(0, 0);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(0, 0);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(0, 0);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(116, 397);
            this.button1.Margin = new System.Windows.Forms.Padding(2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(67, 28);
            this.button1.TabIndex = 7;
            this.button1.Text = "Options";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(45, 397);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(67, 28);
            this.button2.TabIndex = 8;
            this.button2.Text = "LRM";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // atcComms1
            // 
            this.atcComms1.Location = new System.Drawing.Point(23, 81);
            this.atcComms1.Margin = new System.Windows.Forms.Padding(1);
            this.atcComms1.Name = "atcComms1";
            this.atcComms1.Size = new System.Drawing.Size(685, 187);
            this.atcComms1.TabIndex = 15;
            this.atcComms1.Load += new System.EventHandler(this.atcComms1_Load);
            // 
            // signIn1
            // 
            this.signIn1.BackColor = System.Drawing.Color.White;
            this.signIn1.Location = new System.Drawing.Point(23, 81);
            this.signIn1.Margin = new System.Windows.Forms.Padding(1);
            this.signIn1.Name = "signIn1";
            this.signIn1.Size = new System.Drawing.Size(691, 187);
            this.signIn1.TabIndex = 13;
            this.signIn1.Load += new System.EventHandler(this.signIn1_Load);
            // 
            // lrmDatabase1
            // 
            this.lrmDatabase1.Location = new System.Drawing.Point(21, 81);
            this.lrmDatabase1.Margin = new System.Windows.Forms.Padding(1);
            this.lrmDatabase1.Name = "lrmDatabase1";
            this.lrmDatabase1.Size = new System.Drawing.Size(685, 187);
            this.lrmDatabase1.TabIndex = 16;
            this.lrmDatabase1.Load += new System.EventHandler(this.lrmDatabase1_Load);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.button3.FontSize = MetroFramework.MetroButtonSize.Tall;
            this.button3.FontWeight = MetroFramework.MetroButtonWeight.Regular;
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Location = new System.Drawing.Point(45, 328);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 45);
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
            this.button5.Location = new System.Drawing.Point(138, 328);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(74, 45);
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
            this.button7.Location = new System.Drawing.Point(218, 328);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(74, 45);
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
            this.button6.Location = new System.Drawing.Point(298, 328);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(103, 45);
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
            this.button4.Location = new System.Drawing.Point(407, 328);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 45);
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
            this.label3.Location = new System.Drawing.Point(24, 285);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(132, 25);
            this.label3.TabIndex = 24;
            this.label3.Text = "Not Connected!";
            this.label3.UseCustomForeColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 376);
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
            this.Connect.Location = new System.Drawing.Point(218, 276);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(349, 77);
            this.Connect.TabIndex = 29;
            this.Connect.Text = "Connect To Flight Simulator";
            this.Connect.UseCustomBackColor = true;
            this.Connect.UseCustomForeColor = true;
            this.Connect.UseSelectable = true;
            this.Connect.Click += new System.EventHandler(this.button1_Click);
            // 
            // HR1
            // 
            this.HR1.AutoScroll = true;
            this.HR1.AutoScrollMinSize = new System.Drawing.Size(685, 20);
            this.HR1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.HR1.Location = new System.Drawing.Point(21, 313);
            this.HR1.Name = "HR1";
            this.HR1.Size = new System.Drawing.Size(685, 24);
            this.HR1.TabIndex = 30;
            this.HR1.Text = "<style> hr { display: block; margin-top: 0.5em; margin-bottom: 0.5em; margin-left" +
    ": auto; margin-right: auto; border-style: inset; border-width: 1px; } </style><h" +
    "r>";
            // 
            // HR
            // 
            this.HR.AutoScroll = true;
            this.HR.AutoScrollMinSize = new System.Drawing.Size(685, 20);
            this.HR.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(17)))), ((int)(((byte)(17)))), ((int)(((byte)(17)))));
            this.HR.Location = new System.Drawing.Point(21, 63);
            this.HR.Name = "HR";
            this.HR.Size = new System.Drawing.Size(685, 24);
            this.HR.TabIndex = 31;
            this.HR.Text = "<style> hr { display: block; margin-top: 0.5em; margin-bottom: 0.5em; margin-left" +
    ": auto; margin-right: auto; border-style: inset; border-width: 1px; } </style><h" +
    "r>";
            // 
            // options1
            // 
            this.options1.Location = new System.Drawing.Point(25, 84);
            this.options1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.options1.Name = "options1";
            this.options1.Size = new System.Drawing.Size(685, 187);
            this.options1.TabIndex = 33;
            // 
            // serverList1
            // 
            this.serverList1.Location = new System.Drawing.Point(25, 84);
            this.serverList1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.serverList1.Name = "serverList1";
            this.serverList1.Size = new System.Drawing.Size(685, 187);
            this.serverList1.TabIndex = 34;
            // 
            // userControl11
            // 
            this.userControl11.BackColor = System.Drawing.Color.White;
            this.userControl11.Location = new System.Drawing.Point(25, 81);
            this.userControl11.Margin = new System.Windows.Forms.Padding(2);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(685, 242);
            this.userControl11.TabIndex = 35;
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 426);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.HR1);
            this.Controls.Add(this.HR);
            this.Controls.Add(this.userControl11);
            this.Controls.Add(this.atcComms1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.signIn1);
            this.Controls.Add(this.lrmDatabase1);
            this.Controls.Add(this.serverList1);
            this.Controls.Add(this.options1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Form1";
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
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
        private MetroFramework.Drawing.Html.HtmlPanel HR1;
        private MetroFramework.Drawing.Html.HtmlPanel HR;
        private Options options1;
        private ServerList serverList1;
        private UserControl1 userControl11;
    }
}

