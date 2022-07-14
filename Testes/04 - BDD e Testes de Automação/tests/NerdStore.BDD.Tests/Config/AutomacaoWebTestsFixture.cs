using Bogus;
using Xunit;

namespace NerdStore.BDD.Tests.Config
{
    [CollectionDefinition(nameof(AutomacaoWebFixtureCollection))]
    public class AutomacaoWebFixtureCollection : ICollectionFixture<AutomacaoWebTestsFixture>
    {
    }

    public class AutomacaoWebTestsFixture
    {
        public SeleniumHelper BrowserHelper;
        public readonly ConfigurationHelper Configuration;

        public Usuario.Usuario Usuario;


        public AutomacaoWebTestsFixture()
        {
            Usuario = new Usuario.Usuario();
            Configuration = new ConfigurationHelper();
            BrowserHelper = new SeleniumHelper(Browser.Chrome, Configuration, false);
        }

        public void GerarDadosUsuario()
        {
            var faker = new Faker("pt_BR");
            Usuario.Email = faker.Internet.Email().ToLower();

            // "@1Ab"_ -> Contém pelo menos 1 caracter especial, 1 número, 1 letra maiúscula e 1 letra minuscula
            Usuario.Senha = faker.Internet.Password(8, false, "", "@1Ab_");
        }
    }
}