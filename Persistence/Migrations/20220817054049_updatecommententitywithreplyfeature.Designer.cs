﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence.Contexts;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220817054049_updatecommententitywithreplyfeature")]
    partial class updatecommententitywithreplyfeature
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entites.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ParentCategoryId");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Domain.Entites.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfRegisteryComment")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NewsId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfDisLikes")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfLikes")
                        .HasColumnType("int");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("int");

                    b.Property<int>("StatusComment")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.HasIndex("ParentCommentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Domain.Entites.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("NewsId")
                        .HasColumnType("int");

                    b.Property<string>("Src")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("Domain.Entites.News", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("HotNews")
                        .HasColumnType("int");

                    b.Property<string>("ImageTitle")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MetaDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NewsStatus")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfVisited")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Slug")
                        .IsUnique();

                    b.ToTable("News");
                });

            modelBuilder.Entity("Domain.Entites.NewsBody", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("BodyParagraph")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NewsId")
                        .HasColumnType("int");

                    b.Property<string>("TitleParagraph")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NewsId");

                    b.ToTable("NewsBodies");
                });

            modelBuilder.Entity("Domain.Entites.Tags", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("NewsTags", b =>
                {
                    b.Property<int>("NewsId")
                        .HasColumnType("int");

                    b.Property<int>("TagsId")
                        .HasColumnType("int");

                    b.HasKey("NewsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("NewsTags");
                });

            modelBuilder.Entity("Domain.Entites.Category", b =>
                {
                    b.HasOne("Domain.Entites.Category", "ParentCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("ParentCategoryId");

                    b.Navigation("ParentCategory");
                });

            modelBuilder.Entity("Domain.Entites.Comment", b =>
                {
                    b.HasOne("Domain.Entites.News", "News")
                        .WithMany("Comments")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entites.Comment", "ParentComment")
                        .WithMany("Replies")
                        .HasForeignKey("ParentCommentId");

                    b.Navigation("News");

                    b.Navigation("ParentComment");
                });

            modelBuilder.Entity("Domain.Entites.Image", b =>
                {
                    b.HasOne("Domain.Entites.News", "News")
                        .WithMany("Images")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("News");
                });

            modelBuilder.Entity("Domain.Entites.News", b =>
                {
                    b.HasOne("Domain.Entites.Category", "Category")
                        .WithMany("News")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.Entites.NewsBody", b =>
                {
                    b.HasOne("Domain.Entites.News", "News")
                        .WithMany("NewsBodies")
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("News");
                });

            modelBuilder.Entity("NewsTags", b =>
                {
                    b.HasOne("Domain.Entites.News", null)
                        .WithMany()
                        .HasForeignKey("NewsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entites.Tags", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entites.Category", b =>
                {
                    b.Navigation("News");

                    b.Navigation("SubCategories");
                });

            modelBuilder.Entity("Domain.Entites.Comment", b =>
                {
                    b.Navigation("Replies");
                });

            modelBuilder.Entity("Domain.Entites.News", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Images");

                    b.Navigation("NewsBodies");
                });
#pragma warning restore 612, 618
        }
    }
}
