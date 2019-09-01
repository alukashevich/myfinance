using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinance.Data
{
    public class MonthPayment
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public decimal Payment { get; set; }
        public decimal PersentegesPyment { get; set; }
        public decimal Persentege { get; set; }
        public virtual Credit Credit { get; set; }
    }
}
