using System;
using System.Collections.Generic;
using Xunit;
using Moq;
using MyFinance.Repo;
using AutoMapper;
using MyFinance.Data;
using System.Threading.Tasks;
using MyFinance.Services;
using MyFinance.Data.DTOs;
using System.Linq;
using MyFinance.Models;

namespace MyFinance.XUnitTestProject.Services.Test
{
    public class CreditServiceTest
    {
        [Fact]
        public void CalculateDeifferentCredit()
        {
            var unitOfWork = new Mock<IUnitOfWork>();

            var creditDTO = GetCreditDto();
            var credit = GetCredit();

            unitOfWork.Setup(repo => repo.Credits.Get(GetCreditDto().Id)).Returns(credit);
            unitOfWork.Setup(repo => repo.SpecialPercentagePeriods.AddRange(new List<SpecialPercentagePeriod>())).Returns(() => null);
            unitOfWork.Setup(repo => repo.MonthPayments.AddRange(new List<MonthPayment>())).Returns(() => null);
            unitOfWork.Setup(repo => repo.Complete()).Returns(() => Task.Run(() => 1));

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            var mapper = mockMapper.CreateMapper();


            var creditService = new CreditService(unitOfWork.Object, mapper);

            var result = creditService.Calculate(creditDTO);

            var firstPersantege = GetCreditDto().SpecialPersentages.Count > 0 ? GetCreditDto().SpecialPersentages.First().MonthPercentage : GetCreditDto().BasePercenteges;

            Assert.True(result.Result.IsSuccess);

            Assert.Equal(GetCreditDto().MonthsCount, result.Result?.ReturnValue?.MonthPayments.Count());

            var percentegePayment = GetCreditDto().Amount * firstPersantege / 100 / 12;

            Assert.Equal(percentegePayment, result.Result.ReturnValue.MonthPayments.FirstOrDefault()?.PersentegesPyment);

        }

        private async Task<Credit> GetCredit()
        {
            return await Task.Run(() => new Credit
            {
                Id = new Guid("afef49de-fd82-e911-9ecb-9ab95a49928e")
            });
        }

        private CreditDTO GetCreditDto()
        {
            return new CreditDTO
            {
                Id = new Guid("afef49de-fd82-e911-9ecb-9ab95a49928e"),
                Amount = 50000m,
                BasePercenteges = 14.5m,
                MonthsCount = 10,
                CreditType = Data.Enums.CreditType.Differentiated,
                SpecialPersentages = new List<SpecialPercentagePeriodDTO>
                {
                    new SpecialPercentagePeriodDTO
                    {
                        MonthCount = 3,
                        MonthPercentage = 9
                    }
                }
            };
        }
    }
}
