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
    public partial class RegisterForm : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CAPTION = 0x2;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }
        private Form1 m_RetForm = null;
        public RegisterForm(Form1 return_form)
        {
            m_RetForm = return_form;
            InitializeComponent();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
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

            User u = new User(email, pass);
            UserDB.current_user = u;
            UserDB.AddUserToDB(u);

            MainForm f = new MainForm(m_RetForm);
            f.Show();
            this.Close();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.ToString();
            string pass = textBox2.Text.ToString();

            User u = new User(email, pass);
            UserDB.AddUserToDB(u);
            //UserDB.current_user = u;
            UserDB.current_user = UserDB.SearchUser(email, pass);

            MainForm f = new MainForm(m_RetForm);
            f.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            m_RetForm.Show();
            this.Close();
        }
    }
}
