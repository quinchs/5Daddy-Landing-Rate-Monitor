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
using Newtonsoft.Json;
using MetroFramework;
using MetroFramework.Forms;
using static _5Daddy_Landing_Monitor.SignIn;
using System.Threading;

namespace _5Daddy_Landing_Monitor
{
    public partial class Form1 : MetroForm
    {
        
        public Form1()
        {
            InitializeComponent();

            userControl11.Hide();
            button2.Hide();
            button4.Hide();
            button6.Hide();
            button5.Hide();
            button7.Hide();
            button3.Hide();
            signIn1.Visible = false;
            label3.Visible = false;
            serverList1.Hide();
            options1.Hide();
           
            button1.Hide();
            atcComms1.Visible = false;
            lrmDatabase1.Visible = false;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(GlobalData.landinglistsJsonPath)) { File.Create(GlobalData.landinglistsJsonPath).Close(); }
            if (!File.Exists(GlobalData.ErrorLog)) { File.Create(GlobalData.ErrorLog).Close(); }

            //handles memory login
            if (!File.Exists(GlobalData._5DatFile)) { File.Create(GlobalData._5DatFile).Close(); }
            string dat = File.ReadAllText(GlobalData._5DatFile);
            if (dat != "")
            {
                string dB64 = "";
                try { dB64 = Encoding.ASCII.GetString(Convert.FromBase64String(dat)); }
                catch(Exception ex) { GlobalData.ErrorLogInput(ex, "ERROR"); return; }
                try
                {
                    LoginDetail detail = JsonConvert.DeserializeObject<LoginDetail>(dB64);
                    memloging = true;
                    memloginName = detail.UserName;
                    memloginToken = detail.UserToken;
                }
                catch(Exception ex) { GlobalData.ErrorLogInput(ex, "ERROR"); return; }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(ModifierKeys.HasFlag(Keys.Control))
            {
                ConnectionStatus(true);
            }
            else
            {
                try
                {
                    FSUIPCConnection.Open(FlightSim.Any);
                    
                    ConnectionStatus();
                }
                catch (FSUIPCException ex)
                {
                    GlobalData.ErrorLogInput(ex, "WARNING");
                    MessageBox.Show("Error: " + ex.Message, "Uh oh!", MessageBoxButtons.OK);
                }
            }
        }
        void ConnectionStatus(bool dev = false)
        {
            if(FSUIPCConnection.IsOpen || dev)
            {
                bool stat = true;
                try { stat = MasterServer.Connect().Result; }
                catch (Exception ex) { }
                if (stat)
                {
                    GlobalData.CurrentFlightSim = FSUIPCConnection.FlightSimVersionConnected;
                    Connect.Hide();
                    pictureBox1.Hide();
                    button3.Show();
                    label1.Hide();
                    button7.Show();
                    button4.Show();
                    button5.Show();
                    
                    label3.Visible = true;
                    label3.ForeColor = Color.Green;
                    label3.Text = "Connected!";
                    button2.Show();
                    options1.Hide();
                    button6.Show();
                    button1.Show();
                    userControl11.Show();
                    Thread t = new Thread(HandlePos);
                    t.Start();
                }
            }
        }

        private void HandlePos()
        {
            System.Timers.Timer timer1 = new System.Timers.Timer()
            {
                Interval = 15000,
                Enabled = true
            };
            timer1.Elapsed += GetandSendPosData;
            
        }

        private async void GetandSendPosData(object sender, EventArgs e)
        {
            try
            {
                if (!GlobalData.Offlinemode)
                {
                    HTTPData data = new HTTPData();
                    data.Header = "Position_Data";
                    data.Auth = GlobalData.Auth;
                    data.Body = new Dictionary<string, string>()
                {
                    {"Latitude", GlobalData.Lat.ToString() },
                    {"Longitude", GlobalData.Long.ToString()},
                    {"Speed", GlobalData.speed.ToString() },
                    {"Heading", GlobalData.Heading.ToString() }
                };
                    await MasterServer.SendandRecieveTCPData(data);
                }
            }
            catch { }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void refreshRateToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        
        private void button3_Click(object sender, EventArgs e)
        {
            FSUIPCConnection.Close();
            UserControl1.timerOn = false;
            userControl11.Hide();
            button2.Hide();
            button4.Hide();
            button6.Hide();
            button5.Hide();
            button7.Hide();
            button3.Hide();
            signIn1.Visible = false;
            pictureBox1.Show();
            label3.Visible = false;
            Connect.Show();
            label1.Show();
            serverList1.Hide();
            options1.Hide();
            button1.Hide();
            atcComms1.Visible = false;
            lrmDatabase1.Visible = false;

            label3.Text = "Not Connected";
            label3.ForeColor = Color.Red;
            
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

        private void button1_Click_2(object sender, EventArgs e)
        {
            options1.Show();
            serverList1.Hide();
            signIn1.Visible = false;
            userControl11.Hide();
            lrmDatabase1.Visible = false;
            atcComms1.Visible = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            options1.Hide();
            signIn1.Visible = false;
            serverList1.Hide();
            userControl11.Show();
            lrmDatabase1.Visible = false;
            atcComms1.Visible = false;
        }
        
    }
}
