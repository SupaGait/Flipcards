using System.Runtime.InteropServices.ComTypes;
using System.Windows.Input;
using Flipcards.Utils;
using FlipcardsModel;

namespace Flipcards.Viewmodel{
    class FlipCardViewModel : ObservableObject
    {
        #region fields
        private Flipcard _flipcard;
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
