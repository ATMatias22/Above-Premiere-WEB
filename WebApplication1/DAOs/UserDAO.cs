using System;
using System.Collections.Generic;
using WebApplication1.DAOs;
using MySql.Data.MySqlClient;

namespace Above_Premiere.Modelo
{
    public class UserDAO

    {
        public static UserDAO instance = null;

        public UserDAO()
        {
      
        }

        public static UserDAO getInstance()
        {
            if (instance == null)
            {
                instance = new UserDAO();
            }
            return instance;
        }



        public User loginUser(User user)
        {

            var queryBuilder = DBConnection.getInstance().getQueryBuilder();
            
            queryBuilder.setQuery("SELECT * FROM users WHERE username=@username AND password=@password");
            queryBuilder.addParam("@username", user.UserName);
            queryBuilder.addParam("@password", user.Password);
            var dataReader = DBConnection.getInstance().select(queryBuilder);

            User userFound = null;

            while (dataReader.Read())
            {
                userFound = new User(dataReader.GetString(1), dataReader.GetString(2));
            }

            if (userFound == null)
            {
                throw new Exception("Some of your credentials are wrong");
            }

            return userFound;
        }

        public User registerUser(User user)
        {
            try
            {
                var queryBuilder = DBConnection.getInstance().getQueryBuilder();

                queryBuilder.setQuery("INSERT INTO users (username,password) VALUES (@username,@password)");
                queryBuilder.addParam("@username", user.UserName);
                queryBuilder.addParam("@password", user.Password);

                DBConnection.getInstance().abm(queryBuilder);

                user.setKey();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return user;

        }


        /*----------------*/

    }
}
