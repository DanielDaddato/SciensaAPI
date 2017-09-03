using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sciensa.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Sciensa.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/banco/mercado/preco")]
    public class PrecosController : Controller
    {
        private readonly HttpClient httpClient;
        string servico = "http://dev.markitondemand.com";

        public PrecosController(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string codigo)
        {
            string proxyUrl = $"{servico}/Api/v2/Quote/json?symbol=" + codigo;
            HttpResponseMessage response = await httpClient.GetAsync(proxyUrl);

            return Json(JsonConvert.DeserializeObject<Preco>(await response.Content.ReadAsStringAsync()));            
        }
    }
}
