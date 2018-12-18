using System;
using System.Configuration;
using System.Data.SqlClient;

namespace TaxaConvenio.Dados.OAD
{
    public abstract class BaseDAL : IDisposable
    {
        private SqlServerOAD oad;
        protected SqlDataReader reader;
        ConnectionStringSettings conexao = null;

        public SqlServerOAD OAD
        {
            get
            {
                if (oad == null)
                {
                    if (conexao == null)
                        oad = new SqlServerOAD();
                    else
                        oad = new SqlServerOAD(conexao);
                }
                return oad;
            }
            set
            {
                oad = value;
            }
        }

        public BaseDAL()
        {
        }

        public BaseDAL(ConnectionStringSettings conn)
        {
            conexao = conn;
        }

        public void Dispose()
        {
            this.oad.Dispose();
            this.reader.Dispose();
        }
    }
}
