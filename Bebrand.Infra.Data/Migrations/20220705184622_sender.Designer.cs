﻿// <auto-generated />
using System;
using Bebrand.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bebrand.Infra.Data.Migrations
{
    [DbContext(typeof(BebrandContext))]
    [Migration("20220705184622_sender")]
    partial class sender
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bebrand.Domain.Models.Area", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Area");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.Client", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AccountManager")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("AriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasDefaultValue(new Guid("00000000-0000-0000-0000-000000000000"));

                    b.Property<string>("Birthday")
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Call")
                        .HasColumnType("int");

                    b.Property<string>("Case")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Completeaddress")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Facebooklink")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Field")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Instagramlink")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Lastfeedback")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name_of_business")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Nameofcontact")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<string>("Phoneone")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Phonetwo")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(MAX)");

                    b.Property<int>("Religion")
                        .HasColumnType("int");

                    b.Property<string>("ServiceProvded")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("Typeclient")
                        .HasColumnType("int");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(MAX)");

                    b.HasKey("Id");

                    b.HasIndex("AriaId");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SalesDirector");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.Hr", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Hr");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.Jobs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("JobsTitle")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Jobs");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.ServiceProvider", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ClientID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("ServiceId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientID");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServiceProviders");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.TeamLeader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("SalesDirectorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SalesDirectorId");

                    b.ToTable("TeamLeaders");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.TeamMember", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("LName")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid>("TeamLeaderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TeamLeaderId");

                    b.ToTable("TeamMembers");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.VacanciesMail", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Attachement")
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Sender")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("TextBody")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UniqueIds")
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("JobId");

                    b.ToTable("VacanciesMails");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.Client", b =>
                {
                    b.HasOne("Bebrand.Domain.Models.Area", "Area")
                        .WithMany("Clients")
                        .HasForeignKey("AriaId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Area");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.ServiceProvider", b =>
                {
                    b.HasOne("Bebrand.Domain.Models.Client", "Client")
                        .WithMany("ServiceProviders")
                        .HasForeignKey("ClientID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bebrand.Domain.Models.Service", "Service")
                        .WithMany("ServiceProviders")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.TeamLeader", b =>
                {
                    b.HasOne("Bebrand.Domain.Models.Customer", "SalesDirector")
                        .WithMany("TeamLeaders")
                        .HasForeignKey("SalesDirectorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SalesDirector");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.TeamMember", b =>
                {
                    b.HasOne("Bebrand.Domain.Models.TeamLeader", "TeamLeader")
                        .WithMany("TeamMembers")
                        .HasForeignKey("TeamLeaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TeamLeader");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.VacanciesMail", b =>
                {
                    b.HasOne("Bebrand.Domain.Models.Jobs", "Jobs")
                        .WithMany("vacanciesMails")
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Jobs");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.Area", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.Client", b =>
                {
                    b.Navigation("ServiceProviders");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.Customer", b =>
                {
                    b.Navigation("TeamLeaders");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.Jobs", b =>
                {
                    b.Navigation("vacanciesMails");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.Service", b =>
                {
                    b.Navigation("ServiceProviders");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.TeamLeader", b =>
                {
                    b.Navigation("TeamMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
