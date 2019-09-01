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
    public class MonthPaymentController : BaseController<MonthPaymentDTO>
    {
        private readonly IMonthPaymentSrvice _monthPaymentService;
        public MonthPaymentController(IMonthPaymentSrvice monthPaymentService): base(monthPaymentService)
        {
            _monthPaymentService = monthPaymentService;
        }

        [HttpPost("{creditId}/calculate")]
        public async Task<Result<CreditDTO>> Calculate(/*[FromRoute]Guid creditId, [FromBody] MonthPaymentDTO monthPayment*/)
        {
            var creditId = new Guid("afef49de-fd82-e911-9ecb-9ab95a49928e");
            var monthPayment = new MonthPaymentDTO
            {
                Number = 1,
                Payment = 6000
            };
            return await _monthPaymentService.Calculate(creditId, monthPayment);
        }

        [HttpPost("calculatelist/{creditId}")]
        public async Task<Result<CreditDTO>> CalculateList([FromBody] List<MonthPaymentDTO> payments, [FromRoute] Guid creditId)
        {
            return await _monthPaymentService.Calculate(creditId, payments);
        }
    }
}