using NerdStore.BDD.Tests.Config;
using NerdStore.BDD.Tests.Usuario;
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
        private readonly LoginUsuarioTela _loginUsuarioTela;

        private string _urlProduto;

        public Pedido_AdicionarItemAoCarrinhoStepDefinitions(AutomacaoWebTestsFixture testsFixture)
        {
            _testsFixture = testsFixture;
            _pedidoTela = new PedidoTela(testsFixture.BrowserHelper);
            _loginUsuarioTela = new LoginUsuarioTela(testsFixture.BrowserHelper);
        }

        [Given(@"O usuario esteja logado")]
        public void DadoOUsuarioEstejaLogado()
        {
            // Arrange
            var usuario = new Usuario.Usuario
            {
                Email = "teste@teste.com",
                Senha = "Teste@123"
            };
            _testsFixture.Usuario = usuario;

            // Act 
            var login = _loginUsuarioTela.Login(usuario);

            // Assert
            Assert.True(login);
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
            // Assert
            Assert.True(_pedidoTela.ObterQuantidadeNoEstoque() > 0);
        }

        [Given(@"N??o tenha nenhum produto adicionado ao carrinho")]
        public void GivenNaoTenhaNenhumProdutoAdicionadoAoCarrinho()
        {
            // Act
            _pedidoTela.NavegarParaCarrinhoDeCompras();
            _pedidoTela.ZerarCarrinhoDeCompras();

            // Assert
            Assert.Equal(0, _pedidoTela.ObterValorTotalCarrinho());

            _pedidoTela.NavegarParaUrl(_urlProduto);
        }

        [When(@"O usu??rio adicionar uma unidade ao carrinho")]
        public void QuandoOUsuarioAdicionarUmaUnidadeAoCarrinho()
        {
            // Act
            _pedidoTela.ClicarEmComprarAgora();
        }

        [Then(@"O usu??rio ser?? redirecionado ao resumo da compra")]
        public void EntaoOUsuarioSeraRedirecionadoAoResumoDaCompra()
        {
            // Assert
            Assert.True(_pedidoTela.ValidarSeEstaNoCarrinhoDeCompras());
        }

        [Then(@"O valor total do pedido ser?? exatamente o valor do item adicionado")]
        public void EntaoOValorTotalDoPedidoSeraExatamenteOValorDoItemAdicionado()
        {// Arrange
            var valorUnitario = _pedidoTela.ObterValorUnitarioProdutoCarrinho();
            var valorCarrinho = _pedidoTela.ObterValorTotalCarrinho();

            // Assert
            Assert.Equal(valorUnitario, valorCarrinho); throw new PendingStepException();
        }

        [When(@"O usu??rio adicionar um item acima da quantidade m??xima permitida")]
        public void QuandoOUsuarioAdicionarUmItemAcimaDaQuantidadeMaximaPermitida()
        {
            // Arrange 
            _pedidoTela.ClicarAdicionarQuantidadeItens(Vendas.Domain.Pedido.MAX_UNIDADES_ITEM + 1);

            // Act
            _pedidoTela.ClicarEmComprarAgora();
        }

        [Then(@"Receber?? uma mensagem de erro mencionando que foi ultrapassada a quantidade limite")]
        public void EntaoReceberaUmaMensagemDeErroMencionandoQueFoiUltrapassadaAQuantidadeLimite()
        {
            // Arrange
            var mensagem = _pedidoTela.ObterMensagemDeErroProduto();

            // Assert
            Assert.Contains($"A quantidade m??xima de um item ?? {Vendas.Domain.Pedido.MAX_UNIDADES_ITEM}", mensagem);
        }

        [Given(@"O mesmo produto j?? tenha sido adicionado ao carrinho anteriormente")]
        public void DadoOMesmoProdutoJaTenhaSidoAdicionadoAoCarrinhoAnteriormente()
        {
            // Act
            _pedidoTela.NavegarParaCarrinhoDeCompras();
            _pedidoTela.ZerarCarrinhoDeCompras();
            _pedidoTela.AcessarVitrineDeProdutos();
            _pedidoTela.ObterDetalhesDoProduto();
            _pedidoTela.ClicarEmComprarAgora();

            // Assert
            Assert.True(_pedidoTela.ValidarSeEstaNoCarrinhoDeCompras());

            _pedidoTela.VoltarNavegacao();
        }

        [Then(@"A quantidade de itens daquele produto ter?? sido acrescida em uma unidade a mais")]
        public void EntaoAQuantidadeDeItensDaqueleProdutoTeraSidoAcrescidaEmUmaUnidadeAMais()
        {
            // Assert
            Assert.True(_pedidoTela.ObterQuantidadeDeItensPrimeiroProdutoCarrinho() == 2);
        }

        [Then(@"O valor total do pedido ser?? a multiplica????o da quantidade de itens pelo valor unitario")]
        public void EntaoOValorTotalDoPedidoSeraAMultiplicacaoDaQuantidadeDeItensPeloValorUnitario()
        {
            // Arrange
            var valorUnitario = _pedidoTela.ObterValorUnitarioProdutoCarrinho();
            var valorCarrinho = _pedidoTela.ObterValorTotalCarrinho();
            var quantidadeUnidades = _pedidoTela.ObterQuantidadeDeItensPrimeiroProdutoCarrinho();

            // Assert
            Assert.Equal(valorUnitario * quantidadeUnidades, valorCarrinho);
        }

        [When(@"O usu??rio adicionar a quantidade m??xima permitida ao carrinho")]
        public void QuandoOUsuarioAdicionarAQuantidadeMaximaPermitidaAoCarrinho()
        {
            // Arrange 
            _pedidoTela.ClicarAdicionarQuantidadeItens(Vendas.Domain.Pedido.MAX_UNIDADES_ITEM);

            // Act
            _pedidoTela.ClicarEmComprarAgora();
        }
    }
}
