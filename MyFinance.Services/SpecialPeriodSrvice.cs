using AutoMapper;
using MyFinance.Data;
using MyFinance.Data.DTOs;
using MyFinance.Data.DTOs.Results;
using MyFinance.Repo;
using MyFinance.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace MyFinance.Services
{
    public class SpecialPeriodSrvice : BaseService<SpecialPercentagePeriodDTO, SpecialPercentagePeriod>, ISpecialPeriodSrvice
    {
        public SpecialPeriodSrvice(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _typeName = nameof(SpecialPercentagePeriod);
        }

        public override async Task<Result<SpecialPercentagePeriodDTO>> Create(SpecialPercentagePeriodDTO dto)
        {
            var entity = _mapper.Map<SpecialPercentagePeriod>(dto);

            var credit = await _unitOfWork.Credits.Get(dto.Credit?.Id ?? Guid.Empty);

            if (credit == null)
                return ErrorNotFound(dto, nameof(Credit));

            entity.Credit = credit;

            await _baseRepository.Add(entity);

            if (await _unitOfWork.Complete() > 0)
            {
                _mapper.Map(entity, dto);
                return SuccessCreate(dto, dto.Id);
            }
            else
                return ErrorCreate(dto, Guid.Empty);
        }
    }
}
