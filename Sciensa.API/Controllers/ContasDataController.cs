using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using System.Threading;
using Sciensa.API.Models;

namespace Sciensa.API.Controllers
{
    [Route("api/[controller]")]
    public class ContasDataController : Controller
    { 

        private readonly IReliableStateManager stateManager;

        public ContasDataController(IReliableStateManager stateManager)
        {
            this.stateManager = stateManager;
        }



        [HttpGet("{*clienteID}")]
        public async Task<IActionResult> Get(int clienteid)
        {
            var ct = new CancellationToken();

            var contasDictionary = await this.stateManager.GetOrAddAsync<IReliableDictionary<int, Conta>>("Contas");

            using (ITransaction tx = this.stateManager.CreateTransaction())
            {
                var list = await contasDictionary.CreateEnumerableAsync(tx);

                var enumerator = list.GetAsyncEnumerator();

                var result = new List<Conta>();

                while (await enumerator.MoveNextAsync(ct))
                {
                    if (((Conta)enumerator.Current.Value).ClienteID == clienteid)
                    {
                        result.Add(enumerator.Current.Value);
                    }
                }

                return Json(result);
            }
        }

        [HttpPost("{*conta}")]
        public async Task<IActionResult> Post([FromBody]Conta conta)
        {
            var contasDictionary = await this.stateManager.GetOrAddAsync<IReliableDictionary<int, Conta>>("Contas");

            using (ITransaction tx = this.stateManager.CreateTransaction())
            {

                var idx = await contasDictionary.GetCountAsync(tx);

                await contasDictionary.AddAsync(tx, (int)(idx + 1), conta);
                await tx.CommitAsync();
            }

            return new OkResult();
        }

        [HttpPut("{*conta}")]
        public async Task<IActionResult> Put([FromBody]Conta conta)
        {
            var ct = new CancellationToken();

            var contasDictionary = await this.stateManager.GetOrAddAsync<IReliableDictionary<int, Conta>>("Contas");

            using (ITransaction tx = this.stateManager.CreateTransaction())
            {

                var list = await contasDictionary.CreateEnumerableAsync(tx);

                var enumerator = list.GetAsyncEnumerator();

                KeyValuePair<int,Conta> item = new KeyValuePair<int, Conta>();

                while (await enumerator.MoveNextAsync(ct))
                {
                    if (((Conta)enumerator.Current.Value).ContaID == conta.ContaID && ((Conta)enumerator.Current.Value).ClienteID == conta.ClienteID)
                    {
                        item = enumerator.Current;
                    }
                }
                if (item.Key == 0)
                {
                    return new NotFoundObjectResult(conta);
                }
                var idx = await contasDictionary.GetCountAsync(tx);

                await contasDictionary.TryUpdateAsync(tx, (int)item.Key, conta, item.Value);
                await tx.CommitAsync();
            }

            return new OkResult();
        }
    }
}
