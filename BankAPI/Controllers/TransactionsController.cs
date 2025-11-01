using AutoMapper;
using BankAPI.Models;
using BankAPI.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using System.Transactions;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        // Implement transaction-related endpoints here
        private ITransactionService _transactionService;
        IMapper _mapper;
        public TransactionsController(ITransactionService transactionService, IMapper mapper)
        {
            _transactionService = transactionService;
            _mapper = mapper;
        }
        // Add transaction-related methods here
        [HttpPost]
        [Route("create_transaction")]
        public IActionResult CreateTransaction([FromBody] TrasanctionRequestDto transactionRequest)
        {
            // Implementation for creating a transaction
            if (!ModelState.IsValid)
                return BadRequest(transactionRequest);
            var transaction = _mapper.Map<Models.Transaction>(transactionRequest);
            return Ok(_transactionService.CreateNewTransaction(transaction));
        }
        [HttpPost]
        [Route("make_deposit")]
        public IActionResult MakeDeposit(string AccountNumber, decimal Amount, string TransactionPin)
        {
            if (!Regex.IsMatch(AccountNumber, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$"))
                return BadRequest("Account number must be 10-digit");
            //  var transaction = _mapper.Map<Models.Transaction>(depositRequest);
            return Ok(_transactionService.MakeDeposit(AccountNumber, Amount, TransactionPin));
        }
        [HttpPost]
        [Route("make_withdrawl")]
        public IActionResult MakeWithdraw(string AccountNumber, decimal Amount, string TransactionPin)
        {
            if (!Regex.IsMatch(AccountNumber, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$"))
                return BadRequest("Account number must be 10-digit");
            //  var transaction = _mapper.Map<Models.Transaction>(depositRequest);
            return Ok(_transactionService.MakeDeposit(AccountNumber, Amount, TransactionPin));
        }
        [HttpPost]
        [Route("make_funds_transfer")]
        public IActionResult MakeTransfer(string FromAccount, string ToAccount, decimal Amount, string TransactionPin)
        {
            if (!Regex.IsMatch(FromAccount, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$") ||
                !Regex.IsMatch(ToAccount, @"^[0][1-9]\d{9}$|^[1-9]\d{9}$"))
                return BadRequest("Account number must be 10-digit");
            //  var transaction = _mapper.Map<Models.Transaction>(depositRequest);
            return Ok(_transactionService.MakeTransfer(FromAccount, ToAccount, Amount, TransactionPin));
        }
    }
}
