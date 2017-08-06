using System;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Media;
using ProjectUtils.Binding;
using FlipcardsModel;

namespace Flipcards.Viewmodel{
    class FlipCardViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Brush OriginalBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xF9, 0xFF, 0xD3));
        private readonly Brush TranslatedBackground = new SolidColorBrush(Color.FromArgb(0xFF, 0xC0, 0xF4, 0xFF));

        #region fields
        private ICommand _flipcardCommand;
        #endregion

        #region properties
        public Flipcard FlipcardModel{ get; set; }
        public Brush Background { get; set; }
        public ICommand FlipCommand {
            get {
                return _flipcardCommand ??
                       (_flipcardCommand = new RelayCommand(
                           param => FlipCard(),
                           param => true)
                       );
            }
        }

        #endregion


        /// <summary>
        /// Flip the flipcard
        /// </summary>
        private void FlipCard()
        {
            FlipcardModel.Flipped = !FlipcardModel.Flipped;
            Background = FlipcardModel.Flipped ? TranslatedBackground : OriginalBackground;
        }

        /// <summary>
        /// Construct the viewmodel for the given flipcard.
        /// </summary>
        /// <param name="flipcard">flipcard presented by the viewmodel</param>
        public FlipCardViewModel(Flipcard flipcard) {
            FlipcardModel = flipcard;
            Background = OriginalBackground;
        }
    }
}
