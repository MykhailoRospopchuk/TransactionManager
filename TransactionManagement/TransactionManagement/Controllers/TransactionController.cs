using MediatR;
using Microsoft.AspNetCore.Mvc;
using TransactionManagement.Model.RequestModel;
using TransactionManagement.Queries;
using TransactionManagement.Model.Entities;
using Microsoft.AspNetCore.Authorization;
using TransactionManagement.Commands;

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
        /// <summary>
        /// Get filtered data by status (can be several status) and type of transaction, by the name of the Customer
        /// </summary>
        /// <param name="filterRequest"></param>
        /// <response code="200">Returns a JSON</response>
        // GET: api/<TransactionController>/get-filtered
        [HttpGet("get-filtered")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> GetFilterTransactions([FromQuery] FilterTransactionRequest filterRequest)
        {
            IEnumerable<TransactRecord> transactionData = await _sender.Send(new GetFullFilterTransactionQuery(filterRequest));

            if (transactionData is null)
            {
                return NoContent();
            }

            return Ok(transactionData);
        }

        /// <summary>
        /// Update the transaction status by its ID
        /// </summary>
        /// <param name="updateTransaction"></param>
        // GET: api/<TransactionController>/update-transaction
        [HttpPut("update-transaction")]
        public async Task<IActionResult> UpdateTransactions([FromQuery] UpdateTransactionRequest updateTransaction)
        {
            await _sender.Send(new UpdateTransactionCommand(updateTransaction));

            return Ok();
        }
    }
}
