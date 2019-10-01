using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net.Http;
using System.IO;

namespace _5Daddy_Landing_Monitor
{
    class MasterServer
    {
        private static HttpClient msClient = new HttpClient();
        //private static Stream msStream;
        internal static Uri URI = new Uri("http://localhost:8081/"); //master server ip
        
        public async static Task<bool> Connect()
        {
            TCPJsonData data = new TCPJsonData();
            data.Header = "New_Client";
            data.Body = new Dictionary<string, string>();
            data.Body.Add("Version", GlobalData.Version);
            string rcved = "";
            msClient.Timeout = new TimeSpan(50000000);
            try { rcved = await SendandRecieveTCPData(data); }
            catch(Exception)
            {
                var msgb = MessageBox.Show("Error Connecting to Master server, Continue in offline mode?", "Uh Oh!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if(msgb == DialogResult.Yes) { GlobalData.Offlinemode = true; return true; }
            }
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
        public async static Task<string> SendandRecieveTCPData(TCPJsonData data)
        {
            try
            {
                string json = JsonConvert.SerializeObject(data);
                var req = new HttpRequestMessage()
                {
                    Content = new ByteArrayContent(Encoding.ASCII.GetBytes(json)),
                    Method = new HttpMethod("POST"),
                    RequestUri = URI,
                };
                var msg = msClient.SendAsync(req).Result;
                Stream rstream = await msg.Content.ReadAsStreamAsync();
                byte[] buff = new byte[1024];
                int rec = rstream.Read(buff, 0, Convert.ToInt32(rstream.Length));
                byte[] databuff = new byte[rec];
                Array.Copy(buff, databuff, rec);
                string text = Encoding.ASCII.GetString(databuff);
                return text;
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
