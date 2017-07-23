using System;
using System.Windows.Input;
using ProjectUtils.Binding;
using FlipcardsModel;

namespace Flipcards.Viewmodel{
    class FlipCardViewModel : ObservableObject
    {
        #region fields
        private Flipcard _flipcard;
        private ICommand _flipcardCommand;
        #endregion

        #region properties
        public Flipcard FlipcardModel
        {
            get {return _flipcard;}
            private set
            {
                if(value != _flipcard)
                {
                    OnPropertyChanged(nameof(FlipcardModel));
                    _flipcard = value;
                }
            }
        }

        public ICommand FlipCommand {
            get {
                return _flipcardCommand ??
                       (_flipcardCommand = new RelayCommand(
                           param => FlipCard(),
                           param => true)
                       );
            }
        }

        private void FlipCard()
        {
            _flipcard.Flipped = !_flipcard.Flipped;
        }

        #endregion

        /// <summary>
        /// Construct the viewmodel for the given flipcard.
        /// </summary>
        /// <param name="flipcard">flipcard presented by the viewmodel</param>
        public FlipCardViewModel(Flipcard flipcard) {
            FlipcardModel = flipcard;
        }
    }
}
