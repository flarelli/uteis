using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TaxaConvenio.Dados.OAD
{
    public class SqlServerOAD : IDisposable
    {
        #region Variaveis
        private SqlCommand cmd;
        private SqlDataAdapter adapter;
        private SqlConnection conn;
        private SqlTransaction trans = null;
        private bool usaTransacao;
        #endregion

        #region Propriedades
        /// <summary>
        /// Retorna um DataAdapter
        /// </summary>
        private SqlDataAdapter Adapter
        {
            get
            {
                if (adapter == null)
                {
                    adapter = new SqlDataAdapter();
                }
                return adapter;
            }
        }

        /// <summary>
        /// Define a procedure do comando
        /// </summary>
        public string Procedure
        {
            set
            {
                //Limpa os parâmetros
                cmd.Parameters.Clear();
                cmd.CommandText = value;
                cmd.CommandType = CommandType.StoredProcedure;
            }
        }

        /// <summary>
        /// Define a procedure do comando
        /// </summary>
        public string Texto
        {
            set
            {
                //Limpa os parâmetros
                cmd.Parameters.Clear();
                cmd.CommandText = value;
                cmd.CommandType = CommandType.Text;
            }
        }

        #endregion

        #region Construtores

        public SqlServerOAD(ConnectionStringSettings connectionStringSettings)
        {
            CriaConexao(connectionStringSettings);
        }
        public SqlServerOAD()
        {
            CriaConexao("ConnectionString");
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Inicia a transação no banco.
        /// </summary>
        public void IniciaTransacao()
        {
            cmd.Transaction = trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
            usaTransacao = true;
        }

        /// <summary>
        /// Commita a transação
        /// </summary>
        public void CommitTransacao()
        {
            trans.Commit();
            if (conn.State != ConnectionState.Closed)
                conn.Close();
        }

        public void RollbackTransacao()
        {
            trans.Rollback();
            if (conn.State != ConnectionState.Closed)
                conn.Close();
        }

        private void CriaConexao(string connectionString)
        {
            CriaConexao(ConfigurationManager.ConnectionStrings[connectionString]);
        }

        /// <summary>
        /// Cria a conexão, o comando e associa a conexão ao comando
        /// </summary>
        /// <param name="connectionString">String de Conexão no Web.Config</param>
        private void CriaConexao(ConnectionStringSettings connectionString)
        {
            conn = new SqlConnection();
            conn.ConnectionString = connectionString.ConnectionString;
            conn.Open();
            conn.FireInfoMessageEventOnUserErrors = false;
            conn.StatisticsEnabled = false;

            cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
        }

        /// <summary>
        /// Realiza o Select no banco
        /// </summary>
        /// <returns>DataTable com o resultado da Consulta</returns>
        public DataTable Select()
        {
            try
            {
                DataTable dt = new DataTable();
                Adapter.SelectCommand = cmd;
                Adapter.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                if (!usaTransacao)
                    conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// Realiza o Select no banco usando o DataReader
        /// </summary>
        /// <returns>DataReader com o resultado da Consulta</returns>
        public SqlDataReader SelectReader(CommandBehavior commandBehavior)
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                return cmd.ExecuteReader(commandBehavior);
            }
            catch (Exception ex)
            {
                if (!usaTransacao)
                    conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// Execua um select que retorna apenas 1 linha e 1 coluna
        /// </summary>
        /// <returns>Valor da consulta</returns>
        public object ExecutaScalar()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                return cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                if (!usaTransacao)
                    conn.Close();
                throw ex;
            }
        }

        /// <summary>
        /// Executa um comando no banco, Insert, Update,Delete
        /// </summary>
        /// <returns></returns>
        public int ExecutaComando()
        {
            try
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (!usaTransacao)
                    conn.Close();
                throw ex;
            }
        }


        /// <summary>
        /// Adiciona parâmetro
        /// </summary>
        /// <param name="nome"></param>
        /// <param name="valor"></param>
        /// <param name="tipo"></param>
        public void AdicionaParametro(string nome, object valor, SqlDbType tipo)
        {
            if (valor == null) valor = DBNull.Value;
            SqlParameter par = new SqlParameter(nome, tipo);
            par.Value = valor;
            cmd.Parameters.Add(par);
        }

        public void InsereDataTableBulkInsert(string tabela, DataTable dados)
        {

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
            {
                bulkCopy.DestinationTableName = tabela;

                try
                {
                    bulkCopy.WriteToServer(dados);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (!usaTransacao)
            {
                conn.Close();
            }
        }

        #endregion
    }
}
