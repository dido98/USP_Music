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
    public partial class MainForm : Form
    {
        private const int WM_NCHITTEST = 0x84;
        private const int HT_CAPTION = 0x2;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
        }
        public void update_list()
        {
            List<Song> listOfSong = UserDB.GetSongsList();
            listBox1.Items.Clear();
            foreach (Song s in listOfSong)
            {
                listBox1.Items.Add(s.m_Name);
            }
        }

        private Form1 m_RetForm = null;
        public MainForm(Form1 ret_form)
        {
            this.m_RetForm = ret_form;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.label1.Text = "User: " + UserDB.current_user.m_Email;
            this.update_list();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_RetForm.Show();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            AddSongForm f = new AddSongForm(this);
            f.Show();
            this.Hide();
            // add new song
            // opens new form where we add
            // name
            // singer/group
            // genre
            // year
            // youtube link
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;

            string song_name = listBox1.Items[listBox1.SelectedIndex].ToString();

            foreach (Song s in UserDB._SongsList)
            {
                if (s.m_Name == song_name)
                {
                    UserDB.RemoveSong(song_name);
                    this.update_list();
                    break;
                }
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem == null)
                return;

            string song_name = listBox1.Items[listBox1.SelectedIndex].ToString();

            Song song = new Song();
            foreach (Song s in UserDB._SongsList)
            {
                if (s.m_Name == song_name)
                {
                    song.copy_song(s);
                    break;
                }
            }

            //s.m_URL = "https://www.youtube.com/watch?v=hBz9IoQwmJQ";
            PreviewSongForm f = new PreviewSongForm(this, song);
            f.Show();
            this.Hide();
            // preview song
            // opens form with full info and link for the song
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            m_RetForm.Show();
            this.Close();
        }
    }
}
