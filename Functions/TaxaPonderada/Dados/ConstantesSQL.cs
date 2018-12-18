
namespace TaxaConvenio.Dados
{
    public class ConstantesSQL
    {
        #region [   TAXA PONDERADA  ]

        public const string AtualizarDataRecalculoPonderadaManualPmt = @"UPDATE BSISISCON.dbo.SIMULADOR SET dataRecalculoPonderadaManualAltPmt = @DATA WHERE CODSIM = @CODSIM";

        public const string RecuperarParametroServicoRecalculoPonderada = @"SELECT NOME_PARAMETRO, NOME_ROTINA, NOME_CAMPO_FLAG_ATUALIZAR
	                                                                            FROM BSISISCON.dbo.PARAMETROS_SERVICO_RECALCULO_PONDERADA
	                                                                            WHERE NOME_PARAMETRO = @NomeParametro";

        public const string RecuperarParametrosApiTaxaPonderada = @"select * from bsisiscon.dbo.SIMPARAMETROS (nolock)
	                                                                where codprm in ('TaxaPonderada_utilizar_api',
                                                                                    'TaxaPonderada_url_api',
                                                                                    'TaxaPonderada_rota_calculo_taxa_ponderada',
                                                                                    'TaxaPonderada_level_gravacao_log')";

        public const string RecuperarPropostasPendentesRecalculoPonderadaAlteracaoManual = @"BSISISCON.dbo.USP_BUSCA_PROPOSTA_PENDENTE_RECALCULO_TAXA_PONDERADA";

        public const string RecuperarPropostasPendentesRecalculoPonderadaAlteracaoCEC = @"";

        public const string AtualizaFlagRecalculoPonderadaManual = @"UPDATE BSISISCON.dbo.SIMULADOR
                                                                        SET {0} = @FLAG,
                                                                        identificadorCalculoTaxaPonderada = ISNULL(@IdentificadorCalculoTaxaPonderada, identificadorCalculoTaxaPonderada),
			                                                            taxaponderada = ISNULL(@TaxaPonderada, taxaponderada),
			                                                            prazoOrigemPonderado = ISNULL(@PrazoOrigemPonderado, prazoOrigemPonderado)
                                                                        WHERE CODSIM = @CODSIM";

        public const string GRAVAR_LOG_API = @"INSERT INTO BSISISCON.dbo.LOG_API_TAXA_PONDERADA (TIPO_LOG, USUARIO, CLASSE, METODO, MENSAGEM, CONTRATOS_PROPOSTAS)
	                                            VALUES (@TIPO, @USUARIO, @CLASSE, @METODO, @MENSAGEM, @CONTRATOS);";

        public const string AtualizaDadosSimulacaoCalculoTaxaPonderada = @"UPDATE BSISISCON..SIMULADOR
		                                                                    SET IdentificadorCalculoTaxaPonderada = ISNULL(@IdentificadorCalculoTaxaPonderada, IdentificadorCalculoTaxaPonderada),
			                                                                    TaxaPonderada = ISNULL(@TaxaPonderada, TaxaPonderada),
			                                                                    PrazoOrigemPonderado = ISNULL(@PrazoOrigemPonderado, PrazoOrigemPonderado)
		                                                                    WHERE codsim = @codsim";

        public const string RecuperarDadosCalculoSimulacao = @"SELECT	S.codsim COD_SIMULACAO,
		                                                                S.numconpor NUM_CONTRATO_ORIGEM,
		                                                                S.prdsim COD_PRODUTO_REFIN,
		                                                                T.PMCODORG4 COD_CONVENIO,
		                                                                S.qtdparabrconpor TOTAL_PARCELAS_ORIGEM,
		                                                                S.valparconpor VALOR_PARCELA,
		                                                                S.qtdtotparconpor TOTAL_PARCELAS_REFIN,
		                                                                S.taxconpor TAXA_CONTRATO_ORIGEM,
		                                                                S.TaxaPonderada TAXA_PONDERADA,
		                                                                S.IdentificadorCalculoTaxaPonderada ID_CALCULO_TAXA_PONDERADA,
		                                                                S.TaxaOrigemPonderada TAXA_ORIGEM_PONDERADA,
		                                                                S.PrazoOrigemPonderado PRAZO_ORIGEM_PONDERADO,
		                                                                S.PrazoLiquido PRAZO_LIQUIDO
	                                                                FROM BSISISCON..SIMULADOR S (NOLOCK)
	                                                                INNER JOIN BSOAUTORIZ..TPARA T (NOLOCK)
		                                                                ON T.PMCODPRD = S.prdsim
	                                                                WHERE S.CODSIM = @codSimulacao";

        public const string RecuperarParametrosTaxaProduto = @"SELECT	COD_PRODUTO,
                                                                        NOME_PRODUTO,
		                                                                COD_CONVENIO,
		                                                                COD_TIPO_PRODUTO,
		                                                                TIPO_PRECIFICACAO,
		                                                                FAIXA1_MIN,
		                                                                FAIXA1_MAX,
		                                                                FAIXA2_MIN,
		                                                                FAIXA2_MAX,
		                                                                FAIXA3_MIN,
		                                                                FAIXA3_MAX,
		                                                                FAIXA4_MIN,
		                                                                FAIXA4_MAX,
		                                                                FAIXA5_MIN,
		                                                                FAIXA5_MAX,
		                                                                FAIXA6_MIN,
		                                                                FAIXA6_MAX,
		                                                                TAXA1,
		                                                                TAXA2,
		                                                                TAXA3,
		                                                                TAXA4,
		                                                                TAXA5,
		                                                                TAXA6
	                                                                FROM BSISISCON..TAXAS_COMP (NOLOCK)
	                                                                WHERE COD_PRODUTO = @codProduto";

        public const string RecuperarParametrosTaxaProdutosPort = @"SELECT	COD_PRODUTO,
                                                                            NOME_PRODUTO,
		                                                                    COD_CONVENIO,                                                                    
		                                                                    COD_TIPO_PRODUTO,
		                                                                    TIPO_PRECIFICACAO,
		                                                                    FAIXA1_MIN,
		                                                                    FAIXA1_MAX,
		                                                                    FAIXA2_MIN,
		                                                                    FAIXA2_MAX,
		                                                                    FAIXA3_MIN,
		                                                                    FAIXA3_MAX,
		                                                                    FAIXA4_MIN,
		                                                                    FAIXA4_MAX,
		                                                                    FAIXA5_MIN,
		                                                                    FAIXA5_MAX,
		                                                                    FAIXA6_MIN,
		                                                                    FAIXA6_MAX,
		                                                                    TAXA1,
		                                                                    TAXA2,
		                                                                    TAXA3,
		                                                                    TAXA4,
		                                                                    TAXA5,
		                                                                    TAXA6
	                                                                    FROM BSISISCON..TAXAS_COMP (NOLOCK)
	                                                                    WHERE COD_CONVENIO = @CodConvenio
																		    AND COD_TIPO_PRODUTO = '13'
                                                                            AND NOME_PRODUTO NOT LIKE '%EXCECAO%'
                                                                        ORDER BY COD_PRODUTO";

        #endregion
    }
}
