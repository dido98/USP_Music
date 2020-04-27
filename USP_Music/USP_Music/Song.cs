using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USP_Music
{
    public class Song
    {
        public void copy_song(Song s)
        {
            this.m_iID = s.m_iID;
            this.m_Name = s.m_Name;
            this.m_Actor = s.m_Actor;
            this.m_Genre = s.m_Genre;
            this.m_Year = s.m_Year;
            this.m_URL = s.m_URL;
        }

        public int m_iID { get; set; } 
        public string m_Name { get; set; }
        public string m_Actor { get; set; }
        public string m_Genre { get; set; }
        public string m_Year { get; set; }
        public string m_URL { get; set; }
    }
}
