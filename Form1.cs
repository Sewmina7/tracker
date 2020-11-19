using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TrackerV2
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        string email = "";
        public Form1(string _email)
        {
            Properties.Settings.Default.Reset();
            InitializeComponent();
            email = _email;

            if(Properties.Settings.Default.cache == null)
            {
                Properties.Settings.Default.cache = new StringCollection();
                Properties.Settings.Default.Save();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.TopMost = checkBox1.Checked;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        Task curTask = new Task("Tracker");
        private void update_Tick(object sender, EventArgs e)
        {
            DateTime startOfDay = DateTime.Now.Date;

            progressBar1.Value = (int)((DateTime.Now - startOfDay).TotalSeconds / 86400f*100f);
            label3.Text = progressBar1.Value + " %";
            string curTaskName= GetActiveWindowTitle();
            if(curTaskName == null) { return; }
          /*  if (curTaskName.Contains("-"))
            {
                curTaskName = curTaskName.Substring(curTaskName.LastIndexOf('-'));
            }*/
            double timeSpent = (curTask.endTime - curTask.startedTime).TotalSeconds;
            label2.Text = curTask.taskName +"  :"+ timeSpent;

            if(curTaskName != curTask.taskName)
            {
                addTaskToCache(curTask);
                curTask = new Task(curTaskName);
            }
                
            curTask.endTime = DateTime.Now;            
        }

        public void addTaskToCache(Task task)
        {
            string json = JsonConvert.SerializeObject(task);
            Properties.Settings.Default.cache.Add(json);
            Properties.Settings.Default.Save();
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Has this task been useful to yourself?", "USEFUL?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(result == DialogResult.Yes)
            {
                
            }
            else
            {

            }
            
           // Properties.Settings.Default.cache.Add()
        }

        private void button2_Click(object sender, EventArgs e)
        {
            history his = new history();
            his.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }


    [Serializable]
    public class Task
    {
        public string taskName { get; set; }
        public DateTime startedTime { get; set; }
        public DateTime endTime { get; set; }
        public bool useful { get; set; }
        public Task(string _taskname)
        {
            taskName = _taskname;
            startedTime = DateTime.Now;
            endTime = DateTime.Now;
            useful = false;
        }
    }
}
