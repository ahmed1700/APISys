﻿// <auto-generated />
using System;
using Entities.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CompanyEmployees.Migrations
{
    [DbContext(typeof(RepositryContext))]
    [Migration("20200218182332_databasecreatin")]
    partial class databasecreatin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Entities.Models.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CompanyId");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Country");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("Entities.Models.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EmployeeId");

                    b.Property<string>("Age")
                        .IsRequired();

                    b.Property<Guid>("CompanyId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.Property<string>("Position")
                        .IsRequired()
                        .HasMaxLength(70);

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Entities.Models.Employee", b =>
                {
                    b.HasOne("Entities.Models.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}