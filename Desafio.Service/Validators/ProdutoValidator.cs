using Desafio.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Service.Validators
{
    public class ProdutoValidator : AbstractValidator<Produtos>
    {
        public ProdutoValidator()
        {

            RuleFor(e => e.Descricao).NotNull().NotEmpty().WithMessage("Descrição obrigatória");
        }

       
    }


}
