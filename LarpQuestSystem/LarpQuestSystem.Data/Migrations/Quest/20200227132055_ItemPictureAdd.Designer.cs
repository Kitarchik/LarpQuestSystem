﻿// <auto-generated />
using System;
using LarpQuestSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LarpQuestSystem.Data.Migrations.Quest
{
    [DbContext(typeof(LarpSystemContext))]
    [Migration("20200227132055_ItemPictureAdd")]
    partial class ItemPictureAdd
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LarpQuestSystem.Data.Model.MaterialManagement.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AmountPerPayedRequestInLocation")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("UnitOfMeasure")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.MaterialManagement.MaterialsRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Customer")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<bool>("IsPayed")
                        .HasColumnType("bit");

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.ToTable("MaterialsRequests");
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.MaterialManagement.SingleMaterialRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MaterialId")
                        .HasColumnType("int");

                    b.Property<int>("MaterialsRequestId")
                        .HasColumnType("int");

                    b.Property<int>("QuantityOrdered")
                        .HasColumnType("int");

                    b.Property<int>("QuantityServed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaterialId");

                    b.HasIndex("MaterialsRequestId");

                    b.ToTable("SingleMaterialRequests");
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.Chain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Chains");
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AmountReady")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<int>("ItemType")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<string>("PictureLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<int>("RequestsPayed")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.Npc", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Npcs");
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<int>("LocationId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<int>("NumberOfPayedAccounts")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("LocationId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.Quest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AmountToPrint")
                        .HasColumnType("int");

                    b.Property<string>("ArtisticTextLink")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<string>("CompletionComment")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Complexity")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<bool>("IsArtisticTextReady")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPrinted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTechnicalDescriptionReady")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(250)")
                        .HasMaxLength(250);

                    b.Property<int>("QuestEndingId")
                        .HasColumnType("int");

                    b.Property<int>("QuestGiverId")
                        .HasColumnType("int");

                    b.Property<string>("TechnicalDescriptionLink")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.HasIndex("QuestEndingId");

                    b.HasIndex("QuestGiverId");

                    b.ToTable("Quests");
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.QuestChain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChainId")
                        .HasColumnType("int");

                    b.Property<int>("QuestId")
                        .HasColumnType("int");

                    b.Property<int>("StepNumber")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChainId");

                    b.HasIndex("QuestId");

                    b.ToTable("QuestChains");
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.QuestItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AmountNeeded")
                        .HasColumnType("int");

                    b.Property<bool>("IsReady")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTechnicalDocumentReady")
                        .HasColumnType("bit");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("QuestId")
                        .HasColumnType("int");

                    b.Property<int>("StartingNpcId")
                        .HasColumnType("int");

                    b.Property<string>("TechnicalDocumentForNpc")
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("QuestId");

                    b.HasIndex("StartingNpcId");

                    b.ToTable("QuestItems");
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.QuestPlayer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("QuestId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.HasIndex("QuestId");

                    b.ToTable("QuestPlayers");
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.MaterialManagement.MaterialsRequest", b =>
                {
                    b.HasOne("LarpQuestSystem.Data.Model.QuestSystem.Location", "Location")
                        .WithMany("MaterialsRequests")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.MaterialManagement.SingleMaterialRequest", b =>
                {
                    b.HasOne("LarpQuestSystem.Data.Model.MaterialManagement.Material", "Material")
                        .WithMany("SingleMaterialRequests")
                        .HasForeignKey("MaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LarpQuestSystem.Data.Model.MaterialManagement.MaterialsRequest", "MaterialsRequest")
                        .WithMany("SingleMaterialRequests")
                        .HasForeignKey("MaterialsRequestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.Npc", b =>
                {
                    b.HasOne("LarpQuestSystem.Data.Model.QuestSystem.Location", "Location")
                        .WithMany("Npcs")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.Player", b =>
                {
                    b.HasOne("LarpQuestSystem.Data.Model.QuestSystem.Location", "Location")
                        .WithMany("Players")
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.Quest", b =>
                {
                    b.HasOne("LarpQuestSystem.Data.Model.QuestSystem.Npc", "QuestEnding")
                        .WithMany("EndingQuests")
                        .HasForeignKey("QuestEndingId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("LarpQuestSystem.Data.Model.QuestSystem.Npc", "QuestGiver")
                        .WithMany("StartingQuests")
                        .HasForeignKey("QuestGiverId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.QuestChain", b =>
                {
                    b.HasOne("LarpQuestSystem.Data.Model.QuestSystem.Chain", "Chain")
                        .WithMany("QuestChains")
                        .HasForeignKey("ChainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LarpQuestSystem.Data.Model.QuestSystem.Quest", "Quest")
                        .WithMany("QuestChains")
                        .HasForeignKey("QuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.QuestItem", b =>
                {
                    b.HasOne("LarpQuestSystem.Data.Model.QuestSystem.Item", "Item")
                        .WithMany("QuestItems")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LarpQuestSystem.Data.Model.QuestSystem.Quest", "Quest")
                        .WithMany("QuestItems")
                        .HasForeignKey("QuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LarpQuestSystem.Data.Model.QuestSystem.Npc", "StartingNpc")
                        .WithMany("ItemsOnStart")
                        .HasForeignKey("StartingNpcId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LarpQuestSystem.Data.Model.QuestSystem.QuestPlayer", b =>
                {
                    b.HasOne("LarpQuestSystem.Data.Model.QuestSystem.Player", "Player")
                        .WithMany("QuestPlayers")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LarpQuestSystem.Data.Model.QuestSystem.Quest", "Quest")
                        .WithMany("QuestPlayers")
                        .HasForeignKey("QuestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}