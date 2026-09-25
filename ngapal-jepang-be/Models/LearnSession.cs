using ngapal_jepang_be.Helper;

namespace ngapal_jepang_be.Models
{
    public class LearnSession
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

   
        /*
         * Statistics
         */
        public int MissCount { get; set; }
        public int CorrectCount { get; set; }

        /// <summary>
        /// Auto delete the session in one day if not used 
        /// </summary>
        public DateTime ExpiredAt { get; set; } = DateTime.UtcNow.AddDays(1);
        public LearnCategory LearnCategory { get; set; }

        //FK
        public ICollection<Flashcard> FlashcardQueue { get; set; } = new List<Flashcard>();
        public Deck Deck { get; set; } = null!;
        public Guid DeckId { get; set; }
    }
}
