using System.Collections.Generic;

namespace FlipcardsModel {
    public class FlipcardDeck {
        public List<Flipcard> Flipcards { get; set; } = new List<Flipcard>();

        public FlipcardDeck()
        {
            FlipcardDatabase testDatabase = new FlipcardDatabase();
            foreach (var flipcardWord in testDatabase.FlipcardsWords)
            {
                Flipcards.Add(
                    new Flipcard(flipcardWord.Words)
                    {
                        OriginalLanguage = Language.Dutch,
                        TranslatedLanguage = Language.German,
                    }
                );
            }
        }
    }
}
