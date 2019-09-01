using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinance.Data
{
    public class SpecialPercentagePeriod: BaseObj
    {
        public int MonthCount { get; set; }
        public decimal MonthPercentage { get; set; }

        public virtual Credit Credit { get; set; }
    }
}
