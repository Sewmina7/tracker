using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace TrackerV2
{

    public partial class register : Form
    {
        login loginForm;
        public register(login _loginForm)
        {
            InitializeComponent();
            loginForm = _loginForm;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != textBox4.Text)
            {
                MessageBox.Show("Password doesn't match", "Couldn't proceed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (textBox3.Text.Length < 6)
            {
                MessageBox.Show("Please enter a password that has more than 6 characters", "Couldn't proceed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!textBox1.Text.Contains("@"))
            {
                MessageBox.Show("Please enter a valid email address", "Couldn't proceed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (textBox2.Text.Length < 4)
            {
                MessageBox.Show("Please enter a username with atleast 4 characters", "Couldn't proceed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string result=(Web.GetPost("http://xperience419inc.000webhostapp.com/register.php", "email", textBox1.Text, "name", textBox2.Text, "password", textBox3.Text));
            if(result.ToLower() == "success")
            {
                loginForm.doneRegister(textBox1.Text, textBox3.Text);
                MessageBox.Show("Your new account created successfully! you may log in now.", result, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void register_Load(object sender, EventArgs e)
        {

        }
    }

}
