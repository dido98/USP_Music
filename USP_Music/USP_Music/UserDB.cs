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
        public static User current_user;
        private static string sqlConnStr = @"Server = (local); Database = music; Trusted_Connection = True;";

        public static List<Song> _SongsList = new List<Song>();
        public static Dictionary<User, List<Song>> _UsersDict = new Dictionary<User, List<Song>>();

        public static void LoadFromSQL()
        {
            _SongsList = GetSongsList();
        }

        public static List<Song> GetSongsList()
        {
            List<Song> TaskIdsList = new List<Song>();

            using (SqlConnection sqlConn = new SqlConnection(sqlConnStr))
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();

                    //string cmd_str = String.Format("SELECT * FROM [SONG]");
                    string cmd_str = String.Format("SELECT s.ID as sid, s.NAME as sname, s.SINGER as ssinger, s.GENRE as sgenre, s.RELEASE as srelease, s.YOUTUBE_URL as surl FROM [SONG] as s JOIN RELATIONS as r ON s.ID = r.ID_SONG JOIN [USER] as u ON u.ID = r.ID_USER WHERE u.ID = '{0}';", UserDB.current_user.m_iID);
                    
                    SqlCommand cmd = new SqlCommand(cmd_str, sqlConn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Song s = new Song();
                        s.m_iID = Int32.Parse(reader["sid"].ToString());
                        s.m_Name = reader["sname"].ToString();
                        s.m_Actor = reader["ssinger"].ToString();
                        s.m_Genre = reader["sgenre"].ToString();
                        s.m_Year = reader["srelease"].ToString();
                        s.m_URL = reader["surl"].ToString();
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
            List<User> list_users = new List<User>();

            using (SqlConnection sqlConn = new SqlConnection(sqlConnStr))
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();

                    string cmd_str = String.Format("SELECT * FROM [USER]");

                    SqlCommand cmd = new SqlCommand(cmd_str, sqlConn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        User dbu = new User(reader["EMAIL"].ToString(), reader["PASSWORD"].ToString());
                        dbu.m_iID = Int32.Parse(reader["ID"].ToString());
                        list_users.Add(dbu);
                    }

                    cmd.Dispose();
                    reader.Close();

                    sqlConn.Close();
                }
            }

            foreach(User c in list_users)
            {
                if (c.m_Email == email && c.m_Password == pass)
                    return c;
            }

            return null;
        }

        public static void AddUserToDB(User u)
        {
            using (SqlConnection sqlConn = new SqlConnection(sqlConnStr))
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();

                    string cmd_str =
                        String.Format("INSERT INTO [USER] VALUES('{0}', '{1}')", u.m_Email, u.m_Password);

                    SqlCommand cmd = new SqlCommand(cmd_str, sqlConn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    sqlConn.Close();
                }
            }
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
            int song_id = 0;
            using (SqlConnection sqlConn = new SqlConnection(sqlConnStr))
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();

                    string cmd_str = String.Format("SELECT * FROM [SONG] WHERE [NAME] = '{0}' AND YOUTUBE_URL = '{1}'",
                        s.m_Name, s.m_URL);

                    SqlCommand cmd = new SqlCommand(cmd_str, sqlConn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        song_id = Int32.Parse(reader["ID"].ToString());
                    }

                    cmd.Dispose();
                    reader.Close();

                    sqlConn.Close();
                }
            }

            using (SqlConnection sqlConn = new SqlConnection(sqlConnStr))
            {
                if (sqlConn.State != ConnectionState.Open)
                {
                    sqlConn.Open();

                    string cmd_str =
                        String.Format("INSERT INTO [RELATIONS] VALUES('{0}', '{1}')",
                        UserDB.current_user.m_iID, song_id);

                    SqlCommand cmd = new SqlCommand(cmd_str, sqlConn);
                    cmd.ExecuteNonQuery();
                    cmd.Dispose();

                    sqlConn.Close();
                }
            }
        }
    }
}
