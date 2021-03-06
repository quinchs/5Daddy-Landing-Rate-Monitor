﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;
using System.Media;

namespace _5Daddy_Landing_Monitor
{
    static class GlobalData
    {
        internal const string Version = "1.0.0";
        internal static bool Offlinemode = false;
        internal static bool LoggedIn = false;
        internal static string Username;
        internal static KeyValuePair<string, string> KPV { get; set; }
        internal static string ErrorLog = Environment.CurrentDirectory + @"\Data\ErrorLog.txt";
        internal static ushort COM1act;
        internal static double Lat;
        internal static SoundPlayer sp = new SoundPlayer($"{Environment.CurrentDirectory}\\Data\\Notification.wav");
        internal static double Long;
        internal static int Heading;
        internal static int speed;
        internal static string Auth;
        internal static LRMServerData CurrentConnectedLRMServer { get; set; }
        internal static string _5DatFile = Environment.CurrentDirectory + @"\Data\5LRM.5DAT";
        internal static string landinglistsJsonPath = Environment.CurrentDirectory + @"\Data\LandingScores.json";
        internal static ushort COM1sby;
        internal static FSUIPC.FsVersion CurrentFlightSim;
        internal static void ErrorLogInput(Exception ex, string errorVal)
        {
            try
            {
                var cur = File.ReadAllText(ErrorLog);
                var cont = cur + "\n-------------" + errorVal + "-------------- \n" + ex + "\n-------------" + errorVal + "-------------- \n";
                File.WriteAllText(ErrorLog, cont);
            }
            catch(Exception exc)
            {
                
            }
        }
        internal static void sendJSONdata(HTTPData data)
        {
            //if(socket != null)
            //{
            //    if (LoggedIn)
            //    {
            //        if (!data.Equals(null))
            //        {
            //            string jsonString = JsonConvert.SerializeObject(data);
            //            byte[] dat = Encoding.ASCII.GetBytes(jsonString);
            //            socket.Send(dat);
            //        }
            //        else
            //            throw new Exception("Data was null!");
            //    }
            //    else
            //        throw new Exception("User not logged in!");
            //}
            //else { throw new Exception("The current server socket was null!", new SocketException()); }
        }
    }
    static class GlobalInstances
    {
        internal static UserControl1 UserControl1 { get; set; }

        internal static ServerList serverlist1 { get; set; }

        internal static SignIn signin1 { get; set; }

        internal static Options options1 { get; set; }

        internal static LRMDatabase LRMDatabase1 { get; set; }

        internal static ATCComms atccomms1 { get; set; }
    }
    public struct LRMServerData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string PlayerCount { get; set; }
        public Uri serverURI { get; set; }
    }

    public struct HTTPData
    {
        public string Header { get; set; }
        public Dictionary<string, string> Body { get; set; }
        public string Auth { get; set; }
    }
    
}
