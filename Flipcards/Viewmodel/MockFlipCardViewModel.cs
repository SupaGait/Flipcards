using FlipcardsModel;

namespace Flipcards.Viewmodel {
    class MockFlipCardViewModel : MainViewModel
    {
        public MockFlipCardViewModel()
        {
            foreach (var flipcard in _flipcardDeck.Flipcards) {
                Flipcards.Add(new FlipCardViewModel(flipcard));
            }
        }
    }
}
