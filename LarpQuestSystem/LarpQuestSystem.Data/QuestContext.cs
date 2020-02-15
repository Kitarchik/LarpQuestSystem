using LarpQuestSystem.Data.Model;
using LarpQuestSystem.Data.Model.QuestSystem;
using Microsoft.EntityFrameworkCore;

namespace LarpQuestSystem.Data
{
    public sealed class QuestContext : DbContext
    {
        public DbSet<Quest> Quests { get; set; }
        public DbSet<QuestChain> QuestChains { get; set; }
        public DbSet<QuestItem> QuestItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Npc> Npcs { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Chain> Chains { get; set; }
        public DbSet<QuestPlayer> QuestPlayers { get; set; }
        public DbSet<Player> Players { get; set; }

        public QuestContext(DbContextOptions<QuestContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .HasMany(i => i.QuestItems)
                .WithOne(q => q.Item)
                .HasForeignKey(q => q.ItemId);
            modelBuilder.Entity<QuestItem>()
                .HasOne(x => x.Quest)
                .WithMany(x => x.QuestItems)
                .HasForeignKey(x => x.QuestId);
            modelBuilder.Entity<QuestItem>()
                .HasOne(x => x.Item)
                .WithMany(x => x.QuestItems)
                .HasForeignKey(x => x.ItemId);
            modelBuilder.Entity<QuestItem>()
                .HasOne(x => x.StartingNpc)
                .WithMany(x => x.ItemsOnStart)
                .HasForeignKey(x => x.StartingNpcId);
            modelBuilder.Entity<Quest>()
                .HasOne(x => x.QuestGiver)
                .WithMany(x => x.StartingQuests)
                .HasForeignKey(x => x.QuestGiverId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Quest>()
                .HasOne(x => x.QuestEnding)
                .WithMany(x => x.EndingQuests)
                .HasForeignKey(x => x.QuestEndingId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Quest>()
                .HasMany(x => x.QuestPlayers)
                .WithOne(x => x.Quest)
                .HasForeignKey(x => x.QuestId);
            modelBuilder.Entity<Chain>()
                .HasMany(x => x.QuestChains)
                .WithOne(x => x.Chain)
                .HasForeignKey(x => x.ChainId);
            modelBuilder.Entity<QuestChain>()
                .HasOne(x => x.Chain)
                .WithMany(x => x.QuestChains)
                .HasForeignKey(x => x.ChainId);
            modelBuilder.Entity<QuestChain>()
                .HasOne(x => x.Quest)
                .WithMany(x => x.QuestChains)
                .HasForeignKey(x => x.QuestId);
            modelBuilder.Entity<Npc>()
                .HasOne(x => x.Location)
                .WithMany(x => x.Npcs)
                .HasForeignKey(x => x.LocationId);
            modelBuilder.Entity<QuestPlayer>()
                .HasOne(x => x.Quest)
                .WithMany(x => x.QuestPlayers)
                .HasForeignKey(x => x.QuestId);
            modelBuilder.Entity<QuestPlayer>()
                .HasOne(x => x.Player)
                .WithMany(x => x.QuestPlayers)
                .HasForeignKey(x => x.PlayerId);
            modelBuilder.Entity<Player>()
                .HasOne(x => x.Location)
                .WithMany(x => x.Players)
                .HasForeignKey(x => x.LocationId);

            modelBuilder.Entity<Chain>()
                .Property(x => x.Name)
                .HasMaxLength(250);
            modelBuilder.Entity<Chain>()
                .Property(x => x.Description)
                .HasMaxLength(2000);
            modelBuilder.Entity<Chain>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<Item>()
                .Property(x => x.Name)
                .HasMaxLength(250);
            modelBuilder.Entity<Item>()
                .Property(x => x.Description)
                .HasMaxLength(2000);
            modelBuilder.Entity<Item>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<Quest>()
                .Property(x => x.Name)
                .HasMaxLength(250);
            modelBuilder.Entity<Quest>()
                .Property(x => x.Description)
                .HasMaxLength(2000);
            modelBuilder.Entity<Quest>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<Quest>()
                .Property(x => x.ArtisticTextLink)
                .HasMaxLength(2000);
            modelBuilder.Entity<Quest>()
                .Property(x => x.TechnicalDescriptionLink)
                .HasMaxLength(2000);
            modelBuilder.Entity<Quest>()
                .Property(x => x.CompletionComment)
                .HasMaxLength(2000);
            modelBuilder.Entity<Npc>()
                .Property(x => x.Name)
                .HasMaxLength(250);
            modelBuilder.Entity<Npc>()
                .Property(x => x.Description)
                .HasMaxLength(2000);
            modelBuilder.Entity<Npc>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<Location>()
                .Property(x => x.Name)
                .HasMaxLength(250);
            modelBuilder.Entity<Location>()
                .Property(x => x.Description)
                .HasMaxLength(2000);
            modelBuilder.Entity<Location>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<Player>()
                .Property(x => x.Name)
                .HasMaxLength(250);
            modelBuilder.Entity<Player>()
                .Property(x => x.Description)
                .HasMaxLength(2000);
            modelBuilder.Entity<Player>()
                .HasIndex(x => x.Name)
                .IsUnique();
            modelBuilder.Entity<QuestItem>()
                .Property(x => x.TechnicalDocumentForNpc)
                .HasMaxLength(2000);
        }
    }
}
