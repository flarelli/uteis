using AutomacaoBDD.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace AutomacaoBDD.Functions
{
    public class Screenshot
    {
        private static string fileName;
        private static string nomeArquivoCompleto;
        public static string localArquivoCompleto;
        public static string primeiroArquivo;
        private static List<string> arquivos = new List<string>();

        public static string TakeScreenshotProposta(string numProposta)
        {
            var feature = FeatureContext.Current.FeatureInfo.Title.ToString().Trim().Replace(" ", "");
            var scenario = ScenarioContext.Current.ScenarioInfo.Title.ToString().Trim().Replace(" ", "");

            var tempo = string.Format("{0:dd-MM-yyyy-hh-mm-ss}", DateTime.Now);
            var dateTime = GeneralHelpers.GetCurrentDate("dd-MM-yyyy");
            var localArquivoBase = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "Evidências");

            localArquivoCompleto = string.Format("{0}\\{1}\\{2}\\{3}", localArquivoBase, feature, scenario, dateTime);

            if (!Directory.Exists(localArquivoCompleto))
            {
                Directory.CreateDirectory(localArquivoCompleto);
            }

            ScreenshotImageFormat extensaoArquivo = ScreenshotImageFormat.Jpeg;

            if (numProposta != null)
            {
                arquivos.AddRange(Directory.GetFiles(localArquivoCompleto));

                var nome = localArquivoCompleto + "\\" + numProposta + ".Jpeg";
                if (arquivos.Contains(nome))
                {
                    fileName = null;
                }
                else
                {
                    fileName = numProposta;
                }                
            }
            else
            {
                List<string> arquivos = new List<string>(Directory.GetFiles(localArquivoCompleto));
                var nome = localArquivoCompleto + "\\" + "Print" + tempo + ".Jpeg";
                if (arquivos.Contains(nome))
                {
                    fileName = null;
                }
                else
                {
                    fileName = "Print" + tempo;
                }
                arquivos.Clear();
            }

            if(fileName != null)
            {
                nomeArquivoCompleto = string.Format("{0}\\{1}.{2}", localArquivoCompleto, fileName, extensaoArquivo.ToString());

                try
                {
                    ((ITakesScreenshot)DriverFactory.INSTANCE)
                        .GetScreenshot()
                        .SaveAsFile(nomeArquivoCompleto, extensaoArquivo);
                }
                catch (Exception ex)
                {
                    var mensagemErro = string.Format("{0}\r\n{1}\r\n\r\n{2}",
                        "Ocorreu um erro ao tirar o print da evidência.",
                        ex.Message,
                        ex.StackTrace);

                    Console.WriteLine(mensagemErro);
                }
            }
            
            return nomeArquivoCompleto;
        }
    }
}
