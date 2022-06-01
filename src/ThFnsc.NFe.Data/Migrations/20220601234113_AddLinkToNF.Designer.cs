﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThFnsc.NFe.Data.Context;

#nullable disable

namespace ThFnsc.NFe.Data.Migrations
{
    [DbContext(typeof(NFContext))]
    [Migration("20220601234113_AddLinkToNF")]
    partial class AddLinkToNF
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("NFNotifierScheduledGeneration", b =>
                {
                    b.Property<int>("NotifiersId")
                        .HasColumnType("int");

                    b.Property<int>("ScheduledGenerationsId")
                        .HasColumnType("int");

                    b.HasKey("NotifiersId", "ScheduledGenerationsId");

                    b.HasIndex("ScheduledGenerationsId");

                    b.ToTable("NFNotifierScheduledGeneration");
                });

            modelBuilder.Entity("ThFnsc.NFe.Core.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("longtext");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Complement")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Neighborhood")
                        .HasColumnType("longtext");

                    b.Property<string>("PostalCode")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("State")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Street")
                        .HasColumnType("longtext");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("PostalCode");

                    b.HasIndex("State");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ThFnsc.NFe.Core.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("DocIdentifier")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("DocType")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("DeletedAt");

                    b.HasIndex("DocIdentifier");

                    b.HasIndex("DocType");

                    b.HasIndex("Email");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("ThFnsc.NFe.Core.Entities.IssuedNFe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("AliquotPercentage")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("DocumentToId")
                        .HasColumnType("int");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset>("IssuedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LinkToNF")
                        .HasColumnType("longtext");

                    b.Property<int?>("ProviderId")
                        .HasColumnType("int");

                    b.Property<string>("ReturnedContent")
                        .HasColumnType("longtext");

                    b.Property<byte[]>("ReturnedPDF")
                        .HasColumnType("MEDIUMBLOB");

                    b.Property<string>("ReturnedXMLContent")
                        .HasColumnType("longtext");

                    b.Property<string>("SentContent")
                        .HasColumnType("longtext");

                    b.Property<int>("Series")
                        .HasColumnType("int");

                    b.Property<string>("ServiceDescription")
                        .HasColumnType("longtext");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<bool?>("Success")
                        .HasColumnType("tinyint(1)");

                    b.Property<float>("Value")
                        .HasColumnType("float");

                    b.Property<string>("VerificationCode")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("DeletedAt");

                    b.HasIndex("DocumentToId");

                    b.HasIndex("IssuedAt");

                    b.HasIndex("ProviderId");

                    b.HasIndex("Series");

                    b.HasIndex("Success");

                    b.HasIndex("VerificationCode");

                    b.ToTable("NFes");
                });

            modelBuilder.Entity("ThFnsc.NFe.Core.Entities.NFNotifier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<TimeSpan>("Delay")
                        .HasColumnType("time(6)");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("JsonData")
                        .HasColumnType("longtext");

                    b.Property<string>("NotifierType")
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("DeletedAt");

                    b.ToTable("NFNotifiers");
                });

            modelBuilder.Entity("ThFnsc.NFe.Core.Entities.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Data")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("IssuerId")
                        .HasColumnType("int");

                    b.Property<string>("TownHallType")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("DeletedAt");

                    b.HasIndex("IssuerId");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("ThFnsc.NFe.Core.Entities.ScheduledGeneration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("AliquotPercentage")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CronPattern")
                        .HasColumnType("longtext");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("Enabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("ProviderId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceDescription")
                        .HasColumnType("longtext");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<int?>("ToDocumentId")
                        .HasColumnType("int");

                    b.Property<float>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("DeletedAt");

                    b.HasIndex("Enabled");

                    b.HasIndex("ProviderId");

                    b.HasIndex("ToDocumentId");

                    b.ToTable("ScheduledGenerations");
                });

            modelBuilder.Entity("NFNotifierScheduledGeneration", b =>
                {
                    b.HasOne("ThFnsc.NFe.Core.Entities.NFNotifier", null)
                        .WithMany()
                        .HasForeignKey("NotifiersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ThFnsc.NFe.Core.Entities.ScheduledGeneration", null)
                        .WithMany()
                        .HasForeignKey("ScheduledGenerationsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ThFnsc.NFe.Core.Entities.Document", b =>
                {
                    b.HasOne("ThFnsc.NFe.Core.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ThFnsc.NFe.Core.Entities.IssuedNFe", b =>
                {
                    b.HasOne("ThFnsc.NFe.Core.Entities.Document", "DocumentTo")
                        .WithMany()
                        .HasForeignKey("DocumentToId");

                    b.HasOne("ThFnsc.NFe.Core.Entities.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId");

                    b.Navigation("DocumentTo");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("ThFnsc.NFe.Core.Entities.Provider", b =>
                {
                    b.HasOne("ThFnsc.NFe.Core.Entities.Document", "Issuer")
                        .WithMany()
                        .HasForeignKey("IssuerId");

                    b.Navigation("Issuer");
                });

            modelBuilder.Entity("ThFnsc.NFe.Core.Entities.ScheduledGeneration", b =>
                {
                    b.HasOne("ThFnsc.NFe.Core.Entities.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId");

                    b.HasOne("ThFnsc.NFe.Core.Entities.Document", "ToDocument")
                        .WithMany()
                        .HasForeignKey("ToDocumentId");

                    b.Navigation("Provider");

                    b.Navigation("ToDocument");
                });
#pragma warning restore 612, 618
        }
    }
}
