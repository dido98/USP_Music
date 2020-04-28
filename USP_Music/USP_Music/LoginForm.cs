using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USP_Music
{
    public partial class LoginForm : Form
    {
        private Form1 m_RetForm = null;
        public LoginForm(Form1 return_form)
        {
            m_RetForm = return_form;
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_RetForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.ToString();
            string pass = textBox2.Text.ToString();

            User u = UserDB.SearchUser(email, pass);

            if (u != null)
                UserDB.cuurent_user = u;
            else
                return;

            MainForm f = new MainForm(m_RetForm);
            f.Show();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            m_RetForm.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.ToString();
            string pass = textBox2.Text.ToString();

            User u = UserDB.SearchUser(email, pass);

            if (u != null)
                UserDB.cuurent_user = u;
            else
                return;

            MainForm f = new MainForm(m_RetForm);
            f.Show();
            this.Close();
        }
    }
}
