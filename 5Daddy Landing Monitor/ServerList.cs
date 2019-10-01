using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Newtonsoft.Json;

namespace _5Daddy_Landing_Monitor
{
    public partial class ServerList : UserControl
    {
        List<LRMServer> serverList = new List<LRMServer>();
        public List<Thread> threadList = new List<Thread>(); 
        List<KeyValuePair<string, string>> serverNameIP = new List<KeyValuePair<string, string>>();
        int I = 0;
        private delegate void SafeCallDelegate();
        //string serverListFile = Environment.CurrentDirectory + @"\LRM_ServerList.txt";
        internal static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public ServerList()
        {
            InitializeComponent();
            this.VisibleChanged += GetServers;
            label5.Hide();
        }

        private void GetServers(object sender, EventArgs e)
        {
            if(GlobalData.Offlinemode)
            {
                label1.Hide();
                label5.Show();
                dataGridView1.Hide();
                button1.Hide();
                button2.Hide();
                button3.Hide();
                comboBox1.Hide();
                label2.Hide();
            }
            else
            {
                if (Visible)
                {
                    label2.Hide();
                    TCPJsonData data = new TCPJsonData();
                    data.Header = "Get_Servers";
                    data.Auth = GlobalData.Auth;
                    byte[] sendBytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data));
                    //MasterServer.MasterServerSocket.ReceiveTimeout = 5000;
                    try { MasterServer.SendandRecieveTCPData(data); }
                    catch(Exception ex) { MessageBox.Show("Could not Refresh sevrers. The Master Didnt Respond!", "Uh Oh!", MessageBoxButtons.OK, MessageBoxIcon.Error); return; }
                    byte[] buffer = new byte[1024];
                    int Recievebuf = 0; //MasterServer.MasterServerSocket.Receive(buffer);
                    byte[] databuff = new byte[Recievebuf];
                    Array.Copy(buffer, databuff, Recievebuf);
                    string text = Encoding.ASCII.GetString(databuff);
                    
                    if (text.Contains("Header\":\"Server_Error"))
                    {
                        if(GlobalData.Auth == "")
                        {
                            label2.Show();
                        }
                        else
                        {
                            TCPJsonData recdata = JsonConvert.DeserializeObject<TCPJsonData>(text);
                            MessageBox.Show($"Invalad Request! Server Responce:\n{recdata.Body.FirstOrDefault(x => x.Key == "Reason").Value}", "Uh Oh!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            GlobalData.ErrorLogInput(new Exception("Invalad Authentication Token!"), "ERROR");
                        }
                    }
                    if (text.Contains("Header\":\"LRMServers_List"))
                    {
                        label2.Hide();
                        MasterServer.LRMServerClientListTCP serverListData = JsonConvert.DeserializeObject<MasterServer.LRMServerClientListTCP>(text);
                        if (serverListData.Auth == GlobalData.Auth)
                        {
                            serverList = serverListData.Body;
                            updataFormItems();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unknown Server Responce!", "Uh Oh!");
                    }
                }
            }
        }

        public struct LRMServer
        {
            public string Name { get; set; }
            public string Type { get; set; }
            public int CurrentUsers { get; set; }
            public string IP { get; set; }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                TCPJsonData data = new TCPJsonData()
                {
                    Auth = GlobalData.Auth,
                    Header = "Get_Servers",
                    Body = new Dictionary<string, string>()
                };

                try { MasterServer.SendandRecieveTCPData(data); }
                catch(Exception ex) { GlobalData.ErrorLogInput(ex, "ERROR"); }
                byte[] buffer = new byte[1024];
                int Recievebuf = 0;// MasterServer.MasterServerSocket.Receive(buffer);
                byte[] databuff = new byte[Recievebuf];
                Array.Copy(buffer, databuff, Recievebuf);
                string text = Encoding.ASCII.GetString(databuff);
                //var header = JsonConvert.DeserializeObject<Dictionary<string, string>>(text).FirstOrDefault(x => x.Key == "Header").Value;
                if (text.Contains("Header\":\"Server_Error"))
                {
                    if (GlobalData.Auth == "")
                    {
                        label2.Show();
                    }
                    else
                    {
                        TCPJsonData recdata = JsonConvert.DeserializeObject<TCPJsonData>(text);
                        MessageBox.Show($"Invalad Request! Server Responce:\n{recdata.Body.FirstOrDefault(x => x.Key == "Reason").Value}", "Uh Oh!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        GlobalData.ErrorLogInput(new Exception("Invalad Authentication Token!"), "ERROR");
                    }
                }
                if (text.Contains("Header\":\"LRMServers_List"))
                {
                    label2.Hide();
                    MasterServer.LRMServerClientListTCP serverListData = JsonConvert.DeserializeObject<MasterServer.LRMServerClientListTCP>(text);
                    if(serverListData.Auth == GlobalData.Auth)
                    {
                        serverList = serverListData.Body;
                        updataFormItems();
                    }
                }
                else
                {
                    MessageBox.Show("Unknown Server Responce!", "Uh Oh!");
                }
            }  
            catch(Exception ex)
            {
                GlobalData.ErrorLogInput(ex, "WARNING");
            }
        }
        
        private void updataFormItems()
        {
            try
            {
                if (comboBox1.InvokeRequired || dataGridView1.InvokeRequired)
                {
                    var d = new SafeCallDelegate(updataFormItems);
                    Invoke(d, new object[] { });
                }
                else
                {
                    I = 0;
                    dataGridView1.Rows.Clear();
                    comboBox1.Items.Clear();
                    foreach(var server in serverList)
                    {
                        var dat = new KeyValuePair<string, string>(server.Name, server.IP);
                        comboBox1.Items.Add(dat);
                        serverNameIP.Add(dat);
                        comboBox1.DisplayMember = "key";
                        comboBox1.ValueMember = "value";
                        string stat = "Online!";

                        dataGridView1.Rows[I].Cells[2].Style.ForeColor = Color.Green;

                        dataGridView1.Rows[I].Cells[0].Value = server.Name;
                        dataGridView1.Rows[I].Cells[1].Value = server.CurrentUsers;
                        dataGridView1.Rows[I].Cells[2].Value = stat;
                        dataGridView1.Rows[I].Cells[3].Value = server.Type;
                        dataGridView1.Rows[I].Visible = true;
                        dataGridView1.Rows.Add();

                        I++;
                    }
                }
            }
            catch(Exception ex)
            {
                GlobalData.ErrorLogInput(ex, "WARNING");
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            var name = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            var kvp = serverNameIP.FirstOrDefault(a => a.Key == name.ToString() );

            if(!comboBox1.Items.Contains(kvp))
            {
                var newkvp = new KeyValuePair<string, string>(name.ToString(), name.ToString());
                comboBox1.Items.Add(newkvp); //item
                comboBox1.SelectedItem = newkvp;
            }
            else
            {
                comboBox1.SelectedItem = name;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (_clientSocket.Connected)
                {
                    TCPJsonData jdata = new TCPJsonData()
                    {
                        Body = new Dictionary<string, string>(),
                        Header = "Disconnect",
                        Auth = GlobalData.Auth
                    };
                    GlobalData.sendJSONdata(jdata);
                    _clientSocket.Shutdown(SocketShutdown.Both);
                    _clientSocket.Close();
                    if (!_clientSocket.Connected)
                    {
                        label1.Text = "Connected to: None";
                        label1.ForeColor = Color.Red;
                        //GlobalData.socket = null;
                        //MessageBox.Show("Disconnected!", "Disconnect message");
                        _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    }
                    else
                    {
                        MessageBox.Show("Failed to Disconnect.. :(", "Uh Oh!");
                    }
                }
                else
                {
                    _clientSocket.Shutdown(SocketShutdown.Both);
                    _clientSocket.Close();
                }
            }
            catch(Exception ex)
            {
                //_clientSocket.Shutdown(SocketShutdown.Both);
                //_clientSocket.Close();
                //if (!_clientSocket.Connected)
                //{
                //    label1.Text = "Connected to: None";
                //    label1.ForeColor = Color.Red;
                //    GlobalData.socket = null;
                //    //MessageBox.Show("Disconnected!", "Disconnect message");
                //    _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                //}
                //else
                //{
                //    MessageBox.Show("Failed to Disconnect.. :(", "Uh Oh!");
                //}
            }
        }

        private void ServerList_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            //if(GlobalData.socket is null)
            {
                try
                {
                    if (comboBox1.SelectedItem != null)
                    {
                        KeyValuePair<string, string> KVP = (KeyValuePair<string, string>)comboBox1.SelectedItem;
                        if (KVP.Value != null)
                        {
                            _clientSocket.Connect(KVP.Value, 7878);
                            TCPJsonData jdata = new TCPJsonData()
                            {
                                Auth = GlobalData.Auth,
                                Header = "Connect",
                                Body = new Dictionary<string, string>()
                            };
                            jdata.Body.Add("Discord_Username", GlobalData.Username);
                            GlobalData.sendJSONdata(jdata);
                            label1.Text = "Connected to: " + KVP.Key;
                            label1.ForeColor = Color.Green;
                            _clientSocket.ReceiveTimeout = 5000;
                            byte[] ResponceBuff = new byte[1024];
                            int lng = 0;
                            try { lng = _clientSocket.Receive(ResponceBuff); }
                            catch (Exception ex) { MessageBox.Show("Server Timed out!, Disconnecting!", "Uh Oh!", MessageBoxButtons.OK); GlobalData.ErrorLogInput(ex, "Sever Time Out!"); _clientSocket.Disconnect(true); return; }
                            byte[] recBytes = new byte[lng];
                            Array.Copy(ResponceBuff, recBytes, lng);
                            string responce = Encoding.ASCII.GetString(recBytes);
                            TCPJsonData responceData = JsonConvert.DeserializeObject<TCPJsonData>(responce);
                            if (responceData.Auth == GlobalData.Auth)
                            {
                                if (responceData.Header == "Success_Connect")
                                {
                                    LRMServerData serv = new LRMServerData()
                                    {
                                        Name = responceData.Body.FirstOrDefault(x => x.Key == "Name").Value,
                                        Type = responceData.Body.FirstOrDefault(x => x.Key == "Type").Value,
                                        PlayerCount = responceData.Body.FirstOrDefault(x => x.Key == "Players").Value,
                                        serverSocket = _clientSocket,
                                    };
                                }
                                else { MessageBox.Show("Server Responded with an Invalad Responce!", "Uh Oh!", MessageBoxButtons.OK); }
                            }
                            else
                            {
                                MessageBox.Show("Server Responded with an Invalad Responce!", "Uh Oh!", MessageBoxButtons.OK);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    GlobalData.ErrorLogInput(ex, "ERROR");
                    MessageBox.Show("Error: " + ex.Message, "Uh Oh!");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }
    }
}
