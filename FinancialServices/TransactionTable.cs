using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialServices
{
    internal class TransactionTable : BaseAccount
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set;}
        public string TransactionType { get; set; } 
    }
}
