using MyFinance.Data.DTOs;
using MyFinance.Data.DTOs.Results;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFinance.Services.Interfaces
{
    public interface IMonthPaymentSrvice : IBaseService<MonthPaymentDTO>
    {
        Task<Result<CreditDTO>> Calculate(Guid creditId, MonthPaymentDTO monthPayment);
        Task<Result<CreditDTO>> Calculate(Guid creditId, IList<MonthPaymentDTO> monthPayments);
    }
}
