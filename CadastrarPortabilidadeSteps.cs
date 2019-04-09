using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AutomacaoBDD.Helpers;
using AutomacaoBDD.Functions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using System.Collections.Generic;

namespace AutomacaoBDD.StepsDefinitions
{
    [Binding]
    public class Steps : TestBase
    {
        [AutoInstance]
        CadastrarPage _cadastrarPage;

        [AutoInstance]
        AnexarDocumentacaoPage _anexardocumentacaoPage;

        [Given(@"")]
        public void DadoQueAcessoATelaCadastrar()
        {
            _cadastrarPage.AbrirPaginaCadastro();
        }

        [Given(@"que exibem somente produtos")]
        public void DadoQueExibemSomenteProdutos()
        {
            List<string> listaOpcoesCbx = new List<string>(PageBase.GetAllOptionsComboValue(By.Id("")));
            listaOpcoesCbx.RemoveAt(0);

            List<List<string>> x = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.Produtos);

            foreach (var c in produtos)
            {
                if (listaOpcoesCbx.Contains(c[0]) == true)
                    Console.WriteLine(c[0] + " Produto correto");
            }
        }

        [Given(@"que informo cliente e os dados do produto")]
        public void DadoQueInformoClienteEOsDadosDoProduto(Table table)
        {
            var dictionary = GeneralHelpers.ToDictionary(table);

            _cadastrarPage.PreencherProduto(dictionary["produto"]);
            _cadastrarPage.PreencherNome("AUTOMACAO");
            _cadastrarPage.PreencherCPF(dictionary["cpf"]);
            _cadastrarPage.PreencherDataNascimento("02041987");
            _cadastrarPage.PreencherBanco("001");
            _cadastrarPage.PreencherNumContrato("Randon");
            _cadastrarPage.PreencherQntParcelas(dictionary["qtdparcelas"]);
            _cadastrarPage.PreencherVlrParcela(dictionary["vlrparcela"]);
            _cadastrarPage.ClicarRadionButton();
            _cadastrarPage.PreencherTaxa(dictionary["taxa"]);
            _cadastrarPage.PreencherTotalParcProduto(dictionary["totalparcelas"]);
        }

        [Given(@"clico em prosseguir para carregar os produtos")]
        public void DadoClicoEmProsseguirParaCarregarOsProdutos()
        {
            _cadastrarPage.ClicarProsseguirCarregaProdutos();
        }
        [When(@"clico em prosseguir para carregar os produtos")]
        public void QuandoClicoEmProsseguirParaCarregarOsProdutos()
        {
            _cadastrarPage.ClicarProsseguirCarregaProdutos();
        }

        [Given(@"seleciono um produto")]
        public void DadoSelecionoUmProduto()
        {
            _cadastrarPage.SelecionarProduto();            
        }

        [Given(@"realizo a simulacao com sucesso")]
        public void DadoRealizoASimulacaoComSucesso()
        {
            _cadastrarPage.ClicarGravarSimulacao();
        }
        [When(@"seleciono um produto")]
        public void QuandoSelecionoUmProduto()
        {
            _cadastrarPage.SelecionarProduto();
        }

        [Given(@"clico em simular")]
        public void DadoClicoEmSimular()
        {
            _cadastrarPage.ClicarSimular();
        }

        [When(@"clico em simular")]
        public void QuandoClicoEmSimular()
        {
            _cadastrarPage.ClicarSimular();
        }

        
        
    }
}
