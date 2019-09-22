using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;

namespace _5Daddy_Landing_Monitor
{
    public partial class Options : UserControl
    {
        public Options()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int rfrshrt = 0;
            try
            {
                if(textBox1.Text != String.Empty)
                {
                    rfrshrt = Convert.ToInt32(textBox1.Text);
                    UserControl1.RefreshRate = rfrshrt;
                }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Invalad Number", "Uh Oh!", MessageBoxButtons.OK);
            }
           
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Options_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(SignIn.memloging)
                {
                    var msgb = MessageBox.Show("Are you sure you want to Forget your Account?", "Are you Sure?", MessageBoxButtons.YesNo);
                    if(msgb == DialogResult.Yes)
                    {
                        var memloginName = SignIn.memloginName;
                        var memloginToken = SignIn.memloginToken;
                        string filecont = File.ReadAllText(Environment.CurrentDirectory + @"\5Daddy Landing Monitor.exe.config");
                        if (Regex.IsMatch(filecont, "<!--" + memloginName + "|" + memloginToken + "-->"))
                        {
                            string newrplc = Regex.Replace(filecont, "<!--" + memloginName + "|" + memloginToken + "-->", string.Empty);
                            File.WriteAllText(Environment.CurrentDirectory + @"\5Daddy Landing Monitor.exe.config", newrplc);
                        }
                        MessageBox.Show("Removed account!");
                    }
                }
            }
            catch(Exception ex)
            {
                GlobalData.ErrorLogInput(ex, "Error");
            }
        }
    }
}
