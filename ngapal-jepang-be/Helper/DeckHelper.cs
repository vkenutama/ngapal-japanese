using ngapal_jepang_be.Models;

namespace ngapal_jepang_be.Helper
{
    public static class DeckHelper
    {
        private static readonly float learnModesCount = 3f;

        public static float CalculateProgress(Flashcard flashcard) => (flashcard.ListenProgress + flashcard.ReadProgress + flashcard.OutputProgress) / learnModesCount;

        public static RetentionCategory GetCardCategory(this float value)
        {
            return value switch
            {
                >= 85f => RetentionCategory.Mastered,
                >= 60f => RetentionCategory.Fluent,
                >= 35f => RetentionCategory.Review,
                _ => RetentionCategory.Learn
            };

        }
    }
}
