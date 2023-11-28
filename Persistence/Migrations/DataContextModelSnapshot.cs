﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Persistence;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Animal", b =>
                {
                    b.Property<Guid>("AnimalId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("animal id");

                    b.Property<int>("Age")
                        .HasColumnType("int")
                        .HasComment("animal age");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2")
                        .HasComment("animal birth date");

                    b.Property<int>("BreedId")
                        .HasColumnType("int")
                        .HasComment("animal breed id");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()")
                        .HasComment("animal created on");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("animal description");

                    b.Property<int>("Gender")
                        .HasColumnType("int")
                        .HasComment("animal gender");

                    b.Property<int>("HealthStatus")
                        .HasColumnType("int")
                        .HasComment("animal health status");

                    b.Property<bool>("IsEducated")
                        .HasColumnType("bit")
                        .HasComment("stores if the animal is educated");

                    b.Property<bool>("IsHavingValidDocuments")
                        .HasColumnType("bit")
                        .HasComment("it stores if the animal has valid documents");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime2")
                        .HasComment("animal last modified");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("animal name");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("animal owner id");

                    b.Property<byte[]>("Photo")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasComment("animal photo");

                    b.Property<string>("SocialMedia")
                        .HasColumnType("nvarchar(max)")
                        .HasComment("animal social media");

                    b.Property<double?>("Weight")
                        .HasColumnType("float")
                        .HasComment("animal weight");

                    b.HasKey("AnimalId");

                    b.HasIndex("BreedId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Animals", t =>
                        {
                            t.HasComment("animal table");
                        });
                });

            modelBuilder.Entity("Domain.AnimalCategory", b =>
                {
                    b.Property<int>("AnimalCategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("animal category id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnimalCategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("animal category name");

                    b.HasKey("AnimalCategoryId");

                    b.ToTable("AnimalCategories", t =>
                        {
                            t.HasComment("animal category table");
                        });
                });

            modelBuilder.Entity("Domain.AnimalMatch", b =>
                {
                    b.Property<Guid>("AnimalId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("animal id");

                    b.Property<Guid>("MatchId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("match id");

                    b.HasKey("AnimalId", "MatchId");

                    b.HasIndex("MatchId");

                    b.ToTable("AnimalMatches", null, t =>
                        {
                            t.HasComment("animal match table");
                        });
                });

            modelBuilder.Entity("Domain.Breed", b =>
                {
                    b.Property<int>("BreedId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("breed id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BreedId"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int")
                        .HasComment("animal category id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("breed name");

                    b.HasKey("BreedId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Breeds", t =>
                        {
                            t.HasComment("breed table");
                        });
                });

            modelBuilder.Entity("Domain.Conversation", b =>
                {
                    b.Property<Guid>("ConversationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("conversation id");

                    b.Property<DateTime>("StartedOn")
                        .HasColumnType("datetime2")
                        .HasComment("timestamp when the conversation started");

                    b.HasKey("ConversationId");

                    b.ToTable("Conversations", t =>
                        {
                            t.HasComment("conversation table");
                        });
                });

            modelBuilder.Entity("Domain.Match", b =>
                {
                    b.Property<Guid>("MatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier")
                        .HasComment("match id");

                    b.Property<DateTime>("MatchOn")
                        .HasColumnType("datetime2")
                        .HasComment("timestamp when the match is done");

                    b.HasKey("MatchId");

                    b.ToTable("Matches", t =>
                        {
                            t.HasComment("match table");
                        });
                });

            modelBuilder.Entity("Domain.Message", b =>
                {
                    b.Property<Guid>("AnimalId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("message animal id");

                    b.Property<Guid>("ConversationId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("message conversation id");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(350)
                        .HasColumnType("nvarchar(350)")
                        .HasComment("message content");

                    b.Property<DateTime>("SentOn")
                        .HasColumnType("datetime2")
                        .HasComment("timestamp when the message is sent");

                    b.HasKey("AnimalId", "ConversationId");

                    b.HasIndex("ConversationId");

                    b.ToTable("Messages", null, t =>
                        {
                            t.HasComment("message table");
                        });
                });

            modelBuilder.Entity("Domain.Passion", b =>
                {
                    b.Property<int>("PassionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("passion id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PassionId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasComment("passion name");

                    b.HasKey("PassionId");

                    b.ToTable("Passions", t =>
                        {
                            t.HasComment("passion table");
                        });
                });

            modelBuilder.Entity("Domain.Photo", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)")
                        .HasComment("photo id");

                    b.Property<Guid?>("AnimalId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("photo animal id");

                    b.Property<bool>("IsMain")
                        .HasColumnType("bit")
                        .HasComment("stores if the photo is main");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("photo url");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.ToTable("Photos", t =>
                        {
                            t.HasComment("photo table");
                        });
                });

            modelBuilder.Entity("Domain.Swipe", b =>
                {
                    b.Property<Guid>("SwiperAnimalId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("swiper animal id");

                    b.Property<Guid>("SwipeeAnimalId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("swipee animal id");

                    b.Property<DateTime>("SwipedOn")
                        .HasColumnType("datetime2")
                        .HasComment("timestamp when the swipe is made");

                    b.Property<bool>("SwipedRight")
                        .HasColumnType("bit")
                        .HasComment("it stores of the swipe is right");

                    b.HasKey("SwiperAnimalId", "SwipeeAnimalId");

                    b.HasIndex("SwipeeAnimalId");

                    b.ToTable("Swipes", null, t =>
                        {
                            t.HasComment("swipe table");
                        });
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("user address");

                    b.Property<int?>("Age")
                        .HasColumnType("int")
                        .HasComment("user age");

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("user city");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)")
                        .HasComment("user description");

                    b.Property<string>("Education")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("user education");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int?>("Gender")
                        .HasColumnType("int")
                        .HasComment("user gender");

                    b.Property<string>("JobTitle")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("user job title");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasComment("user name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("PhotoId")
                        .HasColumnType("nvarchar(450)")
                        .HasComment("user photo id");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("PhotoId");

                    b.ToTable("AspNetUsers", null, t =>
                        {
                            t.HasComment("user table");
                        });
                });

            modelBuilder.Entity("Domain.UserPassion", b =>
                {
                    b.Property<int>("PassionId")
                        .HasColumnType("int")
                        .HasComment("passion id");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier")
                        .HasComment("user id");

                    b.HasKey("PassionId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UsersPassions", null, t =>
                        {
                            t.HasComment("user passion table");
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Domain.Animal", b =>
                {
                    b.HasOne("Domain.Breed", "Breed")
                        .WithMany("Animals")
                        .HasForeignKey("BreedId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", "Owner")
                        .WithMany("Animals")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Breed");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Domain.AnimalMatch", b =>
                {
                    b.HasOne("Domain.Animal", "Animal")
                        .WithMany("AnimalMatches")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Match", "Match")
                        .WithMany("AnimalMatches")
                        .HasForeignKey("MatchId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Match");
                });

            modelBuilder.Entity("Domain.Breed", b =>
                {
                    b.HasOne("Domain.AnimalCategory", "Category")
                        .WithMany("Breeds")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Domain.Message", b =>
                {
                    b.HasOne("Domain.Animal", "Animal")
                        .WithMany("Messages")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Animal");

                    b.Navigation("Conversation");
                });

            modelBuilder.Entity("Domain.Photo", b =>
                {
                    b.HasOne("Domain.Animal", "Animal")
                        .WithMany("Photos")
                        .HasForeignKey("AnimalId");

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("Domain.Swipe", b =>
                {
                    b.HasOne("Domain.Animal", "SwipeeAnimal")
                        .WithMany("SwipesFrom")
                        .HasForeignKey("SwipeeAnimalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Domain.Animal", "SwiperAnimal")
                        .WithMany("SwipesTo")
                        .HasForeignKey("SwiperAnimalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("SwipeeAnimal");

                    b.Navigation("SwiperAnimal");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.HasOne("Domain.Photo", "Photo")
                        .WithMany()
                        .HasForeignKey("PhotoId");

                    b.Navigation("Photo");
                });

            modelBuilder.Entity("Domain.UserPassion", b =>
                {
                    b.HasOne("Domain.Passion", "Passion")
                        .WithMany("UsersPassions")
                        .HasForeignKey("PassionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", "User")
                        .WithMany("UsersPassions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Passion");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole<System.Guid>", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<System.Guid>", b =>
                {
                    b.HasOne("Domain.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Animal", b =>
                {
                    b.Navigation("AnimalMatches");

                    b.Navigation("Messages");

                    b.Navigation("Photos");

                    b.Navigation("SwipesFrom");

                    b.Navigation("SwipesTo");
                });

            modelBuilder.Entity("Domain.AnimalCategory", b =>
                {
                    b.Navigation("Breeds");
                });

            modelBuilder.Entity("Domain.Breed", b =>
                {
                    b.Navigation("Animals");
                });

            modelBuilder.Entity("Domain.Conversation", b =>
                {
                    b.Navigation("Messages");
                });

            modelBuilder.Entity("Domain.Match", b =>
                {
                    b.Navigation("AnimalMatches");
                });

            modelBuilder.Entity("Domain.Passion", b =>
                {
                    b.Navigation("UsersPassions");
                });

            modelBuilder.Entity("Domain.User", b =>
                {
                    b.Navigation("Animals");

                    b.Navigation("UsersPassions");
                });
#pragma warning restore 612, 618
        }
    }
}
