﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Partner.Dal;

namespace Partner.Dal.Migrations
{
    [DbContext(typeof(PartnerDbContext))]
    [Migration("20221208130530_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Partner.Domain.Entities.Contact", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("PartnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("PartnerId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Partner.Domain.Entities.DeliveryPartner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ContactPerson")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaxNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Partners");
                });

            modelBuilder.Entity("Partner.Domain.Entities.TrackingNumber", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid?>("DeliveryPartnerId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("DeliveryPartnerId");

                    b.ToTable("TrackingNumber");
                });

            modelBuilder.Entity("Partner.Domain.Entities.Contact", b =>
                {
                    b.HasOne("Partner.Domain.Entities.DeliveryPartner", "Partner")
                        .WithMany("Contacts")
                        .HasForeignKey("PartnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("Partner.Domain.Entities.AddressData", "Address", b1 =>
                        {
                            b1.Property<Guid>("ContactId")
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

                            b1.HasKey("ContactId");

                            b1.ToTable("Contacts");

                            b1.WithOwner()
                                .HasForeignKey("ContactId");
                        });

                    b.Navigation("Address");

                    b.Navigation("Partner");
                });

            modelBuilder.Entity("Partner.Domain.Entities.TrackingNumber", b =>
                {
                    b.HasOne("Partner.Domain.Entities.DeliveryPartner", null)
                        .WithMany("TrackingNumbers")
                        .HasForeignKey("DeliveryPartnerId");
                });

            modelBuilder.Entity("Partner.Domain.Entities.DeliveryPartner", b =>
                {
                    b.Navigation("Contacts");

                    b.Navigation("TrackingNumbers");
                });
#pragma warning restore 612, 618
        }
    }
}
