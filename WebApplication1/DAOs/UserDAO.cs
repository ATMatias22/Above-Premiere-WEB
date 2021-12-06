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
            
            queryBuilder.setQuery("SELECT u.username, u.password, p.product_key FROM users u inner join product_keys p on p.id = u.associated_key WHERE username=@username AND password=@password");
            queryBuilder.addParam("@username", user.UserName);
            queryBuilder.addParam("@password", user.Password);
            var dataReader = DBConnection.getInstance().select(queryBuilder);

            User userFound = null;

            while (dataReader.Read())
            {
                userFound = new User(dataReader.GetString(0), dataReader.GetString(1),dataReader.GetString(2));
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
                int idKey = verifyKeyAndItWasNotUsed(user.Key);

                var queryBuilder = DBConnection.getInstance().getQueryBuilder();

                queryBuilder.setQuery("INSERT INTO users (username,password,associated_key) VALUES (@username,@password,@key)");
                queryBuilder.addParam("@username", user.UserName);
                queryBuilder.addParam("@password", user.Password);
                queryBuilder.addParam("@key", idKey);

                DBConnection.getInstance().abm(queryBuilder);

                useKey(idKey);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }

            return user;

        }

        public int verifyKeyAndItWasNotUsed(string key)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();
            queryBuilder.setQuery($"SELECT * FROM product_keys where product_key = @key AND is_used = 0");
            queryBuilder.addParam("@key", key);
            var dataReader = DBConnection.getInstance().select(queryBuilder);


            if (dataReader.Read())
            {
                return dataReader.GetInt32(0); 
            }

            throw new Exception("The key does not exist or has already been used");

        }

        public void useKey(int idKey)
        {
            var queryBuilder = DBConnection.getInstance().getQueryBuilder();
            queryBuilder.setQuery($"UPDATE product_keys SET is_used = 1 WHERE id = @key");
            queryBuilder.addParam("@key", idKey);
            DBConnection.getInstance().abm(queryBuilder);
        }

        /*----------------*/

    }
}
