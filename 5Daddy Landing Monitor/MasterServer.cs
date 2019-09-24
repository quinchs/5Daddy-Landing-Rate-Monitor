using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace _5Daddy_Landing_Monitor
{
    class MasterServer
    {
        internal static Socket MasterServerSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
        internal const string IP = "192.168.2.116"; //master server ip
        
        public static bool Connect()
        {
            try
            {
                MasterServerSocket.Connect(IP, 7979);
            }
            catch(SocketException ex)
            {
                var msgbox = MessageBox.Show("Could not connect to Master server, Would you like to use offline mode?", "Uh Oh!", MessageBoxButtons.YesNo);
                if (msgbox == DialogResult.Yes)
                {
                    GlobalData.Offlinemode = true;
                    return true;
                }
                else { return false; }
               
            }
            TCPJsonData data = new TCPJsonData();
            data.Header = "New_Client";
            data.Body = new Dictionary<string, string>();
            data.Body.Add("Version", GlobalData.Version);
            byte[] contactBytes = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(data));
            MasterServerSocket.Send(contactBytes);
            byte[] recieveBuffer = new byte[1024];
            var ints = MasterServerSocket.Receive(recieveBuffer);
            byte[] recdata = new byte[ints];
            Array.Copy(recieveBuffer, recdata, ints);
            string rcved = Encoding.ASCII.GetString(recdata);
            TCPJsonData recievedData = JsonConvert.DeserializeObject<TCPJsonData>(rcved);
            if (recievedData.Header == "Good_Version")
            {
                //good
                return true;
            }
            if(recievedData.Header == "Bad_Version")
            {
                //bad
                MessageBox.Show("Client out of date. Please install the new version at https://www.5fsx.com/lrm/ to access Online mode!, you are now in Offline Mode!", "Uh Oh!", MessageBoxButtons.OK);
                GlobalData.Offlinemode = true;
                return true;

            }
            else
            {
                var m = MessageBox.Show("Unknown Responce From Master Server, Would you like to play in offline mode?", "Uh Oh!", MessageBoxButtons.YesNo);
                if(m == DialogResult.Yes)
                {
                    GlobalData.Offlinemode = true;
                    return true;
                }
                else { return false; }
                    
                //unknown responce
            }

        }
        public static void SendTCPData(TCPJsonData data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);
                byte[] sendingBytes = Encoding.ASCII.GetBytes(json);
                MasterServerSocket.Send(sendingBytes);
            }
            catch(SocketException ex)
            {
                throw new Exception("Could Not Send Data to Master Server!", ex);
            }
        }
        public struct LRMServerClientListTCP
        {
            public string Header { get; set; }
            public List<ServerList.LRMServer> Body { get; set; }
            public string Auth { get; set; }
        }
    }
}
