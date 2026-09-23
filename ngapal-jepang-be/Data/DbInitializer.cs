using ngapal_jepang_be.Models;

namespace ngapal_jepang_be.Data
{
    public class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // Ensure db is created
            context.Database.EnsureCreated();

            // If there is any user, cancel seeding
            if(context.Users.Any())
            {
                return;
            }

            /*
             * Proceed the seeding
             */

            // 1. user seed
            var user = new User()
            {
                Id = Guid.NewGuid(),
                Name = "Vincent Kenutama",
                Username = "vkenutama",
                CreatedAt = DateTime.Now,
            };
            

            // 2. deck seed
            var deck = new Deck()
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Name = "Deck Pertama",
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now,
            };

            // 3. flashcard seed
            var flashcard = new Flashcard()
            {
                Id = Guid.NewGuid(),
                DeckId = deck.Id,
                Kanji = "用事",
                Meaning = new() { "Urusan" },
                Hiragana = "ようじ",
                Description = "Deskripsi urusan",
                CreatedAt = DateTime.Now
            };

            //Add to db
            context.Users.Add(user);
            context.Decks.Add(deck);
            context.Flashcards.Add(flashcard);

            //Save changes
            context.SaveChanges();
        }
    }
}
