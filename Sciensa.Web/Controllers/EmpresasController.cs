using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sciensa.Web.Models;
using System.Collections.Generic;
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

        /// <summary>
        /// Lista as empresas listadas no mercado de ações 
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(string codigo)
        {            
            string proxyUrl = $"{servico}/Api/v2/Lookup/json?input=" + codigo;
            HttpResponseMessage response = await httpClient.GetAsync(proxyUrl);

            var retorno = await response.Content.ReadAsStringAsync();


            return Json(JsonConvert.DeserializeObject<List<Empresa>>(retorno));
            
        }

    }
}
