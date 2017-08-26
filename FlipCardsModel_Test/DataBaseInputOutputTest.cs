using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlipcardsModel;

namespace FlipCardsModel_Test {
    [TestClass]
    public class DataBaseInputOutputTest {
        [TestMethod]
        public void TestSaveAndLoad() {
            FlipcardDatabase db = new FlipcardDatabase();
            var io = new DataBaseInputOutput(db);

            Debug.WriteLine(" Test GKL ");
            Debug.WriteLine(db.FlipcardsWords);
            foreach (var flipcardWord in db.FlipcardsWords)
            {
                Debug.WriteLine(flipcardWord);
            }

            io.Save();
        }
    }
}
