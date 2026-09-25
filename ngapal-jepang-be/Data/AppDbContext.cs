using System;
using Microsoft.EntityFrameworkCore;
using ngapal_jepang_be.Models;

namespace ngapal_jepang_be.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Flashcard> Flashcards => Set<Flashcard>();
    public DbSet<Deck> Decks => Set<Deck>();
    public DbSet<LearnSession> LearnSessions => Set<LearnSession>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Config EF core relation
        modelBuilder.Entity<Deck>()
            .HasOne(d => d.User)
            .WithMany(u => u.Decks)
            .HasForeignKey(d => d.UserId);

        modelBuilder.Entity<Flashcard>()
            .HasOne(f => f.Deck)
            .WithMany(d => d.Flashcards)
            .HasForeignKey(f => f.DeckId);

        modelBuilder.Entity<LearnSession>()
            .HasMany(ls => ls.FlashcardQueue)
            .WithMany(f => f.LearnSessions);
    }

}
