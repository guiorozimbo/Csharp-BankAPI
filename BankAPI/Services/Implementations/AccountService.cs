using BankAPI.DAL;
using BankAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using BankAPI.Service;


namespace BankAPI.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private YouBakingDbContext _dbcontextFactory;

        public AccountService(YouBakingDbContext dbcontextFactory)
        {
            _dbcontextFactory = dbcontextFactory;
        }

        public Account Authenticate(string AccountNumber, string pin)
        {
            var account = _dbcontextFactory.Accounts.Where(x => x.AccountNumberGenerated == AccountNumber).SingleOrDefault();
            if (account == null)
                return null;
            if (!VerifyPinHash(pin, account.pinHash, account.pinSalt)) 
                return null;

            // authentication successful
            return account;
        }
        private static bool VerifyPinHash(string pin, byte[] pinHash, byte[] pinSalt)
        {
            if (string.IsNullOrWhiteSpace(pin)) throw new ArgumentException("pinHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(pinSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(pin));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != pinHash[i]) return false;
                }
            }
            return true;
        }

        public Account Create(Account account, string Pin, string ConfirmPin)
        {
            if (_dbcontextFactory.Accounts.Any(x => x.Email == account.Email)) throw new ApplicationException("An account already exists with this email");
           
            if (!Pin.Equals(ConfirmPin)) throw new ApplicationException("Pin and Confirm Pin do not match");
            byte[] pinHash, pinSalt;

            CreatePinHash(Pin,out pinHash, out pinSalt);
            account.pinHash = pinHash;
            account.pinSalt = pinSalt;

            _dbcontextFactory.Add(account);
            _dbcontextFactory.SaveChanges();
            return account;
        }
        private static void CreatePinHash(string Pin,out byte[] pinHash,out byte[] pinSalt)
        {
            using (var hmac =new System.Security.Cryptography.HMACSHA512())
            {
                pinSalt = hmac.Key;
                pinHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(Pin));
            }
        }

        public void Delete(int id)
        {
            var account = _dbcontextFactory.Accounts.Find(id);
            if(account != null)
            {
                _dbcontextFactory.Accounts.Remove(account);
                _dbcontextFactory.SaveChanges();
            }
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _dbcontextFactory.Accounts.ToList();
        }

        public Account GetByAccountNumber(string AccountNumber)
        {
            throw new NotImplementedException();
        }

        public Account GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Account account, string pin = null)
        {
            throw new NotImplementedException();
        }
    }
}
