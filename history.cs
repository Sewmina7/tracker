using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TrackerV2
{
    public partial class history : Form
    {
        public history()
        {
            InitializeComponent();
            StringCollection collection = Properties.Settings.Default.cache;
            this.Text = "(" + collection.Count + ")";
            foreach (string task_json in collection)
            {
                Task task = JsonConvert.DeserializeObject<Task>(task_json);

                ListViewItem item = new ListViewItem(task.taskName);
                TimeSpan spentTime = (task.endTime - task.startedTime);
                string time = "";
                if (spentTime.TotalHours > 0)
                {
                    time += spentTime.Hours.ToString("n0") + "h ";
                }
                if (spentTime.TotalMinutes > 0) { time += spentTime.Minutes.ToString("n0") + "m "; }
                time = spentTime.Seconds.ToString("n0") + "s";
                item.SubItems.Add(time);
                item.SubItems.Add(String.Format("{0:MM/dd} {0:HH:mm:ss}", task.startedTime));
                item.SubItems.Add(String.Format("{0:MM/dd} {0:HH:mm:ss}", task.endTime));

                listView1.Items.Add(item);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
