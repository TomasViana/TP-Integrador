using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace Data.Database
{
    public class Adapter
    {
        protected void OpenConnection()
        {
            string connectionString;
            connectionString = @"Server=.\SQLEXPRESS;Database=Academia;Integrated Security=true; User=net; Password=net;";
            // connectionString = ConfigurationManager.ConnectionStrings[consKeyDefaultCnnString].ConnectionString;
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
        }

        protected void CloseConnection()
        {
            sqlConn.Close();
            sqlConn = null;
        }

        protected SqlDataReader ExecuteReader(String commandText)
        {
            throw new Exception("Metodo no implementado");
        }

        //Clave por defecto a utlizar para la cadena de conexion
        const string consKeyDefaultCnnString = "ConnStringLocal";

        private SqlConnection _sqlconn;

        public SqlConnection sqlConn {
            get {
                return _sqlconn;
            }
            set {
                _sqlconn = value;
            }
        }
    }
}