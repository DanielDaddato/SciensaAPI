using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Newtonsoft.Json;
using Sciensa.Web.Models;
using System.Text;
using System.Net.Http.Headers;

namespace Sciensa.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/contas")]
    public class ContasController : Controller
    {

        private readonly HttpClient _httpClient;
        string servico = "http://localhost:19081/SciensaAPI/Sciensa.API/api/ContasData";
        string tipo = "Int64Range";
        string chave = "0";
        public ContasController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet("{clienteID}")]
        public async Task<IActionResult> Get(int clienteID)
        {

            HttpResponseMessage resposta = await _httpClient.GetAsync($"{servico}/{clienteID}?PartitionKind={tipo}&PartitionKey={chave}");


            var contas = JsonConvert.DeserializeObject<List<Conta>>(await resposta.Content.ReadAsStringAsync());

            return Json(contas);
        }

        [HttpPost("{*conta}")]
        public async Task<IActionResult> Post([FromBody]Conta conta)
        {
            string payload = JsonConvert.SerializeObject(conta);
            StringContent postContent = new StringContent(payload, Encoding.UTF8, "application/json");
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string proxyUrl = $"{servico}/{conta}?PartitionKind={tipo}&PartitionKey={chave}";

            HttpResponseMessage response = await _httpClient.PostAsync(proxyUrl, postContent);

            return new ContentResult()
            {
                StatusCode = (int)response.StatusCode,
                Content = await response.Content.ReadAsStringAsync()
            };
        }

        [HttpPut("{*conta}")]
        public async Task<IActionResult> Put([FromBody]Conta conta)
        {
            string payload = JsonConvert.SerializeObject(conta);
            StringContent postContent = new StringContent(payload, Encoding.UTF8, "application/json");
            postContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string proxyUrl = $"{servico}/{conta}?PartitionKind={tipo}&PartitionKey={chave}";

            HttpResponseMessage response = await _httpClient.PutAsync(proxyUrl, postContent);

            return new ContentResult()
            {
                StatusCode = (int)response.StatusCode,
                Content = await response.Content.ReadAsStringAsync()
            };
        }
    }
}
