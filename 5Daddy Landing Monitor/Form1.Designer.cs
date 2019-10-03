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
            this.Connect = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.lrmDatabase1 = new _5Daddy_Landing_Monitor.LRMDatabase();
            this.userControl11 = new _5Daddy_Landing_Monitor.UserControl1();
            this.options1 = new _5Daddy_Landing_Monitor.Options();
            this.serverList1 = new _5Daddy_Landing_Monitor.ServerList();
            this.atcComms1 = new _5Daddy_Landing_Monitor.ATCComms();
            this.signIn1 = new _5Daddy_Landing_Monitor.SignIn();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Connect
            // 
            this.Connect.Font = new System.Drawing.Font("Segoe Print", 20F, System.Drawing.FontStyle.Bold);
            this.Connect.Location = new System.Drawing.Point(479, 99);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(523, 118);
            this.Connect.TabIndex = 1;
            this.Connect.Text = "Connect To Flight Sim";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(8, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(436, 331);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(462, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(586, 112);
            this.label1.TabIndex = 3;
            this.label1.Text = "Brought to you by 5Quin (quin#3017) in association with 5Daddy (5Daddy#6717) 5Dad" +
    "dy © FSX\r\n";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(144, 287);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(169, 43);
            this.label2.TabIndex = 4;
            this.label2.Text = "Not Connected";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(8, 13);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(936, 290);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 43);
            this.button1.TabIndex = 7;
            this.button1.Text = "Options";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(830, 290);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 43);
            this.button2.TabIndex = 8;
            this.button2.Text = "LRM";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(8, 290);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 43);
            this.button3.TabIndex = 9;
            this.button3.Text = "Disconnect";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(724, 290);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(100, 43);
            this.button4.TabIndex = 10;
            this.button4.Text = "Servers";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(299, 290);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 43);
            this.button5.TabIndex = 12;
            this.button5.Text = "Account";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(520, 290);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(198, 43);
            this.button6.TabIndex = 14;
            this.button6.Text = "Discord ATC Comms";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(405, 290);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(109, 43);
            this.button7.TabIndex = 17;
            this.button7.Text = "Database";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // lrmDatabase1
            // 
            this.lrmDatabase1.Location = new System.Drawing.Point(8, -3);
            this.lrmDatabase1.Name = "lrmDatabase1";
            this.lrmDatabase1.Size = new System.Drawing.Size(1028, 287);
            this.lrmDatabase1.TabIndex = 16;
            this.lrmDatabase1.Load += new System.EventHandler(this.lrmDatabase1_Load);
            // 
            // userControl11
            // 
            this.userControl11.Location = new System.Drawing.Point(12, -3);
            this.userControl11.Name = "userControl11";
            this.userControl11.Size = new System.Drawing.Size(1036, 287);
            this.userControl11.TabIndex = 13;
            // 
            // options1
            // 
            this.options1.Location = new System.Drawing.Point(12, -3);
            this.options1.Name = "options1";
            this.options1.Size = new System.Drawing.Size(1036, 287);
            this.options1.TabIndex = 13;
            // 
            // serverList1
            // 
            this.serverList1.Location = new System.Drawing.Point(12, -3);
            this.serverList1.Name = "serverList1";
            this.serverList1.Size = new System.Drawing.Size(1036, 287);
            this.serverList1.TabIndex = 13;
            // 
            // atcComms1
            // 
            this.atcComms1.Location = new System.Drawing.Point(8, -3);
            this.atcComms1.Name = "atcComms1";
            this.atcComms1.Size = new System.Drawing.Size(1028, 287);
            this.atcComms1.TabIndex = 15;
            this.atcComms1.Load += new System.EventHandler(this.atcComms1_Load);
            // 
            // signIn1
            // 
            this.signIn1.Location = new System.Drawing.Point(12, -3);
            this.signIn1.Name = "signIn1";
            this.signIn1.Size = new System.Drawing.Size(1036, 287);
            this.signIn1.TabIndex = 13;
            this.signIn1.Load += new System.EventHandler(this.signIn1_Load);
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1048, 345);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.lrmDatabase1);
            this.Controls.Add(this.atcComms1);
            this.Controls.Add(this.signIn1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "5Daddy LRM";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        
        private System.Windows.Forms.Button Connect;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        
        private System.Windows.Forms.Button button5;
        private SignIn signIn1;
        private System.Windows.Forms.Button button6;
        private ATCComms atcComms1;
        private LRMDatabase lrmDatabase1;
        private UserControl1 userControl11;
        private Options options1;
        private ServerList serverList1;
        private System.Windows.Forms.Button button7;
    }
}

