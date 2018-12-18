using AutomacaoBDD.Helpers;
using AutomacaoBDD.Functions;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace AutomacaoBDD.Pages.FuncaoWeb.AndamentoEsteira
{
    public class FuncaoWebLoginPage : PageBase
    {

        #region Mapeamento dos campos

        IWebElement txtlogin => DriverFactory.INSTANCE.FindElement(By.Id("EUSUARIO"));

        IWebElement txtsenha => DriverFactory.INSTANCE.FindElement(By.Id("ESENHA"));

        IWebElement btnEntrar => DriverFactory.INSTANCE.FindElement(By.Id("LKENTRAR"));

        IWebElement usuariologado => DriverFactory.INSTANCE.FindElement(By.Id("FMENUPFL1LUSU"));

        #endregion

        #region Ações


        public string usuarioinformado { set; get; }

        public void AbrirPaginaFuncaoWeb()
        {
            NavigateTo(ConfigurationManager.AppSettings["FuncaoWeb"]);
        }
        public void PreencherLogin(string login)
        {
            Click(txtlogin);
            SendKey(txtlogin, login);
            usuarioinformado = login;
        }

        public void PreencherSenha(string senha)
        {
            Click(txtsenha);
            SendKey(txtsenha, senha);
        }

        public void ClicarEntrar()
        {
            Click(btnEntrar);
        }

        public string RetornaUsuarioLogado()
        {
            string user = PageBase.RetornaTextLabel(By.Id("FMENUPFL1LUSU"));
            return user;
        }
        #endregion
    }
}
