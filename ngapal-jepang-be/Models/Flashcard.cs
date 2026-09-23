namespace ngapal_jepang_be.Models;

public class Flashcard
{
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Konten utama kartu
    /// </summary>
    public string Kanji { get; set; } = string.Empty;
    public string Hiragana { get; set; } = string.Empty;
    public List<string> Meaning { get; set; } = new();
    public string Description { get; set; } = string.Empty;

    // Foregin key and navigation
    public Guid DeckId { get; set; }
    public Deck Deck { get; set; } = null!;

    /// <summary>
    /// Progress of the card
    /// </summary>
    public float ReadProgress { get; set; }
    public float ListenProgress { get; set; }
    public float OutputProgress { get; set; }
    public float OverallProgress => (ReadProgress + ListenProgress + OutputProgress / 3f);

    /// <summary>
    /// Url to audio
    /// </summary>
    public string? AudioUrl { get; set; }


}
