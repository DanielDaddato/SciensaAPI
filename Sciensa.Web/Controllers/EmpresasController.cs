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

        // POST: api/Empresas
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Empresas/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
