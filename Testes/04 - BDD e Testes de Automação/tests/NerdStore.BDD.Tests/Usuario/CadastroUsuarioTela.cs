using NerdStore.BDD.Tests.Config;

namespace NerdStore.BDD.Tests.Usuario
{
    public class CadastroUsuarioTela : BaseUsuarioTela
    {
        public CadastroUsuarioTela(SeleniumHelper helper) : base(helper)
        {
        }

        public void ClicarNoLinkRegistrar()
        {
            Helper.ClicarLinkPorTexto("Register");
        }

        public void PreencherFormularioRegistro(Usuario usuario)
        {
            Helper.PreencherTextBoxPorId("Input_Email", usuario.Email);
            Helper.PreencherTextBoxPorId("Input_Password", usuario.Senha);
            Helper.PreencherTextBoxPorId("Input_ConfirmPassword", usuario.Senha);
        }

        public bool ValidarPreenchimentoFormularioRegistro(Usuario usuario)
        {
            if (Helper.ObterValorTextBoxPorId("Input_Email") != usuario.Email) return false;
            if (Helper.ObterValorTextBoxPorId("Input_Password") != usuario.Senha) return false;
            if (Helper.ObterValorTextBoxPorId("Input_ConfirmPassword") != usuario.Senha) return false;

            return true;
        }

        public void ClicarNoBotaoRegistrar()
        {
            // Somente obter elemento por XPath se o mesmo não possuir "id", "name" ou outr a propriedade chave
            // pois caso o front-end seja alterado e seja adicionado por exemplo mais uma "div" o teste
            // irá falhar, tendo que ser refatorado.
            var botao = Helper.ObterElementoPorXPath("/html/body/div/main/div/div/form/button");
            botao.Click();
        }
    }
}