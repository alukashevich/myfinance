using MyFinance.Data.DTOs.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Services.Interfaces
{
    public interface IBaseService<DTOType>
    {
        Result<List<DTOType>> GetAll();
        Task<Result<DTOType>> Get(Guid id);
        Task<Result<DTOType>> Create(DTOType dto);
        Task<Result<DTOType>> Update(DTOType dto);
        Task<Result<bool>> Delete(Guid id);
    }
}
