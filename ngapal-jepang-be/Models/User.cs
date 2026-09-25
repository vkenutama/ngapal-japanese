using System;

namespace ngapal_jepang_be.Models;

public class User
{
    // Credential
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string? PictureUrl { get; set; }
    public string Password { get; set; } = string.Empty;

    // FK
    public ICollection<Deck> Decks { get; set; } = new List<Deck>();

    // Date
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
