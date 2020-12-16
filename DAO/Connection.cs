using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DAO
{
    public class Connection
    {
        public SqlConnection sqlConn = null;
        //Usuário e Senha do SQL Server
        public string strConn = @"server = localhost; database = SUPLEMENTOS; user = MEU USUARIO; password = MINHA SENHA; MultipleActiveResultSets=True";

        public static SqlCommand command = new SqlCommand();

        public static SqlParameter parameter = new SqlParameter();

        private static DataSet dataset;
        private static SqlDataAdapter adapter;


        public SqlConnection connection()
        {
            try
            {

                sqlConn = new SqlConnection(strConn);

                if (sqlConn.State == ConnectionState.Closed)
                {

                    sqlConn.Open();
                }

                return sqlConn;
            }
            catch (SqlException ex)
            {

                throw ex;
            }

        }

        public void Open()
        {
            sqlConn.Open();
        }

        public void Close()
        {
            sqlConn.Close();
        }

        public void AddParaMeter(string nome, SqlDbType tipo, int tamanho, object valor)
        {

            parameter = new SqlParameter();
            parameter.ParameterName = nome;
            parameter.SqlDbType = tipo;
            parameter.Size = tamanho;
            parameter.Value = valor;
            command.Parameters.Add(parameter);
        }

        public void AddParameter(string nome, SqlDbType tipo, object valor)
        {
            SqlParameter parameter = new SqlParameter();
            parameter.ParameterName = nome;
            parameter.SqlDbType = tipo;
            parameter.Value = valor;
            command.Parameters.Add(parameter);
        }
        public void RemoveParameter(string pnome)
        {
            if (command.Parameters.Contains(pnome))
            {
                command.Parameters.Remove(pnome);
            }
        }
        public void ClearParameter()
        {
            command.Parameters.Clear();
        }
        public DataTable ExecuteQuery(string SQL)
        {
            try
            {
                command.Connection = connection();
                command.CommandText = SQL;
                command.ExecuteScalar();
                IDataReader dtreader = command.ExecuteReader();
                DataTable dtresult = new DataTable();
                dtresult.Load(dtreader);
                sqlConn.Close();
                return dtresult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ExecuteDML(string SQL)
        {
            try
            {
                command.Connection = connection();
                command.CommandText = SQL;
                int result = command.ExecuteNonQuery();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataSet ExecuteDataSet(string SQL)
        {
            dataset = new DataSet();
            command.Connection = connection();
            command.CommandText = SQL;
            adapter = new SqlDataAdapter(command);
            adapter.Fill(dataset);
            return dataset;
        }

        public DataTable ExecuteDataTable(string SQL)
        {
            var table = new DataTable();
            command.Connection = connection();
            command.CommandText = SQL;
            adapter = new SqlDataAdapter(command);
            adapter.Fill(table);
            return table;
        }

        public SqlDataReader ExecuteReader(string SQL)
        {
            SqlDataReader reader;
            command.Connection = connection();
            command.CommandText = SQL;
            reader = command.ExecuteReader();
            return reader;
        }
    }
}
