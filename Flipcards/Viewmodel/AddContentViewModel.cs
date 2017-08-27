using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using FlipcardsModel;
using ProjectUtils.Binding;

namespace Flipcards.Viewmodel {
    class AddContentViewModel : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly FlipcardDatabase _flipcardsDatabase;
        private readonly FlipcardDeck _flipcardsDeck;
        private ICommand _addContentCommand;
        private DeckStatus _deckStatus;

        public AddContentViewModel(FlipcardDatabase flipcardDatabase, FlipcardDeck flipcardDeck, DeckStatus deckStatus)
        {
            _flipcardsDatabase = flipcardDatabase;
            _flipcardsDeck = flipcardDeck;
            _deckStatus = deckStatus;
        }

        public string OriginalText { get; set; } = "";
        public string TranslatedText { get; set; } = "";
        public string Tags { get; set; } = "";

        public ICommand AddCommand
        {
            get {
                return _addContentCommand ??
                       (_addContentCommand = new RelayCommand(
                           param => AddContent(),
                           param => true/*(OriginalText.Length != 0 && TranslatedText.Length != 0)*/)
                       );
            }
        }

        private void AddContent()
        {
            FlipcardWord word = new FlipcardWord {
                // TODO correct all fields from input
                Key = OriginalText,
                Tags = Tags.Split(' ', ',', ';').ToList(),
                Words = new Dictionary<Language, string>
                {
                    {Language.Dutch, OriginalText},
                    {Language.German, TranslatedText},
                }
            };

            _flipcardsDatabase.AddWord(word);
            _flipcardsDeck.AddFlipCard(new Flipcard(word.Words, _deckStatus));

            OriginalText = "";
            TranslatedText = "";
        }
    }
}
