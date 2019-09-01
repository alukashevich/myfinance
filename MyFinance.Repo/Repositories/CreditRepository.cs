using Microsoft.EntityFrameworkCore;
using MyFinance.Data;
using MyFinance.Repo.Intarfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyFinance.Repo.Repositories
{
    class CreditRepository : BaseRepository<Credit>, ICreditRepository
    {
        public CreditRepository(DbContext context) : base(context)
        {
        }
    }
}
