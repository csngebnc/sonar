using FluentValidation;
using Order.Application.Features.ParcelAggregate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Validators
{
    public class ParcelSizeDtoValidator : AbstractValidator<ParcelSizeDto>
    {
        public ParcelSizeDtoValidator()
        {
            RuleFor(x => x.Width)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Height)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Depth)
                .NotEmpty()
                .GreaterThan(0);

            RuleFor(x => x.Weight)
                .NotEmpty()
                .GreaterThan(0);
        }
    }
}
