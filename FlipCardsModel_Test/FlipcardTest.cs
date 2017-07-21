using System;
using System.Collections.Generic;
using FlipcardsModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FlipCardsModel_Test {
    [TestClass]
    public class FlipcardTest {
        private Flipcard createDefaultFlipcard()
        {
            var dict = new Dictionary<Language, string>
            {
                {Language.Dutch, "stoel"},
                {Language.German, "sitz"}
            };
            var flipcard = new Flipcard(dict) {
                OriginalLanguage = Language.Dutch,
                TranslatedLanguage = Language.German
            };
            return flipcard;
        }

        [TestMethod]
        public void TestDefaultFlipcard()
        {
            var flipcard = createDefaultFlipcard();
            Assert.AreEqual(false, flipcard.Flipped);
            Assert.AreEqual("stoel", flipcard.GetWord(Language.Dutch));
            Assert.AreEqual("sitz", flipcard.GetWord(Language.German));

        }
    }
}
