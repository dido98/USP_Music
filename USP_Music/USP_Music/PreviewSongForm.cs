using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace USP_Music
{
    public partial class PreviewSongForm : Form
    {
        private MainForm m_RetForm = null;
        private Song m_Song = null;
        public PreviewSongForm(MainForm ret_form, Song s)
        {
            this.m_Song = s;
            this.m_RetForm = ret_form;
            InitializeComponent();
        }

        private void PreviewSongForm_Load(object sender, EventArgs e)
        {
            string youtube_url = this.m_Song.m_URL;
            string thumbnail = youtube_url.Replace("https://www.youtube.com/watch?v=", "http://i3.ytimg.com/vi/");
            thumbnail += "/maxresdefault.jpg";

            pictureBox1.ImageLocation = thumbnail;

            this.label1.Text = "Song Name:";
            this.label2.Text = "Singer/Group: ";
            this.label3.Text = "Genre: ";
            this.label4.Text = "Release Year: ";

            this.label5.Text = this.m_Song.m_Name;
            this.label6.Text = this.m_Song.m_Actor;
            this.label7.Text = this.m_Song.m_Genre;
            this.label8.Text = this.m_Song.m_Year;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.m_RetForm.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string url = this.m_Song.m_URL;
            Process.Start(url);
        }
    }
}
