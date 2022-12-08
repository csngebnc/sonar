using FluentValidation;
using MassTransit;
using MediatR;
using Order.Application.Features.ParcelAggregate.Data;
using Order.Application.Validators;
using Order.Domain.Entities.ExtraOptionAggregate;
using Order.Domain.Entities.LockerAggregate;
using Order.Domain.Entities.ParcelAggregate;
using SharedKernel.Enums;
using Status.Interface.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Order.Application.Features.ParcelAggregate.Commands
{
    public class SendParcelsCommand : IRequest<List<string>>
    {
        public List<ParcelDto> Parcels { get; set; }
        public PersonDataDto Sender { get; set; }
        public AddressDataDto PickupAddress { get; set; }

        public class ParcelDto
        {
            public PersonDataDto Receiver { get; set; }
            public ParcelSizeDto Size { get; set; }
            public ParcelType Type { get; set; }
            public Guid? LockerId { get; set; }
            public AddressDataDto DeliveryAddress { get; set; }
            public List<Guid> ExtraOptions { get; set; }
        }
    }

    public class SendMultipleParcelCommandHandler : IRequestHandler<SendParcelsCommand, List<string>>
    {
        private readonly IParcelRepository parcelRepository;
        private readonly ILockerRepository lockerRepository;
        private readonly IPublishEndpoint publishEndpoint;
        private readonly IExtraOptionRepository extraOptionRepository;

        public SendMultipleParcelCommandHandler(IParcelRepository parcelRepository, ILockerRepository lockerRepository, IPublishEndpoint publishEndpoint, IExtraOptionRepository extraOptionRepository)
        {
            this.parcelRepository = parcelRepository;
            this.lockerRepository = lockerRepository;
            this.publishEndpoint = publishEndpoint;
            this.extraOptionRepository = extraOptionRepository;
        }

        public async Task<List<string>> Handle(SendParcelsCommand request, CancellationToken cancellationToken)
        {
            var count = await parcelRepository.GetCountAsync() + 1000000000;
            var parcels = new List<Parcel>();
            var options = await extraOptionRepository.GetAllListAsync();
            var lockers = await lockerRepository.GetAllListAsync();

            foreach (var parcelDto in request.Parcels)
            {
                var parcel = new Parcel
                {
                    Sender = new()
                    {
                        Name = request.Sender.Name,
                        Email = request.Sender.Email,
                        PhoneNumber = request.Sender.PhoneNumber
                    },
                    Receiver = new()
                    {
                        Name = parcelDto.Receiver.Name,
                        Email = parcelDto.Receiver.Email,
                        PhoneNumber = parcelDto.Receiver.PhoneNumber
                    },
                    PickupAddress = new()
                    {
                        Country = request.PickupAddress.Country,
                        County = request.PickupAddress.County,
                        PostalCode = request.PickupAddress.PostalCode,
                        City = request.PickupAddress.City,
                        StreetAndNumber = request.PickupAddress.StreetAndNumber,
                        Other = request.PickupAddress.Other
                    },
                    ExtraOptions = options.Where(option => parcelDto.ExtraOptions.Contains(option.Id)).ToList(),
                    Size = new()
                    {
                        Width = parcelDto.Size.Width,
                        Height = parcelDto.Size.Height,
                        Depth = parcelDto.Size.Depth,
                        Weight = parcelDto.Size.Weight
                    },
                    RegisteredAt = DateTime.UtcNow,
                    Type = parcelDto.Type,
                };

                parcel.TrackingNumber = $"CB{(count++)+1}";

                if (parcel.Type == ParcelType.LOCKER)
                {
                    var locker = lockers.Single(x => x.Id == parcelDto.LockerId);
                    parcel.LockerId = locker.Id;
                    parcel.DeliveryAddress = locker.Address;
                }
                else
                {
                    parcel.DeliveryAddress = new()
                    {
                        Country = parcelDto.DeliveryAddress.Country,
                        County = parcelDto.DeliveryAddress.County,
                        PostalCode = parcelDto.DeliveryAddress.PostalCode,
                        City = parcelDto.DeliveryAddress.City,
                        StreetAndNumber = parcelDto.DeliveryAddress.StreetAndNumber,
                        Other = parcelDto.DeliveryAddress.Other
                    };
                }

                parcels.Add(parcel);
            }


            await parcelRepository.InsertRangeAsync(parcels);

            await publishEndpoint.Publish(new ParcelsCreatedEvent
            {
                Parcels = parcels
                    .Select(parcel => new ParcelsCreatedEvent.ParcelItem
                        {
                            ParcelId = parcel.Id,
                            TrackingNumber = parcel.TrackingNumber,
                            Type = parcel.Type
                        })
                    .ToList()
            }, cancellationToken);

            await publishEndpoint.Publish(new Shipping.Interface.Events.ParcelsCreatedEvent
            {
                Parcels = parcels.Select(x => new Shipping.Interface.Events.ParcelsCreatedEvent.ParcelItem
                {
                    CashOnDeliveryPrice = x.CashOnDeliveryPrice,
                    DeliveryAddress = new()
                    {
                        City = x.DeliveryAddress.City,
                        Country = x.DeliveryAddress.Country,
                        County = x.DeliveryAddress.County,
                        Other = x.DeliveryAddress.Other,
                        PostalCode = x.DeliveryAddress.PostalCode,
                        StreetAndNumber = x.DeliveryAddress.StreetAndNumber,
                    },
                    ExtraOptions = x.ExtraOptions.Select(y => new Shipping.Domain.Entities.ExtraOptionAggregate.ExtraOption
                    {
                        Id = y.Id,
                        Name = y.Name,
                        Price = y.Price,
                    }).ToList(),
                    LockerId = x.LockerId,
                    ParcelId = x.Id,
                    TrackingNumber = x.TrackingNumber,
                    PickupAddress = new()
                    {
                        City = x.PickupAddress.City,
                        Country = x.PickupAddress.Country,
                        County = x.PickupAddress.County,
                        Other = x.PickupAddress.Other,
                        PostalCode = x.PickupAddress.PostalCode,
                        StreetAndNumber = x.PickupAddress.StreetAndNumber
                    },
                    Receiver = new()
                    {
                        Email = x.Receiver.Email,
                        Name = x.Receiver.Name,
                        PhoneNumber = x.Receiver.PhoneNumber,
                    },
                    Sender = new()
                    {
                        Name = x.Sender.Name,
                        PhoneNumber = x.Sender.PhoneNumber,
                        Email = x.Sender.Email,
                    },
                    Size = new()
                    {
                        Depth = x.Size.Depth,
                        Height = x.Size.Height,
                        Weight = x.Size.Weight,
                        Width = x.Size.Width,
                    },
                    Type = x.Type,
                    RegisteredAt = x.RegisteredAt,
                }).ToList()
            });

            return parcels
                .OrderBy(parcel => parcel.TrackingNumber)
                .Select(parcel => parcel.TrackingNumber)
                .ToList();
        }
    }

    public class SendMultipleParcelCommandValidator : AbstractValidator<SendParcelsCommand>
    {
        public SendMultipleParcelCommandValidator()
        {
            RuleForEach(x => x.Parcels)
                .SetValidator(new ParcelDtoValidator());

            RuleFor(x => x.Sender)
            .SetValidator(new PersonDataDtoValidator());

            RuleFor(x => x.PickupAddress)
                .SetValidator(new AddressDataDtoValidator());
        }

        public class ParcelDtoValidator : AbstractValidator<SendParcelsCommand.ParcelDto>
        {
            public ParcelDtoValidator()
            {

                RuleFor(x => x.Receiver)
                    .SetValidator(new PersonDataDtoValidator());

                RuleFor(x => x.Size)
                    .SetValidator(new ParcelSizeDtoValidator());

                RuleFor(x => x.DeliveryAddress)
                    .SetValidator(new AddressDataDtoValidator());

                RuleFor(x => x.Type)
                    .NotEmpty()
                    .IsInEnum();

                RuleFor(x => x.LockerId)
                    .NotEmpty()
                        .When(x => x.Type == ParcelType.LOCKER);
            }
        }
    }
}
