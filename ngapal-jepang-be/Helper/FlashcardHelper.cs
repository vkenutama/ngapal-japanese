using Microsoft.Extensions.Primitives;
using ngapal_jepang_be.Models;

namespace ngapal_jepang_be.Helper
{
    public static class FlashcardHelper
    {
        public static List<Flashcard>? GetBatchFlashcards(string text)
        {
            //使用    しよう penggunaan;pemakaian   ini adalah deskripsi
            //行く    いく pergi deskripsi iku
            //保つ    たもつ menyimpan,menjaga   deskripsi dari tamotsu
            List<Flashcard> flashcards = new();
            string[] lines = text.Split(new[] { ";", "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

            foreach (string line in lines)
            {
                string[] parts = line.Split("\t");

                if (parts == null || parts.Length != 4)
                    continue;

                var meanings = parts[2].Split(",", StringSplitOptions.None).ToList();

                Flashcard flashcard = new()
                {
                    Kanji = parts[0],
                    Hiragana = parts[1],
                    Meaning = meanings,
                    Description = parts[3],
                    CreatedAt = DateTime.Now,
                };

                flashcards.Add(flashcard);
            }

            return flashcards;
        }
    }

    public enum RetentionCategory
    {
        Learn,
        Review,
        Fluent,
        Mastered
    }
}
