using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace TrackerV2
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            if (Properties.Settings.Default.positive == null) { Properties.Settings.Default.positive = new StringCollection(); Properties.Settings.Default.Save(); }
            if (Properties.Settings.Default.negative == null) { Properties.Settings.Default.negative = new StringCollection(); Properties.Settings.Default.Save(); }
        }

        private void Settings_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            modifyTagList tagModifier = new modifyTagList(true);
            tagModifier.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            modifyTagList tagModifier = new modifyTagList(false);
            tagModifier.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Properties.Settings.Default.cache = new StringCollection();
            Properties.Settings.Default.Save();
        }
    }
}
