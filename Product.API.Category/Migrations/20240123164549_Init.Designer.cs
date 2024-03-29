﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Product.API.Category.Infrastructure.Configuration;

#nullable disable

namespace Product.API.Category.Migrations
{
    [DbContext(typeof(CategoryDbContext))]
    [Migration("20240123164549_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Product.API.Category.Infrastructure.Entities.CategoryEntity", b =>
                {
                    b.Property<int>("CatId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CatId"));

                    b.Property<int?>("CategoryEntityCatId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentCatId")
                        .HasColumnType("int");

                    b.HasKey("CatId");

                    b.HasIndex("CategoryEntityCatId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Product.API.Category.Infrastructure.Entities.CategoryEntity", b =>
                {
                    b.HasOne("Product.API.Category.Infrastructure.Entities.CategoryEntity", null)
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryEntityCatId");
                });

            modelBuilder.Entity("Product.API.Category.Infrastructure.Entities.CategoryEntity", b =>
                {
                    b.Navigation("Subcategories");
                });
#pragma warning restore 612, 618
        }
    }
}
