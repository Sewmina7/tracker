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
        bool loaded = false;
        public StringCollection jsonCollection;
        public history()
        {
            InitializeComponent();
            StringCollection collection = Properties.Settings.Default.cache;
            jsonCollection = collection;
          //  this.Text = "(" + collection.Count + ")";
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
                item.Checked = task.useful;
                listView1.Items.Add(item);
            }

            loaded = true;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            reconfigureList();
            this.Close();
        }

        private void history_Load(object sender, EventArgs e)
        {
            this.listView1.CheckBoxes = true;
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            reconfigureList();
        }

        void reconfigureList()
        {
            for (int i = 0; i < listView1.Items.Count; i++)
            {
                Task task = JsonConvert.DeserializeObject<Task>(jsonCollection[i]);

                if (listView1.Items[i].Checked != task.useful)
                {
                    task.useful = listView1.Items[i].Checked;
                    if (jsonCollection[i] == Properties.Settings.Default.cache[i])
                    {
                        Properties.Settings.Default.cache[i] = JsonConvert.SerializeObject(task);
                        Properties.Settings.Default.Save();
                        return;
                    }
                    else
                    {
                        for (int x = 0; x < Properties.Settings.Default.cache.Count; x++)
                        {
                            string task_json = Properties.Settings.Default.cache[x];
                            if (task_json == jsonCollection[i])
                            {
                                Properties.Settings.Default.cache[x] = JsonConvert.SerializeObject(task);
                                Properties.Settings.Default.Save();
                                return;
                                break;
                            }
                        }
                    }

                    MessageBox.Show("NOT FOUND!");
                    MessageBox.Show(jsonCollection[i] + Environment.NewLine + Properties.Settings.Default.cache[i]);
                    MessageBox.Show(jsonCollection[i].GetHashCode() + Environment.NewLine + Properties.Settings.Default.cache[i].GetHashCode());
                }
            }

        }
    }
}
