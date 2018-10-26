using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace AutomacaoBDD.Helpers
{
    public class DatabaseFactory
    {

        private static SqlConnection GetDBConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            SqlConnection connection = new SqlConnection(connectionString);

            return connection;
        }

        public static List<List<string>> DBRetornarListaDadosQuery(String query)
        {

            DataSet ds = new DataSet();
            List<string> lista = new List<string>();
            List<List<string>> resultado = new List<List<string>>();

            using (SqlCommand cmd = new SqlCommand(query, GetDBConnection()))
            {
                cmd.CommandTimeout = Int32.Parse("60");
                cmd.Connection.Open();
                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader());
                ds.Tables.Add(table);
                cmd.Connection.Close();
            }

            if (ds.Tables[0].Columns.Count == 0)
            {
                return null;
            }

            try
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        lista.Add(ds.Tables[0].Rows[i][j].ToString());
                    }
                    resultado.Add(lista);
                    lista = new List<string>();
                }
            }
            catch (Exception)
            {
                return null;
            }
            return resultado;
        }    

        public static List<string> DBRetornarDadosQuery(String query)
        {

            DataSet ds = new DataSet();
            List<string> lista = new List<string>();

            using (SqlCommand cmd = new SqlCommand(query, GetDBConnection()))
            {
                cmd.CommandTimeout = Int32.Parse("60");
                cmd.Connection.Open();
                DataTable table = new DataTable();
                table.Load(cmd.ExecuteReader());
                ds.Tables.Add(table);
                cmd.Connection.Close();
            }

            if (ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            try
            {
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {                    
                    lista.Add(ds.Tables[0].Rows[0][i].ToString());                    
                }
            }
            catch (Exception)
            {
                return null;
            }
            return lista;
        }        

        public static void DBExecutarQuery(String query)
        {
            using (SqlCommand cmd = new SqlCommand(query, GetDBConnection()))
            {
                try
                {
                    cmd.CommandTimeout = Int32.Parse(ConfigurationManager.AppSettings["DBConnectionTimeout"]);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
                finally
                {
                    cmd.Connection.Close();
                }
            }
        }
    }
}