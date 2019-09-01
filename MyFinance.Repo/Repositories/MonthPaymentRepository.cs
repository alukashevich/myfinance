using Microsoft.EntityFrameworkCore;
using MyFinance.Data;
using MyFinance.Repo.Intarfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFinance.Repo.Repositories
{
    class MonthPaymentRepository : BaseRepository<MonthPayment>, IMonthPaymentRepository
    {
        public MonthPaymentRepository(DbContext context) : base(context)
        {
        }

        public IQueryable<MonthPayment> GetPayments(Guid creditId)
        {
            return MyFinContext.MonthPayments.Where(x => x.Credit.Id == creditId);
        }
    }
}
