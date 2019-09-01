using System;

namespace MyFinance.Data
{
    public class Wallet : BaseObj
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public WalletType WalletType { get; set; }

        public User User { get; set; }
        public Currency Currency { get; set; }
    }
}
