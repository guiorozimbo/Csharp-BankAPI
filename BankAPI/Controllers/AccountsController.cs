using AutoMapper;
using BankAPI.Models;
using BankAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountService;
        IMapper _mapper;
        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        //register account new 
        [HttpPost]
        [Route("register_new_account")]
        public IActionResult Register([FromBody] RegisterNewAccountModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // map model to entity
            var account = _mapper.Map<BankAPI.Models.Account>(model);
            return Ok(_accountService.Create(account, model.Pin, model.ConfirmPin));
        }
    
        [HttpGet]
        [Route("get_all_accounts")]
        public IActionResult GetAllAccounts()
        {
            var accounts = _accountService.GetAllAccounts();
            var clearneadAccounts = _mapper.Map<IList<GetAccountModel>>(accounts);
            return Ok(clearneadAccounts);
        }
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            if (ModelState.IsValid) return BadRequest(model);
           return Ok(_accountService.Authenticate(model.AccountNumber.ToString(), model.Pin));

           // var account = _accountService.GetById(model);
           // var clearneadAccount = _mapper.Map<GetAccountModel>(account);
           // return Ok(clearneadAccount);
        }
        [HttpGet]
        [Route("get_by_account_number")]
        public IActionResult GetByAccountNumber([FromQuery] string AccountNumber)
        {
            if(!Regex.IsMatch(AccountNumber, @"^[0][1-9]\d{9}$^[1-9]\d{9}$")) return BadRequest("Account number must be a 10-digit number.");
            var account = _accountService.GetByAccountNumber(AccountNumber);
            var clearneadAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(clearneadAccount);
        }
        [HttpGet]
        [Route("get_account_by_id")]
        public IActionResult GetById([FromQuery] int id)
        {
            var account = _accountService.GetById(id);
            var clearneadAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(clearneadAccount);
        }
        [HttpPut]
        [Route("update_account")]
        public IActionResult UpdateAccount([FromBody] UpdateAccountModel model,string Pin)
        {
            if (!ModelState.IsValid) return BadRequest(model);
            var account = _mapper.Map<BankAPI.Models.Account>(model);
            var pin =model.Pin;
            _accountService.Update(account, model.Pin);
            return Ok();
        }
    }
}
