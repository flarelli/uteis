using AutomacaoBDD.Helpers;
using AutomacaoBDD.Pages.FuncaoWeb.AndamentoEsteira;
using System;
using TechTalk.SpecFlow;

namespace AutomacaoBDD.StepsDefinitions.FuncaoWeb.LoginFuncaoWebSteps
{
    [Binding]
    public class LoginFuncaoWebSteps : TestBase
    {
        FuncaoWebLoginPage _funcaowebloginpage = new FuncaoWebLoginPage();

        
        [Given(@"que eu acesso a tela FuncaoWeb")]
        public void DadoQueEuAcessoATelaFuncaoWeb()
        {
            _funcaowebloginpage.AbrirPaginaFuncaoWeb();
        }
        
        [When(@"informo o usuario e a senha")]
        public void QuandoInformoOUsuarioEASenha(Table table)
        {
            var dictionary = GeneralHelpers.ToDictionary(table);
            _funcaowebloginpage.PreencherLogin(dictionary["usuario"]);
            _funcaowebloginpage.PreencherSenha(dictionary["senha"]);
        }
        
        [When(@"aciono o Entrar")]
        public void QuandoAcionoOEntrar()
        {
            _funcaowebloginpage.ClicarEntrar();
        }
        
        [Then(@"o usuario deve ser autenticado corretamente")]
        public void EntaoOUsuarioDeveSerAutenticadoCorretamente()
        {
            if (_funcaowebloginpage.RetornaUsuarioLogado() == _funcaowebloginpage.usuarioinformado)
                Console.WriteLine("Usuario Logou corretamente");
        }
    }
}
