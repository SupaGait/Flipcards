using FlipcardsModel;

namespace Flipcards.Viewmodel {
    class MockFlipCardViewModel : MainViewModel
    {
        public MockFlipCardViewModel()
        {
            foreach (var flipcard in _flipcardDeckShown.Flipcards) {
                FlipcardsVm.Add(new FlipCardViewModel(flipcard));
            }
        }
    }
}
