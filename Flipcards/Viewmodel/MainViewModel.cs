using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Flipcards.View;
using FlipcardsModel;
using ProjectUtils.Binding;

namespace Flipcards.Viewmodel {
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        #region fields
        private static FlipcardDatabase _flipcardDatabase = new FlipcardDatabase();
        protected FlipcardDeck _flipcardDeck = new FlipcardDeck(_flipcardDatabase);
        private ICommand _showFlipCardsCommand;
        private ICommand _addContentCommand;
        #endregion

        #region properties

        /// <summary>
        /// A Collection of flipcards shown in the view
        /// </summary>
        public ObservableCollection<FlipCardViewModel> Flipcards { get; set; } = new ObservableCollection<FlipCardViewModel>();

        /// <summary>
        /// Show the cards
        /// </summary>
        public ICommand ShowFlipCardsCommand {
            get {
                return _showFlipCardsCommand ??
                       (_showFlipCardsCommand = new RelayCommand(
                           param => PopulateModel(),
                           param => true)
                       );
            }
        }

        public ICommand AddContentCommand
        {
            get {
                return _addContentCommand ??
                       (_addContentCommand = new RelayCommand(
                           param => OpenAddContentWindow(),
                           param => true)
                       );
            }
        }

        #endregion

        public MainViewModel()
        {
            _flipcardDeck.Flipcards.CollectionChanged += Flipcards_CollectionChanged;
        }

        private void Flipcards_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e) {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var flipcard in e.NewItems.OfType<Flipcard>())
                    {
                        Flipcards.Add(new FlipCardViewModel(flipcard));
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    // TODO
                    break;
                case NotifyCollectionChangedAction.Reset:
                    Flipcards.Clear();
                    break;
            }
        }

        private void OpenAddContentWindow() {
            AddContentViewModel viewModel = new AddContentViewModel(_flipcardDatabase, _flipcardDeck);
            AddContentView view = new AddContentView {DataContext = viewModel};
            view.Show();
        }

        /// <summary>
        /// Fill with data
        /// </summary>
        private void PopulateModel()
        {
            foreach (var flipcard in _flipcardDeck.Flipcards)
            {
                Flipcards.Add(new FlipCardViewModel(flipcard));
            }
        }
    }
}
