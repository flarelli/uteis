using Xceed.Words.NET;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using System;
using System.IO;
using OpenQA.Selenium.Remote;
using AutomacaoBDD.Helpers;

namespace AutomacaoBDD.Functions.GeradorDocumentoWord
{
    public class GerarDocumentoWord
    {
        public static string nomeArquivo;


        public static void CriaDocumento(string localImagem, string nomeDoc)
        {
            var feature = FeatureContext.Current.FeatureInfo.Title.ToString().Trim().Replace(" ", "");
            var scenario = ScenarioContext.Current.ScenarioInfo.Title.ToString().Trim().Replace(" ", "");

            var tempo = string.Format("{0:dd-MM-yyyy-hh-mm-ss}", DateTime.Now);
            var dateTime = GeneralHelpers.GetCurrentDate("dd-MM-yyyy");
            var localArquivoBase = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "Evidências");
            var localArquivosDeletados = AppDomain.CurrentDomain.BaseDirectory.Replace("bin\\Debug\\", "Arquivos-Deletados"+"\\"+ tempo);

            var localArquivoCompleto = string.Format("{0}\\{1}\\{2}\\{3}", localArquivoBase, feature, scenario, dateTime);
            
            if (!Directory.Exists(localArquivosDeletados))
            {
                Directory.CreateDirectory(localArquivosDeletados);
            }

            if (nomeDoc == null)
            {
                nomeDoc = tempo;

                List<string> arquivos = new List<string>(Directory.GetFiles(localImagem));
                if (arquivos.Contains(nomeDoc + ".docx"))
                {
                    for (int i = 0; i <= arquivos.Count; i++)
                    {
                        using (FileStream fs = new FileStream(nomeDoc + ".docx", FileMode.Open))
                        {
                            //abre um documento
                            using (var document = DocX.Load(fs))
                            {
                                //Instancia a imagem ao Xceed.Words.NET
                                Xceed.Words.NET.Image img = document.AddImage(arquivos[i]);

                                // Cria um tipo Picture desta imagem
                                Picture pic = img.CreatePicture(400, 600);

                                //Insere e configura a imagem
                                Paragraph p0 = document.InsertParagraph();

                                p0.InsertPicture(pic, 0)
                                    .SpacingAfter(30)
                                    .Alignment = Alignment.center;

                                //salva o documento


                                nomeArquivo = localArquivoBase + "\\" + nomeDoc + ".docx";
                                document.SaveAs(nomeArquivo);
                            }
                        }
                    }
                }
                else
                {
                    //cria um novo documento
                    using (var document = DocX.Create(nomeDoc + ".docx"))
                    {
                        document.InsertParagraph("Teste: " + nomeDoc
                            )
                            .FontSize(15d)
                            .Bold()
                            .SpacingAfter(50d)
                            .Alignment = Alignment.left;

                        for (int i = 0; i < arquivos.Count; i++)
                        {
                            //Instancia a imagem ao Xceed.Words.NET
                            Xceed.Words.NET.Image img = document.AddImage(arquivos[i]);

                            // Cria um tipo Picture desta imagem
                            Picture pic = img.CreatePicture(400, 600);

                            //Insere e configura a imagem
                            Paragraph p0 = document.InsertParagraph();

                            p0.InsertPicture(pic, 0)
                                .SpacingAfter(30)
                                .Alignment = Alignment.center;

                            //salva o documento
                            nomeArquivo = localArquivoCompleto + "\\" + nomeDoc + ".docx";
                            document.SaveAs(nomeArquivo);

                            Directory.Move(arquivos[i], localArquivosDeletados);
                        }
                    }
                }

            }
        }
    }
}
