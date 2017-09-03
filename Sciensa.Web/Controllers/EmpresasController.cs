using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sciensa.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/banco/mercado/pesquisa")]
    public class EmpresasController : Controller
    {
        private readonly HttpClient httpClient;
        string servico = "http://dev.markitondemand.com";

        public EmpresasController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string codigo)
        {            
            string proxyUrl = $"{servico}/Api/v2/Lookup/json?input=" + codigo;
            HttpResponseMessage response = await httpClient.GetAsync(proxyUrl);

            return new ContentResult()
            {
                StatusCode = (int)response.StatusCode,
                Content = await response.Content.ReadAsStringAsync()
            };
        }

    }
}
