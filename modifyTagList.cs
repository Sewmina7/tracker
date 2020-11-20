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

namespace TrackerV2
{
    public partial class modifyTagList : Form
    {
        bool positive;
        public modifyTagList(bool _positive)
        {
            InitializeComponent();
            positive = _positive;
            load();
        }

        void load()
        {
            label1.Text = "Modify " + ((positive) ? "Positive" : "Negative") + " Tags";

            listBox1.Items.Clear();

            StringCollection collection = (positive) ? Properties.Settings.Default.positive : Properties.Settings.Default.negative;


            foreach (string tag in collection)
            {
                listBox1.Items.Add(tag);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Enabled = (listBox1.SelectedIndex >= 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string newTag = Prompt.ShowDialog("new tag", "Add a useful tag");
            if(newTag == "") { return; }
            if (positive)
            {
                Properties.Settings.Default.positive.Add(newTag);
            }
            else
            {
                Properties.Settings.Default.negative.Add(newTag);
            }

            Properties.Settings.Default.Save();
            load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public static class Prompt
    {
        public static string ShowDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 130,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };
            Label textLabel = new Label() { Left = 30, Top = 10, Text = text };
            TextBox textBox = new TextBox() { Left = 30, Top = 30, Width = 400 };
            Button confirmation = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 60, DialogResult = DialogResult.OK };
            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
