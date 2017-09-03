using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using Microsoft.ServiceFabric.Data.Collections;
using Microsoft.ServiceFabric.Data;
using Sciensa.API.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace Sciensa.API.Controllers
{
    [Produces("application/json")]
    [Route("api/ClientesData")]
    public class ClientesDataController : Controller
    {
        private readonly IReliableStateManager stateManager;

        public ClientesDataController(IReliableStateManager stateManager)
        {
            this.stateManager = stateManager;
        }
        // GET: api/ClientesData
        [HttpGet]
        public async Task<IActionResult> Get(int clienteid)
        {
            var ct = new CancellationToken();

            var votesDictionary = await this.stateManager.GetOrAddAsync<IReliableDictionary<int, Cliente>>("Clientes");

            using (ITransaction tx = this.stateManager.CreateTransaction())
            {
                var list = await votesDictionary.CreateEnumerableAsync(tx);

                var enumerator = list.GetAsyncEnumerator();

                var result = new List<Cliente>();

                while (await enumerator.MoveNextAsync(ct))
                {
                    result.Add(enumerator.Current.Value);
                }

                return Json(result);
            }
        }
        
        // POST: api/ClientesData
        [HttpPost("{*cliente}")]
        public async Task<IActionResult> Post([FromBody]Cliente cliente)
        {
            var votesDictionary = await this.stateManager.GetOrAddAsync<IReliableDictionary<int, Cliente>>("Clientes");

            using (ITransaction tx = this.stateManager.CreateTransaction())
            {

                var idx = await votesDictionary.GetCountAsync(tx);

                await votesDictionary.AddAsync(tx, (int)(idx + 1), cliente);
                await tx.CommitAsync();
            }

            return new OkResult();
        }
        
    }
}
