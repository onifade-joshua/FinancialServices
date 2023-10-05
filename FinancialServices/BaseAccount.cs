using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialServices
{
    internal class BaseAccount
    {
       public BaseAccount() { 
            UserId = Guid.NewGuid().ToString();
            AccountId = Guid.NewGuid().ToString();
        }
        public string AccountId { get; set; }
        public string AccountType { get; set; }
        public string UserId { get; set; }
        public decimal Balance { get; set; }
    }
}
