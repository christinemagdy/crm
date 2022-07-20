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
    [Migration("20211116095901_AreaBehavior")]
    partial class AreaBehavior
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

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .HasColumnType("varchar(100)");

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

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Case")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Completeaddress")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Facebooklink")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Field")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Instagramlink")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Lastfeedback")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ModifiedBy")
                        .HasMaxLength(450)
                        .HasColumnType("varchar(450)");

                    b.Property<DateTime>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name_of_business")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Nameofcontact")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Number")
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Position")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Religion")
                        .HasColumnType("int");

                    b.Property<string>("ServiceProvded")
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Website")
                        .HasColumnType("varchar(100)");

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

            modelBuilder.Entity("Bebrand.Domain.Models.TeamLeader", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
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

            modelBuilder.Entity("Bebrand.Domain.Models.Client", b =>
                {
                    b.HasOne("Bebrand.Domain.Models.Area", "Area")
                        .WithMany("Clients")
                        .HasForeignKey("AriaId")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Area");
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

            modelBuilder.Entity("Bebrand.Domain.Models.Area", b =>
                {
                    b.Navigation("Clients");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.Customer", b =>
                {
                    b.Navigation("TeamLeaders");
                });

            modelBuilder.Entity("Bebrand.Domain.Models.TeamLeader", b =>
                {
                    b.Navigation("TeamMembers");
                });
#pragma warning restore 612, 618
        }
    }
}
