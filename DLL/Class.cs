using MySql.Data.MySqlClient;
using System;

namespace testschool.DLL
{
    public class Login 
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string user;
        private string password;
        private string port;
        private string connectionString;
        private string sslM;

        public Login()
        {           

            server = "localhost";
            database = "school";
            user = "root";
            password = "123456";
            port = "3306";
            sslM = "none";
            connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}; SslMode={5}", server, port, user, password, database, sslM);

            connection = new MySqlConnection(connectionString);
        }
        public void TestOpen()
        {
            try
            {
                connection.Open();


                
            }
            catch (MySqlException ex)
            {
            }
            finally
            {
                connection.Close();
            }
        }
    }




    }
