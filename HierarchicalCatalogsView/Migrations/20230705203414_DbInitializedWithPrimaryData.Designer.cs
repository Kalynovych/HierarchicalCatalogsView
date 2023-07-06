﻿// <auto-generated />
using System;
using HierarchicalCatalogsView.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HierarchicalCatalogsView.Migrations
{
    [DbContext(typeof(CatalogContext))]
    [Migration("20230705203414_DbInitializedWithPrimaryData")]
    partial class DbInitializedWithPrimaryData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0-preview.5.23280.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HierarchicalCatalogsView.Models.Catalog", b =>
                {
                    b.Property<int>("CatalogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CatalogId"));

                    b.Property<string>("CatalogName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentId")
                        .HasColumnType("int");

                    b.HasKey("CatalogId");

                    b.HasIndex("ParentId");

                    b.ToTable("Catalogs");

                    b.HasData(
                        new
                        {
                            CatalogId = 1,
                            CatalogName = "Creating Digital Images"
                        },
                        new
                        {
                            CatalogId = 2,
                            CatalogName = "Resources",
                            ParentId = 1
                        },
                        new
                        {
                            CatalogId = 3,
                            CatalogName = "Evidence",
                            ParentId = 1
                        },
                        new
                        {
                            CatalogId = 4,
                            CatalogName = "Graphic Products",
                            ParentId = 1
                        },
                        new
                        {
                            CatalogId = 5,
                            CatalogName = "Primary Sources",
                            ParentId = 2
                        },
                        new
                        {
                            CatalogId = 6,
                            CatalogName = "Secondary Sources",
                            ParentId = 2
                        },
                        new
                        {
                            CatalogId = 7,
                            CatalogName = "Process",
                            ParentId = 4
                        },
                        new
                        {
                            CatalogId = 8,
                            CatalogName = "Final Product",
                            ParentId = 4
                        });
                });

            modelBuilder.Entity("HierarchicalCatalogsView.Models.Catalog", b =>
                {
                    b.HasOne("HierarchicalCatalogsView.Models.Catalog", "ParentCatalog")
                        .WithMany("ChildCatalogs")
                        .HasForeignKey("ParentId");

                    b.Navigation("ParentCatalog");
                });

            modelBuilder.Entity("HierarchicalCatalogsView.Models.Catalog", b =>
                {
                    b.Navigation("ChildCatalogs");
                });
#pragma warning restore 612, 618
        }
    }
}
