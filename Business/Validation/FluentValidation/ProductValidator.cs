using Entities.Concrete;
using Entities.Dto.ProductDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    partial class ProductValidator:AbstractValidator<ProductAddDto>
    {
        public ProductValidator()
        {
            RuleFor(p=>p.Name).NotNull().WithMessage("Name bos qoyula bilmez").MinimumLength(3).WithMessage("minimum uzunluq 3 olmalidir");
            RuleFor(p => p.Price).NotNull().WithMessage("Price bos qoyla bilmez").GreaterThan(20);
        }
    }
}
