using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DAOs
{
    class DBConnection
    {

        private static DBConnection instance = null;
        MySqlConnection conn;


        private DBConnection() { }
        public static DBConnection getInstance()
        {
            if (instance == null)
            {
                DBConnection.instance = new DBConnection();
                DBConnection.instance.connect();
            }

            return DBConnection.instance;

        }

        public void connect()
        {
            var connString = "Server=localhost;Database=abovepremiere;Uid=root;Pwd=;";
            this.conn = new MySqlConnection(connString);
        }

        public QueryBuilder getQueryBuilder()
        {
            return new QueryBuilder(this.conn);
        }

        public IDataReader select(QueryBuilder query)
        {

            this.conn.Open();
            var dataReader = query.comm.ExecuteReader();

            var dt = new DataTable();
            dt.Load(dataReader);
            this.conn.Close();

            return dt.CreateDataReader();

        }

        public void abm(QueryBuilder query)
        {

            try
            {
                this.conn.Open();
                query.comm.ExecuteNonQuery();
            }
            catch (MySqlException mse)
            {
                if (mse.Number == 1062)
                {
                    throw new Exception("This user already exists, please choose another");
                }

                throw new Exception(mse.Message);
            }
            finally
            {
                this.conn.Close();
            }
        }


        public class QueryBuilder
        {

            public MySqlConnection conn;
            public MySqlCommand comm;

            public QueryBuilder(MySqlConnection conn)
            {
                this.conn = conn;
            }

            public void setQuery(string sql)
            {
                this.comm = new MySqlCommand(sql, this.conn);
            }

            public QueryBuilder addParam(string param, int value)
            {

                this.comm.Parameters.AddWithValue(param, value);
                return this;

            }

            public QueryBuilder addParam(string param, string value)
            {

                this.comm.Parameters.AddWithValue(param, value);
                return this;

            }

            public QueryBuilder addParam(string param, bool value)
            {

                this.comm.Parameters.AddWithValue(param, value);
                return this;

            }


        }
    }
}
