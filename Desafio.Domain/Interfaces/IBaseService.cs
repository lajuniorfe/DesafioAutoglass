using Desafio.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Domain.Interfaces
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        public TOutputModel Add<TInputModel, TOutputModel, TValidator>(TInputModel objInput)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;

        //public TOutputModel Delete<TInputModel, TOutputModel, TValidator>(int objInput)
        //    where TValidator : AbstractValidator<TEntity>
        //    where TInputModel : class
        //    where TOutputModel : class;

        public IEnumerable<TOutputModel> Get<TOutputModel>(int NumeroPagina, int quantidadeRegistro) where TOutputModel: class;
        public TOutputModel GetById<TOutputModel>(int Codigo) where TOutputModel : class;


        public TOutputModel Update<TInputModel, TOutputModel, TValidator>(TInputModel objInput)
            where TValidator : AbstractValidator<TEntity>
            where TInputModel : class
            where TOutputModel : class;

    }
}
