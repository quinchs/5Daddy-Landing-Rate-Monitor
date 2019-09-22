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
        private delegate void SafeCallDelegate(string[] addlist, string IP, bool status);
        //string serverListFile = Environment.CurrentDirectory + @"\LRM_ServerList.txt";
        internal static Socket _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        public ServerList()
        {
            InitializeComponent();
            this.VisibleChanged += GetServers;
        }

        private void GetServers(object sender, EventArgs e)
        {
            if(Visible)
            {
                TCPJsonData data = new TCPJsonData();
                data.Header = "Get_Servers";
                data.Auth = GlobalData.Auth;
                byte[] sendBytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data));
                MasterServer.MasterServerSocket.Send(sendBytes);
                int recieved = MasterServer.MasterServerSocket.Receive(new byte[1024]);
                byte[] databuff = new byte[recieved];
                Array.Copy(new byte[1024], databuff, recieved);
                string text = Encoding.ASCII.GetString(databuff);

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
                serverNameIP.Clear();
                comboBox1.Items.Clear();
                dataGridView1.Rows.Clear();
                foreach(Thread thrd in threadList)
                {
                    try
                    {
                        thrd.Abort();
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                if(serverList.Count != 1)
                {
                    dataGridView1.Rows.Add(serverList.Count - 1);
                }
                dataGridView1.Refresh();
                I = 0;
                foreach(var item in serverList)
                {
                    dataGridView1.Rows[I].Cells[0].Value = "Working...";
                    dataGridView1.Rows[I].Cells[1].Value = "Working...";
                    dataGridView1.Rows[I].Cells[2].Value = "Working...";
                    I++;
                    Thread t = new Thread(() => checkStatus(item));
                    threadList.Add(t);
                    t.Start();
                    
                }
                I = 0;
                
            }  
            catch(Exception ex)
            {
                GlobalData.ErrorLogInput(ex, "WARNING");
            }
        }

        private void checkStatus(string IP)
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //192.168.1.64 - Bot pc ip
            //173.176.218.37
            //192.168.2.116
            try
            {

                socket.SendTimeout = 4000;
                socket.ReceiveTimeout = 4000;
                socket.Connect(IP, 7878);
                TCPJsonData jData = new TCPJsonData()
                {
                    Header = "Ping",
                    Body = new Dictionary<string, string>(),
                    Auth = GlobalData.Auth
                };
                GlobalData.sendJSONdata(jData);
                byte[] recieveBuf = new byte[1024];
                int rec = socket.Receive(recieveBuf);
                byte[] data = new byte[rec];
                Array.Copy(recieveBuf, data, rec);
                string[] addlist = Encoding.ASCII.GetString(data).Split('|');
                updataFormItems(addlist, IP, true);
                socket.Close();
            }
            catch (SocketException)
            {
                string[] temp = new string[]
                {
                        "serverdata",
                        IP,
                        "0",
                        "undef"
                };
                updataFormItems(temp, IP, false);
            }
        }

        private void updataFormItems(string[] addlist, string IP, bool status)
        {
            try
            {
                if (comboBox1.InvokeRequired || dataGridView1.InvokeRequired)
                {
                    var d = new SafeCallDelegate(updataFormItems);
                    Invoke(d, new object[] { addlist, IP, status });
                }
                else
                {
                    if (addlist[0] == "serverdata")
                    {
                        if(status)
                        {
                            var dat = new KeyValuePair<string, string>(addlist[1], IP);
                            comboBox1.Items.Add(dat);
                            serverNameIP.Add(dat);
                            comboBox1.DisplayMember = "key";
                            comboBox1.ValueMember = "value";
                        }
                        
                        string stat = "";

                        if (status) { stat = "Online!"; dataGridView1.Rows[I].Cells[2].Style.ForeColor = Color.Green; } else { stat = "Offline!"; dataGridView1.Rows[I].Cells[2].Style.ForeColor = Color.Red; }
                        
                        dataGridView1.Rows[I].Cells[0].Value = addlist[1];
                        dataGridView1.Rows[I].Cells[1].Value = addlist[2];
                        dataGridView1.Rows[I].Cells[2].Value = stat;
                        dataGridView1.Rows[I].Cells[3].Value = addlist[3];
                        dataGridView1.Rows[I].Visible = true;

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
                        Header = "disconnect",
                        Auth = GlobalData.Auth
                    };
                    GlobalData.sendJSONdata(jdata);
                    _clientSocket.Shutdown(SocketShutdown.Both);
                    _clientSocket.Close();
                    if (!_clientSocket.Connected)
                    {
                        label1.Text = "Connected to: None";
                        label1.ForeColor = Color.Red;
                        GlobalData.socket = null;
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
                _clientSocket.Shutdown(SocketShutdown.Both);
                _clientSocket.Close();
                if (!_clientSocket.Connected)
                {
                    label1.Text = "Connected to: None";
                    label1.ForeColor = Color.Red;
                    GlobalData.socket = null;
                    //MessageBox.Show("Disconnected!", "Disconnect message");
                    _clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
                else
                {
                    MessageBox.Show("Failed to Disconnect.. :(", "Uh Oh!");
                }
            }
            
            
        }

        private void ServerList_Load(object sender, EventArgs e)
        {

        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(GlobalData.socket is null)
            {
                try
                {
                    if (comboBox1.SelectedItem != null)
                    {
                        KeyValuePair<string, string> KVP = (KeyValuePair<string, string>)comboBox1.SelectedItem;
                        if (KVP.Value != null)
                        {
                            _clientSocket.Connect(KVP.Value, 7878);
                            GlobalData.socket = _clientSocket;
                            TCPJsonData jdata = new TCPJsonData()
                            {
                                Auth = GlobalData.Auth,
                                Header = "Connected",
                                Body = new Dictionary<string, string>()
                            };
                            jdata.Body.Add("Discord_Username", GlobalData.Username);
                            GlobalData.sendJSONdata(jdata);
                            label1.Text = "Connected to: " + KVP.Key;
                            label1.ForeColor = Color.Green;
                            GlobalData.KPV = KVP;
                            for (int i = 0; i != dataGridView1.Rows.Count; i++)
                            {
                                if ((string)dataGridView1.Rows[i].Cells[0].Value == KVP.Key)
                                {
                                    GlobalData.serverType = (string)dataGridView1.Rows[i].Cells[3].Value;
                                }
                            }
                            //MessageBox.Show("Connected to: " + KVP.Key, "Connected!");
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

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != null)
            {
                if(!serverList.Contains(textBox1.Text))
                {
                    IPAddress tempip;
                    if (IPAddress.TryParse(textBox1.Text, out tempip))
                    {
                        serverList.Add(textBox1.Text);
                        var tmp = File.ReadAllText(serverListFile);
                        var newlst = tmp + textBox1.Text + "\n";
                        File.WriteAllText(serverListFile, newlst);
                        MessageBox.Show("Added " + textBox1.Text + " To Server list!", "Sucess!");
                    }
                    else
                    {
                        MessageBox.Show("Invalad IP", "Uh Oh!");
                    }
                } 
                else { MessageBox.Show("Server already in Server List!", "Uh Oh!"); }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                KeyValuePair<string, string> KVP = (KeyValuePair<string, string>)comboBox1.SelectedItem;
                if (KVP.Value == "24.202.243.232")
                {
                    MessageBox.Show("Cannot Remove Official Servers!", "Uh Oh!");
                }
                else
                {
                    var result = MessageBox.Show("Are you sure you want to remove " + KVP.Key + " From the server list?", "Are you Sure?", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        if (serverList.Contains(KVP.Value)) { serverList.Remove(KVP.Value); MessageBox.Show("Removed " + KVP.Key + "!"); }
                    }
                    if (result == DialogResult.No) { }
                }
                
            }
            else { MessageBox.Show("Error: No server selected!", "Uh Oh!"); }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
