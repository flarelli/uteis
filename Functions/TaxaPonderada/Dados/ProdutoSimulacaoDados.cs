using System;
using System.Collections.Generic;
using System.Data;
using TaxaConvenio.Dados.OAD;
using TaxaConvenio.Model;

namespace TaxaConvenio.Dados
{
    public class ProdutoSimulacaoDados : BaseDAL
    {
        public ProdutoSimulacaoModel RecuperarParametrosProduto(string codigoProduto)
        {
            using (OAD)
            {
                ProdutoSimulacaoModel parametros = new ProdutoSimulacaoModel();

                OAD.Texto = ConstantesSQL.RecuperarParametrosTaxaProduto;
                OAD.AdicionaParametro("codProduto", codigoProduto, SqlDbType.VarChar);

                DataTable dt = OAD.Select();

                if (dt.Rows.Count > 0)
                {
                    parametros.CodigoConvenio = dt.Rows[0]["COD_CONVENIO"].ToString();
                    parametros.CodigoProduto = dt.Rows[0]["COD_PRODUTO"].ToString();
                    parametros.NomeProduto = dt.Rows[0]["NOME_PRODUTO"].ToString();
                    parametros.Descricao = string.Format("{0} - {1}", dt.Rows[0]["COD_PRODUTO"].ToString(), dt.Rows[0]["NOME_PRODUTO"].ToString());
                    parametros.CodigoTipoProduto = dt.Rows[0]["COD_TIPO_PRODUTO"].ToString();
                    parametros.TipoPrecificacao = dt.Rows[0]["TIPO_PRECIFICACAO"].ToString();
                    parametros.Faixa1_Max = Convert.ToInt32(dt.Rows[0]["FAIXA1_MAX"] != System.DBNull.Value ? dt.Rows[0]["FAIXA1_MAX"] : "0");
                    parametros.Faixa1_Min = Convert.ToInt32(dt.Rows[0]["FAIXA1_MIN"] != System.DBNull.Value ? dt.Rows[0]["FAIXA1_MIN"] : "0");
                    parametros.Faixa2_Max = Convert.ToInt32(dt.Rows[0]["FAIXA2_MAX"] != System.DBNull.Value ? dt.Rows[0]["FAIXA2_MAX"] : "0");
                    parametros.Faixa2_Min = Convert.ToInt32(dt.Rows[0]["FAIXA2_MIN"] != System.DBNull.Value ? dt.Rows[0]["FAIXA2_MIN"] : "0");
                    parametros.Faixa3_Max = Convert.ToInt32(dt.Rows[0]["FAIXA3_MAX"] != System.DBNull.Value ? dt.Rows[0]["FAIXA3_MAX"] : "0");
                    parametros.Faixa3_Min = Convert.ToInt32(dt.Rows[0]["FAIXA3_MIN"] != System.DBNull.Value ? dt.Rows[0]["FAIXA3_MIN"] : "0");
                    parametros.Faixa4_Max = Convert.ToInt32(dt.Rows[0]["FAIXA4_MAX"] != System.DBNull.Value ? dt.Rows[0]["FAIXA4_MAX"] : "0");
                    parametros.Faixa4_Min = Convert.ToInt32(dt.Rows[0]["FAIXA4_MIN"] != System.DBNull.Value ? dt.Rows[0]["FAIXA4_MIN"] : "0");
                    parametros.Faixa5_Max = Convert.ToInt32(dt.Rows[0]["FAIXA5_MAX"] != System.DBNull.Value ? dt.Rows[0]["FAIXA5_MAX"] : "0");
                    parametros.Faixa5_Min = Convert.ToInt32(dt.Rows[0]["FAIXA5_MIN"] != System.DBNull.Value ? dt.Rows[0]["FAIXA5_MIN"] : "0");
                    parametros.Faixa6_Max = Convert.ToInt32(dt.Rows[0]["FAIXA6_MAX"] != System.DBNull.Value ? dt.Rows[0]["FAIXA6_MAX"] : "0");
                    parametros.Faixa6_Min = Convert.ToInt32(dt.Rows[0]["FAIXA6_MIN"] != System.DBNull.Value ? dt.Rows[0]["FAIXA6_MIN"] : "0");
                    parametros.Taxa1 = Convert.ToDouble(dt.Rows[0]["TAXA1"] != System.DBNull.Value ? dt.Rows[0]["TAXA1"] : "0");
                    parametros.Taxa2 = Convert.ToDouble(dt.Rows[0]["TAXA2"] != System.DBNull.Value ? dt.Rows[0]["TAXA2"] : "0");
                    parametros.Taxa3 = Convert.ToDouble(dt.Rows[0]["TAXA3"] != System.DBNull.Value ? dt.Rows[0]["TAXA3"] : "0");
                    parametros.Taxa4 = Convert.ToDouble(dt.Rows[0]["TAXA4"] != System.DBNull.Value ? dt.Rows[0]["TAXA4"] : "0");
                    parametros.Taxa5 = Convert.ToDouble(dt.Rows[0]["TAXA5"] != System.DBNull.Value ? dt.Rows[0]["TAXA5"] : "0");
                    parametros.Taxa6 = Convert.ToDouble(dt.Rows[0]["TAXA6"] != System.DBNull.Value ? dt.Rows[0]["TAXA6"] : "0");
                }

                return parametros;
            }
        }

        public ProdutoSimulacaoModel[] RecuperarParametrosProdutosPort(string codigoConvenio)
        {
            using (OAD)
            {
                List<ProdutoSimulacaoModel> parametros = new List<ProdutoSimulacaoModel>();

                OAD.Texto = ConstantesSQL.RecuperarParametrosTaxaProdutosPort;
                OAD.AdicionaParametro("CodConvenio", codigoConvenio, SqlDbType.VarChar);

                DataTable dt = OAD.Select();

                foreach (DataRow row in dt.Rows)
                {
                    ProdutoSimulacaoModel p = new ProdutoSimulacaoModel();

                    p.CodigoConvenio = row["COD_CONVENIO"].ToString();
                    p.CodigoProduto = row["COD_PRODUTO"].ToString();
                    p.NomeProduto = row["NOME_PRODUTO"].ToString();
                    p.Descricao = string.Format("{0} - {1}", row["COD_PRODUTO"].ToString(), row["NOME_PRODUTO"].ToString());
                    p.CodigoTipoProduto = row["COD_TIPO_PRODUTO"].ToString();
                    p.TipoPrecificacao = row["TIPO_PRECIFICACAO"].ToString();
                    p.Faixa1_Max = Convert.ToInt32(row["FAIXA1_MAX"] != System.DBNull.Value ? row["FAIXA1_MAX"] : "0");
                    p.Faixa1_Min = Convert.ToInt32(row["FAIXA1_MIN"] != System.DBNull.Value ? row["FAIXA1_MIN"] : "0");
                    p.Faixa2_Max = Convert.ToInt32(row["FAIXA2_MAX"] != System.DBNull.Value ? row["FAIXA2_MAX"] : "0");
                    p.Faixa2_Min = Convert.ToInt32(row["FAIXA2_MIN"] != System.DBNull.Value ? row["FAIXA2_MIN"] : "0");
                    p.Faixa3_Max = Convert.ToInt32(row["FAIXA3_MAX"] != System.DBNull.Value ? row["FAIXA3_MAX"] : "0");
                    p.Faixa3_Min = Convert.ToInt32(row["FAIXA3_MIN"] != System.DBNull.Value ? row["FAIXA3_MIN"] : "0");
                    p.Faixa4_Max = Convert.ToInt32(row["FAIXA4_MAX"] != System.DBNull.Value ? row["FAIXA4_MAX"] : "0");
                    p.Faixa4_Min = Convert.ToInt32(row["FAIXA4_MIN"] != System.DBNull.Value ? row["FAIXA4_MIN"] : "0");
                    p.Faixa5_Max = Convert.ToInt32(row["FAIXA5_MAX"] != System.DBNull.Value ? row["FAIXA5_MAX"] : "0");
                    p.Faixa5_Min = Convert.ToInt32(row["FAIXA5_MIN"] != System.DBNull.Value ? row["FAIXA5_MIN"] : "0");
                    p.Faixa6_Max = Convert.ToInt32(row["FAIXA6_MAX"] != System.DBNull.Value ? row["FAIXA6_MAX"] : "0");
                    p.Faixa6_Min = Convert.ToInt32(row["FAIXA6_MIN"] != System.DBNull.Value ? row["FAIXA6_MIN"] : "0");
                    p.Taxa1 = Convert.ToDouble(row["TAXA1"] != System.DBNull.Value ? row["TAXA1"] : "0");
                    p.Taxa2 = Convert.ToDouble(row["TAXA2"] != System.DBNull.Value ? row["TAXA2"] : "0");
                    p.Taxa3 = Convert.ToDouble(row["TAXA3"] != System.DBNull.Value ? row["TAXA3"] : "0");
                    p.Taxa4 = Convert.ToDouble(row["TAXA4"] != System.DBNull.Value ? row["TAXA4"] : "0");
                    p.Taxa5 = Convert.ToDouble(row["TAXA5"] != System.DBNull.Value ? row["TAXA5"] : "0");
                    p.Taxa6 = Convert.ToDouble(row["TAXA6"] != System.DBNull.Value ? row["TAXA6"] : "0");

                    parametros.Add(p);
                }

                return parametros.ToArray();
            }
        }
    }
}
