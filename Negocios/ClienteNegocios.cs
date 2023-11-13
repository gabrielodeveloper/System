using AcessoBancoDados;
using ObjetoTransferencia;
using System;
using System.Data;

namespace Negocios
{
    public class ClienteNegocios
    {
        AcessoDadosSqlServer acessoDadosSqlServer = new AcessoDadosSqlServer();

        public string Inserir(Cliente cliente)
        {

            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@Nome", cliente.Nome);
                acessoDadosSqlServer.AdicionarParametros("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.AdicionarParametros("@Sexo", cliente.Sexo);
                acessoDadosSqlServer.AdicionarParametros("@LimiteCompra", cliente.LimiteCompra);

                string idClente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteInserir").ToString();

                return idClente;
            }
            catch (Exception error)
            {

                return error.Message;
            }

        }

        public string Alterar(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IdCliente", cliente.IdCliente);
                acessoDadosSqlServer.AdicionarParametros("@Nome", cliente.Nome);
                acessoDadosSqlServer.AdicionarParametros("@DataNascimento", cliente.DataNascimento);
                acessoDadosSqlServer.AdicionarParametros("@Sexo", cliente.Sexo);
                acessoDadosSqlServer.AdicionarParametros("@LimiteCompra", cliente.LimiteCompra);

                string idClente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteAlterar").ToString();

                return idClente;

            }
            catch (Exception error)
            {

                return error.Message;
            }
        }

        public string Excluir(Cliente cliente)
        {
            try
            {
                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IdCliente", cliente.IdCliente);
                string idClente = acessoDadosSqlServer.ExecutarManipulacao(CommandType.StoredProcedure, "uspClienteExcluir").ToString();

                return idClente;
            }
            catch (Exception error)
            {

               return error.Message;
            }
 
        }

        public ClienteColecao ConsultarPorNome(string nome)
        {
            try
            {
                ClienteColecao clienteColecao = new ClienteColecao();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@Nome", nome);

                DataTable dataTableCliente = acessoDadosSqlServer.ExcutarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorNome");

                foreach (DataRow row in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                    cliente.Nome = Convert.ToString(row["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(row["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(row["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(row["LimiteCompra"]);

                    clienteColecao.Add(cliente);
                }

                return clienteColecao;
            }
            catch (Exception error)
            {
                throw new Exception("Não foi possível consultar cliente por nome, Detalhes " + error.Message);
            }
        }

        public ClienteColecao ConsultarPorId(int idCliente)
        {
            try
            {
                ClienteColecao clienteColecao = new ClienteColecao();

                acessoDadosSqlServer.LimparParametros();
                acessoDadosSqlServer.AdicionarParametros("@IdCliente", idCliente);

                DataTable dataTableCliente = acessoDadosSqlServer.ExcutarConsulta(CommandType.StoredProcedure, "uspClienteConsultarPorId");

                foreach (DataRow row in dataTableCliente.Rows)
                {
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = Convert.ToInt32(row["IdCliente"]);
                    cliente.Nome = Convert.ToString(row["Nome"]);
                    cliente.DataNascimento = Convert.ToDateTime(row["DataNascimento"]);
                    cliente.Sexo = Convert.ToBoolean(row["Sexo"]);
                    cliente.LimiteCompra = Convert.ToDecimal(row["LimiteCompra"]);

                    clienteColecao.Add(cliente);
                }

                return clienteColecao;
            }
            catch (Exception error)
            {
                throw new Exception("Não foi possível consultar cliente por código  , Detalhes " + error.Message);
            }
        }

    }
}
