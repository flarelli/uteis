using AutomacaoBDD.Helpers;
using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using AutomacaoBDD.Functions;
using System.Threading;

namespace AutomacaoBDD.Pages.SimuladorPortabilidade
{
    public class AnexarDocumentacaoPage : PageBase
    {

        #region Mapeamento dos elementos da tela
        IWebElement nomeDocumento1 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_1_lblNomeDocumento"));

        IWebElement nomeDocumento2 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_2_lblNomeDocumento"));

        IWebElement nomeDocumento3 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_3_lblNomeDocumento"));

        IWebElement nomeDocumento4 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_4_lblNomeDocumento"));

        IWebElement nomeDocumento5 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_5_lblNomeDocumento"));

        IWebElement nomeDocumento6 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_6_lblNomeDocumento"));

        IWebElement nomeDocumento7 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_7_lblNomeDocumento"));

        IWebElement nomeDocumento8 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_8_lblNomeDocumento"));

        IWebElement btnAnexar1 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_1_btnAnexar"));

        IWebElement btnEscolherArquivo1 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_1_fupArquivo"));

        IWebElement btnSalvarAnexo1 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_1_btnSalvarAnexo"));

        IWebElement btnCancelarAnexo1 => DriverFactory.INSTANCE.FindElement(By.XPath("//td[@id='MainContent_1_tdFileUpload']//input[@id='btnCancelarAnexo']"));

        IWebElement btnAnexar2 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_2_btnAnexar"));

        IWebElement btnEscolherArquivo2 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_2_fupArquivo"));

        IWebElement btnSalvarAnexo2 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_2_btnSalvarAnexo"));

        IWebElement btnCancelarAnexo2 => DriverFactory.INSTANCE.FindElement(By.XPath("//td[@id='MainContent_2_tdFileUpload']//input[@id='btnCancelarAnexo']"));

        IWebElement btnAnexar3 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_3_btnAnexar"));

        IWebElement btnEscolherArquivo3 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_3_fupArquivo"));

        IWebElement btnSalvarAnexo3 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_3_btnSalvarAnexo"));

        IWebElement btnCancelarAnexo3 => DriverFactory.INSTANCE.FindElement(By.Id("//td[@id='MainContent_3_tdFileUpload']//input[@id='btnCancelarAnexo']"));

        IWebElement btnAnexar4 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_4_btnAnexar"));

        IWebElement btnEscolherArquivo4 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_4_fupArquivo"));

        IWebElement btnSalvarAnexo4 => DriverFactory.INSTANCE.FindElement(By.Id("MainContent_4_btnSalvarAnexo"));

        IWebElement btnCancelarAnexo4 => DriverFactory.INSTANCE.FindElement(By.Id("//td[@id='MainContent_4_tdFileUpload']//input[@id='btnCancelarAnexo']"));

        IWebElement btnSalvarObservacao => DriverFactory.INSTANCE.FindElement(By.Id("btnSalvarObservacao"));

        IWebElement btnCancelarObservacao => DriverFactory.INSTANCE.FindElement(By.Id("btnCancelarObservacao"));

        IWebElement btnAprovar => DriverFactory.INSTANCE.FindElement(By.Id("btnAprovar"));

        IWebElement btnReprovar => DriverFactory.INSTANCE.FindElement(By.Id("btnReprovar"));

        IWebElement btnVoltar => DriverFactory.INSTANCE.FindElement(By.Id("btnVoltar"));

        IWebElement linkCartaoAnexo => DriverFactory.INSTANCE.FindElement(By.Id("linkCartao"));

        IWebElement btnGravarAnexos => DriverFactory.INSTANCE.FindElement(By.Id("btnVoltar"));

        #endregion


        #region Ações Anexar Documentação

        public static string numProposta { set; get; }

        public void ValidaMensagemCartaoAnexar(string mensagem)
        {
            if (mensagem == null || mensagem == "")
            {
                Console.WriteLine("CPF sem disponibilidade de cartão e/ou usuário não é correspondente/atendente.");
            }
            else if (mensagem == RetornaTextLabel(By.Id("divCartaoCorrespondente")))
            {
                Click(linkCartaoAnexo);
                var telacartao = "BEM-VINDO AO";
                Switch();
                if (RetornaTextLabel(By.XPath("//*[@id='wrapper']/section/div/div[1]/div/div/h1/span")) == telacartao)
                {
                    Console.WriteLine("Direcionamento correto para o Olá");
                }
            }
            else if (mensagem == RetornaTextLabel(By.Id("divCartaoAtendente")))
            {
                Console.WriteLine("Mensagem correta do atendente.");
            }
            RetornaPrimeiraPagina();
        }

        public void AnexaDocumento()
        {
            List<string> texto = new List<string>();
            List<string> botaoanexar = new List<string>();
            List<string> inserirarquivo = new List<string>();
            List<string> salvar = new List<string>();

            if (CampoOculto(By.Id("MainContent_1_lblNomeDocumento")) == false)
            {
                texto.Add(RetornaTextLabel(By.Id("MainContent_1_lblNomeDocumento")).Trim());
                botaoanexar.Add("MainContent_1_btnAnexar");
                inserirarquivo.Add("MainContent_1_fupArquivo");
                salvar.Add("MainContent_1_btnSalvarAnexo");
            }
            if (CampoOculto(By.Id("MainContent_2_lblNomeDocumento")) == false)
            {
                texto.Add(RetornaTextLabel(By.Id("MainContent_2_lblNomeDocumento")).Trim());
                botaoanexar.Add("MainContent_2_btnAnexar");
                inserirarquivo.Add("MainContent_2_fupArquivo");
                salvar.Add("MainContent_2_btnSalvarAnexo");
            }
            if (CampoOculto(By.Id("MainContent_3_lblNomeDocumento")) == false)
            {
                texto.Add(RetornaTextLabel(By.Id("MainContent_3_lblNomeDocumento")).Trim());
                botaoanexar.Add("MainContent_3_btnAnexar");
                inserirarquivo.Add("MainContent_3_fupArquivo");
                salvar.Add("MainContent_3_btnSalvarAnexo");
            }
            if (CampoOculto(By.Id("MainContent_4_lblNomeDocumento")) == false)
            {
                texto.Add(RetornaTextLabel(By.Id("MainContent_4_lblNomeDocumento")).Trim());
                botaoanexar.Add("MainContent_4_btnAnexar");
                inserirarquivo.Add("MainContent_4_fupArquivo");
                salvar.Add("MainContent_4_btnSalvarAnexo");
            }

            for (int y = 0; y < texto.Count; ++y)
            {
                if (texto[y].ToString().Contains("Obrigatório") == true)
                {
                    DriverFactory.INSTANCE.FindElement(By.Id(botaoanexar[y])).Click();
                    var arquivo = DriverFactory.INSTANCE.FindElement(By.Id(inserirarquivo[y]));
                    arquivo.SendKeys("C:/Users/OTC90814/Pictures/validado.jpg");
                    DriverFactory.INSTANCE.FindElement(By.Id(salvar[y])).Click();
                    Thread.Sleep(1000);
                }
            }
        }

        public void ClicarEmGravarAnexação()
        {
            Click(btnGravarAnexos);
        }
    }
}


    #endregion


