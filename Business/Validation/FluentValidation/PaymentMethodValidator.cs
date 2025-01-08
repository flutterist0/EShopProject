using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Validation.FluentValidation
{
    public class PaymentMethodValidator: AbstractValidator<PaymentMethod>
    {
        public PaymentMethodValidator()
        {
            RuleFor(p => p.MethodName).NotNull().WithMessage("Bos qoyula bilmez").MinimumLength(3).WithMessage("Minimum length 3 dur");
        }
    }
}
