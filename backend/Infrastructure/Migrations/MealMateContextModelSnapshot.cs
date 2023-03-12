﻿// <auto-generated />
using Infrastructure.database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MealMateContext))]
    partial class MealMateContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("domain.Category", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("character varying(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("domain.Item", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("character varying(36)");

                    b.Property<string>("CategoryId")
                        .HasColumnType("character varying(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("domain.ShoppingList", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("character varying(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("ShoppingLists");
                });

            modelBuilder.Entity("domain.shopping_list.Entry", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("character varying(36)");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("character varying(36)");

                    b.Property<string>("Qualifier")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ShoppingListId")
                        .IsRequired()
                        .HasColumnType("character varying(36)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("domain.Item", b =>
                {
                    b.HasOne("domain.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("domain.shopping_list.Entry", b =>
                {
                    b.HasOne("domain.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("domain.ShoppingList", "ShoppingList")
                        .WithMany("Entries")
                        .HasForeignKey("ShoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("domain.Category", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("domain.ShoppingList", b =>
                {
                    b.Navigation("Entries");
                });
#pragma warning restore 612, 618
        }
    }
}
