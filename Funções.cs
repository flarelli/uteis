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
        
        protected static string RetornaDouble(string value)
        {
            string regex = "[0-9]{0,15}[,]{0,1}[0-9]{0,4}$";
            MatchCollection matches = new Regex(regex).Matches(value);

            foreach (Match m in matches)
            {
                return m.Groups[0].Value;
            }

            return null;
        }
        
        public static void CriarPrintEvidencias(string x, string pasta)
        {
            if (x == null || x == "")
            {
                var imagem = GeneralHelpers.TakeScreenshot(pasta);
            }
            else
            {
                var imagem = Screenshot.TakeScreenshot(x);
                
            }
        }
        public static string RetornaValor(string value)
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
