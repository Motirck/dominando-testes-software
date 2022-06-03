using Microsoft.VisualStudio.TestPlatform.TestHost;
using NerdStore.WebApp.Tests.Config;

namespace NerdStore.WebApp.Tests
{
    public class UsuarioTests
    {
        private readonly IntegrationTestsFixture<Program> _testsFixture;

        public UsuarioTests(IntegrationTestsFixture<Program> testsFixture)
        {
            _testsFixture = testsFixture;
        }
    }
}