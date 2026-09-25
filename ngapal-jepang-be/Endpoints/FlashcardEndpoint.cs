using ngapal_jepang_be.Data;
using ngapal_jepang_be.Models;

namespace ngapal_jepang_be.Endpoints
{
    public static class FlashcardEndpoint
    {
        private static readonly string apiUrl = "/api/flashcard";
        private static readonly string tags = "Flashcard";

        public static void MapFlashcardEnpoint(this WebApplication app)
        {
            var group = app.MapGroup(apiUrl).WithTags(tags);

            //Get flashcard by id
            group.MapGet("/{id:guid}", async (Guid id, AppDbContext db ) => {
                Flashcard? card = await db.Flashcards.FindAsync(id);

                if (card == null)
                    return Results.NotFound("Card not found");

                return Results.Ok(card);

            });
        }
    }
}
