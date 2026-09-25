using ngapal_jepang_be.Models;

namespace ngapal_jepang_be.Helper
{
    public static class LearnSessionHelper
    {
        public static float SessionProgress(LearnSession learnSession)
        {
            var deck = learnSession.Deck;
            
            int deckFlashcardCount = deck.Flashcards.Count;
            int ongoingFlashcardCount = learnSession.FlashcardQueue.Count;

            return (float)(deckFlashcardCount - ongoingFlashcardCount) / deckFlashcardCount;
        }
    }
}
