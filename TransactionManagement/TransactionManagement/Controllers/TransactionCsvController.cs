using Microsoft.AspNetCore.Mvc;
using MediatR;
using TransactionManagement.Queries;
using TransactionManagement.Commands;
using TransactionManagement.Model.RequestModel;
using TransactionManagement.Model.Entities;
using TransactionManagement.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;
using TransactionManagement.Model.Consts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransactionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TransactionCsvController : ControllerBase
    {
        private readonly ICSVService _csvService;

        private readonly ISender _sender;

        public TransactionCsvController(ICSVService csvService, ISender sender)
        {
            _csvService = csvService;
            _sender = sender;
        }

        // GET: api/<TransactionCsvController>/get-all-csv
        [HttpGet("get-all-csv")]
        public async Task<IActionResult> GetAllTransactions()
        {
            IEnumerable<TransactRecord> transactionData = await _sender.Send(new GetAllTransactionQuery());

            if (transactionData is null)
            {
                return NoContent();
            }

            var transactions = _csvService.CreateCSV<TransactRecord>(transactionData);

            return File(transactions, "text/csv", "transactions.csv");
        }

        // GET: api/<TransactionCsvController>/get-filtered-csv
        [HttpGet("get-filtered-csv")]
        public async Task<IActionResult> GetFilterTransactions([FromQuery] FilterTransactionCsvRequest filterRequest)
        {
            IEnumerable<TransactRecord> transactionData = await _sender.Send(new GetFilterTransactionQuery(filterRequest));

            if (transactionData is null)
            {
                return NoContent();
            }

            var transactions = _csvService.CreateCSV<TransactRecord>(transactionData);

            return File(transactions, "text/csv", "filter_transactions.csv");
        }

        // POST api/<TransactionCsvController>/post-csv
        [HttpPost("post-csv")]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var transactions = _csvService.ReadCSV<TransactRecord>(file.OpenReadStream());

            await _sender.Send(new AddTransactionCommand(transactions));

            return Ok();
        }
    }
}
