using System;

namespace MyFinance.Data
{
    public class Currency: BaseObj
    {
        public string Name { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
