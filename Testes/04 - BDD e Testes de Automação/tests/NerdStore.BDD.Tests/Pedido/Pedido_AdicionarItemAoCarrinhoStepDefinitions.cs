using NerdStore.BDD.Tests.Config;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;
using Xunit;

namespace NerdStore.BDD.Tests.Pedido
{
    [Binding]
    [CollectionDefinition(nameof(AutomacaoWebFixtureCollection))]
    public class Pedido_AdicionarItemAoCarrinhoStepDefinitions
    {
        private readonly AutomacaoWebTestsFixture _testsFixture;
        private readonly PedidoTela _pedidoTela;
        private string _urlProduto;

        public Pedido_AdicionarItemAoCarrinhoStepDefinitions(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _pedidoTela = new PedidoTela(testsFixture.BrowserHelper);
        }

        [Given(@"Que um produto esteja na vitrine")]
        public void DadoQueUmProdutoEstejaNaVitrine()
        {
            // Arrange

            //_testsFixture.BrowserHelper.IrParaUrl("https://desenvolvedor.io");
            //_testsFixture.BrowserHelper.ClicarLinkPorTexto("Entrar");
            //_testsFixture.BrowserHelper.PreencherTextBoxPorId("Email", "contato@teste.com");

            _pedidoTela.AcessarVitrineDeProdutos();

            // Act
            _pedidoTela.ObterDetalhesDoProduto();
            _urlProduto = _pedidoTela.ObterUrl();

            // Assert
            Assert.True(_pedidoTela.ValidarProdutoDisponivel());
        }

        [Given(@"Esteja disponivel no estoque")]
        public void DadoEstejaDisponivelNoEstoque()
        {
            throw new PendingStepException();
        }

        [Given(@"O usuario esteja logado")]
        public void DadoOUsuarioEstejaLogado()
        {
            throw new PendingStepException();
        }

        [When(@"O usuário adicionar uma unidade ao carrinho")]
        public void QuandoOUsuarioAdicionarUmaUnidadeAoCarrinho()
        {
            throw new PendingStepException();
        }

        [Then(@"O usuário será redirecionado ao resumo da compra")]
        public void EntaoOUsuarioSeraRedirecionadoAoResumoDaCompra()
        {
            throw new PendingStepException();
        }

        [Then(@"O valor total do pedido será exatamente o valor do item adicionado")]
        public void EntaoOValorTotalDoPedidoSeraExatamenteOValorDoItemAdicionado()
        {
            throw new PendingStepException();
        }

        [When(@"O usuário adicionar um item acima da quantidade máxima permitida")]
        public void QuandoOUsuarioAdicionarUmItemAcimaDaQuantidadeMaximaPermitida()
        {
            throw new PendingStepException();
        }

        [Then(@"Receberá uma mensagem de erro mencionando que foi ultrapassada a quantidade limite")]
        public void EntaoReceberaUmaMensagemDeErroMencionandoQueFoiUltrapassadaAQuantidadeLimite()
        {
            throw new PendingStepException();
        }

        [Given(@"O mesmo produto já tenha sido adicionado ao carrinho anteriormente")]
        public void DadoOMesmoProdutoJaTenhaSidoAdicionadoAoCarrinhoAnteriormente()
        {
            throw new PendingStepException();
        }

        [Then(@"A quantidade de itens daquele produto terá sido acrescida em uma unidade a mais")]
        public void EntaoAQuantidadeDeItensDaqueleProdutoTeraSidoAcrescidaEmUmaUnidadeAMais()
        {
            throw new PendingStepException();
        }

        [Then(@"O valor total do pedido será a multiplicação da quantidade de itens pelo valor unitario")]
        public void EntaoOValorTotalDoPedidoSeraAMultiplicacaoDaQuantidadeDeItensPeloValorUnitario()
        {
            throw new PendingStepException();
        }

        [When(@"O usuário adicionar a quantidade máxima permitida ao carrinho")]
        public void QuandoOUsuarioAdicionarAQuantidadeMaximaPermitidaAoCarrinho()
        {
            throw new PendingStepException();
        }
    }
}
