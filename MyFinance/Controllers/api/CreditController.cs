using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Data.DTOs;
using MyFinance.Data.DTOs.Results;
using MyFinance.Data.Enums;
using MyFinance.Services.Interfaces;

namespace MyFinance.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditController : BaseController<CreditDTO>
    {
        private readonly ICreditSrvice _creditSrvice;
        public CreditController(ICreditSrvice creditService) : base(creditService)
        {
            _creditSrvice = creditService;
        }

        [HttpPost("calculate")]
        public async Task<Result<CreditDTO>> Calculate([FromBody] CreditDTO credit)
        {
            //*********SAMPLE DATA****************************

            //var credit = new CreditDTO
            //{
            //    Id = new Guid("afef49de-fd82-e911-9ecb-9ab95a49928e"),
            //    Amount = 50000m,
            //    BasePercenteges = 14.5m,
            //    MonthsCount = 10,
            //    SpecialPersentages = new List<SpecialPercentagePeriodDTO>
            //    {
            //        new SpecialPercentagePeriodDTO
            //        {
            //            MonthCount = 3,
            //            MonthPercentage = 9
            //        }
            //    }
            //};
            return await _creditSrvice.Calculate(credit);
        }
    }
}