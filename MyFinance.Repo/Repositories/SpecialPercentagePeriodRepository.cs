using Microsoft.EntityFrameworkCore;
using MyFinance.Data;
using MyFinance.Repo.Intarfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyFinance.Repo.Repositories
{
    class SpecialPercentagePeriodRepository : BaseRepository<SpecialPercentagePeriod>, ISpecialPercentagePeriodRepository
    {
        public SpecialPercentagePeriodRepository(DbContext context) : base(context)
        {
        }
    }
}
