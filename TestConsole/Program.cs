using AdoNetDBAccess.AcessoDB;
using AdoNetDBAccess.DQL;
using AdoNetDBAccess.Enumerators;

using System;
using System.Data;
using System.Data.SqlClient;

namespace TestConsole
{
    class Program
    {
        SqlConnection _conn     = new SqlConnection();
        SqlDataReader _dr       = null;
        DataTable _dataTable    = null;
        DataSet _dataSet        = null;
        SqlCommand _command     = null;
        SqlDataAdapter _adapter = null;
        string _strSQL          = "SELECT * FROM departamento";
        string _procedure       = "SP_LISTA_DEPARTAMENTO";

        static void Main(string[] args)
        {
            Program program = new Program();

            //Abre conexão com DB
            program.TestDBConnectionAppConfig();

            //Retornando DataReader - Query
            //program.RetrieveDataReaderFromQuery();

            //Retornnando DataReader - Stored Procedure
            //program.RetrieveDataReaderFromProcedure();

            //Retornando DataTable - Stored Procedure
            //program.RetrieveDataReaderToDataTable();

            //Retornando DataSet - Stored Procedure
            //program.RetrieveDataSet();

            //Retornando arquivo XML - DataSet
            //DataSet ds = program.CreateDataSet();
            //program.RetrieveDataSetToXml(ds);

            //Retornando arquivo XML - DataTable
            DataTable dt = program.CreateDataTable();
            program.RetrieveDataTableToXML(dt);
        }

        private DataTable CreateDataTable()
        {
            _dataTable = new DataTable("departamento");

            _command = new SqlCommand(_procedure, _conn);
            _command.CommandType = CommandType.StoredProcedure;
            _dr = _command.ExecuteReader();

            _dataTable = new DataTable();
            _dataTable.Load(_dr);

            return _dataTable;
        }

        private DataSet CreateDataSet()
        {
            _dataSet = new DataSet();

            _command = new SqlCommand(_strSQL, _conn);
            _adapter = new SqlDataAdapter(_command);
            _adapter.Fill(_dataSet, _strSQL);

            return _dataSet;
        }

        private void RetrieveDataTableToXML(DataTable dataTable)
        {
            DataQuery retrieve = new DataQuery(_conn, _dataTable);
            string xmlString = retrieve.RetrieveDataTableToXML(dataTable);

            Console.WriteLine(xmlString);

            Console.ReadKey();
        }

        private void RetrieveDataSetToXml(DataSet dataSet)
        {
            DataQuery retrieve = new DataQuery(_conn, dataSet);
            string xmlString = retrieve.returnDataSettoXML(dataSet);

            Console.WriteLine(xmlString);

            Console.ReadKey();
        }

        private void RetrieveDataSet()
        {
            DataQuery retrieve = new DataQuery(_conn, _procedure);
            _dataSet = retrieve.returnDataSet();

            _dataTable = _dataSet.Tables[0];

            Console.WriteLine("\nCodigo" + "     " + "Departamento" + "      " + "Localizacao");

            foreach (DataRow row in _dataTable.Rows)
            {
                Console.WriteLine(row[0].ToString().Trim() + "           " + row[1].ToString().Trim() + "           " + row[2].ToString().Trim());
            }
            Console.ReadKey();

            retrieve  = null;
            _dataTable = null;
        }

        private void RetrieveDataReaderToDataTable()
        {
            DataQuery retrieve = new DataQuery(_conn, _procedure);
            _dataTable = retrieve.returnDataReaderToDataTable();

            Console.WriteLine("\nCodigo" + "     " + "Departamento" + "      " + "Localizacao");

            foreach (DataRow row in _dataTable.Rows)
            {
                Console.WriteLine(row[0].ToString().Trim() + "           " + row[1].ToString().Trim() + "           " + row[2].ToString().Trim());
            }
            Console.ReadKey();

            retrieve  = null;
            _dataTable = null;
        }

        private void RetrieveDataReaderFromProcedure()
        {
            DataQuery retrieve = new DataQuery(_conn, _procedure);
            _dr = retrieve.returnDataReaderFromProcedure();

            Console.WriteLine("\nCodigo" + "     " + "Departamento" + "      " + "Localizacao");
            while (_dr.Read())
            {
                Console.WriteLine(_dr[0].ToString().Trim() + "           " + _dr[1].ToString().Trim() + "           " + _dr[2].ToString().Trim());
            }
            Console.ReadKey();

            retrieve = null;
            _dr = null;
        }

        private void RetrieveDataReaderFromQuery()
        {
            DataQuery retrieve = new DataQuery(_conn, _strSQL);
            _dr = retrieve.returnDataReaderFromQuery();

            Console.WriteLine("\nCodigo" + "     " + "Departamento" + "      " + "Localizacao");
            while (_dr.Read())
            {
                Console.WriteLine(_dr[0].ToString().Trim() + "           " + _dr[1].ToString().Trim() + "           " + _dr[2].ToString().Trim());
            }
            Console.ReadKey();

            retrieve = null;
            _dr = null;
        }

        private void TestDBConnectionAppConfig()
        {
            //cria a instância da classe
            DBConnection DBAccess = new DBConnection(enumDataBase.SQLServer);

            //request do método para connectionString no appConfig
            _conn = DBAccess.appConnectionString(@"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=GuiaPratico;Data Source=localhost\SQLEXPRESS01");

            if (_conn.State == ConnectionState.Closed)
            {
                Console.WriteLine("CONEXÃO FECHADA!");
                Console.ReadKey();
            }
            else
                Console.WriteLine("CONEXÃO ABERTA!");
        }
    }
}