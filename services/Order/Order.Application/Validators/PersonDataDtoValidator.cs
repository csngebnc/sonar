using FluentValidation;
using Order.Application.Features.ParcelAggregate.Data;

namespace Order.Application.Validators
{
    internal class PersonDataDtoValidator : AbstractValidator<PersonDataDto>
    {
        public PersonDataDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}