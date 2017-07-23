using System.Collections.Generic;
using System.Collections.ObjectModel;
using Flipcards.Utils;
using FlipcardsModel;

namespace Flipcards.Viewmodel {
    class MainViewModel : ObservableObject {
        public ObservableCollection<Flipcard> Flipcards { get; set; }

        public MainViewModel()
        {
            var dict = new Dictionary<Language, string>
            {
                {Language.Dutch, "stoel"},
                {Language.German, "sitz"}
            };
            Flipcards = new ObservableCollection<Flipcard>
            {
                new Flipcard(dict),
                new Flipcard(dict)
            };
        }
    }
}
