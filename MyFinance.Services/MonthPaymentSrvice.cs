using AutoMapper;
using MyFinance.Data;
using MyFinance.Data.DTOs;
using MyFinance.Data.DTOs.Results;
using MyFinance.Repo;
using MyFinance.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Services
{
    public class MonthPaymentSrvice : BaseService<MonthPaymentDTO, MonthPayment>, IMonthPaymentSrvice
    {
        public MonthPaymentSrvice(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<Result<CreditDTO>> Calculate(Guid creditId, IList<MonthPaymentDTO> monthPayments)
        {
            var creditEntity = await _unitOfWork.Credits.Get(creditId);
            if (creditEntity == null)
                return new Result<CreditDTO>(BusinessResultState.Error, null, string.Format(MESSAGE_NOT_FOUND, nameof(Credit)));

            foreach (var payment in monthPayments)
            {
                if (creditEntity.MonthPayments?.Count < payment.Number)
                    return new Result<CreditDTO>(BusinessResultState.Error, null, string.Format(MESSAGE_NOT_FOUND, nameof(MonthPayment)));

                await Calculate(creditEntity, payment);
            }

            if (await _unitOfWork.Complete() > 0)
                return new Result<CreditDTO>(BusinessResultState.Success, _mapper.Map<CreditDTO>(creditEntity));

            return new Result<CreditDTO>(BusinessResultState.Error, null, string.Format(MESSAGE_UPDATE_ERROR, nameof(Credit), nameof(MonthPayment)));
        }

        public async Task<Result<CreditDTO>> Calculate(Guid creditId, MonthPaymentDTO monthPayment)
        {
            var creditEntity = await _unitOfWork.Credits.Get(creditId);

            if (creditEntity == null)
                return new Result<CreditDTO>(BusinessResultState.Error, null, string.Format(MESSAGE_NOT_FOUND, nameof(Credit)));

            if (creditEntity.MonthPayments?.Count < monthPayment.Number)
                return new Result<CreditDTO>(BusinessResultState.Error, null, string.Format(MESSAGE_NOT_FOUND, nameof(MonthPayment)));

            await Calculate(creditEntity, monthPayment);

            if (await _unitOfWork.Complete() > 0)
                return new Result<CreditDTO>(BusinessResultState.Success, _mapper.Map<CreditDTO>(creditEntity));

            return new Result<CreditDTO>(BusinessResultState.Error, null, string.Format(MESSAGE_UPDATE_ERROR, nameof(Credit), nameof(MonthPayment)));
        }

        private async Task Calculate(Credit credit, MonthPaymentDTO monthPayment)
        {
            var creditAmount = credit.Amount;
            for (int i = 0; i < monthPayment.Number; i++)
            {
                var payment = credit.MonthPayments.FirstOrDefault(x => x.Number == i + 1);
                if (i == monthPayment.Number - 1)
                {
                    payment.Payment = monthPayment.Payment;
                }
                creditAmount -= payment.Payment - payment.PersentegesPyment;
            }

            var newMonthMainDebtPayment = creditAmount / (credit.MonthsCount - monthPayment.Number);

            for (int i = monthPayment.Number + 1; i < credit.MonthPayments.Count; i++)
            {
                var payment = credit.MonthPayments.FirstOrDefault(x => x.Number == i);
                payment.PersentegesPyment = creditAmount * payment.Persentege / 100 / 12;

                payment.Payment = payment.PersentegesPyment + newMonthMainDebtPayment;

                if (creditAmount - newMonthMainDebtPayment < 0)
                {
                    newMonthMainDebtPayment = creditAmount;
                }

                creditAmount -= newMonthMainDebtPayment;
            }
            //await _unitOfWork.Complete();
        }
    }
}
