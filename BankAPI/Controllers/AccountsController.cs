using AutoMapper;
using BankAPI.Models;
using BankAPI.Service;
using Microsoft.AspNetCore.Mvc;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private IAccountService _accountService;
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
        public IActionResult Authenticate([FromBody] AuthenticateModel)
        {
            var account = _accountService.GetById(id);
            var clearneadAccount = _mapper.Map<GetAccountModel>(account);
            return Ok(clearneadAccount);
        }
    }
}
