using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransactionManagement.Model.RequestModel;
using TransactionManagement.Queries;
using TransactionManagement.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace TransactionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TransactionController : ControllerBase
    {
        private readonly ISender _sender;

        public TransactionController(ISender sender)
        {
            _sender = sender;
        }

        // GET: api/<TransactionController>/get-filtered
        [HttpGet("get-filtered")]
        public async Task<IActionResult> GetFilterTransactions([FromQuery] FilterTransactionRequest filterRequest)
        {
            IEnumerable<TransactRecord> transactionData = await _sender.Send(new GetFullFilterTransactionQuery(filterRequest));

            if (transactionData is null)
            {
                return NoContent();
            }

            return Ok(transactionData);
        }
    }
}
