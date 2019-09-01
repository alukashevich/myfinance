using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFinance.Data.DTOs.Results;
using MyFinance.Services.Interfaces;

namespace MyFinance.Controllers.api
{
    [ApiController]
    public class BaseController<DtoType> : ControllerBase
    {
        protected readonly IBaseService<DtoType> _baseService;

        public BaseController(IBaseService<DtoType> service)
        {
            _baseService = service;
        }

        [HttpGet]
        public virtual Result<List<DtoType>> GetAll()
        {
            return _baseService.GetAll();
        }

        [HttpGet("{id}")]
        public virtual async Task<Result<DtoType>> Get([FromRoute] Guid id)
        {
            return await _baseService.Get(id);
        }

        [HttpPost]
        public virtual async Task<Result<DtoType>> Create([FromBody] DtoType dto)
        {
            return await _baseService.Create(dto);
        }

        [HttpPut("{id}")]
        public virtual async Task<Result<DtoType>> Update([FromRoute] Guid id, [FromBody] DtoType dto)
        {
            return await _baseService.Update(dto);
        }

        [HttpDelete("{id}")]
        public virtual async Task<Result<bool>> Delete([FromRoute] Guid id)
        {
            return await _baseService.Delete(id);
        }
    }
}