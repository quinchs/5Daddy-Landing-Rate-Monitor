using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using FSUIPC;

namespace _5Daddy_Landing_Monitor
{
    public partial class Form1 : Form
    {
        private UserControl1 userControl11 = new UserControl1();
        private Options options1 = new Options();
        private ServerList serverList1 = new ServerList();

        public Form1()
        {
            InitializeComponent();
            this.Controls.Add(this.options1);
            this.Controls.Add(this.serverList1);
            this.Controls.Add(this.userControl11);

            userControl11.Hide();
            button2.Hide();
            button4.Hide();
            button6.Hide();
            button5.Hide();
            button7.Hide();
            button3.Hide();
            signIn1.Visible = false;
            serverList1.Hide();
            options1.Hide();
            button1.Hide();
            atcComms1.Visible = false;
            lrmDatabase1.Visible = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //handles memory login
            string tstMtch = File.ReadAllText(Environment.CurrentDirectory + @"\5Daddy Landing Monitor.exe.config");

            //set global instances
            GlobalInstances.atccomms1 = atcComms1;
            GlobalInstances.LRMDatabase1 = lrmDatabase1;
            GlobalInstances.options1 = options1;
            GlobalInstances.signin1 = signIn1;
            GlobalInstances.UserControl1 = userControl11;
            GlobalInstances.serverlist1 = serverList1;

            if (Regex.IsMatch(tstMtch, "<!--(.*?)-->"))
            {
                var mth = Regex.Matches(tstMtch, "<!--(.*?)-->");
                string nameToken = mth[0].Value.Trim('<', '!', '-', '>');
                string[] vs = nameToken.Split('|');
                SignIn.memloging = true;
                SignIn.memloginName = vs[0];
                SignIn.memloginToken = vs[1];
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                FSUIPCConnection.Open(FlightSim.Any);
                ConnectionStatus();
            }
            catch(FSUIPCException ex)
            {
                GlobalData.ErrorLogInput(ex, "WARNING");
                MessageBox.Show("Error: " + ex.Message, "Uh oh!", MessageBoxButtons.OK);
            }
        }
        void ConnectionStatus()
        {
            if(FSUIPCConnection.IsOpen)
            {
                Connect.Hide();
                pictureBox1.Hide();
                button3.Show();
                label1.Hide();
                button7.Show();
                button4.Show();
                button5.Show();
                label2.ForeColor = Color.Green;
                label2.Text = "Connected!";
                button2.Show();
                options1.Hide();
                button6.Show();
                button1.Show();
                userControl11.Show();
            }
        }
        
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void refreshRateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(FSUIPCConnection.IsOpen)
            {

                options1.Hide();
                signIn1.Visible = false;
                serverList1.Hide();
                userControl11.Show();
                lrmDatabase1.Visible = false;
                atcComms1.Visible = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (FSUIPCConnection.IsOpen)
            {
                options1.Show();
                serverList1.Hide();
                signIn1.Visible = false;
                userControl11.Hide();
                lrmDatabase1.Visible = false;
                atcComms1.Visible = false;
            }
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            FSUIPCConnection.Close();
            userControl11.Hide();
            lrmDatabase1.Visible = false;
            signIn1.Visible = false;
            serverList1.Hide();
            atcComms1.Visible = false;
            options1.Hide();
            UserControl1.timerOn = false;
            button2.Hide();
            button4.Hide();
            button3.Hide();
            button5.Hide();
            button6.Hide();
            options1.Hide();
            button1.Hide();
            pictureBox1.Show();
            Connect.Show();
            label1.Show();

            label2.Text = "Not Connected";
            label2.ForeColor = Color.Red;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            options1.Hide();
            userControl11.Hide();
            serverList1.Show();
            lrmDatabase1.Hide();
            signIn1.Visible = false;
            atcComms1.Visible = false;
            lrmDatabase1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            options1.Hide();
            lrmDatabase1.Hide();
            userControl11.Hide();
            serverList1.Hide();
            signIn1.Visible = true;
            atcComms1.Visible = false;
            lrmDatabase1.Visible = false;
        }

        private void signIn1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            options1.Hide();
            userControl11.Hide();
            serverList1.Hide();
            lrmDatabase1.Hide();
            signIn1.Visible = false;
            atcComms1.Visible = true;
            lrmDatabase1.Visible = false;
        }

        private void atcComms1_Load(object sender, EventArgs e)
        {

        }

        private void lrmDatabase1_Load(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            options1.Hide();
            userControl11.Hide();
            serverList1.Hide();
            lrmDatabase1.Show();
            signIn1.Visible = false;
            atcComms1.Visible = false;
            lrmDatabase1.Visible = true;
        }
    }
}
