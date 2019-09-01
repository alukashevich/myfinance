using MyFinance.Data.DTOs;
using MyFinance.Data.DTOs.Results;
using System.Threading.Tasks;

namespace MyFinance.Services.Interfaces
{
    public interface ICreditSrvice: IBaseService<CreditDTO>
    {
        Task<Result<CreditDTO>> Calculate(CreditDTO credit);
    }
}
