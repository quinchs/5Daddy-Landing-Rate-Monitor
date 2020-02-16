using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using System.Diagnostics;

namespace _5Daddy_Landing_Monitor
{
    public partial class SignIn : UserControl
    {
        private delegate void SafeCallDelegate(string text);

        internal static bool memloging = false;
        internal static string memloginName = "";
        internal static string memloginToken = "";
        internal static bool resetLogin = false;
        public SignIn()
        {
            InitializeComponent();
            button2.Hide();
            timer1.Interval = 1000;
            timer1.Enabled = false;
            label4.Text = "Not logged in..";
            label4.ForeColor = Color.Red;
            button2.Hide();
            VisibleChanged += SignIn_VisibleChanged;
        }

        private void SignIn_VisibleChanged(object sender, EventArgs e)
        {

            if (Visible)
            {
                if (GlobalData.Offlinemode == true)
                {
                    //check if connected to a server
                    label5.Show();
                    button1.Hide();
                    button2.Hide();
                    //label1.Hide();
                    label4.Hide();
                    checkBox1.Hide();
                }
                if (GlobalData.Offlinemode == false)
                {
                    //check if connected to a server
                    label5.Hide();
                    button1.Show();
                    button2.Show();
                    //label1.Show();
                    label4.Show();
                    checkBox1.Show();

                }
            }
        }

        private void SignIn_Load(object sender, EventArgs e)
        {
            if (memloging)
            {
                button1.Text = "Sign In";

                checkBox1.Checked = true;
                button2.Hide();
            }
            button2.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!GlobalData.LoggedIn)
            {
                if (!memloging)
                {
                    try
                    {
                        HttpListener l = new HttpListener();
                        l.Prefixes.Add("http://localhost:8080/oauth/");
                        l.Start();
                        Process.Start("https://discordapp.com/api/oauth2/authorize?client_id=602365472905756691&redirect_uri=http%3A%2F%2Flocalhost%3A8080%2Foauth%2F&response_type=code&scope=identify");

                        HttpListenerContext context = l.GetContext();
                        HttpListenerRequest request = context.Request;
                        string id = request.QueryString["code"];
                        HTTPData data = new HTTPData()
                        {
                            Header = "Validate_User",
                            Body = new Dictionary<string, string>()
                            {
                                {"code", id }
                            }
                        };
                        string res = MasterServer.SendandRecieveTCPData(data).Result;

                        var resData = JsonConvert.DeserializeObject<HTTPData>(res);
                        string Discordusername = "";
                        string Auth = "";
                        if (resData.Header == "Authenticated")
                        {
                            Discordusername = resData.Body.FirstOrDefault(x => x.Key == "Discord_Username").Value;
                            Auth = resData.Auth;

                            GlobalData.Username = Discordusername;
                            GlobalData.Auth = Auth;
                            GlobalData.LoggedIn = true;
                        }
                        HttpListenerResponse _1response = context.Response;

                        Stream dummyS = _1response.OutputStream;
                        byte[] buffer = System.Text.Encoding.UTF8.GetBytes($"<h1 style=\"color: #5e9ca0;\"><span style=\"color: #000000;\">Thank you, {GlobalData.Username} you have been Authenticated! you can close this window</span></h1><p><span style=\"color: #000000;\" > ps.Thanks for using the LRM :D </span></p><p> &nbsp;</p>");
                        _1response.ContentLength64 = buffer.Length;
                        dummyS.Write(buffer, 0, buffer.Length);
                        label4.Text = GlobalData.Username.Split('#')[0];
                        label4.ForeColor = Color.Green;
                        button1.Text = "Sign out";
                        l.Stop();
                        l.Close();

                        //var chars = RandomString(10);
                        //MD5 hash = MD5.Create();
                        //hashChars = GetMd5Hash(hash, chars);
                        //Socket socket = GlobalData.socket;
                        //var name = textBox1.Text;
                        ////byte[] buffer = Encoding.ASCII.GetBytes("valadate|" + name + "|" + hashChars + "|" + chars); //send hashed chars for 5bot
                        ////socket.Send(buffer);
                        //TCPJsonData dat = new TCPJsonData()
                        //{
                        //    Header = "Valadate_User",
                        //    Body = new Dictionary<string, string>(),
                        //    Auth = GlobalData.Auth
                        //};
                        //dat.Body.Add("Discord_Name", name);
                        //dat.Body.Add("User_Hash", hashChars);
                        //dat.Body.Add("User_Token", chars);
                        //MasterServer.MasterServerSocket.ReceiveTimeout = -1;
                        //MasterServer.SendTCPData(dat);

                        //textBox2.Text = chars;
                        //_name = name;
                        //_socket = socket;
                        //Thread t = new Thread(awaitRecieve);
                        //t.Start();
                    }
                    catch (Exception ex)
                    {
                        GlobalData.ErrorLogInput(ex, "ERROR");
                        MessageBox.Show("error: " + ex.Message, "Uh Oh!");
                    }
                }
                //if (memloging)
                //{
                //    //if (MasterServer.MasterServerSocket.Connected != false) //check if were connected to a server
                //    {
                //        try
                //        {
                //            TCPJsonData data = new TCPJsonData()
                //            {
                //                Header = "Login_User",
                //                Body = new Dictionary<string, string>()
                //            };
                //            data.Body.Add("Discord_Name", memloginName);
                //            data.Body.Add("User_Hash", memloginToken);
                //            MasterServer.SendandRecieveTCPData(data);
                //            byte[] rBuf = new byte[1024];
                //            int rInt = MasterServer.MasterServerSocket.Receive(rBuf);
                //            byte[] databuff = new byte[rInt];
                //            Array.Copy(rBuf, databuff, rInt);
                //            string text = Encoding.ASCII.GetString(databuff);
                //            TCPJsonData rData = JsonConvert.DeserializeObject<TCPJsonData>(text);
                //            if(rData.Header == "Logged_In")
                //            {
                //                string discordName = rData.Body.FirstOrDefault(x => x.Key == "Discord_Name").Value;
                //                string UserHash = rData.Body.FirstOrDefault(x => x.Key == "User_Hash").Value;
                //                if (rData.Auth != null && UserHash == rData.Auth)
                //                {
                //                    GlobalData.LoggedIn = true;
                //                    GlobalData.Username = discordName;
                //                    GlobalData.Auth = UserHash;
                //                    connected();
                //                }
                //            }
                //            if(rData.Header == "Unknown_Login")
                //            {

                //            }
                //            if(rData.Header == "Server_Error")
                //            {

                //            }
                //        }
                //        catch (Exception x)
                //        {
                //            MessageBox.Show("Error: " + x.Message);
                //        }
                //    }
                //}
            }
            else if (GlobalData.LoggedIn)
            {
                if (!checkBox1.Checked)
                {
                    if (memloging)
                    {
                        string filecont = File.ReadAllText(Environment.CurrentDirectory + @"\5Daddy Landing Monitor.exe.config");
                        if (Regex.IsMatch(filecont, "<!--" + memloginName + "|" + memloginToken + "-->"))
                        {
                            string newrplc = Regex.Replace(filecont, "<!--" + memloginName + "|" + memloginToken + "-->", string.Empty);
                            File.WriteAllText(Environment.CurrentDirectory + @"\5Daddy Landing Monitor.exe.config", newrplc);
                            button1.Text = "Login with Discord";
                        }
                    }
                }

                GlobalData.Username = "";
                GlobalData.LoggedIn = false;
                memloginToken = "";
                memloginName = "";
                memloging = false;
                timer1.Enabled = false;
                label4.Text = "Not logged in..";
                label4.ForeColor = Color.Red;
            }
        }
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void connected()
        {
            setconnect("Logged in!");

            if (checkBox1.Checked)
            {
                LoginDetail detail = new LoginDetail();
                detail.UserName = GlobalData.Username;
                detail.UserToken = GlobalData.Auth;
                string json = JsonConvert.SerializeObject(detail);
                string B64 = Convert.ToBase64String(Encoding.ASCII.GetBytes(json));
                File.WriteAllText(GlobalData._5DatFile, B64);
            }
        }
        public struct LoginDetail
        {
            public string UserName { get; set; }
            public string UserToken { get; set; }
        }
        private void setconnect(string text)
        {
            if (label4.InvokeRequired || button2.InvokeRequired || button2.InvokeRequired)
            {
                var d = new SafeCallDelegate(setconnect);
                Invoke(d, new object[] { text });
            }
            else
            {
                timer1.Enabled = true;
                button2.Show();
                label4.Text = text;
                button1.Text = "Sign In";
                label4.ForeColor = Color.Green;
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
        public void Logout()
        {
            if (!GlobalData.LoggedIn)
            {
                GlobalData.Username = "";
                GlobalData.LoggedIn = false;
                label4.Text = "Not logged in..";
                label4.ForeColor = Color.Red;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (resetLogin)
            {
                Logout();
                resetLogin = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }



        private void SignIn_Load_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}