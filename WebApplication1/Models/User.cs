using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Above_Premiere.Modelo
{
    public class User
    {
        string userName;
        string password;
        string key;
        

        public User(string username, string password, string key)
        {
            this.userName = username;
            this.password = password;
            this.key = key;
        }

        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Key { get => key; set => key = value; }

    }
}
