﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ODataASPNetCoreDemo.Data.Entities;

#nullable disable

namespace ODataASPNetCoreDemo.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240523093423_InitialDB")]
    partial class InitialDB
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ODataASPNetCoreDemo.Data.Entities.Gadgets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Gadgets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Brand = "Nhat",
                            Cost = 10m,
                            ProductName = "Product1",
                            Type = "Fruit"
                        },
                        new
                        {
                            Id = 2,
                            Brand = "Trung",
                            Cost = 20m,
                            ProductName = "Product2",
                            Type = "Sea food"
                        },
                        new
                        {
                            Id = 3,
                            Brand = "Han",
                            Cost = 30m,
                            ProductName = "Product3",
                            Type = "Food"
                        },
                        new
                        {
                            Id = 4,
                            Brand = "Nhat",
                            Cost = 15m,
                            ProductName = "Product4",
                            Type = "Sea food"
                        },
                        new
                        {
                            Id = 5,
                            Brand = "Nhat",
                            Cost = 17m,
                            ProductName = "Product5",
                            Type = "Fruit"
                        },
                        new
                        {
                            Id = 6,
                            Brand = "Trung",
                            Cost = 40m,
                            ProductName = "Product6",
                            Type = "Food"
                        },
                        new
                        {
                            Id = 7,
                            Brand = "Han",
                            Cost = 12m,
                            ProductName = "Product7",
                            Type = "Food"
                        },
                        new
                        {
                            Id = 8,
                            Brand = "Nhat",
                            Cost = 18m,
                            ProductName = "Product8",
                            Type = "Sea food"
                        },
                        new
                        {
                            Id = 9,
                            Brand = "Trung",
                            Cost = 50m,
                            ProductName = "Product9",
                            Type = "Fruit"
                        },
                        new
                        {
                            Id = 10,
                            Brand = "Viet Nam",
                            Cost = 90m,
                            ProductName = "Product10",
                            Type = "Sea food"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
