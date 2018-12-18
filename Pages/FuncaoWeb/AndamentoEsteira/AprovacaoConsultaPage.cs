using AutomacaoBDD.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomacaoBDD.Pages.FuncaoWeb.AndamentoEsteira
{
    public class AprovacaoConsultaPage : PageBase
    {
        #region Mapeamento Campos Tela Consulta

        IWebElement btnlocalizar => DriverFactory.INSTANCE.FindElement(By.Id("BBLOCALIZAR"));

        IWebElement btnfiltro => DriverFactory.INSTANCE.FindElement(By.Id("BBFILTRO"));

        IWebElement btnatualizar => DriverFactory.INSTANCE.FindElement(By.Id("BBATUALIZA"));

        IWebElement btnvoltar => DriverFactory.INSTANCE.FindElement(By.Id("BBVOLTAR"));

        IWebElement txtdescproposta => DriverFactory.INSTANCE.FindElement(By.Id("EPESQ"));

        IWebElement rbopcaopornome => DriverFactory.INSTANCE.FindElement(By.XPath("//*[@id='PESQUISA']/input[1]"));

        IWebElement rbopcaoporproposta => DriverFactory.INSTANCE.FindElement(By.XPath("//*[@id='PESQUISA']/input[2]"));

        IWebElement rbopcaopordatabase => DriverFactory.INSTANCE.FindElement(By.XPath("//*[@id='PESQUISA']/input[3]"));

        IWebElement rbopcaoporcpfcnpj => DriverFactory.INSTANCE.FindElement(By.XPath("//*[@id='PESQUISA']/input[4]"));

        IWebElement btnpesquisar => DriverFactory.INSTANCE.FindElement(By.Id("IWBUTTON2"));

        IWebElement linkproposta => DriverFactory.INSTANCE.FindElement(By.CssSelector("[href*='#']"));

        IWebElement btnaprovar => DriverFactory.INSTANCE.FindElement(By.Id("BBAPROVA"));

        IWebElement btnenviar => DriverFactory.INSTANCE.FindElement(By.Id("BBENVIAR"));
        IWebElement btnaprovacaodetalhada => DriverFactory.INSTANCE.FindElement(By.Id("BTNAPROVAR"));
        

        #endregion

        #region Ações Menu

        public static string propostainformada { set; get; }
        public void AcessaMenuEsteira()
        {
            MoveToElement(By.Id("IWMENU1_submenu2"));
        }

        public void AcessaSubMenuAprovacaoConsulta()
        {
            MoveToElement(By.Id("IWMENU1_submenu30"));
        }
        
        public void ClicarLocalizar()
        {
            Click(btnlocalizar);
        }
        
        public void ClicarVoltar()
        {
            Click(btnvoltar);
        }

        public void EscolherChavePesquisa(string chave)
        {
            if (chave == "Nome do Cliente")
                Click(rbopcaopornome);
            else if (chave == "Proposta")
                Click(rbopcaoporproposta);
            else if (chave == "Data Base")
                Click(rbopcaopordatabase);
            else if (chave == "CPF")
                Click(rbopcaoporcpfcnpj);
        }
        public void PreencherDescPesquisaProposta(string proposta)
        {
            Click(txtdescproposta);
            SendKey(txtdescproposta,proposta);
            propostainformada = proposta;
        }

        public void ClicarPesquisarProposta()
        {
            Click(btnpesquisar);
        }
        public void ClicarPropostaEncontrada()
        {
            Click(linkproposta);
        }
        public void ClicarProposta(string faseatual)
        {
            IWebElement linkfase = DriverFactory.INSTANCE.FindElement(By.PartialLinkText(("@fase").Replace("@fase",faseatual)));
            Click(linkfase);
        }
        public void MarcarAFase(string faseatual)
        {
            IWebElement linkfase2 = DriverFactory.INSTANCE.FindElement(By.PartialLinkText(("@fase").Replace("@fase", faseatual)));
            Click(linkfase2);
        }

        public bool BotaoAprovacaoEstaHabilitado()
        {
            if (IsVisible(By.Id("BBAPROVA")) == true)
                return true;
            else return false;
        }
        public void AprovaFase()
        {
            if (IsVisible(By.Id("BBAPROVA")) == true)
                Click(btnaprovar);
        }
        public void EnviaDocumentacao()
        {
            if (IsVisible(By.Id("BBENVIAR")) == true)
                Click(btnenviar);
        }
        public bool FaseDetalhadaEstaHabilitada()
        {
            if (IsVisible(By.Id("BTNAPROVAR")) == true)
                return true;
            return false;
        }
        public void AprovaFaseDetalhada()
        {
            if (IsVisible(By.Id("BTNAPROVAR")) == true)
                Click(btnaprovacaodetalhada);
        }
        public void SairDaProposta()
        {
            if (IsVisible(By.Id("BBVOLTAR")) == true)
                Click(btnvoltar);
        }
        #endregion
    }
}
