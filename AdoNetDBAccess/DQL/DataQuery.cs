using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml.XPath;

namespace AdoNetDBAccess.DQL
{
    public class DataQuery
    {
        internal SqlConnection _connection = null;
        internal SqlDataReader _dataReader  = null;
        internal SqlDataAdapter _adapter    = null;
        internal DataSet _dataSet           = null;
        internal SqlCommand _command        = null;
        internal DataTable _dataTable       = null;
        internal string _sqlQuery           = string.Empty;
        internal string _stringReturn       = string.Empty;

        public DataQuery(SqlConnection connection, string retrieveSql)
        {
            _connection = connection;
            _sqlQuery = retrieveSql;
        }

        public DataQuery(SqlConnection connection, DataSet dataSet)
        {
            _connection = connection;
            _dataSet    = dataSet;
        }

        public DataQuery(SqlConnection connection, DataTable dataTable)
        {
            _connection = connection;
            _dataTable = dataTable;
        }

        public string RetrieveDataTableToXML(DataTable dataTable)
        {
            try
            {
                //_stringReturn = dataTable.WriteXml()
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _stringReturn;
        }

        public string returnDataSettoXML(DataSet dataSet)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    using (TextWriter streamWriter = new StreamWriter(memoryStream))
                    {
                        var xmlSerializer = new XmlSerializer(typeof(DataSet));
                        xmlSerializer.Serialize(streamWriter, dataSet);
                        _stringReturn = Encoding.UTF8.GetString(memoryStream.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _stringReturn;
        }

        public DataSet returnDataSet()
        {
            try
            {
                _dataSet = new DataSet();

                _command = new SqlCommand(_sqlQuery, _connection);
                _adapter = new SqlDataAdapter(_command);
                _adapter.Fill(_dataSet, _sqlQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _dataSet;
        }

        public DataTable returnDataReaderToDataTable()
        {
            try
            {
                _command = new SqlCommand(_sqlQuery, _connection);
                _command.CommandType = CommandType.StoredProcedure;
                _dataReader = _command.ExecuteReader();

                _dataTable = new DataTable();
                _dataTable.Load(_dataReader);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _dataTable;
        }

        public SqlDataReader returnDataReaderFromProcedure()
        {
            try
            {
                _command = new SqlCommand(_sqlQuery, _connection);
                _command.CommandType = CommandType.StoredProcedure;
                _dataReader = _command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _dataReader;
        }

        public SqlDataReader returnDataReaderFromQuery()
        {
            try
            {
                _command = new SqlCommand(_sqlQuery, _connection);
                _dataReader = _command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _dataReader;
        }
    }
}