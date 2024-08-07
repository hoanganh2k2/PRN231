﻿// <auto-generated />
using BusinessObject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObject.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20240709054235_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObject.Models.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"));

                    b.Property<string>("AddressName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");

                    b.HasData(
                        new
                        {
                            AddressId = 1,
                            AddressName = "Kỳ Anh",
                            City = "Hà Tĩnh"
                        },
                        new
                        {
                            AddressId = 2,
                            AddressName = "Trung Khánh",
                            City = "Đà Nẵng"
                        },
                        new
                        {
                            AddressId = 3,
                            AddressName = "Chợ",
                            City = "Huế"
                        });
                });

            modelBuilder.Entity("BusinessObject.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Description = "Ngon",
                            Price = 50000,
                            ProductName = "Bánh Cu Đơ",
                            UserId = 1
                        },
                        new
                        {
                            ProductId = 2,
                            Description = "Ngon",
                            Price = 30000,
                            ProductName = "Bánh Cốm",
                            UserId = 2
                        },
                        new
                        {
                            ProductId = 3,
                            Description = "Ngon",
                            Price = 40000,
                            ProductName = "Bánh Dập",
                            UserId = 3
                        },
                        new
                        {
                            ProductId = 4,
                            Description = "Ngon",
                            Price = 20000,
                            ProductName = "Bánh Trôi Nước",
                            UserId = 4
                        },
                        new
                        {
                            ProductId = 5,
                            Description = "Ngon",
                            Price = 10000,
                            ProductName = "Bánh Tày",
                            UserId = 1
                        });
                });

            modelBuilder.Entity("BusinessObject.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("AddressId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            AddressId = 1,
                            Name = "Hoàng Anh"
                        },
                        new
                        {
                            UserId = 2,
                            AddressId = 2,
                            Name = "Hoàng Hà"
                        },
                        new
                        {
                            UserId = 3,
                            AddressId = 3,
                            Name = "Hoàng Ánh"
                        },
                        new
                        {
                            UserId = 4,
                            AddressId = 1,
                            Name = "Hoàng Minh"
                        });
                });

            modelBuilder.Entity("BusinessObject.Models.Product", b =>
                {
                    b.HasOne("BusinessObject.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObject.Models.User", b =>
                {
                    b.HasOne("BusinessObject.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });
#pragma warning restore 612, 618
        }
    }
}
