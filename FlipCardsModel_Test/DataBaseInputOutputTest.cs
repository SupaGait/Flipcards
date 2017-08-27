using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlipcardsModel;

namespace FlipCardsModel_Test {
    [TestClass]
    public class DataBaseInputOutputTest {
        [TestMethod]
        public void TestSaveAndLoadWordsOnly()
        {
            // Save - Populate with test data
            FlipcardDatabase dbSave = new FlipcardDatabase();
            generateTestData(dbSave);
            DumpData(dbSave);
            new DataBaseInputOutput(dbSave).Save();
            

            // Load
            FlipcardDatabase dbLoad = new FlipcardDatabase();
            new DataBaseInputOutput(dbLoad).Load();
            DumpData(dbLoad);

            // Compare
            foreach (var flipcardWord in dbLoad.FlipcardsWords)
            {
                Assert.IsTrue(dbSave.FlipcardsWords.Contains(flipcardWord));
            }
        }

        [TestMethod]
        public void TestSaveAndLoadDecks()
        {
            // TODO refactor the FlipCardDeck constructor and the arguments of FlipCard.

            FlipcardDatabase dbSave = new FlipcardDatabase();
            var deckStatus = new DeckStatus(Language.Dutch, Language.English);
            var flipcardDeck = new FlipcardDeck(dbSave, deckStatus);
            flipcardDeck.AddFlipCard(new Flipcard(new Dictionary<Language, string>(), deckStatus));
            dbSave.AddDeck(flipcardDeck);

            new DataBaseInputOutput(dbSave).Save();
        }

        private void DumpData(FlipcardDatabase db) {
            Debug.WriteLine("FlipcardDatabase dump: ");
            Debug.WriteLine(db.FlipcardsWords);
            foreach (var flipcardWord in db.FlipcardsWords) {
                Debug.WriteLine(flipcardWord);
            }
        }
        private void generateTestData(FlipcardDatabase dbSave)
        {
            var word = new FlipcardWord
            {
                Key = "Chair",
                Tags = new List<string> {"Home", "Furniture"},
                Words = new Dictionary<Language, string>
                {
                    {Language.English, "Chair"},
                    {Language.Dutch, "Stoel"},
                    {Language.German, "Sits"},
                }
            };
            dbSave.FlipcardsWords.Add(word.Key, word);

            word = new FlipcardWord
            {
                Key = "Hello",
                Tags = new List<string> {"Greetings", "Meeting"},
                Words = new Dictionary<Language, string>
                {
                    {Language.English, "Hello"},
                    {Language.Dutch, "Hallo"},
                    {Language.German, "Gutentag"},
                }
            };
            dbSave.FlipcardsWords.Add(word.Key, word);
        }
    }
}
