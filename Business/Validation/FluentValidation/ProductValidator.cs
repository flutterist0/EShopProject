using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    partial class ProductValidator:AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p=>p.Name).NotNull().WithMessage("Name bos qoyula bilmez").MinimumLength(3).WithMessage("minimum uzunluq 3 olmalidir");
            RuleFor(p => p.Price).NotNull().WithMessage("Price bos qoyla bilmez").LessThan(20);
        }
    }
}
