using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;

namespace _5Daddy_Landing_Monitor
{
    class LRMServer
    {
        private static HttpClient LRMServerClient;
        internal static int ReceiveTimeout = 5000;
        internal static Uri URI;
        internal static bool Connected = false;

        public static async Task<bool> Connect(string rURI)
        {
            try
            {
                LRMServerClient = new HttpClient();
                URI = new Uri(rURI);
                HTTPData data = new HTTPData()
                {
                    Header = "New_LRMClient",
                    Auth = GlobalData.Auth,
                    Body = new Dictionary<string, string>()
                    {
                        {"Simulator", GlobalData.CurrentFlightSim.Simulator.ToString() }
                    }
                };
                string responce = await SendandRecieveHTTPData(data);
                HTTPData reData = JsonConvert.DeserializeObject<HTTPData>(responce);
                if (reData.Header == "Connected")
                {

                    GlobalData.CurrentConnectedLRMServer = new LRMServerData()
                    {
                        Name = reData.Body.FirstOrDefault(x => x.Key == "Name").Value,
                        Type = reData.Body.FirstOrDefault(x => x.Key == "Type").Value,
                        PlayerCount = reData.Body.FirstOrDefault(x => x.Key == "Player_Count").Value,
                        serverURI = URI
                    };
                    Connected = true;
                    return true;
                }
                if (reData.Header == "Server_Error")
                {
                    string reason = reData.Body.FirstOrDefault(x => x.Key == "Reason").Value;
                    MessageBox.Show($"Error connecting to server: {reason}", "Uh Oh!", MessageBoxButtons.OK);
                    Connected = false;
                    return false;
                }
                if(reData.Header == "Invalad")
                {
                    MessageBox.Show(reData.Body.FirstOrDefault(x => x.Key == "Reason").Value, "Uh Oh!", MessageBoxButtons.OK);
                    Connected = false;
                    return false;
                }
                else
                {
                    MessageBox.Show("Error Connecting to server, Unknown Responce", "Uh Oh!", MessageBoxButtons.OK);
                    Connected = false;
                    return false;
                }
            }
            catch
            {
                MessageBox.Show("Error talking to LRM Server!", "Uh Oh!", MessageBoxButtons.OK);
                Connected = false;
                return false;
            }
        }
        public async static Task<string> SendandRecieveHTTPData(HTTPData data)
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
                LRMServerClient.Timeout = TimeSpan.FromMilliseconds(ReceiveTimeout);
                var msg = LRMServerClient.SendAsync(req).Result;
                Stream rstream = await msg.Content.ReadAsStreamAsync();
                byte[] buff = new byte[1024];
                int rec = rstream.Read(buff, 0, Convert.ToInt32(rstream.Length));
                byte[] databuff = new byte[rec];
                Array.Copy(buff, databuff, rec);
                string text = Encoding.ASCII.GetString(databuff);
                return text;
            }
            catch (Exception ex)
            {
                throw new Exception("Could Not Send Data to Master Server!", ex);
            }
        }
    }
}
