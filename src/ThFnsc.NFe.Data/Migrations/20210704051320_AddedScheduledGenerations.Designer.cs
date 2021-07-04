﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ThFnsc.NFe.Data.Context;

namespace ThFnsc.NFe.Data.Migrations
{
    [DbContext(typeof(NFContext))]
    [Migration("20210704051320_AddedScheduledGenerations")]
    partial class AddedScheduledGenerations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.7");

            modelBuilder.Entity("ThFnsc.NFe.Data.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Complement")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp");

                    b.Property<string>("Neighborhood")
                        .HasColumnType("text");

                    b.Property<string>("PostalCode")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("State")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Street")
                        .HasColumnType("text");

                    b.Property<string>("StreetNumber")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("PostalCode");

                    b.HasIndex("State");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ThFnsc.NFe.Data.Entities.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int?>("AddressId")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp");

                    b.Property<string>("DocIdentifier")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("DocType")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("DeletedAt");

                    b.HasIndex("DocIdentifier");

                    b.HasIndex("DocType");

                    b.HasIndex("Email");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("ThFnsc.NFe.Data.Entities.IssuedNFe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("AliquotPercentage")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp");

                    b.Property<int?>("DocumentToId")
                        .HasColumnType("int");

                    b.Property<string>("ErrorMessage")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("IssuedAt")
                        .HasColumnType("timestamp");

                    b.Property<int?>("ProviderId")
                        .HasColumnType("int");

                    b.Property<string>("ReturnedContent")
                        .HasColumnType("text");

                    b.Property<string>("SentContent")
                        .HasColumnType("text");

                    b.Property<int>("Series")
                        .HasColumnType("int");

                    b.Property<string>("ServiceDescription")
                        .HasColumnType("text");

                    b.Property<int>("ServiceId")
                        .HasColumnType("int");

                    b.Property<bool?>("Success")
                        .HasColumnType("tinyint(1)");

                    b.Property<float>("Value")
                        .HasColumnType("float");

                    b.Property<string>("VerificationCode")
                        .HasColumnType("varchar(767)");

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

            modelBuilder.Entity("ThFnsc.NFe.Data.Entities.MailTemplate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Body")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(767)");

                    b.Property<string>("Subject")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("DeletedAt");

                    b.HasIndex("Name");

                    b.ToTable("MailTemplates");
                });

            modelBuilder.Entity("ThFnsc.NFe.Data.Entities.Provider", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp");

                    b.Property<string>("Data")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp");

                    b.Property<int?>("IssuerId")
                        .HasColumnType("int");

                    b.Property<int?>("SMTPId")
                        .HasColumnType("int");

                    b.Property<string>("TownHallType")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("DeletedAt");

                    b.HasIndex("IssuerId");

                    b.HasIndex("SMTPId");

                    b.ToTable("Providers");
                });

            modelBuilder.Entity("ThFnsc.NFe.Data.Entities.SMTP", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Account")
                        .HasColumnType("text");

                    b.Property<string>("AccountName")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp");

                    b.Property<string>("Host")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<int>("Port")
                        .HasColumnType("int");

                    b.Property<bool>("UseEncryption")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatedAt");

                    b.HasIndex("DeletedAt");

                    b.ToTable("SMTPs");
                });

            modelBuilder.Entity("ThFnsc.NFe.Data.Entities.ScheduledGeneration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("AliquotPercentage")
                        .HasColumnType("float");

                    b.Property<DateTimeOffset>("CreatedAt")
                        .HasColumnType("timestamp");

                    b.Property<string>("CronPattern")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp");

                    b.Property<bool>("Enabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("MailList")
                        .HasColumnType("text");

                    b.Property<int?>("MailTemplateId")
                        .HasColumnType("int");

                    b.Property<int?>("ProviderId")
                        .HasColumnType("int");

                    b.Property<string>("ServiceDescription")
                        .HasColumnType("text");

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

                    b.HasIndex("MailTemplateId");

                    b.HasIndex("ProviderId");

                    b.HasIndex("ToDocumentId");

                    b.ToTable("ScheduledGenerations");
                });

            modelBuilder.Entity("ThFnsc.NFe.Data.Entities.Document", b =>
                {
                    b.HasOne("ThFnsc.NFe.Data.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ThFnsc.NFe.Data.Entities.IssuedNFe", b =>
                {
                    b.HasOne("ThFnsc.NFe.Data.Entities.Document", "DocumentTo")
                        .WithMany()
                        .HasForeignKey("DocumentToId");

                    b.HasOne("ThFnsc.NFe.Data.Entities.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId");

                    b.Navigation("DocumentTo");

                    b.Navigation("Provider");
                });

            modelBuilder.Entity("ThFnsc.NFe.Data.Entities.Provider", b =>
                {
                    b.HasOne("ThFnsc.NFe.Data.Entities.Document", "Issuer")
                        .WithMany()
                        .HasForeignKey("IssuerId");

                    b.HasOne("ThFnsc.NFe.Data.Entities.SMTP", "SMTP")
                        .WithMany()
                        .HasForeignKey("SMTPId");

                    b.Navigation("Issuer");

                    b.Navigation("SMTP");
                });

            modelBuilder.Entity("ThFnsc.NFe.Data.Entities.ScheduledGeneration", b =>
                {
                    b.HasOne("ThFnsc.NFe.Data.Entities.MailTemplate", "MailTemplate")
                        .WithMany()
                        .HasForeignKey("MailTemplateId");

                    b.HasOne("ThFnsc.NFe.Data.Entities.Provider", "Provider")
                        .WithMany()
                        .HasForeignKey("ProviderId");

                    b.HasOne("ThFnsc.NFe.Data.Entities.Document", "ToDocument")
                        .WithMany()
                        .HasForeignKey("ToDocumentId");

                    b.Navigation("MailTemplate");

                    b.Navigation("Provider");

                    b.Navigation("ToDocument");
                });
#pragma warning restore 612, 618
        }
    }
}
