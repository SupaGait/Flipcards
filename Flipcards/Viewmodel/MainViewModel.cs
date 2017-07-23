using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using FlipcardsModel;
using ProjectUtils.Binding;

namespace Flipcards.Viewmodel {
    class MainViewModel : ObservableObject
    {
        #region fields
        private ObservableCollection<FlipCardViewModel> _flipcards = new ObservableCollection<FlipCardViewModel>();
        private ICommand _showFlipCardsCommand;
        #endregion

        #region properties
        /// <summary>
        /// A Collection of flipcards shown in the view
        /// </summary>
        public ObservableCollection<FlipCardViewModel> Flipcards
        {
            get { return _flipcards; }
            set
            {
                if (_flipcards != value)
                {
                    _flipcards = value;
                    OnPropertyChanged(nameof(Flipcards));
                }
            }
        }

        /// <summary>
        /// Show the cards
        /// </summary>
        public ICommand ShowFlipCardsCommand {
            get {
                return _showFlipCardsCommand ??
                       (_showFlipCardsCommand = new RelayCommand(
                           param => PopulateModel(),
                           param => (Flipcards.Count == 0))
                       );
            }
        }
        #endregion

        /// <summary>
        /// Fill with test data
        /// </summary>
        private void PopulateModel()
        {
            FlipcardDeck flipcardDeck = new FlipcardDeck();
            foreach (var flipcard in flipcardDeck.Flipcards)
            {
                Flipcards.Add(new FlipCardViewModel(flipcard));
            }
        }
    }
}
