using BankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using BankAPI.DAL;

namespace BankAPI.Service
{
    public interface IAccountService
    {
        Account Authenticate(string AccountNumber, string pin);
        IEnumerable<Account> GetAllAccounts();
        Account Create(Account account, string pin,string ConfirmPin);
        void Update(Account account, string pin = null);
        void Delete(int id);
        Account GetById(int id);
        Account GetByAccountNumber(string AccountNumber);
    }
}
