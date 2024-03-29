﻿// <auto-generated />
using Infrastructure.database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MealMateContext))]
    [Migration("20231029201222_Remove Qualifier")]
    partial class RemoveQualifier
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TemplateTemplateItem", b =>
                {
                    b.Property<string>("TemplateId")
                        .HasColumnType("character varying(36)");

                    b.Property<string>("TemplateItemsId")
                        .HasColumnType("character varying(36)");

                    b.HasKey("TemplateId", "TemplateItemsId");

                    b.HasIndex("TemplateItemsId");

                    b.ToTable("TemplateTemplateItem");
                });

            modelBuilder.Entity("domain.Item", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("character varying(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

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

            modelBuilder.Entity("domain.Template", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("character varying(36)");

                    b.Property<string>("Instructions")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Templates");
                });

            modelBuilder.Entity("domain.TemplateItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("character varying(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TemplateItem");
                });

            modelBuilder.Entity("domain.shopping_list.Entry", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("character varying(36)");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("character varying(36)");

                    b.Property<string>("ShoppingListId")
                        .IsRequired()
                        .HasColumnType("character varying(36)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("Entries");
                });

            modelBuilder.Entity("TemplateTemplateItem", b =>
                {
                    b.HasOne("domain.Template", null)
                        .WithMany()
                        .HasForeignKey("TemplateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("domain.TemplateItem", null)
                        .WithMany()
                        .HasForeignKey("TemplateItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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

            modelBuilder.Entity("domain.ShoppingList", b =>
                {
                    b.Navigation("Entries");
                });
#pragma warning restore 612, 618
        }
    }
}
