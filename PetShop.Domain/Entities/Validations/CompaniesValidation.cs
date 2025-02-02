using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShop.Domain.Entities.Validations
{
    public class CompaniesValidation : AbstractValidator<Companies>
    {
        public CompaniesValidation() 
        {
            RuleFor(p => p.RegistrationNumber)
                .NotEmpty()
                    .WithMessage("the field RegistrationNumber cannot be empty")
                .MinimumLength(14)
                .MaximumLength(18)
                    .WithMessage("The RegistrationNumber field must be between {MinLength} and {MaxLength} characters");
            
            RuleFor(p=> p.CompanyName)
                .NotEmpty()
                .WithMessage("The field CompanyName Cannot be empty");

            RuleFor(p => p.TradeName)
                .NotEmpty()
                .WithMessage("The field TradeName Cannot be empty");

            RuleFor(p => p.Email)
                .NotEmpty()
                .WithMessage("The field Email Cannot be empty");

            RuleFor(p => p.PhoneNumber)
                .NotEmpty()
                .WithMessage("The field PhoneNumber Cannot be empty");
        }
    }
}
