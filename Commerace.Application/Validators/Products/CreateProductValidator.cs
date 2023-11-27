using Commerace.Application.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerace.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<ProductViewDto>
    {

        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull().WithMessage("Adi bos gecmeyiniz.").MinimumLength(3).MaximumLength(15);



        }


    }
}
