using AutomacaoBDD.Helpers;
using AutomacaoBDD.Pages.SimuladorPortabilidade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomacaoBDD.Functions.TaxaPonderada
{
    public class TaxaPonderada
    {
        public static double CalculoTaxaPonderada(string convenio, double taxapreenchida, string qntParcelasContratoPortado, string qntParcelasTotalContratoPortado)
        {
            double taxaMaxima = Funções.RecuperarMaiorTaxaProduto(convenio);
            List<string> resultado = new List<string>(DatabaseFactory.DBRetornarDadosQuery(Scripts.retornatotalparc.Replace("@convenio", convenio)));

            int prazoMaximo = Int32.Parse(resultado[0]);
            int totalparcelascontratoportado = Int32.Parse(qntParcelasTotalContratoPortado);
            double prazoContratoPortado = Double.Parse(qntParcelasContratoPortado);

            double prazoLiquido = (totalparcelascontratoportado - prazoContratoPortado);
            double taxaPonderadaManual = ((taxaMaxima * prazoLiquido) + (taxapreenchida * prazoContratoPortado)) / prazoMaximo;

            return taxaPonderadaManual;
        }
        public static List<List<string>> RetornaLogChamadaAPITaxaPonderada()
        {
            List<List<string>> resultado = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.verificaLogAPITaxaPonderada);
            return resultado;
        }
    }
}
