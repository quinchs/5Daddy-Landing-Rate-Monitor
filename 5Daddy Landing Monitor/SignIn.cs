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

namespace _5Daddy_Landing_Monitor
{
    public partial class SignIn : UserControl
    {
        private delegate void SafeCallDelegate(string text);

        private string _name = "";
        private string hashChars = "";
        private Socket _socket = null;
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
                if (GlobalData.socket is null)
                {
                    //check if connected to a server
                    label5.Show();
                    button1.Hide();
                    button2.Hide();
                    label1.Hide();
                    label2.Hide();
                    label3.Hide();
                    label4.Hide();
                    textBox1.Hide();
                    textBox2.Hide();
                    checkBox1.Hide();
                }
                if (GlobalData.socket != null)
                {
                    //check if connected to a server
                    label5.Hide();
                    button1.Show();
                    button2.Show();
                    label1.Show();
                    label2.Show();
                    label3.Show();
                    label4.Show();
                    textBox1.Show();
                    textBox2.Show();
                    checkBox1.Show();
                }
            }
        }

        private void SignIn_Load(object sender, EventArgs e)
        {
            if (memloging)
            {
                button1.Text = "Sign In";
                textBox1.Text = memloginName;
                checkBox1.Checked = true;
            }
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
                    if (textBox1.Text != null)
                    {
                        var username = textBox1.Text;
                        if (Regex.IsMatch(username, ".*#[0-9]{4}"))
                        {
                            if (GlobalData.socket != null)
                            {
                                try
                                {
                                    var chars = RandomString(10);
                                    MD5 hash = MD5.Create();
                                    hashChars = GetMd5Hash(hash, chars);
                                    Socket socket = GlobalData.socket;
                                    var name = textBox1.Text;
                                    //byte[] buffer = Encoding.ASCII.GetBytes("valadate|" + name + "|" + hashChars + "|" + chars); //send hashed chars for 5bot
                                    //socket.Send(buffer);
                                    TCPJsonData dat = new TCPJsonData()
                                    {
                                        Header = "Valadate_User",
                                        Body = new Dictionary<string, string>(),
                                        Auth = GlobalData.Auth
                                    };
                                    dat.Body.Add("Discord_Name", name);
                                    dat.Body.Add("User_Hash", hashChars);
                                    dat.Body.Add("User_Token", chars);
                                    MasterServer.SendTCPData(dat);

                                    textBox2.Text = chars;
                                    _name = name;
                                    _socket = socket;
                                    Thread t = new Thread(awaitRecieve);
                                    t.Start();
                                }
                                catch (Exception ex)
                                {
                                    GlobalData.ErrorLogInput(ex, "ERROR");
                                    MessageBox.Show("error: " + ex.Message, "Uh Oh!");
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalad username", "Uh Oh!");
                        }
                    }
                }
                if (memloging)
                {
                    if (GlobalData.socket != null) //check if were connected to a server
                    {
                        try
                        {
                            Socket socket = GlobalData.socket;
                            _socket = socket;
                            if (memloging && memloginName != "" && memloginToken != "") //check login details
                            {
                                button1.Text = "Working...";
                                byte[] dat = Encoding.ASCII.GetBytes("clientgetacount|" + memloginName + "|" + memloginToken);
                                socket.Send(dat);
                                Thread th = new Thread(awaitLogin);
                                th.Start();
                            }
                        }
                        catch (Exception x)
                        {
                            MessageBox.Show("Error: " + x.Message);
                        }
                    }
                }
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
        internal void awaitLogin()
        {
            try
            {
                byte[] recieveBuf = new byte[1024];
                int rec = _socket.Receive(recieveBuf);
                byte[] data = new byte[rec];
                Array.Copy(recieveBuf, data, rec);
                string[] rcved = Encoding.ASCII.GetString(data).Split('|');
                if (rcved[0] == "returnlogin")
                {
                    if (rcved[1] == memloginName)// check if login is for this user
                    {
                        if (rcved[2] == "true")
                        {
                            //logged in
                            setconnect("Logged in!");
                            GlobalData.LoggedIn = true;
                            GlobalData.Username = memloginName;
                        }
                        if (rcved[2] == "false")
                        {
                            MessageBox.Show("couldn't log in as " + memloginName + ", Invalad Token!");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(SocketException)) { MessageBox.Show("Error: " + ex.Message + ", Connection to server closed!", "Uh Oh!"); GlobalData.ErrorLogInput(ex, "WARNING"); }
                else { GlobalData.ErrorLogInput(ex, "ERROR"); }
            }

        }
        internal void awaitRecieve()
        {
            try
            {
                byte[] recieveBuf = new byte[1024];
                int rec = MasterServer.MasterServerSocket.Receive(recieveBuf);
                byte[] data = new byte[rec];
                Array.Copy(recieveBuf, data, rec);
                TCPJsonData recdata = JsonConvert.DeserializeObject<TCPJsonData>(Encoding.ASCII.GetString(data));
                string discordName = recdata.Body.FirstOrDefault(x => x.Key == "Discord_Name").Value;
                if (recdata.Header == "Verified" && discordName == _name)
                {
                    GlobalData.LoggedIn = true;
                    GlobalData.Username = discordName;
                    GlobalData.Auth = recdata.Auth;
                    connected();
                    //MessageBox.Show("Logged in as: " + _name, "Success!");
                }
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(SocketException)) { MessageBox.Show("Error: " + ex.Message + ", Connection to server closed!", "Uh Oh!"); GlobalData.ErrorLogInput(ex, "WARNING"); }
                else { GlobalData.ErrorLogInput(ex, "ERROR"); }
            }

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private void connected()
        {
            setconnect("Logged in!");

            if (checkBox1.Checked)
            {
                byte[] dat = Encoding.ASCII.GetBytes("clientaddacount|" + _name + "|" + hashChars);
                GlobalData.socket.Send(dat);
                var checkacc = File.ReadAllLines(Environment.CurrentDirectory + @"\5Daddy Landing Monitor.exe.config");
                string curr = "";
                foreach (var line in checkacc)
                {
                    if (line != "<!--" + _name + "|" + hashChars + "-->")
                    {
                        curr = curr + line + "\n";
                    }
                }
                if (hashChars == "")
                {
                    MessageBox.Show("Error Saving Login!", "Uh oh!");
                    return;
                }
                string rplc = "<!--" + _name + "|" + hashChars + "-->";
                string newdata = curr + "\n" + rplc;
                File.WriteAllText(Environment.CurrentDirectory + @"\5Daddy Landing Monitor.exe.config", newdata);
            }
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
            if (GlobalData.LoggedIn)
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
    }
}