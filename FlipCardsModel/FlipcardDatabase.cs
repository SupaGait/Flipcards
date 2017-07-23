using System;
using System.Collections.Generic;

namespace FlipcardsModel {
    //using Word = Dictionary<Language, string>;

    public struct FlipcardWord : IComparable {
        public string Key { get; set; }
        public Dictionary<Language, string> Words{ get; set; }
        public List<string> Tags { get; set; }
        
        public int CompareTo(object obj)
        {
            return string.Compare(obj as string, Key, StringComparison.Ordinal);
        }
    }
    public class FlipcardDatabase
    {
        public SortedSet<FlipcardWord> FlipcardsWords{ get; set; }

        public FlipcardDatabase()
        {
            PopulateWithTestData();
        }

        private void PopulateWithTestData()
        {
            FlipcardsWords = new SortedSet<FlipcardWord>
            {
                new FlipcardWord()
                {
                    Key = "Chair",
                    Tags = new List<string> { "Home", "Furniture" },
                    Words = new Dictionary<Language,string>
                    {
                        {Language.English, "Chair"},
                        {Language.Dutch, "Stoel"},
                        {Language.German, "Sits"},
                    }
                },
                new FlipcardWord()
                {
                    Key = "Hello",
                    Tags = new List<string> { "Greetings", "Meeting" },
                    Words = new Dictionary<Language,string>
                    {
                        {Language.English, "Hello"},
                        {Language.Dutch, "Hallo"},
                        {Language.German, "Gutentag"},
                    }
                },
            };
        }
    }
}
