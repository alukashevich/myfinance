using Microsoft.EntityFrameworkCore.Infrastructure;
using MyFinance.Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinance.Data
{
    public class Credit
    {
        private readonly ILazyLoader _lazyLoader;
        public Credit()
        {
        }
        public Credit(ILazyLoader lazyLoader)
        {
            _lazyLoader = lazyLoader;
        }
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public decimal BasePercenteges { get; set; }
        public bool IsStarted { get; set; }
        public int MonthsCount { get; set; }
        public CreditType CreditType { get; set; }

        private List<MonthPayment> _monthPayments;
        public virtual List<MonthPayment> MonthPayments
        {
            get => _lazyLoader.Load(this, ref _monthPayments);
            set => _monthPayments = value;
        }

        private List<SpecialPercentagePeriod> _specialPersentages;
        public virtual List<SpecialPercentagePeriod> SpecialPersentages
        {
            get => _lazyLoader.Load(this, ref _specialPersentages);
            set => _specialPersentages = value;
        }
    }
}
