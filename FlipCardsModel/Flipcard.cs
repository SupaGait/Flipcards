using System;
using System.Collections.Generic;
using ProjectUtils.Binding;

namespace FlipcardsModel
{
    public class Flipcard : ObservableObject {
        #region fields
        private readonly Dictionary<Language, string> _words;
        private readonly DeckStatus _deckStatus;
        private bool _flipped;
        #endregion

        #region properties
        public bool Flipped
        {
            get { return _flipped; }
            set
            {
                if (value != _flipped)
                {
                    _flipped = value;
                    OnPropertyChanged(nameof(Flipped));
                    OnPropertyChanged(nameof(Word));
                    OnPropertyChanged(nameof(CurrentLanguage));
                }
            }
        }
        public string Word => GetWord(Flipped ? _deckStatus.TranslatedLanguage : _deckStatus.OriginalLanguage);
        public Language CurrentLanguage => Flipped ? _deckStatus.TranslatedLanguage : _deckStatus.OriginalLanguage;
        #endregion

        /// <summary>
        /// Default constuctor which takes a dictonary
        /// </summary>
        /// <param name="words"></param>
        /// <param name="deckStatus"></param>
        public Flipcard(Dictionary<Language, string> words, DeckStatus deckStatus)
        {
            _words = words;
            _deckStatus = deckStatus;
        }

        public string GetWord(Language language)
        {
            try
            {
                return _words[language];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "No translation available.";
            }
        }
    }
}
