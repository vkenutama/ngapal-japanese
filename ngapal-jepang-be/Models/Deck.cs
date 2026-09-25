using System;

namespace ngapal_jepang_be.Models;

public class Deck
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name {get; set;} = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastLearnedAt { get; set; }

    
    // Collection navigation to flashcard
    public ICollection<Flashcard> Flashcards { get; set; } = new List<Flashcard>();
    public ICollection<LearnSession> LearnSessions { get; set; } = new List<LearnSession>();


    // FK
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;

}
