using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FlipcardsModel {
    public class FlipcardDeck : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Flipcard> Flipcards { get;} = new ObservableCollection<Flipcard>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="flipcardDatabase"></param>
        public FlipcardDeck(FlipcardDatabase flipcardDatabase)
        {
            foreach (var flipcardWord in flipcardDatabase.FlipcardsWords)
            {
                Flipcards.Add(new Flipcard(flipcardWord.Words, Language.Dutch, Language.German));
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
