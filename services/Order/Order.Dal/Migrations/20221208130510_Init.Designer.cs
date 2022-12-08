﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Order.Dal;

namespace Order.Dal.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20221208130510_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Order.Domain.Entities.ExtraOptionAggregate.ExtraOption", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ExtraOptions");
                });

            modelBuilder.Entity("Order.Domain.Entities.LockerAggregate.Locker", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Lockers");
                });

            modelBuilder.Entity("Order.Domain.Entities.ParcelAggregate.Parcel", b =>
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

                    b.Property<string>("TrackingNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LockerId");

                    b.ToTable("Parcels");
                });

            modelBuilder.Entity("ExtraOptionParcel", b =>
                {
                    b.HasOne("Order.Domain.Entities.ExtraOptionAggregate.ExtraOption", null)
                        .WithMany()
                        .HasForeignKey("ExtraOptionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Order.Domain.Entities.ParcelAggregate.Parcel", null)
                        .WithMany()
                        .HasForeignKey("ParcelsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Order.Domain.Entities.LockerAggregate.Locker", b =>
                {
                    b.OwnsOne("Order.Domain.Entities.Data.AddressData", "Address", b1 =>
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

            modelBuilder.Entity("Order.Domain.Entities.ParcelAggregate.Parcel", b =>
                {
                    b.HasOne("Order.Domain.Entities.LockerAggregate.Locker", "Locker")
                        .WithMany()
                        .HasForeignKey("LockerId");

                    b.OwnsOne("Order.Domain.Entities.Data.AddressData", "DeliveryAddress", b1 =>
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

                    b.OwnsOne("Order.Domain.Entities.Data.AddressData", "PickupAddress", b1 =>
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

                    b.OwnsOne("Order.Domain.Entities.Data.ParcelSize", "Size", b1 =>
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

                    b.OwnsOne("Order.Domain.Entities.Data.PersonData", "Receiver", b1 =>
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

                    b.OwnsOne("Order.Domain.Entities.Data.PersonData", "Sender", b1 =>
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
#pragma warning restore 612, 618
        }
    }
}