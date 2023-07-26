using Microsoft.AspNetCore.Mvc;
using TransactionManagement.Model;
using TransactionManagement.Services;
using MediatR;
using TransactionManagement.Queries;
using TransactionManagement.Commands;
using TransactionManagement.Model.RequestModel;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TransactionManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

            var transactions = _csvService.CreateCSV<TransactRecord>(transactionData);

            return File(transactions, "text/csv", "people.csv");
        }

        // GET: api/<TransactionCsvController>/get-filtered-csv
        [HttpGet("get-filtered-csv")]
        public async Task<IActionResult> GetFilterTransactions([FromQuery] FilterTransactionCsvRequest filterRequest)
        {
            IEnumerable<TransactRecord> transactionData = await _sender.Send(new GetFilterTransactionQuery(filterRequest));

            var transactions = _csvService.CreateCSV<TransactRecord>(transactionData);

            return File(transactions, "text/csv", "people.csv");
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
