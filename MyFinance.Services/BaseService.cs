using AutoMapper;
using MyFinance.Data.DTOs;
using MyFinance.Data.DTOs.Results;
using MyFinance.Repo;
using MyFinance.Repo.Intarfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyFinance.Services
{
    public class BaseService<DTOType, ObjectType>
        where ObjectType : class
        where DTOType : BaseDTO
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected string _typeName;

        protected const string MESSAGE_NOT_FOUND = "{0} not found";
        protected const string MESSAGE_CREATE_SUCCESS = "{0} {1} created successfully";
        protected const string MESSAGE_CREATE_ERROR = "{0} {1} not created";
        protected const string MESSAGE_UPDATE_SUCCESS = "{0} {1} updated successfully";
        protected const string MESSAGE_UPDATE_ERROR = "{0} {1} not updated";
        protected const string MESSAGE_DELETE_SUCCESS = "{0} {1} deleted successfully";
        protected const string MESSAGE_DELETE_ERROR = "{0} {1} not deleted";

        protected IBaseRepository<ObjectType> _baseRepository
        {
            get
            {
                if (_unitOfWork == null)
                    throw new NullReferenceException("_unitOfWork cannot be null");
                var repo = _unitOfWork.Repositories[typeof(ObjectType).Name];

                if (_unitOfWork == null)
                    throw new NullReferenceException("repository not found");

                return repo as IBaseRepository<ObjectType>;
            }
        }

        public BaseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public virtual async Task<Result<DTOType>> Create(DTOType dto)
        {
            var entity = _mapper.Map<ObjectType>(dto);

            await _baseRepository.Add(entity);

            if (await _unitOfWork.Complete() > 0)
            {
                _mapper.Map(entity, dto);
                return SuccessCreate(dto, dto.Id);
            }
            else
                return ErrorCreate(dto, Guid.Empty);
        }

        public async Task<Result<bool>> Delete(Guid id)
        {
            var entity = await _baseRepository.Get(id);
            if (entity == null)
                return ErrorNotFound();
            _baseRepository.Remove(entity);

            if (await _unitOfWork.Complete() > 0)
            {
                return SuccessDelete(id);
            }

            return ErrorDelete(id);
        }

        public virtual async Task<Result<DTOType>> Get(Guid id)
        {
            var entity = await _baseRepository.Get(id);
            if (entity == null)
                return ErrorNotFound(null);

            return SuccessEntity(_mapper.Map<DTOType>(entity));
        }

        public Result<List<DTOType>> GetAll()
        {
            var entities = _baseRepository.GetAll();
            return SuccessEntities(_mapper.Map<List<DTOType>>(entities));
        }

        public async Task<Result<DTOType>> Update(DTOType dto)
        {
            var entity = await _baseRepository.Get(dto.Id);
            if (entity == null)
                return ErrorNotFound(null);

            _mapper.Map(dto, entity);

            if (await _unitOfWork.Complete() > -1)
                return SuccessUpdate(dto, dto.Id);

            return ErrorUpdate(null, dto.Id);
        }

        protected Result<DTOType> ErrorNotFound(DTOType dto, string typeName = null)
        {
            return new Result<DTOType>(BusinessResultState.Error, dto, string.Format(MESSAGE_NOT_FOUND, typeName ?? _typeName));
        }

        protected Result<bool> ErrorNotFound()
        {
            return new Result<bool>(BusinessResultState.Error, false, string.Format(MESSAGE_NOT_FOUND, _typeName));
        }

        protected Result<DTOType> SuccessEntity(DTOType dto)
        {
            return new Result<DTOType>(BusinessResultState.Success, dto);
        }

        protected Result<List<DTOType>> SuccessEntities(List<DTOType> listDTO)
        {
            return new Result<List<DTOType>>(BusinessResultState.Success, listDTO);
        }

        protected Result<DTOType> SuccessCreate(DTOType dto, string name)
        {
            return new Result<DTOType>(BusinessResultState.Success, dto, string.Format(MESSAGE_CREATE_SUCCESS, _typeName, name));
        }

        protected Result<DTOType> SuccessCreate(DTOType dto, Guid uid)
        {
            return new Result<DTOType>(BusinessResultState.Success, dto, string.Format(MESSAGE_CREATE_SUCCESS, _typeName, uid));
        }

        protected Result<DTOType> SuccessCreate(DTOType dto, long requestId)
        {
            return new Result<DTOType>(BusinessResultState.Success, dto, string.Format(MESSAGE_CREATE_SUCCESS, _typeName, requestId));
        }

        protected Result<DTOType> ErrorCreate(DTOType dto, string name)
        {
            return new Result<DTOType>(BusinessResultState.Error, dto, string.Format(MESSAGE_CREATE_ERROR, _typeName, name));
        }

        protected Result<DTOType> ErrorCreate(DTOType dto, Guid uid)
        {
            return new Result<DTOType>(BusinessResultState.Error, dto, string.Format(MESSAGE_CREATE_ERROR, _typeName, uid));
        }

        protected Result<DTOType> SuccessUpdate(DTOType dto, string name)
        {
            return new Result<DTOType>(BusinessResultState.Success, dto, string.Format(MESSAGE_UPDATE_SUCCESS, _typeName, name));
        }

        protected Result<DTOType> SuccessUpdate(DTOType dto, Guid uid)
        {
            return new Result<DTOType>(BusinessResultState.Success, dto, string.Format(MESSAGE_UPDATE_SUCCESS, _typeName, uid));
        }

        protected Result<DTOType> ErrorUpdate(DTOType dto, string name)
        {
            return new Result<DTOType>(BusinessResultState.Error, dto, string.Format(MESSAGE_UPDATE_ERROR, _typeName, name));
        }

        protected Result<DTOType> ErrorUpdate(DTOType dto, Guid uid)
        {
            return new Result<DTOType>(BusinessResultState.Error, dto, string.Format(MESSAGE_UPDATE_ERROR, _typeName, uid));
        }

        protected Result<bool> SuccessDelete(string name)
        {
            return new Result<bool>(BusinessResultState.Success, false, string.Format(MESSAGE_DELETE_SUCCESS, _typeName, name));
        }

        protected Result<bool> SuccessDelete(Guid uid)
        {
            return new Result<bool>(BusinessResultState.Success, false, string.Format(MESSAGE_DELETE_SUCCESS, _typeName, uid));
        }

        protected Result<bool> ErrorDelete(string name)
        {
            return new Result<bool>(BusinessResultState.Error, false, string.Format(MESSAGE_DELETE_ERROR, _typeName, name));
        }

        protected Result<bool> ErrorDelete(Guid uid)
        {
            return new Result<bool>(BusinessResultState.Error, false, string.Format(MESSAGE_DELETE_ERROR, _typeName, uid));
        }
    }
}
