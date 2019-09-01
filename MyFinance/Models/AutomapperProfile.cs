using AutoMapper;
using MyFinance.Data;
using MyFinance.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFinance.Models
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Decimal, Decimal>().ConvertUsing(x => decimal.Round(x, 2));
            CreateMap<CreditDTO, Credit>()
                .ForMember(x => x.MonthPayments, opt => opt.Ignore())
                .ForMember(x => x.SpecialPersentages, opt => opt.Ignore());

            CreateMap<Credit, CreditDTO>();

            CreateMap<MonthPayment, MonthPaymentDTO>()
                .ForMember(x => x.Credit, opt => opt.Ignore());

            CreateMap<MonthPaymentDTO, MonthPayment>();

            CreateMap<SpecialPercentagePeriod, SpecialPercentagePeriodDTO>()
                .ForMember(x => x.Credit, opt => opt.Ignore());

            CreateMap<SpecialPercentagePeriodDTO, SpecialPercentagePeriod>();
        }
    }
}
