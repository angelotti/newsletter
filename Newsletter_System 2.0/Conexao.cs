using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Newsletter_System_2._0
{
    class Conexao
    {
        public SqlConnection sqlConn = null;
        public string strConn = @"server=localhost;database=SUPLEMENTOS;user id=sa;password=123456;";


        public SqlConnection Conectar()
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

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public SqlConnection Desconectar()
        {

            try
            {

                //cn = new OleDbConnection(banco);
                if (sqlConn.State == ConnectionState.Open)
                {
                    sqlConn.Close();
                }
                return sqlConn;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
