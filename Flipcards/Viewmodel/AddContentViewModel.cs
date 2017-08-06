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

        public AddContentViewModel(FlipcardDatabase flipcardDatabase, FlipcardDeck flipcardDeck)
        {
            _flipcardsDatabase = flipcardDatabase;
            _flipcardsDeck = flipcardDeck;
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
            char[] delimiters = {' ', ',',';'};
            FlipcardWord word = new FlipcardWord
            {
                // TODO correct all fields from input
                Key = OriginalText,
                Tags = Tags.Split(delimiters).ToList(),
                Words = new Dictionary<Language, string>
                {
                    {Language.Dutch, OriginalText},
                    {Language.German, TranslatedText},
                }
            };

            _flipcardsDatabase.AddWord(word);
            _flipcardsDeck.AddFlipCard(new Flipcard(word.Words, Language.Dutch, Language.German));

            OriginalText = "";
            TranslatedText = "";
        }
    }
}
