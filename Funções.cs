using AutomacaoBDD.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutomacaoBDD.Functions
{
    public class Funções
    {
        public static List<string> RetornaFaseProposta(String proposta)
        {
            string retornaFaseProposta = "SELECT TOP 1 MPNRDECI FROM  BSOAUTORIZ..CMOVP WHERE MPNRPROP = @proposta".Replace("@proposta", proposta);
            List<string> resultado = new List<string>(DatabaseFactory.DBRetornarDadosQuery(retornaFaseProposta));

            if (resultado != null)
            {
                return resultado;
            }
            else
            {
                return null;
            }            
        }
        protected static string RetornaTaxa(string value)
        {
            string regex = "[0-9]{0,15}[,]{0,1}[0-9]{0,4}$";
            MatchCollection matches = new Regex(regex).Matches(value);

            foreach (Match m in matches)
            {
                return m.Groups[0].Value;
            }

            return null;
        }
        public static double RecuperarMaiorTaxaProduto(string convenio)
        {
            TaxaConvenio.Negocio.ProdutoSimulacaoNegocio negocio = new TaxaConvenio.Negocio.ProdutoSimulacaoNegocio();

            var produtosConvenio = negocio.RecuperarProdutosPort(convenio);

            Tuple<string, double, int, int> produtoPortTaxaMaior = negocio.EncontrarMaiorTaxa(produtosConvenio);

            double taxaConvenio = produtoPortTaxaMaior.Item2;
            int prazoConvenio = produtoPortTaxaMaior.Item3;

            return taxaConvenio;
        }
        public static void CriarPrintEvidencias(string numProposta, string pasta)
        {
            if (numProposta == null || numProposta == "")
            {
                var imagem = GeneralHelpers.TakeScreenshot(pasta);
            }
            else
            {
                var imagem = Screenshot.TakeScreenshotProposta(numProposta);
                //Functions.GerarDocumentoWord.CriaDocumentoEvidenciaProposta(imagem, numProposta);
            }
        }
        public static string RetornaNumProposta(string value)
        {
            string regex = "[0-9]{9}";
            MatchCollection matches = new Regex(regex).Matches(value);

            foreach (Match m in matches)
            {
                return m.Groups[0].Value;
            }

            return null;
        }
    }
}
