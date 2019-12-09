using AdoNetDBAccess.Enumerators;
using System.Data;
using System.Data.SqlClient;

namespace AdoNetDBAccess.AcessoDB
{
    public class DBConnection
    {
        private enumDataBase _dbType;
        private SqlConnection _connection = null;

        public DBConnection(enumDataBase dbType)
        {
            _dbType = dbType;
        }

        public SqlConnection appConnectionString(string connectionString)
        {
            if (_dbType == enumDataBase.SQLServer)
            {
                try
                {
                    _connection = new SqlConnection();
                    _connection.ConnectionString = connectionString;
                    _connection.Open();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

            else if (_dbType == enumDataBase.Access)
            {
                /// TODO
            }

            return _connection;
        }

        public void Open()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        public void Close()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}