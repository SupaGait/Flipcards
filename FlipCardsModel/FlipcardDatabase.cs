using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FlipcardsModel {
    //using Word = Dictionary<Language, string>;
    [DataContract]
    public struct FlipcardWord : IComparable {

        [DataMember]
        public string Key { get; set; }

        [DataMember]
        public Dictionary<Language, string> Words{ get; set; }

        [DataMember]
        public List<string> Tags { get; set; }
        
        public int CompareTo(object obj)
        {
            return string.Compare(obj as string, Key, StringComparison.Ordinal);
        }

        public override string ToString()
        {
            return $"Key: {Key}";
        }
    }

    [DataContract]
    public class FlipcardDatabase
    {
        [DataMember]
        public SortedSet<FlipcardWord> FlipcardsWords{ get; set; }

        public FlipcardDatabase()
        {
            PopulateWithTestData();
        }

        public void AddWord(FlipcardWord flipcardWord)
        {
            FlipcardsWords.Add(flipcardWord);
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
