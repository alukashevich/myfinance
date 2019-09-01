using MyFinance.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinance.Data.DTOs
{
    public class MonthPaymentDTO : BaseDTO
    {
        public int Number { get; set; }
        public decimal Payment { get; set; }
        public decimal PersentegesPyment { get; set; }
        public decimal Persentege { get; set; }
        public CreditDTO Credit { get; set; }

        public decimal DebitPayment => decimal.Round(Payment - PersentegesPyment, 2);
        public decimal Debit { get; set; }
    }
}
