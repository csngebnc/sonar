using FluentValidation;
using Order.Application.Features.ParcelAggregate.Data;

namespace Order.Application.Validators
{
    internal class AddressDataDtoValidator : AbstractValidator<AddressDataDto>
    {
        public AddressDataDtoValidator()
        {
            RuleFor(x => x.Country)
                .NotEmpty();

            RuleFor(x => x.County)
                .NotEmpty();

            RuleFor(x => x.PostalCode)
                .NotEmpty();

            RuleFor(x => x.City)
                .NotEmpty();

            RuleFor(x => x.StreetAndNumber)
                .NotEmpty();
        }
    }
}
