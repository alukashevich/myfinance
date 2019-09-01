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
    public class SpecialPercentageController : BaseController<SpecialPercentagePeriodDTO>
    {
        private readonly ISpecialPeriodSrvice _specSrvice;
        public SpecialPercentageController(ISpecialPeriodSrvice specSrvice) : base(specSrvice)
        {
            _specSrvice = specSrvice;
        }
    }
}