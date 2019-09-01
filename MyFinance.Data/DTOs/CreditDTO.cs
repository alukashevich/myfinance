using MyFinance.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFinance.Data.DTOs
{
    public class CreditDTO : BaseDTO
    {
        public decimal Amount { get; set; }
        public decimal BasePercenteges { get; set; }
        public bool IsStarted { get; set; }
        public int MonthsCount { get; set; }
        public CreditType CreditType { get; set; } = CreditType.Differentiated;

        private List<MonthPaymentDTO> _monthPayments;
        public List<MonthPaymentDTO> MonthPayments
        {
            get
            {
                var list = new List<MonthPaymentDTO>();
                if (_monthPayments == null)
                    return list;
                var amount = Amount;
                foreach (var payment in _monthPayments.OrderBy(x => x.Number))
                {
                    payment.Debit = amount -= payment.DebitPayment;
                    if (payment.Payment < 0)
                    {
                        list.Last().Payment = list.Last().Payment + payment.Payment;
                        break;
                    }
                    list.Add(payment);
                }
                return list;
            }
            set
            {
                _monthPayments = value;
            }
        }

        public List<SpecialPercentagePeriodDTO> SpecialPersentages { get; set; } = new List<SpecialPercentagePeriodDTO>();
        public decimal SumPercenteges => MonthPayments.Sum(x => x.PersentegesPyment);
    }
}
