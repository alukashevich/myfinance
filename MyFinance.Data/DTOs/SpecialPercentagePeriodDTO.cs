using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinance.Data.DTOs
{
    public class SpecialPercentagePeriodDTO: BaseDTO
    {
        public int MonthCount { get; set; }
        public decimal MonthPercentage { get; set; }

        public virtual CreditDTO Credit { get; set; }
    }
}
