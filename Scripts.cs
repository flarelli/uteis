using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomacaoBDD.Helpers
{
    public class Scripts
    {
        public const string retornaStatusAPITaxaPonderada = "select valprm from bsisiscon..simparametros where codprm = 'TaxaPonderada_utilizar_api'";

        public const string conveniosQuePermitemPortabilidade = "SELECT DISTINCT T.PMCODORG4 FROM BSOAUTORIZ..TPARA T JOIN BSOCDC..TORG4 O ON O.O4CODORG = T.PMCODORG4 AND O.O4ATIVA = 'A' AND T.PMCODORG4 <> '001000' WHERE T.PMTPOPER IN (';03') AND T.PMDESCRPRD LIKE '%PORTAB%' AND EXISTS (SELECT 1 FROM BSOAUTORIZ..TPARA T2 (NOLOCK) WHERE T2.PMCODORG4 = T.PMCODORG4 AND T2.PMTPOPER = '13') ORDER BY T.PMCODORG4";

        public const string ligaAPITaxaPonderada = "update bsisiscon..simparametros set valprm = '1' where codprm = 'TaxaPonderada_utilizar_api'";

        public const string desligaAPITaxaPonderada = "update bsisiscon..simparametros set valprm = '0' where codprm = 'TaxaPonderada_utilizar_api'";

        public const string delpropostaCMOVP = "DELETE FROM BSOAUTORIZ.DBO.CMOVP WHERE MPNRPROP IN (SELECT PPNRPROP FROM BSOAUTORIZ.DBO.CPROP WHERE PPCODCLI in (SELECT CLCODCLI FROM BSOAUTORIZ.DBO.CCLIP WHERE clcgc = '@cpf'))";

        public const string delpropostaEPROP = "DELETE FROM BSOAUTORIZ.DBO.EPROP WHERE EPNRPROP IN (SELECT PPNRPROP FROM BSOAUTORIZ.DBO.CPROP WHERE PPCODCLI in (SELECT CLCODCLI FROM BSOAUTORIZ.DBO.CCLIP WHERE clcgc = '@cpf'))";

        public const string delpropostaCOBSE = "DELETE FROM BSOAUTORIZ.DBO.COBSE WHERE OBNRPROP IN (SELECT PPNRPROP FROM BSOAUTORIZ.DBO.CPROP WHERE PPCODCLI in (SELECT CLCODCLI FROM BSOAUTORIZ.DBO.CCLIP WHERE clcgc = '@cpf'))";

        public const string delpropostaSimulador = "DELETE FROM BSISISCON..SIMULADOR WHERE CPFCLI = '@cpf'";

        public const string delpropostaEmprestimo = "DELETE FROM DM01..EMPRESTIMO WHERE NRO_CONTRATO IN (SELECT CONTRATO_ID FROM SCCDEM..FT01 WHERE CONT IN (SELECT PPNRPROP FROM BSOAUTORIZ..CPROP WHERE PPCODCLI IN (SELECT CLCODCLI FROM BSOAUTORIZ.DBO.CCLIP WHERE clcgc = '@cpf')))";

        public const string delpropostaFT01 = "DELETE FROM SCCDEM..FT01 WHERE CONT IN (SELECT PPNRPROP FROM BSOAUTORIZ..CPROP WHERE PPCODCLI in (SELECT CLCODCLI FROM BSOAUTORIZ.DBO.CCLIP WHERE clcgc = '@cpf'))";

        public const string delpropostaCRENP = "DELETE FROM BSOAUTORIZ..CRENP WHERE RENROPER IN (SELECT PPNROPER FROM BSOAUTORIZ..CPROP WHERE PPCODCLI in (SELECT CLCODCLI FROM BSOAUTORIZ.DBO.CCLIP WHERE clcgc = '@cpf'))";

        public const string delpropostaCPROP = "DELETE FROM BSOAUTORIZ.DBO.CPROP WHERE PPCODCLI IN (SELECT CLCODCLI FROM BSOAUTORIZ.DBO.CCLIP WHERE clcgc = '@cpf')";

        public const string retornaCodConvenio = "SELECT O4CODORG FROM BSOCDC..TORG4  WHERE O4NOME = '@nomeconvenio'";

        public const string retornatotalparc = "select max(FAIXA1_MAX) from BSISISCON..taxas_comp where COD_CONVENIO = @convenio AND COD_TIPO_PRODUTO = '13'";

        public const string produtosPadraoCOMP = "SELECT COD_PRODUTO + ' - ' + NOME_PRODUTO FROM BSISISCON..TAXAS_COMP INNER JOIN SCCDEM..SISCON_PARAMPROD ON PRD_CD_PRODUT = COD_PRODUTO WHERE COD_CONVENIO = '011533' AND COD_TIPO_PRODUTO = ';03' AND COD_MEICTT = 1 ORDER BY COD_PRODUTO";

        public const string produtosDigitalCOMP = "SELECT COD_PRODUTO + ' - ' + NOME_PRODUTO FROM BSISISCON..TAXAS_COMP INNER JOIN SCCDEM..SISCON_PARAMPROD ON PRD_CD_PRODUT = COD_PRODUTO WHERE COD_CONVENIO = '011533' AND COD_TIPO_PRODUTO = ';03' AND COD_MEICTT = 5 ORDER BY COD_PRODUTO";

        public const string produtosPadraoComTaxaPonderadaCOMP = "select COD_PRODUTO + ' - ' + NOME_PRODUTO from BSISISCON..taxas_comp " +
            "INNER JOIN SCCDEM..SISCON_PARAMPROD ON PRD_CD_PRODUT = COD_PRODUTO where COD_CONVENIO = '@CodConvenio' and COD_TIPO_PRODUTO = ';03' " +
            "and NOME_PRODUTO not like '%VD%' "+ 
            "and NOME_PRODUTO not like '%ina%' "+
            "and NOME_PRODUTO like '%COMP%'"+
            "AND COD_MEICTT = 1 and(TAXA1 <= @taxaPonderada " +
             "or TAXA2 <= @taxaPonderada " +
             "or TAXA3 <= @taxaPonderada " +
             "or TAXA4 <= @taxaPonderada " +
             "or TAXA5 <= @taxaPonderada " +
             "or TAXA6 <= @taxaPonderada) order by TAXA1 asc";

        public const string produtosDigitalComTaxaPonderadaCOMP = "select COD_PRODUTO + ' - ' + NOME_PRODUTO from BSISISCON..taxas_comp " +
            "INNER JOIN SCCDEM..SISCON_PARAMPROD ON PRD_CD_PRODUT = COD_PRODUTO where COD_CONVENIO = '@CodConvenio' and COD_TIPO_PRODUTO = ';03' " +
            "and NOME_PRODUTO like '%VD%' " +
            "and NOME_PRODUTO not like '%ina%' " +
            "and NOME_PRODUTO like '%COMP%'" + 
            "AND COD_MEICTT = 5 and(TAXA1 <= @taxaPonderada " +
             "or TAXA2 <= @taxaPonderada " +
             "or TAXA3 <= @taxaPonderada " +
             "or TAXA4 <= @taxaPonderada " +
             "or TAXA5 <= @taxaPonderada " +
             "or TAXA6 <= @taxaPonderada) order by TAXA1 asc";


        public const string verificarCpfNegativado = "select top 1 CLCODCLI from BSOAUTORIZ..CNEGP where CLCGC = '@cpf'";

        public const string insereCpfNaListaDeNegativados = "DECLARE @CODIGOCLIENTE VARCHAR(50) " +
           " select top 1 @CODIGOCLIENTE = CLCODCLI from BSOAUTORIZ..CCLIP WITH (NOLOCK) " +
           " insert into BSOAUTORIZ..CNEGP values (@CODIGOCLIENTE,'TESTE AUTOMATIZADO','F','@cpf','Fraude Comprovada',getdate(),'','','','','','',NULL,NULL,NULL,NULL) ";

        public const string CpfNaoNegativado = "delete from BSOAUTORIZ..CNEGP where CLCGC = '@cpf'";

        public const string VerificaCpfSemRepasseMais45Dias = "select top 1 * from bsoautoriz..cclip (nolock) where CLCGC = '@cpf' and bsiutil.dbo.dias_atraso(clcgc,null, getdate(), 'N')>45";

        public const string insereCpfRemRepasseMais45Dias = "declare @cpfanterior numeric(15), @numcontrato numeric(20)"+
            " select top 1 @cpfanterior = E.CNPJ_CPF, @numcontrato = E.NRO_CONTRATO from DM01..EMPRESTIMO E(NOLOCK)"+
            " INNER JOIN DM01..PARCEMPRE P(NOLOCK) ON P.COD_CONTRATO = E.NRO_CONTRATO order by P.DAT_PAGAMENTO desc"+
            " update DM01..EMPRESTIMO SET CNPJ_CPF = '@cpf' WHERE NRO_CONTRATO = @numcontrato" +
            " update DM01..PARCEMPRE SET DAT_PAGAMENTO  = getdate() - 45 where COD_CONTRATO = @numcontrato";

        public const string cpfSemProblemaRepasse = "declare @numcontrato numeric(20) select @numcontrato = E.NRO_CONTRATO from DM01..EMPRESTIMO E (NOLOCK) INNER JOIN DM01..PARCEMPRE P(NOLOCK) " +
            " ON P.COD_CONTRATO = E.NRO_CONTRATO " +
            " WHERE AND E.DATA_CANCELAMENTO = '19000101' " +
            " AND E.NRO_CONTRATO  = P.COD_CONTRATO " +
            " AND P.DAT_PAGAMENTO <> '19000101' "+  
            " update DM01..PARCEMPRE SET DAT_PAGAMENTO  = getdate() where COD_CONTRATO = @numcontrato ";

        public const string verificaLogAPITaxaPonderada = "select top 1 * from bsisiscon..log_api_taxa_ponderada (nolock) WHERE DATA_LOG >= dateadd(minute, -30, getdate()) order by data_log desc";

        public const string tornaCelularInformadoViávelPraVendaDigital = "update BSOAUTORIZ..CCLIP set CLCELULAR = '99548-5394' where CLCELULAR = '@celular'";
    }

}
