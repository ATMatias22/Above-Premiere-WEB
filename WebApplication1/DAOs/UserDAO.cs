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
            registerUser("Matias", "Matias123");
            registerUser("Alejandro", "Alejandro123");
            registerUser("Juan", "Juan123");
            registerUser("Jose", "Jose123");
            registerUser("Maria", "Maria123");
            registerUser("Lucia", "Lucia123");
        }



        public User loginUser(String name, String password)
        {
            User userFound = searchUser(name, password);

            if (userFound == null)
            {
                throw new Exception("Some of your credentials are wrong");
            }

            return userFound;
        }

        public User registerUser(String name, String password)
        {
            if (repeatUser(name))
            {
                throw new Exception("This user already exists, please choose another");
            }
            User newUser = new User(name, password);
            newUser.setKey();
            this.users.Add(newUser);

            return newUser;

        }

        public User searchUser(string username, string password)
        {
            return users.Find(x => x.Name == username && x.Password == password);
        }

        public bool repeatUser(string username)
        {
            return users.Find(x => x.Name == username) != null;
        }



        /*----------------*/
        public List<User> getAllUser()
        {
            return new List<User>(users);
        }
    }
}
