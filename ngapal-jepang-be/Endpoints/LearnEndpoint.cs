using Microsoft.EntityFrameworkCore;
using ngapal_jepang_be.Data;
using ngapal_jepang_be.Helper;
using ngapal_jepang_be.Models;

namespace ngapal_jepang_be.Endpoints
{
    public static class LearnEndpoint
    {
        private static readonly string apiUrl = "/api/learn";
        private static readonly string tags = "Learn";

        /// <summary>
        /// Change this to more user personalized max interval via database will be nice
        /// </summary>
        private const float MAX_INTERVAL = 14f;


        public static void MapLearnEndpoints(this WebApplication app)
        {
            var group = app.MapGroup(apiUrl).WithTags(tags);

            //Learn session when submitted
            group.MapPatch("/session/{id:guid}", async (SubmitDto req, Guid id, AppDbContext db) =>
            {
                //Check if session is valid
                LearnSession? session = await db.LearnSessions
                                                .Where(ls => ls.Id == id)
                                                .Include(ls => ls.Deck)
                                                .Include(ls => ls.FlashcardQueue)
                                                .FirstOrDefaultAsync();

                if (session == null)
                    return Results.NotFound("Session not found");

                //Check if the card exist
                if (string.IsNullOrEmpty(req.CardId))
                    return Results.BadRequest("Card id shouldn't be empty");

                Flashcard? card = session.FlashcardQueue
                                              .FirstOrDefault(f => f.Id == Guid.Parse(req.CardId));

                if (card == null)
                    return Results.NotFound("Card not found");

                AnswerResult result = AnswerResult.Correct; 

                switch (session.LearnCategory)
                {
                    case LearnCategory.Read:
                        //Todo: Implement the scoring logic
                        //1. Answer validation (hiragana and meaning)
                        bool isReadingCorrect = !string.IsNullOrWhiteSpace(req.InputReading) && req.InputReading.Trim().Equals(card.Hiragana, StringComparison.OrdinalIgnoreCase);

                        bool isMeaningCorrect = !string.IsNullOrWhiteSpace(req.InputMeaning) && card.Meaning.Any(m => m.Equals(req.InputMeaning.Trim(), StringComparison.OrdinalIgnoreCase));

                        bool isFullyCorrect = isReadingCorrect && isMeaningCorrect && !req.IsSurender;

                        //2. Scale calculation
                        if (isFullyCorrect) // Is correct answer
                        {
                            card.ReadProgress = Math.Min(1.0f, card.ReadProgress + 0.2f);

                            if (card.ReadProgress >= 1.0f)
                            {
                                session.FlashcardQueue.Remove(card);
                                card.ReadDueDate = DateTime.UtcNow.AddDays(7); //1 week
                            }
                            else
                            {
                                session.FlashcardQueue.Remove(card);
                                card.ReadDueDate = DateTime.UtcNow.AddDays(1);
                            }

                            session.CorrectCount += 1;
                            result = AnswerResult.Correct;
                        }
                        //Is not correct answer
                        else
                        {
                            session.MissCount += 1;
                            card.ReadProgress = Math.Max(0.0f, card.ReadProgress - 0.15f);
                            card.ReadDueDate = DateTime.UtcNow.AddMinutes(10);

                            result = AnswerResult.Wrong;
                        }
                        break;
                }

                await db.SaveChangesAsync();
                return Results.Ok(new
                {
                    Information = new
                    {
                        Result = result.ToString()
                    },
                    Card = new
                    {
                        card.Id,
                        card.Kanji,
                        card.Hiragana,
                        card.Meaning,
                        card.Description,
                        card.AudioUrl,
                        card.ReadProgress,
                        card.ReadDueDate,
                        card.ListenProgress,
                        card.ListenDueDate,
                        card.OutputProgress,
                        card.OutputDueDate,
                        card.OverallProgress
                    }

                });
            });



            /*
             * 
             * Learn Session Management
             * 
             */

            //Create learning session
            group.MapPost("/session", async (CreateSessionDto dto, AppDbContext db) =>
            {
                //Check if the deck exist
                Deck? deck = await db.Decks.Where(d => d.Id == Guid.Parse(dto.DeckId))
                                           .Include(d => d.Flashcards)
                                           .FirstOrDefaultAsync();

                if (deck == null)
                    return Results.NotFound("Deck not found");

                //Check if the same deck learning session is created
                bool isExisting = await db.LearnSessions.AnyAsync(ls => ls.DeckId == deck.Id);

                if (isExisting)
                    return Results.BadRequest("Learning session already created");

                //Make the learning session
                LearnSession learnSession = new LearnSession()
                {
                    DeckId = deck.Id,
                    LearnCategory = (LearnCategory)dto.LearnCategory,
                };

                //Enqueue all the due flashcard
                foreach (var card in deck.Flashcards)
                {
                    bool isDue =
                    (learnSession.LearnCategory == LearnCategory.Read && card.ReadDueDate <= DateTime.UtcNow.Date);

                    if (isDue)
                        learnSession.FlashcardQueue.Add(card);
                }

                await db.LearnSessions.AddAsync(learnSession);
                await db.SaveChangesAsync();

                return Results.Ok(learnSession);
            });



            //Leave/delete the learning session
            group.MapDelete("/session/{id:guid}", async (Guid id, AppDbContext db) =>
            {
                LearnSession? learnSession = await db.LearnSessions.FindAsync(id);

                if (learnSession == null)
                {
                    return Results.NotFound("Learning session not found");
                }

                db.Remove(learnSession!);
                await db.SaveChangesAsync();

                return Results.Ok("Learning session deleted");
            });

            //Get all active learn sessions
            group.MapGet("/session/", async (AppDbContext db) =>
            {
                return Results.Ok(await db.LearnSessions.ToListAsync());
            });

            //Get the deck learn session information
            group.MapGet("/session/{id:guid}", async (Guid id, AppDbContext db) =>
            {
                //Get the learning session id
                LearnSession? session = await db.LearnSessions
                                                .Where(ls => ls.Id == id)
                                                .Include(ls => ls.Deck)
                                                .Include(ls => ls.Deck.Flashcards)
                                                .Include(ls => ls.FlashcardQueue)
                                                .FirstAsync();

                if (session == null)
                    return Results.NotFound("Learn session not found");

                //Get the deck
                Deck? deck = session!.Deck;

                if (deck == null)
                    return Results.NotFound("Deck not found");

                //Get all due cards for read
                int dueFlashcardsCount = session.FlashcardQueue.Count();

                //Re-shuffle flashcard
                session.FlashcardQueue = session.LearnCategory switch
                {
                    LearnCategory.Read =>
                        session.FlashcardQueue.OrderBy(f => f.ReadDueDate).ToList(),

                    _ =>
                        session.FlashcardQueue.OrderBy(f => f.OverallProgress).ToList()
                };

                var flashcards = session.FlashcardQueue.Select(f => new
                {
                    f.Id,
                    f.Kanji,
                    f.Hiragana,
                    f.Meaning,
                    f.Description,
                    f.AudioUrl,
                    f.ReadProgress,
                    f.ReadDueDate,
                    f.ListenProgress,
                    f.ListenDueDate,
                    f.OutputProgress,
                    f.OutputDueDate,
                    f.OverallProgress
                });

                return Results.Ok(new
                {
                    Statistics = new
                    {
                        RemainingCardsCount = dueFlashcardsCount,
                        session.MissCount,
                        session.CorrectCount,
                        SessionProgress = LearnSessionHelper.SessionProgress(session)
                    },
                    Flashcards = flashcards
                });
            });

        }

        public enum AnswerResult
        {
             Correct,
             Wrong
        }

        public record SubmitDto(string CardId, string InputReading, string InputMeaning, bool IsSurender);

        public record CreateSessionDto(string DeckId, int LearnCategory);
    }
}
