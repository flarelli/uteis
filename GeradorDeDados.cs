using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomacaoBDD.Functions.GeradorDeDadosTestes
{
    public static class GeradorDeDados
    {
        public static string GerarCpf()
        {

            var random = new Random();
            string semente;

            do
            {
                semente = random.Next(1, 999999999).ToString().PadLeft(9, '0');
            }
            while (
                semente == "000000000"
                || semente == "111111111"
                || semente == "222222222"
                || semente == "333333333"
                || semente == "444444444"
                || semente == "555555555"
                || semente == "666666666"
                || semente == "777777777"
                || semente == "888888888"
                || semente == "999999999"
            );

            semente += CalcularDigitoVerificador(semente).ToString();
            semente += CalcularDigitoVerificador(semente).ToString();
            return semente;

        }

        public static int CalcularDigitoVerificador(string semente)
        {
            int soma = 0;
            int resto = 0;
            int[] multiplicadores = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int iFinal = multiplicadores.Count();
            var iInicial = iFinal - semente.Length;

            for (int i = iInicial; i < iFinal; i++)
                soma += int.Parse(semente[i - iInicial].ToString()) * multiplicadores[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            return resto;

        }
    }
}

