using AutoMapper;
using MyFinance.Data;
using MyFinance.Data.DTOs;
using MyFinance.Data.DTOs.Results;
using MyFinance.Repo;
using MyFinance.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFinance.Services
{
    public class CreditService : BaseService<CreditDTO, Credit>, ICreditSrvice
    {
        public CreditService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
            _typeName = nameof(Credit);
        }

        public override async Task<Result<CreditDTO>> Get(Guid id)
        {
            var entity = await _baseRepository.Get(id);
            if (entity == null)
                return ErrorNotFound(null);
            var dto = _mapper.Map<CreditDTO>(entity);
            dto.SpecialPersentages = _mapper.Map<List<SpecialPercentagePeriodDTO>>(entity.SpecialPersentages);
            dto.MonthPayments = _mapper.Map<List<MonthPaymentDTO>>(entity.MonthPayments);
            return SuccessEntity(_mapper.Map<CreditDTO>(entity));
        }

        public async Task<Result<CreditDTO>> Calculate(CreditDTO credit)
        {
            var entity = await _unitOfWork.Credits.Get(credit.Id);

            _mapper.Map(credit, entity);

            if (credit.MonthsCount < 1)
                return new Result<CreditDTO>(BusinessResultState.Error, null, "month count can't be 0");

            if (entity.MonthPayments?.Count > 0)
            {
                _unitOfWork.MonthPayments.RemoveRange(entity.MonthPayments);
            }

            if (entity.SpecialPersentages?.Count > 0)
            {
                _unitOfWork.SpecialPercentagePeriods.RemoveRange(entity.SpecialPersentages);
            }

            switch (credit.CreditType)
            {
                case Data.Enums.CreditType.Annuity:
                    return await CalculateAnnuity(entity, credit);
                case Data.Enums.CreditType.Differentiated:
                    return await CalculateDifferentiated(entity, credit);
            }
            return new Result<CreditDTO>(BusinessResultState.Error, null, "type mismatch");
        }

        private async Task<Result<CreditDTO>> CalculateAnnuity(Credit entity, CreditDTO credit)
        {
            return new Result<CreditDTO>();
        }

        private async Task<Result<CreditDTO>> CalculateDifferentiated(Credit entity, CreditDTO credit)
        {
            var payments = new List<MonthPayment>();
            var monthPayment = entity.Amount / entity.MonthsCount;

            var monthNumber = 1;

            var specialPercentages = new List<SpecialPercentagePeriod>();
            foreach (var specialOffer in credit.SpecialPersentages)
            {
                Calculate(monthNumber, monthNumber + specialOffer.MonthCount, specialOffer.MonthPercentage, monthPayment, credit, entity, payments);
                monthNumber += specialOffer.MonthCount;

                var specialPercentage = _mapper.Map<SpecialPercentagePeriod>(specialOffer);
                specialPercentage.Credit = entity;
                specialPercentages.Add(specialPercentage);
            }

            await _unitOfWork.SpecialPercentagePeriods.AddRange(specialPercentages);

            Calculate(monthNumber, credit.MonthsCount + 1, credit.BasePercenteges, monthPayment, credit, entity, payments);

            await _unitOfWork.MonthPayments.AddRange(payments);
            try
            {
                if (await _unitOfWork.Complete() > 0)
                {
                    entity.MonthPayments = payments;
                    return new Result<CreditDTO>(BusinessResultState.Success, _mapper.Map<CreditDTO>(entity));
                }
            }
            catch (Exception e)
            {
                return new Result<CreditDTO>(BusinessResultState.Error, null, e.Message);
            }
            return ErrorUpdate(credit, credit.Id);
        }

        private void Calculate(int startIndex, int lastIndex, decimal percentage, decimal monthPayment, CreditDTO credit, Credit entity, List<MonthPayment> payments)
        {
            for (var i = startIndex; i < lastIndex; i++)
            {
                var percentegePayment = credit.Amount * percentage / 100 / 12;
                var payment = new MonthPayment
                {
                    Number = i,
                    Persentege = percentage,
                    Credit = entity
                };

                payment.Payment = percentegePayment + monthPayment;

                if (credit.Amount - monthPayment < 0)
                {
                    monthPayment = credit.Amount;
                }

                credit.Amount -= monthPayment;

                payment.PersentegesPyment = percentegePayment;
                payments.Add(payment);
            }
        }
    }
}
