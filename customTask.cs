using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrackerV2
{
    public partial class customTask : Form
    {
        public customTask()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length <= 0)
            {
                MessageBox.Show("Please enter a valid task name");
                return;
            }

            Task task = new Task(textBox1.Text, "CustomTask");
            task.useful = (MessageBox.Show("Was this task useful?", "Useful?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes);
            task.startedTime = dateTimePicker1.Value.Date.AddSeconds(dateTimePicker2.Value.TimeOfDay.TotalSeconds);
            task.endTime = dateTimePicker3.Value.Date.AddSeconds(dateTimePicker4.Value.TimeOfDay.TotalSeconds);

            Tracker.addTaskToCache(task);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
