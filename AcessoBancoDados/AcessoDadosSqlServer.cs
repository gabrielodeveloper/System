using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;
using AcessoBancoDados.Properties;

namespace AcessoBancoDados
{
     public class AcessoDadosSqlServer
    {
        private SqlConnection CriarConexao()
        {
            return new SqlConnection(Settings.Default.stringConexao);
        }

        private SqlParameterCollection sqlParameterCollection = new SqlCommand().Parameters;

        public void LimparParametros()
        {
            sqlParameterCollection.Clear();
        }

        public void AdicionarParametros(string nomeParamentro, object valor )
        {
            sqlParameterCollection.Add(new SqlParameter(nomeParamentro, valor));

        }

        public object ExecutarManipulacao(CommandType commandType, string nomeStoredProcedureOuTexto)
        {

            try
            {
                SqlConnection sqlConnection = CriarConexao();
                sqlConnection.Open();

                SqlCommand sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = nomeStoredProcedureOuTexto;
                sqlCommand.CommandTimeout = 7200; // em secondos

                foreach (SqlParameter sqlParameter in sqlParameterCollection)
                {
                    sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
                }

                return sqlCommand.ExecuteScalar();
            }
            catch (Exception error)
            {

                throw new Exception(error.Message);
            }


        }

        public DataTable ExcutarConsulta(CommandType commandType, string nomeStoredProcedureOuTexto)
        {
            SqlConnection sqlConnection = CriarConexao();
            sqlConnection.Open();

            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = commandType;
            sqlCommand.CommandText = nomeStoredProcedureOuTexto;
            sqlCommand.CommandTimeout = 7200;

            foreach(SqlParameter sqlParameter in sqlParameterCollection)
            {
                sqlCommand.Parameters.Add(new SqlParameter(sqlParameter.ParameterName, sqlParameter.Value));
            }

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);

            return dataTable;
        }
    }
}
