using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using FSUIPC;
using System.IO;

namespace _5Daddy_Landing_Monitor
{
    public partial class UserControl1 : UserControl
    {

        private static Offset<uint> airspeed = new Offset<uint>(0x02BC);             // 4-byte offset - Unsigned integer 
        private static Offset<int> verticalSpeed = new Offset<int>(0x02C8);          // 4-byte offset - Signed integer 
        private Offset<ushort> onGround = new Offset<ushort>(0x0366);
        private Offset<ushort> COM1act = new Offset<ushort>(0x034E);  // 2-byte offset - Unsigned short
        private Offset<ushort> COM1sby = new Offset<ushort>(0x311A);
        private Offset<int> windSpeed = new Offset<int>(0x04BA);
        private Offset<int> windDirection = new Offset<int>(0x04DA);  //*360/65536 for heading
        private Offset<int> pitch = new Offset<int>(0x0578);
        private Offset<string> aircraftType = new Offset<string>(0x3D00, 256);
        private Offset<string> aircraftID = new Offset<string>(0x313C, 12);
        private Offset<int> roll = new Offset<int>(0x057C);
        public static int RefreshRate = 50;
        private static int checkGroundTime = 0;
        public static bool timerOn = false;
        internal string airspd = "";
        internal int tmp = 0;
        internal bool rptOnGround = true;
        internal string plnbnk = "";
        internal string windSpd = "";
        internal static bool rptask = true;
        internal string windHdg = "";
        internal string ATCID = "";
        internal string ATCTYPE = "";

        bool ongrnd = true;

        public UserControl1()
        {
            InitializeComponent();
            checkGroundTime = 150;
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            if (FSUIPCConnection.IsOpen)
            {
                timerOn = true;
                checkBox1.Hide();
                timer1.Enabled = timerOn;
                timer1.Interval = RefreshRate;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateScreenData(); 
            if(checkGroundTime >= 150)
            {
                if (FSUIPCConnection.IsOpen)
                {
                    try
                    {
                        FSUIPCConnection.Process();
                        if (GlobalData.COM1act != COM1act.Value) { GlobalData.COM1act = COM1act.Value; }
                        if (GlobalData.COM1sby != COM1sby.Value) { GlobalData.COM1sby = COM1sby.Value; }
                        int fpm = 0;
                        if (!ongrnd)
                        {
                            double verticalSpeedMPS = verticalSpeed.Value / 256d;
                            double verticalSpeedFPM = verticalSpeedMPS * 60d * 3.28084d;
                            var vsFPM = Convert.ToInt32(verticalSpeedFPM);
                            ATCID = aircraftID.Value;
                            ATCTYPE = aircraftType.Value;
                            label10.Text = $"({ATCID} - {ATCTYPE})";
                            fpm = vsFPM;
                            textBox1.Text = vsFPM.ToString();
                            timer1.Enabled = timerOn;
                            double airspeedKnots = (double)airspeed.Value / 128d;
                            airspd = Convert.ToInt32(airspeedKnots).ToString();
                            textBox4.Text = airspd;
                            var plnPitch = (double)pitch.Value * 360 / 4294967296 * -1;

                            WeatherServices ws = FSUIPCConnection.WeatherServices;
                            FsWeather weather = ws.GetWeatherAtAircraft();

                            if (weather.WindLayers.Count > 0)
                            {
                                FsWindLayer windLayer = weather.WindLayers[0];
                                windHdg = windLayer.Direction.ToString("000");
                                windSpd = windLayer.SpeedKnots.ToString("F0");
                                textBox5.Text = windLayer.SpeedKnots.ToString("F0");
                                textBox6.Text = windLayer.Direction.ToString("000");
                            }

                            tmp = Convert.ToInt32(plnPitch);
                            if (tmp >= 0)
                            {
                                textBox2.Text = tmp.ToString() + "▲";
                            }
                            if (tmp < 0)
                            {
                                tmp = tmp * -1;
                                textBox2.Text = tmp.ToString() + "▼";
                            }

                            var plnBank = (double)roll.Value * 360 / 4294967296;
                            var planebnk = "";
                            if (plnBank >= 0)
                            {
                                planebnk = Convert.ToInt32(plnBank).ToString() + "L";
                            }
                            if (plnBank < 0)
                            {
                                planebnk = Convert.ToInt32(plnBank * -1).ToString() + "R";
                            }
                            plnbnk = planebnk;
                            textBox3.Text = planebnk;
                            timer1.Interval = RefreshRate;

                        }


                        string onGroundText = this.onGround.Value > 0 ? "Y" : "N";

                        if (onGroundText == "Y")
                        {
                            checkGroundTime = 0;
                            if (GlobalData.LoggedIn)
                            {
                                if (rptask)
                                {
                                    checkBox1.Show();
                                    rptask = false;
                                }
                            }
                            if (!GlobalData.LoggedIn)
                            {
                                if (rptask)
                                {
                                    checkBox1.Hide();
                                    rptask = true;
                                }
                            }

                            if (rptOnGround)
                            {
                                if (checkBox1.Checked)
                                {
                                    SendData(fpm.ToString(), airspd, tmp.ToString(), plnbnk, windSpd, windHdg, ATCID, ATCTYPE);
                                }

                                rptOnGround = false;

                                label8.Text = "Landed!";
                                ongrnd = true;

                                fpmstringscore(fpm);
                                if(textBox7.Text != "")
                                {
                                    LandingStats ls = new LandingStats();
                                    ls.FPM = fpm.ToString();
                                    ls.Speed = airspd;
                                    ls.Score = textBox7.Text;
                                    ls.Roll = plnbnk;
                                    ls.Pitch = textBox2.Text;
                                    ls.WindSpeed = windSpd;
                                    ls.WindDirection = windHdg;
                                    LRMDatabase.AddStat(ls);
                                }
                            }
                        }
                        if (onGroundText == "N")
                        {
                            label8.Text = "In Air";
                            ongrnd = false;
                            rptOnGround = true;
                        }
                    }
                    catch (FSUIPCException ex)
                    {
                        FSUIPCConnection.Close();
                        timer1.Enabled = false;
                        GlobalData.ErrorLogInput(ex, "ERROR");
                        MessageBox.Show("Error, Cannot read game data: " + ex.Message + ", please Disconnect");
                        return;

                    }
                }
            }
            else
            {
                checkGroundTime++;
            }
        }
        private void fpmstringscore(int fpm)
        {
            if (fpm <= -1500)
            {
                textBox7.Text = "DEAD!";
                return;
            }
            if (fpm <= -700)
            {
                textBox7.Text = "1/10!";
                return;
            }
            if (fpm <= -500)
            {
                textBox7.Text = "Need repair!";
                return;
            }
            if (fpm <= -300)
            {
                textBox7.Text = "Ouch!";
                return;
            }
            if (fpm <= -200)
            {
                textBox7.Text = "Harsh!";
                return;
            }
            if (fpm <= -175)
            {
                textBox7.Text = "Nice!";
                return;
            }
            if (fpm <= -100)
            {
                textBox7.Text = "Smooth!";
                return;
            }
            if (fpm <= -50)
            {
                textBox7.Text = "Butter!";
                return;
            }
        }
        private void SendData(string fpm, string airspd, string plnPitch, string pnlBank, string windSpeed, string windHeading, string aircrftType, string aircraftID)
        {
            try
            {
                if (GlobalData.LoggedIn && GlobalData.CurrentConnectedLRMServer.Type == "LRMComp")
                {
                    TCPJsonData data = new TCPJsonData()
                    {
                        Auth = GlobalData.Auth,
                        Header = "Landing_Data",
                        Body = new Dictionary<string, string>()
                        {
                            {"Fpm", fpm },
                            {"Air_Speed", airspd },
                            {"Plane_Pitch", plnPitch },
                            {"Plane_Bank", pnlBank},
                            {"Wind_Speed", windSpeed },
                            {"Wind_Heading", windHeading },
                            {"Aircraft_Type", aircrftType  },
                            {"Aircraft_ID", aircraftID }
                        }
                    };
                    //Send to master server
                }
            }
            catch(Exception ex)
            {
                GlobalData.ErrorLogInput(ex, "WARNING");
            }
        }

        //double verticalSpeedMPS = (double)verticalSpeed.Value / 256d;

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void updat()
        {

        }
        public void UpdateScreenData()
        {
            if (GlobalData.LoggedIn)
            {
                if(GlobalData.KPV.ToString() != null)

                checkBox1.Show();
                checkBox1.Text = "Send to " + GlobalData.KPV.Key + " LRM Server";
            }
            if (!GlobalData.LoggedIn)
            {
                checkBox1.Hide();
            }
        }
        private void label10_Click(object sender, EventArgs e)
        {

        }
        
    }
}
