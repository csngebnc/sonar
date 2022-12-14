// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shipping.Dal;

namespace Shipping.Dal.Migrations
{
    [DbContext(typeof(ShippingDbContext))]
    partial class ShippingDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExtraOptionParcel", b =>
                {
                    b.Property<Guid>("ExtraOptionsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ParcelsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ExtraOptionsId", "ParcelsId");

                    b.HasIndex("ParcelsId");

                    b.ToTable("ExtraOptionParcel");
                });

            modelBuilder.Entity("Shipping.Domain.Entities.ExtraOptionAggregate.ExtraOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ExtraOptions");
                });

            modelBuilder.Entity("Shipping.Domain.Entities.LockerAggregate.Locker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lockers");
                });

            modelBuilder.Entity("Shipping.Domain.Entities.ParcelAggregate.Events.ParcelEventBase", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ParcelId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ParcelId");

                    b.ToTable("ParcelEventBase");
                });

            modelBuilder.Entity("Shipping.Domain.Entities.ParcelAggregate.Parcel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CashOnDeliveryPrice")
                        .HasColumnType("int");

                    b.Property<Guid?>("LockerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("RegisteredAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<string>("TrackingNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LockerId");

                    b.ToTable("Parcels");
                });

            modelBuilder.Entity("Shipping.Domain.Entities.ShippingUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("85b4a2a3-aa22-40e4-9b91-c72e96ab8e08"),
                            Type = 0,
                            Username = "csngebnc"
                        });
                });

            modelBuilder.Entity("ExtraOptionParcel", b =>
                {
                    b.HasOne("Shipping.Domain.Entities.ExtraOptionAggregate.ExtraOption", null)
                        .WithMany()
                        .HasForeignKey("ExtraOptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Shipping.Domain.Entities.ParcelAggregate.Parcel", null)
                        .WithMany()
                        .HasForeignKey("ParcelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shipping.Domain.Entities.LockerAggregate.Locker", b =>
                {
                    b.OwnsOne("Shipping.Domain.Entities.Data.AddressData", "Address", b1 =>
                        {
                            b1.Property<Guid>("LockerId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("County")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Other")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("StreetAndNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("LockerId");

                            b1.ToTable("Lockers");

                            b1.WithOwner()
                                .HasForeignKey("LockerId");
                        });

                    b.Navigation("Address");
                });

            modelBuilder.Entity("Shipping.Domain.Entities.ParcelAggregate.Events.ParcelEventBase", b =>
                {
                    b.HasOne("Shipping.Domain.Entities.ParcelAggregate.Parcel", null)
                        .WithMany("Events")
                        .HasForeignKey("ParcelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Shipping.Domain.Entities.ParcelAggregate.Parcel", b =>
                {
                    b.HasOne("Shipping.Domain.Entities.LockerAggregate.Locker", "Locker")
                        .WithMany()
                        .HasForeignKey("LockerId");

                    b.OwnsOne("Shipping.Domain.Entities.Data.AddressData", "DeliveryAddress", b1 =>
                        {
                            b1.Property<Guid>("ParcelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("County")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Other")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("StreetAndNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ParcelId");

                            b1.ToTable("Parcels");

                            b1.WithOwner()
                                .HasForeignKey("ParcelId");
                        });

                    b.OwnsOne("Shipping.Domain.Entities.Data.AddressData", "PickupAddress", b1 =>
                        {
                            b1.Property<Guid>("ParcelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("City")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Country")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("County")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Other")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PostalCode")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("StreetAndNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ParcelId");

                            b1.ToTable("Parcels");

                            b1.WithOwner()
                                .HasForeignKey("ParcelId");
                        });

                    b.OwnsOne("Shipping.Domain.Entities.Data.ParcelSize", "Size", b1 =>
                        {
                            b1.Property<Guid>("ParcelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("Depth")
                                .HasColumnType("int");

                            b1.Property<int>("Height")
                                .HasColumnType("int");

                            b1.Property<double>("Weight")
                                .HasColumnType("float");

                            b1.Property<int>("Width")
                                .HasColumnType("int");

                            b1.HasKey("ParcelId");

                            b1.ToTable("Parcels");

                            b1.WithOwner()
                                .HasForeignKey("ParcelId");
                        });

                    b.OwnsOne("Shipping.Domain.Entities.Data.PersonData", "Receiver", b1 =>
                        {
                            b1.Property<Guid>("ParcelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PhoneNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ParcelId");

                            b1.ToTable("Parcels");

                            b1.WithOwner()
                                .HasForeignKey("ParcelId");
                        });

                    b.OwnsOne("Shipping.Domain.Entities.Data.PersonData", "Sender", b1 =>
                        {
                            b1.Property<Guid>("ParcelId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Email")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("Name")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<string>("PhoneNumber")
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ParcelId");

                            b1.ToTable("Parcels");

                            b1.WithOwner()
                                .HasForeignKey("ParcelId");
                        });

                    b.Navigation("DeliveryAddress");

                    b.Navigation("Locker");

                    b.Navigation("PickupAddress");

                    b.Navigation("Receiver");

                    b.Navigation("Sender");

                    b.Navigation("Size");
                });

            modelBuilder.Entity("Shipping.Domain.Entities.ParcelAggregate.Parcel", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
