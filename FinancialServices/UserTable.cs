using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialServices
{
    internal class UserTable : BaseAccount
    {
        public UserTable()
        {
            Id = Guid.NewGuid();
        }
        private Guid Id { get; }

        private string password;
        public string Password
        {
            set
            {
                password = value;
            }
            get
            {
                return password;
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gmail { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string UserName { get; set; }
    }
}
