using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace NerdStore.WebApp.Tests.Config
{
    public static class TestsExtensions
    {
        public static decimal ApenasNumeros(this string value)
        {
            return Convert.ToDecimal(new string(value.Where(char.IsDigit).ToArray()));
        }

        public static void AtribuirToken(this HttpClient client, string token)
        {
            client.AtribuirJsonMediaType();

            // Adiciona outro header o Authorization informando o tipo (Bearer) e o token
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static void AtribuirJsonMediaType(this HttpClient client)
        {
            // Limpa todos os headers da requisição
            client.DefaultRequestHeaders.Clear();

            // Adiciona o header "application/json" para informar que a requisição está vindo no tipo de dado Json
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}