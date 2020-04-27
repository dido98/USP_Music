using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USP_Music
{
    public class User
    {
        public string m_Email { get; set; }
        public string m_Password { get; set; }

        public User(string email, string password)
        {
            m_Email = email;
            m_Password = password;
        }
    }
}
