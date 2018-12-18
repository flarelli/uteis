using AventStack.ExtentReports.Gherkin.Model;
using AutomacaoBDD.Helpers;
using AutomacaoBDD.Functions;
using AutomacaoBDD.Pages.SimuladorPortabilidade;
using OpenQA.Selenium;
using System;
using System.Configuration;
using TechTalk.SpecFlow;
using System.Collections.Generic;
using AutomacaoBDD.Functions.TaxaPonderada;
using AutomacaoBDD.Functions.GeradorDeDadosTestes;
using System.Threading;

namespace AutomacaoBDD.StepsDefinitions.SimuladorPortabilidade.CadastrarPortabilidadeSteps
{
    [Binding]
    public class CadastrarPortabilidadeSteps : TestBase
    {
        CadastrarPortabilidadePage _cadastrarportabilidadePage = new CadastrarPortabilidadePage();
        
        AnexarDocumentacaoPage _anexardocumentacaoPage = new AnexarDocumentacaoPage();

        [Given(@"que acesso a tela Cadastrar Portabilidade")]
        public void DadoQueAcessoATelaCadastrarPortabilidade()
        {
            _cadastrarportabilidadePage.AbrirPaginaCadastroPortabilidade();
        }

        [Given(@"que exibem somente convenios que permitem Portabilidade")]
        public void DadoQueExibemSomenteConveniosQuePermitemPortabilidade()
        {
            List<string> listaOpcoesCbxConvenio = new List<string>(PageBase.GetAllOptionsComboValue(By.Id("ddlConvenio")));
            listaOpcoesCbxConvenio.RemoveAt(0);

            List<List<string>> conveniosPortabilidade = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.conveniosQuePermitemPortabilidade);

            foreach (var c in conveniosPortabilidade)
            {
                if (listaOpcoesCbxConvenio.Contains(c[0]) == true)
                    Console.WriteLine(c[0] + " Convenio correto");
            }
        }

        [Given(@"que informo convenio cliente e os dados do contrato portado")]
        public void DadoQueInformoConvenioClienteEOsDadosDoContratoPortado(Table table)
        {
            var dictionary = GeneralHelpers.ToDictionary(table);

            _cadastrarportabilidadePage.PreencherConvenio(dictionary["convenio"]);
            _cadastrarportabilidadePage.PreencherNome("AUTOMACAO - Mesa originacao");

            if (dictionary.ContainsKey("cpf"))
                    _cadastrarportabilidadePage.PreencherCPF(dictionary["cpf"]);
                    
            _cadastrarportabilidadePage.PreencherDataNascimento("02041987");
            _cadastrarportabilidadePage.PreencherBancoPortabilidade("001");
            _cadastrarportabilidadePage.PreencherNumContrato("Randon");
            _cadastrarportabilidadePage.PreencherQntParcelas(dictionary["qtdparcelas"]);
            _cadastrarportabilidadePage.PreencherVlrParcela(dictionary["vlrparcela"]);
            _cadastrarportabilidadePage.ClicarRadionButtonTaxa();
            _cadastrarportabilidadePage.PreencherTaxaDoContratoPortado(dictionary["taxaPortada"]);
            _cadastrarportabilidadePage.PreencherTotalParcProduto(dictionary["totalparcelas"]);
        }

        [Given(@"clico em prosseguir para carregar os produtos")]
        public void DadoClicoEmProsseguirParaCarregarOsProdutos()
        {
            _cadastrarportabilidadePage.ClicarProsseguirCarregaProdutos();
        }

        [When(@"clico em prosseguir para carregar os produtos")]
        public void QuandoClicoEmProsseguirParaCarregarOsProdutos()
        {
            _cadastrarportabilidadePage.ClicarProsseguirCarregaProdutos();
        }

        [Then(@"exibe a mensagem de erro ""(.*)""")]
        public void EntaoExibeAMensagemDeErro(string mensagem)
        {
            string msgRetornada = _cadastrarportabilidadePage.RetornarMsgValidacao();
            try
            {
                mensagem.Contains(msgRetornada);
            }
            catch
            {
                Console.WriteLine("Validação não feita corretamente.");
            }                
        }


        [Given(@"seleciono um produto")]
        public void DadoSelecionoUmProduto()
        {
            _cadastrarportabilidadePage.SelecionarProduto();            
        }

        [Given(@"realizo a simulacao com sucesso")]
        public void DadoRealizoASimulacaoComSucesso()
        {
            _cadastrarportabilidadePage.ClicarGravarSimulacao();
        }

        [When(@"seleciono um produto")]
        public void QuandoSelecionoUmProduto()
        {
            _cadastrarportabilidadePage.SelecionarProduto();
        }

        [Given(@"clico em simular")]
        public void DadoClicoEmSimular()
        {
            _cadastrarportabilidadePage.ClicarSimular();
        }

        [When(@"clico em simular")]
        public void QuandoClicoEmSimular()
        {
            _cadastrarportabilidadePage.ClicarSimular();
        }

        [Given(@"deve ser exibida a Taxa Ponderada Calculada")]
        public void DadoDeveSerExibidaATaxaPonderadaCalculada()
        {
            var taxacalculada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

            var taxapreenchida = _cadastrarportabilidadePage.RetornaTaxaPonderada().Replace(".",",");

            if (taxapreenchida.Equals(taxacalculada))
                Console.WriteLine("Taxa Calculada com sucesso!");
        }

        [Then(@"deve ser exibida a Taxa Ponderada Calculada")]
        public void EntaoDeveSerExibidaATaxaPonderadaCalculada()
        {
            var taxacalculada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

            var taxapreenchida = _cadastrarportabilidadePage.RetornaTaxaPonderada();

            if (taxapreenchida.Equals(taxacalculada))
                Console.WriteLine("Taxa Calculada com sucesso!");
        }

        [Given(@"prossigo com cadastro")]
        public void DadoProssigoComCadastro()
        {
            _cadastrarportabilidadePage.PreencherUsuarioPortabilidade("RECOMPRA");
            _cadastrarportabilidadePage.PreencherSenhaPortabilidade(ConfigurationManager.AppSettings["SenhaFuncaoRecompra"]);
            _cadastrarportabilidadePage.ClicarEntrar();
            Thread.Sleep(500);
            _cadastrarportabilidadePage.SelecionarLojaAleatoria();
            _cadastrarportabilidadePage.SelecionarOrgaoAleatorio();
            _cadastrarportabilidadePage.PreencherCodigoBeneficio();
            _cadastrarportabilidadePage.PreencherVersaoFormulario("EMP006");
            _cadastrarportabilidadePage.PreencherRenda("5000,00");
            _cadastrarportabilidadePage.ClicarProsseguir();
            _cadastrarportabilidadePage.SelecionarNacionalidade("B");
            _cadastrarportabilidadePage.PreencherNaturalidade("Belo Horizonte");
            _cadastrarportabilidadePage.SelecionarSexo("F");
            _cadastrarportabilidadePage.SelecionarEstadoCivil("SO");
            _cadastrarportabilidadePage.PreencherRG("MG15487899");
            _cadastrarportabilidadePage.PreencherUfRg("MG");
            _cadastrarportabilidadePage.PreencherOrgaoEmissor("sspmg");
            _cadastrarportabilidadePage.PreencherDataEmissao("02022002");
            _cadastrarportabilidadePage.PreencherCep("30570080");
            _cadastrarportabilidadePage.PreencherNumero("133");
            _cadastrarportabilidadePage.PreencherDDDTel("31");
            _cadastrarportabilidadePage.PreencherNumeroTel("Randon");

            //DatabaseFactory.DBExecutarQuery(Scripts.tornaCelularInformadoViávelPraVendaDigital.Replace("@CelularDigital", ConfigurationManager.AppSettings["CelularDigital"]));

            _cadastrarportabilidadePage.PreencherDDDCel("31");
            _cadastrarportabilidadePage.PreencherNumeroCel(ConfigurationManager.AppSettings["CelularDigital"]);

            //Funções.CriarPrintEvidencias(CadastrarPortabilidadePage.numProposta, "Evidências");

            _cadastrarportabilidadePage.PreencherMatricula("Randon");
            _cadastrarportabilidadePage.PreencherNomePai("AUTOMAÇÃO");
            _cadastrarportabilidadePage.PreencherNomeMae("AUTOMAÇÃO");

            _cadastrarportabilidadePage.PreencherEmail("deinf@oleconsignado.com.br");

            _cadastrarportabilidadePage.SelecionarTipoContaCliente("01");
            _cadastrarportabilidadePage.PreencherBancoCliente("001");
            _cadastrarportabilidadePage.PreencherAgenciaCliente("0250");
            _cadastrarportabilidadePage.PreencherDigitoAgenciaCliente("0");
            _cadastrarportabilidadePage.PreencherContaCliente("124666");
            _cadastrarportabilidadePage.PreencherDigitoContaCliente("2");

            _cadastrarportabilidadePage.ClicarBtnProsseguirDados();

            _cadastrarportabilidadePage.PreencherTipoContaTerceiro("01");
            _cadastrarportabilidadePage.PreencherBancoTerceiro("001");
            _cadastrarportabilidadePage.PreencherAgenciaTerceiro("1957");
            _cadastrarportabilidadePage.PreencherDigitoAgenciaTerceiro("0");
            _cadastrarportabilidadePage.PreencherContaTerceiro("13256");
            _cadastrarportabilidadePage.PreencherDigitoContaTerceiro("6");

            _cadastrarportabilidadePage.PreencherCodAverbacao("31213645");
            _cadastrarportabilidadePage.PreencherCodAutenticacao("2121121");

            _cadastrarportabilidadePage.ClicarProsseguirObservacao();
        }

        [When(@"gravarmos a proposta")]
        public void QuandoGravarmosAProposta()
        {
            _cadastrarportabilidadePage.ClicarProsseguirFinal();
        }

        [Then(@"deve ser gerado o numero de Proposta")]
        public string EntaoDeveSerGeradoONumeroDeProposta()
        {
            _cadastrarportabilidadePage.GeraçãoNumeroProposta();
           
            return CadastrarPortabilidadePage.numProposta;
        }

        [Then(@"realizo a anexacao dos documentos")]
        public void EntaoRealizoAAnexacaoDosDocumentos()
        {
            _cadastrarportabilidadePage.ClicarAnexarNaPortabilidade();
            _anexardocumentacaoPage.AnexaDocumento();
            _anexardocumentacaoPage.ClicarEmGravarAnexação();            
        }

        [Given(@"confirmo que o Tipo de Venda e '(.*)'")]
        public void DadoConfirmoQueOTipoDeVendaE(string tipoVenda)
        {
            _cadastrarportabilidadePage.ClicarTipoVenda(tipoVenda);
        }

        [Given(@"taxa ponderada esta habilitada")]
        public void DadoTaxaPonderadaEstaHabilitada()
        {
            DatabaseFactory.DBExecutarQuery(Scripts.ligaAPITaxaPonderada);
        }

        [Given(@"taxa ponderada esta desabilitada")]
        public void DadoTaxaPonderadaEstaDesabilitada()
        {
            DatabaseFactory.DBExecutarQuery(Scripts.desligaAPITaxaPonderada);
        }

        [When(@"taxa ponderada esta desabilitada")]
        public void QuandoTaxaPonderadaEstaDesabilitada()
        {
            DatabaseFactory.DBExecutarQuery(Scripts.desligaAPITaxaPonderada);
        }

        [Given(@"log de chamada da API de Calculo de Taxa Ponderada e gravado")]
        public void DadoLogDeChamadaDaAPIDeCalculoDeTaxaPonderadaEGravado()
        {
            //if (TaxaPonderada.VerificaLogChamadaAPITaxaPonderada() == true)
            //    Console.WriteLine("Log da chamada registrado com sucesso!");
            List<List<string>> logAPI = new List<List<string>>(TaxaPonderada.RetornaLogChamadaAPITaxaPonderada());

            for (int i = 0; i <= logAPI.Count; i++)
            {
                if (logAPI[0][i].Contains(CadastrarPortabilidadePage.convenioselecionado) || logAPI[0][i].Contains(CadastrarPortabilidadePage.taxapreenchida.ToString()) ||
                    logAPI[0][i].Contains(CadastrarPortabilidadePage.valorParcela))
                {
                    Console.WriteLine("Log API criado com sucesso");
                }
            }
        }

        [When(@"log de chamada da API de Calculo de Taxa Ponderada e gravado")]
        public void QuandoLogDeChamadaDaAPIDeCalculoDeTaxaPonderadaEGravado()
        {
            List<List<string>> logAPI = new List<List<string>>(TaxaPonderada.RetornaLogChamadaAPITaxaPonderada());
            
            for(int i = 0; i < logAPI.Count; i++)
            {
                if (logAPI[0][i].Contains(CadastrarPortabilidadePage.convenioselecionado) || logAPI[0][i].Contains(CadastrarPortabilidadePage.taxapreenchida.ToString()) ||
                    logAPI[0][i].Contains(CadastrarPortabilidadePage.valorParcela))
                {
                    Console.WriteLine("Log API criado com sucesso");
                }
            }
        }

        [Given(@"devem ser listados todos os produtos '(.*)' de COMP do convenio selecionado com taxa menor ou igual a taxa ponderada")]
        public void DadoDevemSerListadosTodosOsProdutosDeCOMPDoConvenioSelecionadoComTaxaMenorOuIgualATaxaPonderada(string tipoVenda)
        {
            if (tipoVenda == "Padrao")
            {
                string taxaponderada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida,CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado).ToString();

                var query = Scripts.produtosPadraoComTaxaPonderadaCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado).Replace("@taxaPonderada", taxaponderada);
                List<List<string>> produtos = DatabaseFactory.DBRetornarListaDadosQuery(query.Replace(",","."));
                                
                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in produtos)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]) == true)
                        Console.WriteLine(c[0] + " Produto correto");
                }
            }
            else if (tipoVenda == "Digital")
            {
                var taxaponderada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

                var query = Scripts.produtosDigitalComTaxaPonderadaCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado).Replace("@taxaPonderada", taxaponderada.ToString());
                List<List<string>> produtos = DatabaseFactory.DBRetornarListaDadosQuery(query.Replace(",","."));

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in produtos)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]) == true)
                        Console.WriteLine(c[0] + " Produto correto");
                }
            }
        }

        [Then(@"devem ser listados todos os produtos '(.*)' de COMP do convenio selecionado com taxa menor ou igual a taxa ponderada")]
        public void EntaoDevemSerListadosTodosOsProdutosDeCOMPDoConvenioSelecionadoComTaxaMenorOuIgualATaxaPonderada(string tipoVenda)
        {
            if (tipoVenda == "Padrao")
            {
                var taxaponderada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

                var query = Scripts.produtosPadraoComTaxaPonderadaCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado).Replace("@taxaPonderada", taxaponderada.ToString());
                List<List<string>> produtos = DatabaseFactory.DBRetornarListaDadosQuery(query);

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in produtos)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]) == true)
                        Console.WriteLine(c[0] + " Produto correto");
                }
            }
            else if (tipoVenda == "Digital")
            {
                var taxaponderada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

                var query = Scripts.produtosDigitalComTaxaPonderadaCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado).Replace("@taxaPonderada", taxaponderada.ToString()).Replace(",",".");
                List<List<string>> produtos = DatabaseFactory.DBRetornarListaDadosQuery(query);

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach(var c in produtos)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0].Replace("00", "")) == true)
                        Console.WriteLine(c[0].Replace("00", "") + " Produto correto");
                }
            }
        }

        [Given(@"devem ser listados todos os produtos '(.*)' de COMP do convenio selecionado")]
        public void DadoDevemSerListadosTodosOsProdutosDeCOMPDoConvenioSelecionado(string tipoVenda)
        {
            if (tipoVenda == "Padrao")
            {
                var taxaponderada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

                var query = Scripts.produtosPadraoComTaxaPonderadaCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado).Replace("@taxaPonderada", taxaponderada.ToString());
                List<List<string>> produtos = DatabaseFactory.DBRetornarListaDadosQuery(query);

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in produtos)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]) == true)
                        Console.WriteLine(c[0] + " Produto correto");
                }
            }
            else if (tipoVenda == "Digital")
            {
                var taxaponderada = TaxaPonderada.CalculoTaxaPonderada(0 + CadastrarPortabilidadePage.convenioselecionado, CadastrarPortabilidadePage.taxapreenchida, CadastrarPortabilidadePage.qntParcelasContratoPortado, CadastrarPortabilidadePage.qntParcelasTotaisContratoPortado);

                var query = Scripts.produtosDigitalComTaxaPonderadaCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado).Replace("@taxaPonderada", taxaponderada.ToString());
                List<List<string>> produtos = DatabaseFactory.DBRetornarListaDadosQuery(query);

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in produtos)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]) == true)
                        Console.WriteLine(c[0] + " Produto correto");
                }
            }
        }

        [Then(@"devem ser listados todos os produtos '(.*)' de COMP do convenio selecionado")]
        public void EntaoDevemSerListadosTodosOsProdutosDeCOMPDoConvenioSelecionado(string tipoVenda)
        {
            if (tipoVenda == "Padrao")
            {

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                var query = Scripts.produtosPadraoCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado);
                List<List<string>> retornaProdutosPadraoCOMP = DatabaseFactory.DBRetornarListaDadosQuery(query);

                foreach (var c in retornaProdutosPadraoCOMP)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]))
                        Console.WriteLine("Sucesso na listagem de produtos");
                }
            }
            else if (tipoVenda == "Digital")
            {
                var query = Scripts.produtosDigitalCOMP.Replace("@CodConvenio", 0 + CadastrarPortabilidadePage.convenioselecionado);
                List<List<string>> retornaProdutosDigitaisCOMP = DatabaseFactory.DBRetornarListaDadosQuery(query);

                List<string> listaOpcoesCbxProduto = new List<string>(PageBase.GetAllOptionsCombo(By.Id("ddlProduto")));

                foreach (var c in retornaProdutosDigitaisCOMP)
                {
                    if (listaOpcoesCbxProduto.Contains(c[0]))
                        Console.WriteLine("Sucesso na listagem de produtos");
                }
            }
        }

        [Given(@"gravarmos a proposta")]
        public void DadoGravarmosAProposta()
        {
            _cadastrarportabilidadePage.ClicarProsseguirFinal();
        }

        [When(@"preencho o campo email")]
        public void QuandoPreenchoOCampoEmail(Table table)
        {
            var dictionary = GeneralHelpers.ToDictionary(table);

            _cadastrarportabilidadePage.PreencherUsuarioPortabilidade("RECOMPRA");
            _cadastrarportabilidadePage.PreencherSenhaPortabilidade(ConfigurationManager.AppSettings["SenhaFuncaoRecompra"]);
            _cadastrarportabilidadePage.ClicarEntrar();
            _cadastrarportabilidadePage.SelecionarLojaAleatoria();
            _cadastrarportabilidadePage.SelecionarOrgaoAleatorio();
            _cadastrarportabilidadePage.PreencherCodigoBeneficio();
            _cadastrarportabilidadePage.PreencherVersaoFormulario("EMP006");
            _cadastrarportabilidadePage.PreencherRenda("5000,00");
            _cadastrarportabilidadePage.ClicarProsseguir();

            _cadastrarportabilidadePage.SelecionarNacionalidade("B");
            _cadastrarportabilidadePage.PreencherNaturalidade("Belo Horizonte");
            _cadastrarportabilidadePage.SelecionarSexo("F");
            _cadastrarportabilidadePage.SelecionarEstadoCivil("SO");
            _cadastrarportabilidadePage.PreencherRG("MG15487899");
            _cadastrarportabilidadePage.PreencherUfRg("MG");
            _cadastrarportabilidadePage.PreencherOrgaoEmissor("sspmg");
            _cadastrarportabilidadePage.PreencherDataEmissao("02022002");
            _cadastrarportabilidadePage.PreencherCep("30570080");
            _cadastrarportabilidadePage.PreencherNumero("133");
            _cadastrarportabilidadePage.PreencherDDDTel("31");
            _cadastrarportabilidadePage.PreencherNumeroTel("Randon");
            _cadastrarportabilidadePage.PreencherDDDCel("31");
            _cadastrarportabilidadePage.PreencherNumeroCel("Randon");

            Functions.Funções.CriarPrintEvidencias(CadastrarPortabilidadePage.numProposta, "Evidências");

            _cadastrarportabilidadePage.PreencherMatricula("Randon");
            _cadastrarportabilidadePage.PreencherNomePai("AUTOMAÇÃO");
            _cadastrarportabilidadePage.PreencherNomeMae("AUTOMAÇÃO");

            _cadastrarportabilidadePage.PreencherEmail(dictionary["email_invalido"]);

            _cadastrarportabilidadePage.SelecionarTipoContaCliente("01");
            _cadastrarportabilidadePage.PreencherBancoCliente("001");
            _cadastrarportabilidadePage.PreencherAgenciaCliente("0250");
            _cadastrarportabilidadePage.PreencherDigitoAgenciaCliente("0");
            _cadastrarportabilidadePage.PreencherContaCliente("124666");
            _cadastrarportabilidadePage.PreencherDigitoContaCliente("2");
        }

        [When(@"prossigo com cadastro")]
        public void QuandoProssigoComCadastro()
        {
            _cadastrarportabilidadePage.ClicarBtnProsseguirDados();
        }

        [Then(@"deve ser exibida uma mensagem de e-mail incorreto")]
        public void EntaoDeveSerExibidaUmaMensagemDeE_MailIncorreto()
        {
            if (_cadastrarportabilidadePage.ValidaMsgEmailInvalido() == true)
            {
                Console.WriteLine("Validação de email inválido feita corretamente!");
            }
        }
        
        [When(@"o CPF informado possui restricao por '(.*)'")]
        public void QuandoOCPFInformadoPossuiRestricaoPor(string tipoRestricao)
        {
            string cpfcliente = GeradorDeDados.GerarCpf();
            string cpf = string.Empty;
            string cpfSemMascara = cpfcliente;
            cpf = string.Format("{0}.{1}.{2}-{3}", cpfSemMascara.Substring(0, 3), cpfSemMascara.Substring(3, 3), cpfSemMascara.Substring(6, 3), cpfSemMascara.Substring(9, 2));

            if (tipoRestricao == "Negativacao")
            {
                List<List<string>> resultado = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.verificarCpfNegativado.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));

                if (resultado == null || resultado.Count == 0)
                {
                    DatabaseFactory.DBExecutarQuery((Scripts.insereCpfNaListaDeNegativados.Replace("@cpf", "'" + cpf + "'")));
                    _cadastrarportabilidadePage.PreencherCPF(cpfcliente);
                }
                    
            }
            else if (tipoRestricao == "semRepasse")
            {
                List<List<string>> resultado = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.VerificaCpfSemRepasseMais45Dias.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));

                if (resultado != null || resultado.Count != 0)
                    _cadastrarportabilidadePage.PreencherCPF(resultado[0][0]);

            }
        }

        [Given(@"CPF passou na validacao da lista restritiva")]
        public void DadoCPFPassouNaValidacaoDaListaRestritiva()
        {
            List<List<string>> listanegativado = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.verificarCpfNegativado.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));
            List<List<string>> listasemrepasse = DatabaseFactory.DBRetornarListaDadosQuery(Scripts.VerificaCpfSemRepasseMais45Dias.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));

            if (listanegativado != null || listanegativado.Count > 0)
                DatabaseFactory.DBExecutarQuery(Scripts.CpfNaoNegativado.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));
            if (listasemrepasse != null || listasemrepasse.Count > 0)
                DatabaseFactory.DBExecutarQuery(Scripts.cpfSemProblemaRepasse.Replace("@cpf", CadastrarPortabilidadePage.cpfcliente));
        }

        [Given(@"prossigo o cadastro da portabilidade ate o cadastro do cliente")]
        public void QuandoProssigoOCadastroDaPortabilidadeAteOCadastroDoCliente()
        {
            _cadastrarportabilidadePage.PreencherUsuarioPortabilidade("RECOMPRA");
            _cadastrarportabilidadePage.PreencherSenhaPortabilidade(ConfigurationManager.AppSettings["SenhaFuncaoRecompra"]);
            _cadastrarportabilidadePage.ClicarEntrar();
            _cadastrarportabilidadePage.SelecionarLojaAleatoria();
            _cadastrarportabilidadePage.SelecionarOrgaoAleatorio();
            _cadastrarportabilidadePage.PreencherCodigoBeneficio();
            _cadastrarportabilidadePage.PreencherVersaoFormulario("EMP006");
            _cadastrarportabilidadePage.PreencherRenda("5000,00");
            _cadastrarportabilidadePage.ClicarProsseguir();

            _cadastrarportabilidadePage.SelecionarNacionalidade("B");
            _cadastrarportabilidadePage.PreencherNaturalidade("Belo Horizonte");
            _cadastrarportabilidadePage.SelecionarSexo("F");
            _cadastrarportabilidadePage.SelecionarEstadoCivil("SO");
            _cadastrarportabilidadePage.PreencherRG("MG15487899");
            _cadastrarportabilidadePage.PreencherUfRg("MG");
            _cadastrarportabilidadePage.PreencherOrgaoEmissor("sspmg");
            _cadastrarportabilidadePage.PreencherDataEmissao("02022002");
            _cadastrarportabilidadePage.PreencherCep("30570080");
            _cadastrarportabilidadePage.PreencherNumero("133");
            _cadastrarportabilidadePage.PreencherDDDTel("31");
            _cadastrarportabilidadePage.PreencherNumeroTel("Randon");
            _cadastrarportabilidadePage.PreencherDDDCel("31");
            _cadastrarportabilidadePage.PreencherNumeroCel("Randon");

            Functions.Funções.CriarPrintEvidencias(CadastrarPortabilidadePage.numProposta, "Evidências");

            _cadastrarportabilidadePage.PreencherMatricula("Randon");
            _cadastrarportabilidadePage.PreencherNomePai("AUTOMAÇÃO");
            _cadastrarportabilidadePage.PreencherNomeMae("AUTOMAÇÃO");

            _cadastrarportabilidadePage.PreencherEmail("mesa.originacao@oletecnologia.com.br");

            _cadastrarportabilidadePage.SelecionarTipoContaCliente("01");
            _cadastrarportabilidadePage.PreencherBancoCliente("001");
            _cadastrarportabilidadePage.PreencherAgenciaCliente("0250");
            _cadastrarportabilidadePage.PreencherDigitoAgenciaCliente("0");
            _cadastrarportabilidadePage.PreencherContaCliente("124666");
            _cadastrarportabilidadePage.PreencherDigitoContaCliente("2");

            _cadastrarportabilidadePage.ClicarBtnProsseguirDados();
        }
        public void FechaCadastroPortabilidade()
        {
            _cadastrarportabilidadePage.FechaTela();
        }
    }
}
