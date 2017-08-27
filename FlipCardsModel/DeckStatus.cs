namespace FlipcardsModel
{
    public class DeckStatus
    {
        public Language OriginalLanguage { get; set; }
        public Language TranslatedLanguage { get; set; }

        public DeckStatus(Language originalLanguage, Language translatedLanguage)
        {
            OriginalLanguage = originalLanguage;
            TranslatedLanguage = translatedLanguage;
        }
    }
}