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
            dateTimePicker1.Value = DateTime.Now.Date.AddDays(0);
            dateTimePicker2.Value = dateTimePicker1.Value.Date;
            dateTimePicker3.Value = DateTime.Now.Date.AddDays(1);
            dateTimePicker4.Value = dateTimePicker3.Value.Date;
            load();
            loaded = true;

        }

        public void load()
        {
            listView1.Items.Clear();

            StringCollection collection = Properties.Settings.Default.cache;
            jsonCollection = collection;
            int goodSeconds = 0;
            //  this.Text = "(" + collection.Count + ")";
            foreach (string task_json in collection)
            {
                Task task = JsonConvert.DeserializeObject<Task>(task_json);

                if(textBox1.Text != "")
                {
                    if (!task.taskName.ToLower().Contains(textBox1.Text.ToLower()))
                    {
                        continue;
                    }
                }
                DateTime sTime = dateTimePicker1.Value.Date;
                sTime.AddSeconds(dateTimePicker2.Value.TimeOfDay.TotalSeconds);

                DateTime eTime = dateTimePicker3.Value.Date;
                eTime.AddSeconds(dateTimePicker4.Value.TimeOfDay.TotalSeconds);

                if(task.startedTime < sTime || task.startedTime >eTime)
                {
                    continue;
                }

                ListViewItem item = new ListViewItem(task.taskName);
                TimeSpan spentTime = (task.endTime - task.startedTime);
                string time = "";
                if (spentTime.TotalHours > 0)
                {
                    time += spentTime.Hours.ToString("n0") + "h ";
                }
                if (spentTime.TotalMinutes > 0) { time += spentTime.Minutes.ToString("n0") + "m "; }
                time += spentTime.Seconds.ToString("n0") + "s";
                item.SubItems.Add(time);
                item.SubItems.Add(String.Format("{0:MM/dd} {0:HH:mm:ss}", task.startedTime));
                item.SubItems.Add(String.Format("{0:MM/dd} {0:HH:mm:ss}", task.endTime));
                item.SubItems.Add(task_json.GetHashCode().ToString());
                item.Checked = task.useful;
                if (task.useful)
                {
                    goodSeconds += (int)(task.startedTime - task.endTime).TotalSeconds;
                }
                listView1.Items.Add(item);
            }
            DateTime startOfDay = DateTime.Now.Date;
            int secondsElapsed = (int)(DateTime.Now - startOfDay).TotalSeconds;
            int usefulPerc = (int)((float)goodSeconds / (float)secondsElapsed * -100f);
            pictureBox2.Height = 379 - (int)((float)usefulPerc / 100f * 379f);
            label4.Text = usefulPerc + " %";
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
                for (int x = 0; x < Properties.Settings.Default.cache.Count; x++)
                {
                    string task_json = Properties.Settings.Default.cache[x];
                    if (task_json.GetHashCode().ToString() == listView1.Items[i].SubItems[4].Text)
                    {
                        //  MessageBox.Show(taskJson);
                        Task task = JsonConvert.DeserializeObject<Task>(task_json);
                        task.useful = listView1.Items[i].Checked;

                        Properties.Settings.Default.cache[x] = JsonConvert.SerializeObject(task);
                        Properties.Settings.Default.Save();
                        break;
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            load();
        }
    }
}
