using System.Collections.Generic;

namespace FlipcardsModel
{
    public class Flipcard
    {
        #region fields
        private readonly Dictionary<Language, string> _words;
        #endregion

        #region properties
        public bool Flipped { get; set; } = false;
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
            return _words[language];
        }
    }
}
