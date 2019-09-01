using MyFinance.Data;
using MyFinance.Repo.Intarfaces;
using MyFinance.Repo.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFinance.Repo
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly MyFinDbContext _context;

        public UnitOfWork(MyFinDbContext context)
        {
            _context = context;

            Credits = new CreditRepository(_context);
            MonthPayments = new MonthPaymentRepository(_context);
            SpecialPercentagePeriods = new SpecialPercentagePeriodRepository(_context);

            Repositories = new Dictionary<string, object>();

            Repositories.Add(nameof(Credit), Credits);
            Repositories.Add(nameof(MonthPayment), MonthPayments);
            Repositories.Add(nameof(SpecialPercentagePeriod), SpecialPercentagePeriods);
        }

        public Dictionary<string, object> Repositories { get; private set; }
        public ICreditRepository Credits { get; private set; }
        public IMonthPaymentRepository MonthPayments { get; private set; }
        public ISpecialPercentagePeriodRepository SpecialPercentagePeriods { get; private set; }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
