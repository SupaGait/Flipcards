using System.Collections.Generic;
using FlipcardsModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlipCardsModel_Test {
    [TestClass]
    public class FlipcardTest
    {
        private static readonly string ChairDutch = "Stoel";
        private static readonly string ChairGerman = "Sitz";
        private readonly DeckStatus _deckStatus = new DeckStatus(Language.Dutch, Language.German);

        private Flipcard CreateDefaultFlipcard()
        {
            var dict = new Dictionary<Language, string>
            {
                {Language.Dutch, ChairDutch},
                {Language.German, ChairGerman}
            };
            var flipcard = new Flipcard(dict, _deckStatus);
            return flipcard;
        }

        [TestMethod]
        public void TestDefaultFlipcard()
        {
            var flipcard = CreateDefaultFlipcard();
            Assert.AreEqual(false, flipcard.Flipped);
            Assert.AreEqual(ChairDutch, flipcard.GetWord(Language.Dutch));
            Assert.AreEqual(ChairGerman, flipcard.GetWord(Language.German));
        }

        [TestMethod]
        public void TestFlipcard() {
            var flipcard = CreateDefaultFlipcard();
            Assert.AreEqual(flipcard.Word, ChairDutch);
            flipcard.Flipped = true;
            Assert.AreEqual(flipcard.Word, ChairGerman);
            flipcard.Flipped = false;
            Assert.AreEqual(flipcard.Word, ChairDutch);
        }

        [TestMethod]
        public void TestUnknownLanguage() {
            var flipcard = CreateDefaultFlipcard();
            Assert.AreEqual(false, flipcard.Flipped);
            Assert.AreEqual("No translation available.", flipcard.GetWord(Language.Spanish));
        }
    }
}
