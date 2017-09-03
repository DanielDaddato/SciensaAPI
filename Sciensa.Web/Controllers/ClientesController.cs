using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Sciensa.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sciensa.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Clientes")]
    public class ClientesController : Controller
    {
        HttpClient _httpClient;
        string servico = "http://localhost:19081/SciensaAPI/Sciensa.API/api/ClientesData";
        string tipo = "Int64Range";
        string chave = "0";
        public ClientesController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            HttpResponseMessage resposta = await _httpClient.GetAsync($"{servico}?PartitionKind={tipo}&PartitionKey={chave}");

            var clientes = JsonConvert.DeserializeObject<List<Cliente>>(await resposta.Content.ReadAsStringAsync());

            return Json(clientes);
        }

        // POST: api/Clientes
        [HttpPost("{*cliente}")]
        public async Task<IActionResult> Post([FromBody]Cliente cliente)
        {
            string payload = JsonConvert.SerializeObject(cliente);
            StringContent putContent = new StringContent(payload, Encoding.UTF8, "application/json");
            putContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            string proxyUrl = $"{servico}/{cliente}?PartitionKind={tipo}&PartitionKey={chave}";

            HttpResponseMessage response = await _httpClient.PostAsync(proxyUrl, putContent);

            return new ContentResult()
            {
                StatusCode = (int)response.StatusCode,
                Content = await response.Content.ReadAsStringAsync()
            };
        }
               
    }
}
