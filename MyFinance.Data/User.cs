using System;
using System.Collections.Generic;

namespace MyFinance.Data
{
    public class User : BaseObj
    {
        public string UserName { get; set; }
        public decimal PasswordHash { get; set; }

        public List<Wallet> Wallets { get; set; } = new List<Wallet>();
    }
}
