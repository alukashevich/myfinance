using MyFinance.Repo.Intarfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Repo
{
    public interface IUnitOfWork
    {
        Dictionary<string, object> Repositories { get; }
        ICreditRepository Credits { get; }
        IMonthPaymentRepository MonthPayments { get; }
        ISpecialPercentagePeriodRepository SpecialPercentagePeriods { get; }
        Task<int> Complete();
    }
}
