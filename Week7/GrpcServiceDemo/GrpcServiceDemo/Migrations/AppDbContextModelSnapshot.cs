﻿// <auto-generated />
using GrpcServiceDemo.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GrpcServiceDemo.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GrpcServiceDemo.DataAccess.Employee", b =>
                {
                    b.Property<int>("employeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("employeeId"));

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("employeeId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            employeeId = 1,
                            firstName = "Hoang",
                            lastName = "Anh"
                        },
                        new
                        {
                            employeeId = 2,
                            firstName = "Nguyen",
                            lastName = "Hoang"
                        },
                        new
                        {
                            employeeId = 3,
                            firstName = "My",
                            lastName = "Dung"
                        },
                        new
                        {
                            employeeId = 4,
                            firstName = "Duy",
                            lastName = "Manh"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
