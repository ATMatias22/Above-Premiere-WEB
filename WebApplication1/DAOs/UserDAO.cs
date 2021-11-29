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
                userFound = new User(dataReader.GetString(1), dataReader.GetString(2),dataReader.GetString(3));
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
                verifyKeyAndItWasNotUsed(user.Key);

                var queryBuilder = DBConnection.getInstance().getQueryBuilder();

                queryBuilder.setQuery("INSERT INTO users (username,password,associated_key) VALUES (@username,@password,@key)");
                queryBuilder.addParam("@username", user.UserName);
                queryBuilder.addParam("@password", user.Password);
                queryBuilder.addParam("@key", user.Key);

                DBConnection.getInstance().abm(queryBuilder);

                useKey(user.Key);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return user;

        }

        public void verifyKeyAndItWasNotUsed(string key)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();
            queryBuilder.setQuery($"SELECT * FROM product_keys where product_key = @key AND is_used = 0");
            queryBuilder.addParam("@key", key);
            var dataReader = DBConnection.getInstance().select(queryBuilder);

            if (!dataReader.Read())
            {
                throw new Exception("The key does not exist or has already been used");
            }
        }

        public void useKey(string key)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();
            queryBuilder.setQuery($"UPDATE product_keys SET is_used = 1 WHERE product_key = @key");
            queryBuilder.addParam("@key", key);
            DBConnection.getInstance().abm(queryBuilder);
        }

        /*----------------*/

    }
}
