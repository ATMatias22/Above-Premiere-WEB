using System;
using System.Collections.Generic;

namespace Above_Premiere.Modelo
{
    public class UserDAO

    {
        public static UserDAO instance = null;
        List<User> users;

        public UserDAO()
        {
            this.users = new List<User>();
            createUser();
        }

        public static UserDAO getInstance()
        {

            if (instance == null)
            {
                instance = new UserDAO();
            }

            return instance;

        }

        private void createUser()
        {
            this.users.Add(new User("Matias", "Matias123"));
            this.users.Add(new User("Alejandro", "Alejandro123"));
            this.users.Add(new User("Juan", "Juan123"));
            this.users.Add(new User("Jose", "Jose123"));
            this.users.Add(new User("Maria", "Maria123"));
            this.users.Add(new User("Lucia", "Lucia123"));
        }



        public User loginUser(String name, String password)
        {
            bool userNameWasFound = false;
            User userFound = null;
            int counter = 0;
            while (counter < this.users.Count && !userNameWasFound)
            {
                User auxUser = this.users[counter];
                if (auxUser.Name.Equals(name))
                {
                    if (auxUser.Password.Equals(password))
                    {
                        userFound = auxUser;
                    }
                    userNameWasFound = true;
                }
                counter++;
            }

            if(userFound == null)
            {
                throw new Exception("Alguna de sus credenciales son incorrectas");
            }

            return userFound;
        }

   
    }
}
