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
    public partial class AddSongForm : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CAPTION = 0x2;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }
        private MainForm m_RetForm = null;
        public AddSongForm(MainForm ret_form)
        {
            this.m_RetForm = ret_form;
            InitializeComponent();
        }

        private void AddSongForm_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Song s = new Song();

            s.m_Name = textBox1.Text.ToString();
            s.m_Actor = textBox2.Text.ToString();
            s.m_Genre = textBox3.Text.ToString();
            s.m_Year = textBox4.Text.ToString();
            s.m_URL = textBox5.Text.ToString();
            UserDB.AddSongToDB(s);

            UserDB.GetSongsList();
            this.m_RetForm.update_list();
            this.m_RetForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.m_RetForm.Show();
            this.Close();
        }

        private void label6_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.m_RetForm.Show();
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Song s = new Song();

            s.m_Name = textBox1.Text.ToString();
            s.m_Actor = textBox2.Text.ToString();
            s.m_Genre = textBox3.Text.ToString();
            s.m_Year = textBox4.Text.ToString();
            s.m_URL = textBox5.Text.ToString();
            UserDB.AddSongToDB(s);

            //UserDB.GetSongsList();
            this.m_RetForm.update_list();
            this.m_RetForm.Show();
            this.Close();
        }
    }
}
