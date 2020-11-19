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
    public partial class login : Form
    {
        public login()
        {
            InitializeComponent();
        }

        private void login_Load(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = TrackerV2.Properties.Settings.Default.email;
                textBox2.Text = TrackerV2.Properties.Settings.Default.password;
            }
            catch
            {

            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            register reg = new register(this);
            reg.ShowDialog();
        }

        public void doneRegister(string email, string pass)
        {
            textBox1.Text = email;
            textBox2.Text = pass;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string result = (Web.GetPost("http://xperience419inc.000webhostapp.com/login.php", "email", textBox1.Text, "password", textBox2.Text));
            if (result.ToLower() == "success")
            {
                if (checkBox1.Checked)
                {
                    TrackerV2.Properties.Settings.Default.email = textBox1.Text;
                    TrackerV2.Properties.Settings.Default.password = textBox2.Text;
                    TrackerV2.Properties.Settings.Default.Save();

                    Form1 form = new Form1(textBox1.Text);
                    form.Show();
                    this.Hide();
                }
            }
            else
            {
                MessageBox.Show(result);
            }
        }
    }
}
