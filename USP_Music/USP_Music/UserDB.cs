using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USP_Music
{
    public static class UserDB
    {
        public static User cuurent_user;
        private static string sqlConnStr = @"Server = (local); Database = music; Trusted_Connection = True;";

        public static List<Song> _SongsList = new List<Song>();
        public static Dictionary<User, List<Song>> _UsersDict = new Dictionary<User, List<Song>>();

        public static void LoadFromSQL()
        {
            _SongsList = GetSongsList();
            ;
        }

        public static List<Song> GetSongsList()
        {
            List<Song> TaskIdsList = new List<Song>();

            using (SqlConnection sqlConn = new SqlConnection(sqlConnStr))
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();

                    string cmd_str = String.Format("SELECT * FROM [SONG]");

                    SqlCommand cmd = new SqlCommand(cmd_str, sqlConn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Song s = new Song();
                        s.m_iID = Int32.Parse(reader["ID"].ToString());
                        s.m_Name = reader["NAME"].ToString();
                        s.m_Actor = reader["SINGER"].ToString();
                        s.m_Genre = reader["GENRE"].ToString();
                        s.m_Year = reader["RELEASE"].ToString();
                        s.m_URL = reader["YOUTUBE_URL"].ToString();
                        TaskIdsList.Add(s);
                    }

                    cmd.Dispose();
                    reader.Close();

                    sqlConn.Close();
                }
            }

            _SongsList = TaskIdsList;
            return TaskIdsList;
        }

        public static User SearchUser(string email, string pass)
        {
            return null;
        }

        public static void AddUserToDB(User u)
        {
            ;
        }

        public static void RemoveSong(string song_name)
        {
            foreach (Song s in _SongsList)
            {
                if (s.m_Name != song_name)
                    continue;

                using (SqlConnection sqlConn = new SqlConnection(sqlConnStr))
                {
                    if (sqlConn.State != ConnectionState.Open)
                    {
                        sqlConn.Open();

                        string cmd_str = null;
                        SqlCommand cmd = null;

                        cmd_str = String.Format("DELETE FROM [RELATIONS] WHERE [ID_SONG] = '{0}'", s.m_iID);

                        cmd = new SqlCommand(cmd_str, sqlConn);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        cmd_str = String.Format("DELETE FROM [SONG] WHERE [ID] = '{0}'", s.m_iID);

                        cmd = new SqlCommand(cmd_str, sqlConn);
                        cmd.ExecuteNonQuery();
                        cmd.Dispose();

                        sqlConn.Close();
                    }
                }
            }
        }

        public static void AddSongToDB(Song s)
        {
            using (SqlConnection sqlConn = new SqlConnection(sqlConnStr))
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();

                    string cmd_str =
                        String.Format("INSERT INTO [SONG] VALUES('{0}', '{1}', '{2}', '{3}', '{4}')",
                        s.m_Name, s.m_Actor, s.m_Genre, s.m_Year, s.m_URL);

                    SqlCommand cmd = new SqlCommand(cmd_str, sqlConn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    sqlConn.Close();
                }
            }
        }
    }
}
