using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ngapal_jepang_be.Data;
using ngapal_jepang_be.Helper;
using ngapal_jepang_be.Models;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace ngapal_jepang_be.Endpoints
{
    public static class DeckEndpoint
    {
        private static readonly string apiUrl = "/api/deck";
        private static readonly string tags = "Deck";


        public static void MapDeckEndpoint(this WebApplication app)
        {
            var group = app.MapGroup(apiUrl).WithTags(tags);

            // Get all cards and info for deck screen
            group.MapGet("/{id:guid}/dsviews", async (Guid id, AppDbContext db) =>
            {
                var cards = await db.Flashcards.Where(f => f.DeckId == id).ToListAsync();
                int flashcardCount = cards.Count;

                /*
                 * Mastered = 85 - 100%
                 * Fluent = 60 - 84%
                 * Review = 35 - 59%
                 * Learn = 0 - 34%
                 */
                //Counter
                int learnCount = 0;
                int reviewCount = 0;
                int fluentCount = 0;
                int masteredCount = 0;

                //Actual progress
                float readProgress = 0f;
                float listenProgress = 0f;
                float outputProgress = 0f;
                float overallProgress = 0f;

                //Due learn notification
                int dueReadCount = 0;
                int dueListenCount = 0;
                int dueOutputCount = 0;

                foreach (Flashcard c in cards)
                {
                    switch (DeckHelper.CalculateProgress(c).GetCardCategory())
                    {
                        case RetentionCategory.Learn:
                            learnCount++;
                            break;
                        case RetentionCategory.Review:
                            reviewCount++;
                            break;
                        case RetentionCategory.Fluent:
                            fluentCount++;
                            break;
                        case RetentionCategory.Mastered:
                            masteredCount++;
                            break;
                        default: break;
                    }

                    bool isReadDue = c.ReadDueDate.Date <= DateTime.UtcNow.Date;
                    bool isListenDue = c.ListenDueDate.Date <= DateTime.UtcNow.Date;
                    bool isOutputDue = c.OutputDueDate.Date <= DateTime.UtcNow.Date;

                    //Check for due

                    dueReadCount += (isReadDue) ? 1 : 0;
                    dueListenCount += (isListenDue) ? 1 : 0;
                    dueOutputCount += (isOutputDue) ? 1 : 0;

                    readProgress += c.ReadProgress;
                    listenProgress += c.ListenProgress;
                    outputProgress += c.OutputProgress;
                    overallProgress += DeckHelper.CalculateProgress(c);
                }

                readProgress *= 100f / flashcardCount;
                listenProgress *= 100f / flashcardCount;
                outputProgress *= 100f / flashcardCount;
                overallProgress *= 100f / flashcardCount;

                return Results.Ok(new
                {
                    studyProgressCount = new
                    {
                        cardsCount = flashcardCount,
                        learnC = learnCount,
                        reviewC = reviewCount,
                        fluentC = fluentCount,
                        masteredC = masteredCount,
                        overall = overallProgress
                    },
                    learnProgress = new
                    {
                        readProgress,
                        listenProgress,
                        outputProgress,
                        overallProgress
                    },
                    dueLearnCount = new
                    {
                        dueReadCount,
                        dueListenCount,
                        dueOutputCount
                    },
                    flashcards = cards
                });

            });

            //Batch create cards
            group.MapPatch("/{id:guid}/cards/batch", async (HttpRequest req, Guid id, AppDbContext db) =>
            {
                //Get the deck
                var deck = await db.Decks.FindAsync(id);

                if (deck == null)
                {
                    return Results.BadRequest("Tidak ada deck dengan Id ini");
                }

                //Read the text from body
                using var reader = new StreamReader(req.Body);
                var text = await reader.ReadToEndAsync();

                //Todo: Validation
                List<Flashcard>? flashcards = FlashcardHelper.GetBatchFlashcards(text);

                if (flashcards == null)
                {
                    return Results.BadRequest("Tidak ada kartu yang ditambahkan");
                }


                //Add cards if valid
                foreach (var f in flashcards)
                {
                    f.DeckId = id;
                }

                db.Flashcards.AddRange(flashcards);
                await db.SaveChangesAsync();

                var dbFlashcards = await db.Flashcards.Where(f => f.DeckId == id).ToListAsync();

                return Results.Ok();
            });
        }
    }
}
