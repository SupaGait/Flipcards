using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FlipcardsModel {
    [DataContract]
    public class FlipcardDatabase
    {
        [DataMember]
        public IDictionary<string, FlipcardWord> FlipcardsWords{ get; set; } = new Dictionary<string, FlipcardWord>();

        [DataMember]
        public IDictionary<string, FlipcardDeck> FlipcardDecks { get; set; } = new Dictionary<string, FlipcardDeck>();

        public void AddWord(FlipcardWord flipcardWord)
        {
            try
            {
                FlipcardsWords.Add(flipcardWord.Key, flipcardWord);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void AddDeck(FlipcardDeck deck)
        {
            FlipcardDecks.Add(deck.Name, deck);
        }
    }
}
