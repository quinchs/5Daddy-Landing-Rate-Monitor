using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using System.Threading;

namespace _5Daddy_Landing_Monitor
{
    public partial class ATCComms : UserControl
    {
        internal static TcpClient client = new TcpClient();
        internal static NetworkStream stream = null;
        public ATCComms()
        {
            InitializeComponent();
            this.VisibleChanged += refresh;
            timer1.Interval = 2000;
        }
        private void listen()
        {
            try
            {
                if (GlobalData.socket != null)
                {
                    client.Connect(GlobalData.KPV.Value, 7878);
                    stream = client.GetStream();
                }
            }
            catch(Exception ex)
            {
                GlobalData.ErrorLogInput(ex, "Error");
            }
        }
        private void updatecoms(Channels data)
        {
            int c = 0;
            foreach (var channel in data.channels)
            {
                dataGridView1.Rows[c].Cells[0].Value = channel.channelName;
                foreach(var user in channel.users)
                {
                    dataGridView1.Rows[c].Cells[1].Value = user.avatarURL;
                    dataGridView1.Rows[c].Cells[2].Value = user.userName + "\n";
                }
                c++;
            }
        }
        private static string[] Recieve()
        {
            byte[] bytes = new byte[2048];
            try
            {
                stream.Read(bytes, 0, bytes.Length);
            }
            catch (System.IO.IOException)
            {
                
            }

            stream.Flush();

            return Encoding.UTF8.GetString(bytes).Replace("\0", "").Split('|');
        }
        private void refresh(object sender, EventArgs e)
        {
            if(Visible)
            {
                timer1.Enabled = true;
                string active = "1" + GlobalData.COM1act.ToString("X");
                string standby = "1" + GlobalData.COM1sby.ToString("X");
                if(active != "10")
                {
                    active = active.Insert(3, ".");
                    standby = standby.Insert(3, ".");
                    label1.Text = $"{active} - {standby}";
                }
                listen();
            }
            else
            {
                timer1.Enabled = false;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(stream != null)
            {
                byte[] dat = Encoding.ASCII.GetBytes("getchannels");
                stream.Write(dat, 0, dat.Length);
                string[] input = Recieve();
                switch (input[0])
                {
                    case "updatecoms":
                        Channels data = JsonConvert.DeserializeObject<Channels>(input[1]);
                        updatecoms(data);
                        break;
                }
            }
        }

        private void ATCComms_Load(object sender, EventArgs e)
        {

        }
    }
    class Channels
    {
        internal List<Channel> channels { get; set; }
    }
    class Channel
    {
        internal string channelName { get; set; }
        internal List<Users> users { get; set; }
    }
    class Users
    {
        internal string userName { get; set; }
        internal string avatarURL { get; set; }
    }
}
