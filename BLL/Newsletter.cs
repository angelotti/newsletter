using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class Newsletter
    {
        private static string SQL;
        private int _id;
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _situacao;
        public string situacao
        {
            get { return _situacao; }
            set { _situacao = value; }
        }
        private string _nome;
        public string nome
        {
            get { return _nome; }
            set { _nome = value; }
        }
        private string _email;
        public string email
        {
            get { return _email; }
            set { _email = value; }
        }

        public DataSet Listar()
        {
            DAO.Connection connection = new DAO.Connection();
            connection.ClearParameter();
            SQL = @"select Id, Situacao, Nome, Email from tbl_Newsletter ORDER BY ID DESC";
            return connection.ExecuteDataSet(SQL);
        }

        public void Atualizar()
        {
            DAO.Connection connection = new DAO.Connection();
            connection.ClearParameter();
            SQL = @"update tbl_Newsletter set Situacao=@situacao WHERE id = @id";
            connection.AddParameter("@situacao", SqlDbType.VarChar, _situacao);
            connection.AddParameter("@id", SqlDbType.Int, _id);
            connection.ExecuteDML(SQL);
        }

        public DataSet ListarAtivos()
        {
            DAO.Connection connection = new DAO.Connection();
            connection.ClearParameter();
            SQL = @"select Id, Situacao, Nome, Email from tbl_Newsletter where Situacao='Ativo' ORDER BY ID DESC";
            return connection.ExecuteDataSet(SQL);
        }

        public DataSet ListarInativos()
        {
            DAO.Connection connection = new DAO.Connection();
            connection.ClearParameter();
            SQL = @"select Id, Situacao, Nome, Email from tbl_Newsletter where Situacao='Inativo' ORDER BY ID DESC";
            return connection.ExecuteDataSet(SQL);
        }
    }
}
