using AutoMapper;
using Desafio.Domain.Entities;
using Desafio.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IMapper _mapper;

        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }

        public TOutputModel Add<TInputModel, TOutputModel, TValidator>(TInputModel objInput)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class
        {
            try
            {
                TEntity entity = _mapper.Map<TEntity>(objInput);
                if (entity.Codigo > 0)
                    throw new Exception("Codigo do produto não pode ser maior que zero");

                Validate(entity, Activator.CreateInstance<TValidator>());
                _baseRepository.Insert(entity);

                TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
                return outputModel;
            }
            catch(Exception e)
            {
                 throw e;
            }
        }

        public IEnumerable<TOutputModel> Get<TOutputModel>(int NumeroPagina, int quantidadeRegistro)
            where TOutputModel : class

        {
            if (NumeroPagina <= 0 || quantidadeRegistro <= 0)
                throw new Exception("");

            var entity = _baseRepository.Select(NumeroPagina, quantidadeRegistro);
            var outputmodel = entity.Select(s => _mapper.Map<TOutputModel>(s));

            return outputmodel;
        }
        public TOutputModel GetById<TOutputModel>(int Codigo) where TOutputModel : class
        {
            if (Codigo == 0)
                throw new Exception("Codigo de produto não pode ser igual a zero");

            var entity = _baseRepository.Select(Codigo);
            var outputModel = _mapper.Map<TOutputModel>(entity);

            return outputModel;
        }
        public TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel objInput)
                where TValidator : AbstractValidator<TEntity>
                where TInputModel : class
                where TOutputModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(objInput);

            if (entity.Codigo > 0)
                throw new Exception("Codigo do produto não pode ser maior que zero");

            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Update(entity);

            TOutputModel outputModel = _mapper.Map<TOutputModel>(entity);
            return outputModel;

        }

        private void Validate<TValidator>(TEntity obj, TValidator validator) where TValidator : AbstractValidator<TEntity>
        {
            if (obj == null)
                throw new Exception("Registros não detectados");

            validator.ValidateAndThrow(obj);
        }


    }
}
