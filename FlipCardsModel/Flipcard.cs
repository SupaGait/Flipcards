using System;
using System.Collections.Generic;
using System.Windows.Input;
using ProjectUtils.Binding;

namespace FlipcardsModel
{
    public class Flipcard : ObservableObject {
        #region fields
        private readonly Dictionary<Language, string> _words;
        private bool _flipped = false;
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
                }
            }
        }
        public string Word => GetWord(Flipped ? TranslatedLanguage : OriginalLanguage);
        public Language OriginalLanguage { get; set; }
        public Language TranslatedLanguage { get; set; }

        #endregion

        /// <summary>
        /// Default constuctor which takes a dictonary
        /// </summary>
        /// <param name="words"></param>
        public Flipcard(Dictionary<Language, string> words)
        {
            _words = words;
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
