using NerdStore.BDD.Tests.Config;

namespace NerdStore.BDD.Tests.Pedido
{
    public class PedidoTela : PageObjectModel
    {
        public PedidoTela(SeleniumHelper helper) : base(helper)
        {
        }

        public void AcessarVitrineDeProdutos()
        {
            Helper.IrParaUrl(Helper.Configuration.VitrineUrl);
        }

        public void ObterDetalhesDoProduto(int posicao = 1)
        {
            // Somente obter elemento por XPath se o mesmo não possuir "id", "name" ou outr a propriedade chave
            // pois caso o front-end seja alterado e seja adicionado por exemplo mais uma "div" o teste
            // irá falhar, tendo que ser refatorado.
            Helper.ClicarPorXPath($"html/body/div/main/div/div/div[{posicao}]/span/a");
        }

        public bool ValidarProdutoDisponivel()
        {
            return Helper.ValidarConteudoUrl(Helper.Configuration.ProdutoUrl);
        }
    }
}