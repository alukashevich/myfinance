using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Models;

namespace MyFinance.Controllers
{
    [Route("api/[controller]")]
    public class RegisterController : Controller
    {
        [HttpPost]
        public object Register([FromBody] Register model)
        {
            return new { ReturnValue = model };
        }
    }
}
