using Features.Tests;
using NerdStore.WebApp.MVC;
using NerdStore.WebApp.MVC.Models;
using NerdStore.WebApp.Tests.Config;
using System;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace NerdStore.WebApp.Tests
{
    [TestCaseOrderer("Features.Tests.PriorityOrderer", "Features.Tests")]
    [Collection(nameof(IntegrationApiTestsFixtureCollection))]
    public class PedidoApiTests
    {
        private readonly IntegrationTestsFixture<StartupApiTests> _testsFixture;

        public PedidoApiTests(IntegrationTestsFixture<StartupApiTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }

        [Fact(DisplayName = "Adicionar item em novo pedido"), TestPriority(1)]
        [Trait("Categoria", "Integração API - Pedido")]
        public async Task AdicionarItem_NovoPedido_DeveRetornarComSucesso()
        {
            // Arrange
            var itemInfo = new ItemViewModel
            {
                Id = new Guid("0a767155-c561-4423-9b34-e4fe264fa323"),
                Quantidade = 2
            };

            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var postResponse = await _testsFixture.Client.PostAsJsonAsync("api/carrinho", itemInfo);

            // Assert
            postResponse.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Remover item em pedido existente"), TestPriority(2)]
        [Trait("Categoria", "Integração API - Pedido")]
        public async Task RemoverItem_PedidoExistente_DeveRetornarComSucesso()
        {
            // Arrange
            var produtoId = new Guid("352f683a-4f8d-4e7d-9017-06d68d52e68a");
            await _testsFixture.RealizarLoginApi();
            _testsFixture.Client.AtribuirToken(_testsFixture.UsuarioToken);

            // Act
            var deleteResponse = await _testsFixture.Client.DeleteAsync($"api/carrinho/{produtoId}");

            // Assert
            deleteResponse.EnsureSuccessStatusCode();
        }
    }
}