using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace FlipcardsModel {
    [DataContract]
    public class FlipcardDeck : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        [DataMember]
        public ObservableCollection<Flipcard> Flipcards { get;} = new ObservableCollection<Flipcard>();

        [DataMember]
        public string Name { get; set; } = "Unknown";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="flipcardDatabase"></param>
        /// <param name="deckStatus"></param>
        public FlipcardDeck(FlipcardDatabase flipcardDatabase, DeckStatus deckStatus)
        {
            foreach (var flipcardWord in flipcardDatabase.FlipcardsWords.Values)
            {
                Flipcards.Add(new Flipcard(flipcardWord.Words, deckStatus));
            }
        }

        /// <summary>
        /// Add a card to the deck.
        /// </summary>
        /// <param name="flipcard">The card to add to the deck</param>
        public void AddFlipCard(Flipcard flipcard)
        {
            Flipcards.Add(flipcard);
        }
    }
}
