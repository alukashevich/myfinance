using System;

namespace MyFinance.Data
{
    public class Transaction : BaseObj
    {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public User User { get; set; }
        public Currency Currency { get; set; }
        public Wallet Wallet { get; set; }
    }
}
