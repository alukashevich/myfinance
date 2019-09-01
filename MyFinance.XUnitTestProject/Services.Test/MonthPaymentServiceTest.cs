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
using MyFinance.Services.Interfaces;

namespace MyFinance.XUnitTestProject.Services.Test
{
    public class MonthPaymentServiceTest
    {
        [Fact]
        public void CalculateDeifferentMonthPayment()
        {
            var unitOfWork = new Mock<IUnitOfWork>();

            var creditId = new Guid("afef49de-fd82-e911-9ecb-9ab95a49928e");
            var credit = GetCredit();

            unitOfWork.Setup(repo => repo.Credits.Get(creditId)).Returns(credit);
            unitOfWork.Setup(repo => repo.Complete()).Returns(() => Task.Run(() => 1));

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var monthPaymentService = new MonthPaymentSrvice(unitOfWork.Object, mapper);

            var result = monthPaymentService.Calculate(creditId, GetMonthPaymentDto());

            Assert.True(result.Result.IsSuccess);

            var returnValue = result.Result.ReturnValue;

            Assert.Equal(returnValue.MonthPayments.FirstOrDefault(x => x.Number == GetMonthPaymentDto().Number).Payment, GetMonthPaymentDto().Payment);

        }

        private async Task<Credit> GetCredit()
        {
            return await Task.Run(() => new Credit
            {
                Id = new Guid("afef49de-fd82-e911-9ecb-9ab95a49928e"),
                Amount = 50000m,
                BasePercenteges = 14.5m,
                MonthsCount = 10,
                SpecialPersentages = new List<SpecialPercentagePeriod>
                {
                    new SpecialPercentagePeriod
                    {
                        MonthCount = 3,
                        MonthPercentage = 9
                    }
                },
                MonthPayments = new List<MonthPayment>
                {
                    new MonthPayment
                    {
                        Id = new Guid(),
                        Number = 1,
                        Payment = 5375.00m,
                        PersentegesPyment = 375.00m,
                        Persentege = 9
                    },
                    new MonthPayment
                    {
                        Id = new Guid(),
                        Number = 2,
                        Payment = 5337.5m,
                        PersentegesPyment = 337.50m,
                        Persentege = 9
                    },
                    new MonthPayment
                    {
                        Id = new Guid(),
                        Number = 3,
                        Payment = 5300.00m,
                        PersentegesPyment = 300.00m,
                        Persentege = 9
                    },
                    new MonthPayment
                    {
                        Id = new Guid(),
                        Number = 4,
                        Payment = 5422.92m,
                        PersentegesPyment = 422.92m,
                        Persentege = 14.5m
                    },
                    new MonthPayment
                    {
                        Id = new Guid(),
                        Number = 5,
                        Payment = 5362.50m,
                        PersentegesPyment = 362.50m,
                        Persentege = 14.5m
                    },
                    new MonthPayment
                    {
                        Id = new Guid(),
                        Number = 6,
                        Payment = 5302.08m,
                        PersentegesPyment = 302.08m,
                        Persentege = 14.5m
                    },
                    new MonthPayment
                    {
                        Id = new Guid(),
                        Number = 7,
                        Payment = 5241.67m,
                        PersentegesPyment = 241.67m,
                        Persentege = 14.5m
                    },
                    new MonthPayment
                    {
                        Id = new Guid(),
                        Number = 8,
                        Payment = 5181.25m,
                        PersentegesPyment = 181.25m,
                        Persentege = 14.5m
                    },
                    new MonthPayment
                    {
                        Id = new Guid(),
                        Number = 9,
                        Payment = 5120.83m,
                        PersentegesPyment = 120.83m,
                        Persentege = 14.5m
                    },
                    new MonthPayment
                    {
                        Id = new Guid(),
                        Number = 10,
                        Payment = 5060.42m,
                        PersentegesPyment = 60.42m,
                        Persentege = 14.5m
                    },
                }
            });
        }

        private MonthPaymentDTO GetMonthPaymentDto()
        {
            return new MonthPaymentDTO
            {
                Number = 1,
                Payment = 6000
            };
        }
    }
}
