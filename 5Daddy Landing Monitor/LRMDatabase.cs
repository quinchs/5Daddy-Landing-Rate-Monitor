using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

namespace _5Daddy_Landing_Monitor
{
    public partial class LRMDatabase : UserControl
    {
        internal static int rownum = 0;
        static DataGridView dgv;
        public struct LandingStatList
        {
            public List<LandingStats> LandingStatslist { get; set; }
        }
        public LRMDatabase()
        {
            InitializeComponent();
            dgv = dataGridView1;
        }

        private void LRMDatabasecs_Load(object sender, EventArgs e)
        {
            //handle loading the json
            string scorefile = GlobalData.landinglistsJsonPath;
            try
            {
                var dat = JsonConvert.DeserializeObject<LandingStatList>(File.ReadAllText(scorefile));
                
                foreach (var item in dat.LandingStatslist)
                {
                    dgv.Rows.Add();
                    dgv.Rows[rownum].Cells[0].Value = item.Date;
                    dgv.Rows[rownum].Cells[1].Value = item.Score;
                    dgv.Rows[rownum].Cells[2].Value = item.FPM;
                    dgv.Rows[rownum].Cells[3].Value = item.Speed;
                    dgv.Rows[rownum].Cells[4].Value = item.Pitch;
                    dgv.Rows[rownum].Cells[5].Value = item.Roll;
                    dgv.Rows[rownum].Cells[6].Value = item.WindSpeed;
                    dgv.Rows[rownum].Cells[7].Value = item.WindDirection;
                    dgv.Refresh();
                    rownum++;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show($"It Fockin Broke M8: {ex}", "Darn M8");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        internal static void AddStat(LandingStats stats)
        {
            dgv.Rows.Add();
            dgv.Rows[rownum].Cells[0].Value = stats.Date;
            dgv.Rows[rownum].Cells[1].Value = stats.Score;
            dgv.Rows[rownum].Cells[2].Value = stats.FPM;
            dgv.Rows[rownum].Cells[3].Value = stats.Speed;
            dgv.Rows[rownum].Cells[4].Value = stats.Pitch;
            dgv.Rows[rownum].Cells[5].Value = stats.Roll;
            dgv.Rows[rownum].Cells[6].Value = stats.WindSpeed;
            dgv.Rows[rownum].Cells[7].Value = stats.WindDirection;
            dgv.Refresh();
            //checkEmptyRow();
            SaveScores(stats);
            rownum++;
        }
        private static void checkEmptyRow()
        {
            for (int i = 0; i != dgv.Rows.Count; i++)
            {
                if(dgv.Rows[i].Cells[0].Value == null)
                {
                    dgv.Rows.Remove(dgv.Rows[i]);
                    rownum = rownum - 1;
                }
            }
        }
        private static void SaveScores(LandingStats stats)
        {
            //load the LandingStats
            string scorefile = GlobalData.landinglistsJsonPath;
            if (!File.Exists(scorefile)) { File.Create(scorefile).Close(); }
            try
            {
                var dat = JsonConvert.DeserializeObject<LandingStatList>(File.ReadAllText(scorefile));
                dat.LandingStatslist.Add(stats);
                string json = JsonConvert.SerializeObject(dat, Formatting.Indented);
                File.WriteAllText(scorefile, json);
            }
            catch
            {
                LandingStatList l = new LandingStatList
                {
                    LandingStatslist = new List<LandingStats>()
                };
                l.LandingStatslist.Add(stats);
                string json = JsonConvert.SerializeObject(l, Formatting.Indented);
                File.WriteAllText(scorefile, json);
            }
        }
    }
    public struct LandingStats
    {
        public string Date { get; set; }

        public string Speed { get; set; }

        public string FPM { get; set; }

        public string Pitch { get; set; }

        public string Roll { get; set; }

        public string WindSpeed { get; set; }

        public string WindDirection { get; set; }

        public string Score { get; set; }
    }
}
