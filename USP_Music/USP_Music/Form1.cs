using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USP_Music
{
    public partial class Form1 : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CAPTION = 0x2;

        public Form1()
        {
            InitializeComponent();

            toolTip1.SetToolTip(label2, "Authors:\n  Daniel Bodurov\n  Dimitar Nikolov\n  Yordan Yordanov");
            label1.BringToFront();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            // login + register
            // list of added songs
            // add new song
            // remove song
            // search
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            LoginForm f = new LoginForm(this);
            this.Hide();
            f.Show();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            RegisterForm f = new RegisterForm(this);
            this.Hide();
            f.Show();
        }
    }
}
